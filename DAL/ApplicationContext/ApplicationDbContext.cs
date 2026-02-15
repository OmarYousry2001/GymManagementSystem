
using Domains.Entities;
using Domains.Entities.Identity;
using Domains.Identity;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.ApplicationContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser , Domains.Entities.Identity.Role, string>
    {
        private readonly IEncryptionProvider _encryptionProvider;
 
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _encryptionProvider = new GenerateEncryptionProvider("7376416c78014aa894a4daeee1a32276");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.UseEncryption(_encryptionProvider);

            #region Order Confgiration


            #endregion

        }
    }

}
