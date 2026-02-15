using BL.Contracts.GeneralService.CMS;
using BL.Contracts.IMapper;
using BL.Contracts.Services.Custom;
using BL.DTO.Entities;
using BL.GenericResponse;
using BL.Services.Generic;
using DAL.Contracts.Repositories.Generic;
using DAL.Models;
using Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Resources;
using Shared.GeneralModels.SearchCriteriaModels;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BL.Services.Custom
{
    public class ProductService : BaseService<Product, ProductDTO>, IProductService
    {
        private readonly ITableRepository<Product> _productRepository;
        private readonly IFileUploadService _fileUploadService;
        public ProductService(
            ITableRepository<Product> ProductTableRepository,
            IFileUploadService fileUploadService,
            IBaseMapper mapper) : base(ProductTableRepository, mapper)
        {
            _fileUploadService = fileUploadService;
            _productRepository = ProductTableRepository;
            _mapper = mapper;

        }

        public async Task<Response<IEnumerable<GetProductDTO>>> GetAllWithIncludesAsync()
        {
            var entitiesList = await _productRepository.GetAsync(
              x => x.CurrentState == 1,
              x => x.Category
            );
            if (entitiesList == null) return NotFound<IEnumerable<GetProductDTO>>();
            var dtoList = _mapper.MapList<Product, GetProductDTO>(entitiesList);
            return Success(dtoList);
        }
        public async Task<Response<GetProductDTO>> FindByIdWithIncludesAsync(Guid Id)
        {
            var entity = await _productRepository.FindAsync(
          x => x.CurrentState == 1 && x.Id == Id,
          x => x.Category
         );
            if (entity == null) return NotFound<GetProductDTO>();
            var dto = _mapper.MapModel<Product, GetProductDTO>(entity);
            return Success(dto);
        }
        public async Task<Response<bool>> SaveAndUploadImageAsync(ProductDTO entityDTO, Guid userId)
        {


            if (entityDTO.Image == null && entityDTO.ImagePath == null) return BadRequest<bool>(ValidationResources.ImageRequired);

            var entity = _mapper.MapModel<ProductDTO, Product>(entityDTO);

            if (entityDTO.Image != null)
            {
                entity.ImagePath = await _fileUploadService.UploadFileAsync(entityDTO.Image, "Products", entityDTO.ImagePath);
            }

            var isSaved = await _productRepository.SaveAsync(entity, userId);
            if (isSaved) return Success(true);
            else return BadRequest<bool>();

        }
        public async Task<PaginatedResult<GetProductDTO>> GetProductsPaginatedListAsync(BaseSearchCriteriaModel SearchCriterial)
        {
            Expression<Func<Product, bool>> filter = null;

            if (SearchCriterial.CategoryId != null && SearchCriterial.CategoryId != default)
                filter = x => x.CategoryId == SearchCriterial.CategoryId;

            var query = _productRepository
                 .BuildQuery(SearchCriterial.PageNumber,
                  SearchCriterial.PageSize,
                    filter,
                  SearchCriterial.OrderingEnum)
                  .Include(x => x.Category)
                  .Where(x => x.CurrentState == 1);

            //filtering by word
            if (!string.IsNullOrEmpty(SearchCriterial.SearchTerm))
            {
                var searchWords = SearchCriterial.SearchTerm.Split(' ');
                query = query.Where(m => searchWords.All(word =>

                m.Name.ToLower().Contains(word.ToLower()) ||
                m.Description.ToLower().Contains(word.ToLower())

                ));
            }
            var paginatedList = await _mapper
                .ProjectToPaginatedListAsync<GetProductDTO>(query, SearchCriterial.PageNumber, SearchCriterial.PageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;

        }
        public  async Task<Response<IEnumerable<GetProductDTO>>> GetAllNew()
        {
            var entitiesList =await _productRepository.GetTableNoTracking()
            .Where(x => x.CurrentState == 1)
            .OrderByDescending(x => x.CreatedDateUtc)
            .Take(3)
           .ToListAsync();
            if (!entitiesList.Any()) return NotFound<IEnumerable<GetProductDTO>>();
            var dtoList = _mapper.MapList<Product, GetProductDTO>(entitiesList);
            return Success(dtoList);
        }

    }
}
