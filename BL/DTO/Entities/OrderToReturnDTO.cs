using Domains.Order;
using Shared.DTOs.Base;

namespace BL.DTO.Entities
{
    public class OrderToReturnDTO : BaseDTO
    {
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }

        public IReadOnlyList<OrderItemDTO> orderItems { get; set; }
    }
}
