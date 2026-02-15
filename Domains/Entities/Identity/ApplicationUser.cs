using Domains.Entities.Identity;
using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace Domains.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public int CurrentState { get; set; } = 1;
        public DateTime LastLoginDate { get; set; }
        public ICollection<UserRefreshToken> RefreshTokens { get; set; } = new List<UserRefreshToken>();

        [EncryptColumn]
        public string? Code { get; set; }

        // additional properties can be added here as needed, such as FirstName, LastName, etc.
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
