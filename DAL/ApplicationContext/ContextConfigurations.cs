//using Domains.AppMetaData;
//using Domains.Entities;
//using Domains.Entities.Identity;
//using Domains.Identity;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace DAL.ApplicationContext
//{
//    public class ContextConfigurations
//    {
//        private static readonly string seedAdminEmail = "OmarSdah@gmail.com";
//        private static readonly string seedAdminPassword = "Sdah-123";

//        public static async Task SeedDataAsync(ApplicationDbContext context,
//            UserManager<ApplicationUser> userManager,
//            RoleManager<Role> roleManager)
//        {
//            // Seed user first to get admin user ID
//            var adminUserId = await SeedUserAsync(userManager, roleManager);

//            // Seed E-commerce data
//            await SeedProductDataAsync(context, adminUserId);
//        }

//        private static async Task SeedProductDataAsync(ApplicationDbContext context, Guid adminUserId)
//        {
//            // 1. Seed Categories
//            if (!context.Category.Any())
//            {
//                var categories = new List<Category>
//        {
//            new Category
//            {
//                Id = Guid.NewGuid(),
//                Name = "حنفيات",
//                CurrentState = 1,
//                CreatedBy = adminUserId,
//                CreatedDateUtc = DateTime.UtcNow
//            },
//            new Category
//            {
//                Id = Guid.NewGuid(),
//                Name = "دوشات",
//                CurrentState = 1,
//                CreatedBy = adminUserId,
//                CreatedDateUtc = DateTime.UtcNow
//            },
//            new Category
//            {
//                Id = Guid.NewGuid(),
//                Name = "إكسسوارات الحمام",
//                CurrentState = 1,
//                CreatedBy = adminUserId,
//                CreatedDateUtc = DateTime.UtcNow
//            }
//        };

//                await context.Category.AddRangeAsync(categories);
//                await context.SaveChangesAsync();
//            }

//            // 2. Seed Products
//            if (!context.Product.Any())
//            {
//                var faucetCategory = await context.Category.FirstOrDefaultAsync(c => c.Name == "حنفيات");
//                var showerCategory = await context.Category.FirstOrDefaultAsync(c => c.Name == "دوشات");
//                var accessoriesCategory = await context.Category.FirstOrDefaultAsync(c => c.Name == "إكسسوارات الحمام");

//                var products = new List<Product>
//        {
//            new Product
//            {
//                Id = Guid.NewGuid(),
//                Name = "حنفية مطبخ ستانلس ستيل",
//                Description = "حنفية مطبخ عالية الجودة مصنوعة من الستانلس ستيل المقاوم للصدأ",
//                Price = 1200.00m,
//                ImagePath="images/products/smartphone.webp",
//                CategoryId = faucetCategory.Id,
//                CurrentState = 1,
//                CreatedBy = adminUserId,
//                CreatedDateUtc = DateTime.UtcNow
//            },
//            new Product
//            {
//                Id = Guid.NewGuid(),
//                Name = "دوش كهربائي حديث",
//                Description = "دوش كهربائي بخصائص توفير الماء والتحكم في درجة الحرارة",
//                Price = 950.00m,
//                ImagePath="images/products/smartphone.webp",
//                CategoryId = showerCategory.Id,
//                CurrentState = 1,
//                CreatedBy = adminUserId,
//                CreatedDateUtc = DateTime.UtcNow
//            },
//            new Product
//            {
//                Id = Guid.NewGuid(),
//                Name = "حامل صابون وستارة حمام",
//                Description = "إكسسوارات الحمام عالية الجودة لتزيين وتنظيم حمامك",
//                Price = 300.00m,
//                CategoryId = accessoriesCategory.Id,
//                CurrentState = 1,
//                ImagePath="images/products/smartphone.webp",
//                CreatedBy = adminUserId,
//                CreatedDateUtc = DateTime.UtcNow
//            }
//        };

//                await context.Product.AddRangeAsync(products);
//                await context.SaveChangesAsync();
//            }

//            // 3. Seed Settings
//            if (!context.Settings.Any())
//            {
//                var settings = new Settings
//                {
//                    Id = Guid.NewGuid(),
//                    Location = "القاهرة - مصر",
//                    Phone1 = "+20 127 618 3707",
//                    Phone2 = "+20 100 123 4567",
//                    Landline = "02-12345678",
//                    Email = "info@myshop.com",
//                    AboutMe = "نحن منصة تجارة إلكترونية حديثة تقدم منتجات عالية الجودة للمنزل والحياة اليومية.",
//                    LogoPath = "images/logo/myshop-logo.webp",
//                    CopyrightText = "© 2025 MyShop. جميع الحقوق محفوظة.",
//                    CurrentState = 1,
//                    CreatedBy = adminUserId,
//                    CreatedDateUtc = DateTime.UtcNow
//                };

//                await context.Settings.AddAsync(settings);
//                await context.SaveChangesAsync();
//            }
//        }

//        private static async Task<Guid> SeedUserAsync(UserManager<ApplicationUser> userManager,
//                   RoleManager<Role> roleManager)
//        {
//            // Ensure roles exist
//            if (!await roleManager.RoleExistsAsync(Roles.User))
//            {
//                await roleManager.CreateAsync(new Role { Name = Roles.User });
//            }
//            // Ensure roles exist
//            if (!await roleManager.RoleExistsAsync(Roles.Admin))
//            {
//                await roleManager.CreateAsync(new Role { Name = Roles.Admin });
//            }

//            // Ensure admin user exists
//            var adminEmail = seedAdminEmail;
//            var adminUser = await userManager.FindByEmailAsync(adminEmail);
//            if (adminUser == null)
//            {
//                var id = Guid.NewGuid().ToString();
//                adminUser = new ApplicationUser
//                {
//                    Id = id,
//                    UserName = "Admin",
//                    Email = adminEmail,
//                    EmailConfirmed = true,
//                    CreatedDateUtc = DateTime.UtcNow
//                };
//                var result = await userManager.CreateAsync(adminUser, seedAdminPassword);
//                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
//            }

//            // Convert adminUser.Id from string to Guid
//            return Guid.Parse(adminUser.Id);  // Convert to Guid
//        }
//    }
//}
