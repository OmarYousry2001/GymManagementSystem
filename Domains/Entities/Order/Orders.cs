
using Domains.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Order
{
    public class Orders : BaseEntity
    {
        public Orders() { }
        public Orders( decimal total, IReadOnlyList<OrderItem> orderItems)
        {
            Total = total;
            this.orderItems = orderItems;
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public IReadOnlyList<OrderItem> orderItems { get; set; }

        public decimal GetTotal()
        {
            return Total;
        }

    }
}
