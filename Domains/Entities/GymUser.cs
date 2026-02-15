
using Domains.Entities.Base;
using GymManagement.DAL.Entities.Enums;

namespace Domains.Entities
{
    public abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public Address Address { get; set; } = null!;
    }
}
