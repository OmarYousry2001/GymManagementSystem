
using Domains.Entities.Base;

namespace Domains.Entities
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Message { get; set; } = null!;

    }
}
