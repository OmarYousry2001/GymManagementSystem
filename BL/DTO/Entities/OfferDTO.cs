using Microsoft.AspNetCore.Http;
using Resources;
using Shared.CustomValidation;
using Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BL.DTO.Entities
{
    public class OfferDTO : BaseDTO
    {
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceName = "FieldLength", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ValidationResources))]
        public decimal DiscountedPrice { get; set; }
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(1000, MinimumLength = 2, ErrorMessageResourceName = "DescriptionLength", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Description { get; set; } = null!;
        public string? ImagePath { get; set; } = null!;
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp" })]
        [MaxFileSize(5)]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "At least one product must be selected.")]
        public List<Guid> ProductIds { get; set; } = new();

    }

    public class GetOfferDTO : OfferDTO
    {
         public ICollection<GetProductDTO> Products { get; set; } = new HashSet<GetProductDTO> ();
        public int CountProduct => Products.Count();
        public decimal? PriceBeforeDiscount => Products.Sum(x => x.Price);
    }
}
