
using BL.Contracts.GeneralService.CMS;
using BL.Contracts.GeneralService.UserManagement;
using BL.Contracts.IMapper;
using BL.Contracts.Services.Custom;
using BL.DTO.Entities;
using BL.GenericResponse;
using BL.Services.Generic;
using DAL.Contracts.Repositories.Generic;
using DAL.Contracts.UnitOfWork;
using Domains.Entities.Product;
using Domains.Order;
namespace BL.Services
{
    public class OrderService : BaseService<Orders, OrderDTO>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerBasketService _customerBasketService;


        public OrderService(
            ITableRepository<Orders> orderTableRepository,
            IBaseMapper mapper,
            IUnitOfWork unitOfWork,
            ICustomerBasketService customerBasketService,
            IPaymentService paymentService
              )
            : base(unitOfWork.TableRepository<Orders>(), mapper)
        {
            _unitOfWork = unitOfWork;
            _customerBasketService = customerBasketService;

        }


        public async Task<Response<OrderDTO>> CreateOrdersAsync(OrderDTO orderDTO, string BuyerEmail, Guid userId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var basketResponse = await _customerBasketService.GetBasketAsync(orderDTO.BasketId);

                List<OrderItem> orderItems = new List<OrderItem>();

                // Full OrderItem By BasketItems
                foreach (var item in basketResponse.Data.BasketItems)
                {

                    var Product = await _unitOfWork.TableRepository<Product>().FindByIdAsync(item.Id);
                    var orderItem = new OrderItem
                        (Product.Id, item.ImagePath, Product.Name, item.Price, item.Quantity, userId);
                    orderItems.Add(orderItem);

                }


                var subTotal = orderItems.Sum(m => m.Price * m.Quantity);




                var order = new
                         Orders(subTotal, orderItems);

                await _unitOfWork.TableRepository<Orders>().CreateAsync(order, userId);
                await _customerBasketService.DeleteBasketAsync(orderDTO.BasketId);

                await transaction.CommitAsync();
                orderDTO.Id = order.Id;
                return Success(orderDTO);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<Response<IEnumerable<OrderToReturnDTO>>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.TableRepository<Orders>()
                .GetAsync(x => x.CurrentState == 1 );
            var result = _mapper.MapList<Orders, OrderToReturnDTO>(orders);

            result = result.OrderByDescending(m => m.Id).ToList();
            return Success(result);
        }

  

        


    }
}
