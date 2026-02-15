
using Domains.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities.Product
{
    public class Offer : BaseEntity
    {
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountedPrice { get; set; }
   
        public string Description { get; set; } = null!;
        public string? ImagePath { get; set; } = null!;

        public ICollection<OfferProduct> OfferProducts { get; set; } = new HashSet<OfferProduct>();

        ////
        //public decimal? PriceBeforeDiscount { get; set; }
    }
}
