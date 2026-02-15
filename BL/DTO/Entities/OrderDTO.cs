using Shared.DTOs.Base;

namespace BL.DTO.Entities
{
    public class OrderDTO : BaseDTO
    {
        
        public Guid Id { get; set; }
        public string BasketId { get; set; } = null!;
    }

}
