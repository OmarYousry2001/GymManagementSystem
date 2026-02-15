using GymManagement.DAL.Entities.Enums;

namespace Domains.Entities
{
    public class Trainer : GymUser
    {
        public SpecialtiesEnum Specialties { get; set; }

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
