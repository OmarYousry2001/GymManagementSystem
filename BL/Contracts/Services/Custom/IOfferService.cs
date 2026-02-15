using BL.Contracts.Services.Generic;
using BL.DTO.Entities;
using BL.GenericResponse;
using Domains.Entities.Product;

namespace BL.Contracts.Services.Custom
{
    public interface IOfferService : IBaseService<Offer, OfferDTO>
    {
        public  Task<Response<IEnumerable<GetOfferDTO>>> GetAllWithRelationsAsync();
        public Task<Response<GetOfferDTO>> FindByIdWithRelationsAsync(Guid Id);
    }
}
