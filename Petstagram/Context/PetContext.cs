using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petstagram.Models;

namespace Petstagram.Context
{
    public class PetContext : IdentityDbContext<User>
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>()
                .HasMany(x => x.Pictures)
                .WithMany(y => y.Pets)
                .UsingEntity(j => j.ToTable("PetPicture"));
            
        }

    public static async Task CreateAdminuser(IServiceProvider services)
        {
            using(var scoped = services.CreateScope())
            {
                UserManager<User> userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string username = "Admin";
                string password = "Nintendo64!";
                string roleName = "Admin";

                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                if(await userManager.FindByNameAsync(username) == null)
                {
                    User user = new User
                    {
                        UserName = username,
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if(result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
        }
    }
}
