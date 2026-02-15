using Domains.Entities.Base;

namespace Domains.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
