using BL.Contracts.GeneralService.CMS;
using BL.Contracts.IMapper;
using BL.Contracts.Services.Custom;
using BL.DTO.Entities;
using BL.GenericResponse;
using BL.Services.Generic;
using DAL.Contracts.Repositories.Generic;
using Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using Resources;
using System.Linq;

namespace BL.Services
{
    public class OfferService : BaseService<Offer, OfferDTO>, IOfferService
    {
        private readonly ITableRepository<Offer> _offerTableRepository;
        private readonly ITableRepository<OfferProduct> _offerProductTableRepository;

        private readonly IFileUploadService _fileUploadService;
        public OfferService(ITableRepository<Offer> offerTableRepository, ITableRepository<OfferProduct> offerProductTableRepository, IFileUploadService fileUploadService, IBaseMapper mapper) : base(offerTableRepository, mapper)
        {
            _offerTableRepository = offerTableRepository;
            _offerProductTableRepository = offerProductTableRepository;
            _fileUploadService = fileUploadService; 
            _mapper = mapper;
        }

        public override async Task<Response<bool>> SaveAsync(OfferDTO dto, Guid userId)
        {
            if (dto.Image == null && dto.ImagePath == null) return BadRequest<bool>(ValidationResources.ImageRequired);

            if(dto.ProductIds == null || !dto.ProductIds.Any()) return BadRequest<bool>(ValidationResources.ImageRequired);

            // Mapping 
            var entity = _mapper.MapModel<OfferDTO, Offer>(dto);

            if (dto.Image != null)
            {
                entity.ImagePath = await _fileUploadService.UploadFileAsync(dto.Image, "Offers", dto.ImagePath);
            }

            // update
            if(dto.Id!=default)
            {

                    await _offerProductTableRepository.RemoveRangeAsync(x => x.OfferId == dto.Id);
                    _offerTableRepository.Remove(dto.Id);
                    var offerAdded = await _offerTableRepository.AddAndReturnAsync(entity, userId);

                    // select OfferProduct
                    var OfferProducts = dto.ProductIds.Distinct().Select(productId => new OfferProduct
                    {
                        OfferId = offerAdded.Id,
                        ProductId = productId
                    }).ToList();

                   var isSaveds = await _offerProductTableRepository.AddRangeAsync(OfferProducts, userId);
                    if(isSaveds) return Success(true);
                    else return BadRequest<bool>();



            }
            // select OfferProduct
            entity.OfferProducts = dto.ProductIds.Distinct().Select(productId => new OfferProduct
                {
                    ProductId = productId
                }).ToList();



            var isSaved = await _offerTableRepository.SaveAsync(entity, userId);
            if (isSaved) return Success(true);
            else return BadRequest<bool>();

        }

        public  async Task<Response<IEnumerable<GetOfferDTO>>> GetAllWithRelationsAsync()
        {
            var entitiesList = await _offerTableRepository.GetTableNoTracking()
                .Include(p => p.OfferProducts).ThenInclude(x => x.Product)
                .Where(x =>x.CurrentState ==1)
                .OrderBy(x => Guid.NewGuid())
                .Take(6)
                //.AsSplitQuery()
                .ToListAsync();

            if (entitiesList == null) return NotFound<IEnumerable<GetOfferDTO>>();
            var dtoList = _mapper.MapList<Offer, GetOfferDTO>(entitiesList);
            
            return Success(dtoList);
        }

        public async Task<Response<GetOfferDTO>> FindByIdWithRelationsAsync(Guid Id)
        {
            var entity = await _offerTableRepository.GetTableAsTracking()
                .Include(p => p.OfferProducts).ThenInclude(x => x.Product)
               .FirstOrDefaultAsync(x => x.CurrentState == 1 && x.Id == Id);

            if (entity == null) return NotFound<GetOfferDTO>();
            var dto = _mapper.MapModel<Offer, GetOfferDTO>(entity);
            return Success(dto);
        }

    }
}
