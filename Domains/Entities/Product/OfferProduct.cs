
using Domains.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities.Product
{
    public class OfferProduct : BaseEntity
    {
        public Guid OfferId { get; set; }
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; } = null!;
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!;
    }
}
