
using Domains.Entities.Base;

namespace Domains.Entities
{
    public  class Settings : BaseEntity
    {
        public string? Location { get; set; } 
        public string Phone1 { get; set; } = null!;
        public string Phone2 { get; set; } = null!;
        public string? Landline { get; set; } 
        public string Email { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public string LogoPath { get; set; } = null!; // store image path
        public string? CopyrightText { get; set; }
        public string? FacebookLink { get; set; }


    }
}
