using BL.Contracts.IMapper;
using BL.Contracts.Services.Custom;
using BL.DTO.Entities;
using BL.GenericResponse;
using BL.Services.Generic;
using DAL.Contracts.Repositories.Generic;
using Domains.Entities;

namespace BL.Services
{
    public class CommentService : BaseService<Comment, CommentDTO>, ICommentService
    {
        private readonly ITableRepository<Comment> _commentTableRepository;
        public CommentService(ITableRepository<Comment> commentTableRepository, IBaseMapper mapper) : base(commentTableRepository, mapper)
        {
            _commentTableRepository = commentTableRepository;
            _mapper = mapper;
        }
        
        public async Task<Response<IEnumerable<CommentPublicDTO>>> GetAllCommentPublicAsync()
        {

            var entitiesList = await _commentTableRepository.GetAllAsync();
            if (entitiesList == null) return NotFound<IEnumerable<CommentPublicDTO>>();
            var dtoList = _mapper.MapList<Comment, CommentPublicDTO>(entitiesList);
            return Success(dtoList);
        }


    }
}
