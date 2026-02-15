using BL.Contracts.Services.Generic;
using BL.DTO.Entities;
using BL.GenericResponse;
using Domains.Entities;

namespace BL.Contracts.Services.Custom
{
    public interface ICommentService : IBaseService<Comment, CommentDTO>
    {
        public Task<Response<IEnumerable<CommentPublicDTO>>> GetAllCommentPublicAsync();
        
    }
}
