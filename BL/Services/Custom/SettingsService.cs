using BL.Contracts.GeneralService.CMS;
using BL.Contracts.IMapper;
using BL.Contracts.Services.Custom;
using BL.DTO.Entities;
using BL.GenericResponse;
using BL.Services.Generic;
using DAL.Contracts.Repositories.Generic;
using Domains.Entities;
using Resources;

namespace BL.Services
{
    public class SettingsService : BaseService<Settings, SettingsDTO>, ISettingsService
    {
        private readonly ITableRepository<Settings> _settingsTableRepository;
        private readonly IFileUploadService _fileUploadService;
        public SettingsService(ITableRepository<Settings> settingsTableRepository, IFileUploadService fileUploadService, IBaseMapper mapper) : base(settingsTableRepository, mapper)
        {
            _settingsTableRepository = settingsTableRepository;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }

        public override async Task<Response<bool>> SaveAsync(SettingsDTO entityDTO, Guid userId)
        {
            if (entityDTO.Logo == null && entityDTO.LogoPath == null) return BadRequest<bool>(ValidationResources.ImageRequired);

            var entity = _mapper.MapModel<SettingsDTO, Settings>(entityDTO);

            if (entityDTO.Logo != null)
            {
                var imagePath = await _fileUploadService.UploadFileAsync(entityDTO.Logo, "Settings", entityDTO.LogoPath);
            }

            var isSaved = await _settingsTableRepository.SaveAsync(entity, userId);
            if (isSaved) return Success(true);
            else return BadRequest<bool>();

        }

    }
}
