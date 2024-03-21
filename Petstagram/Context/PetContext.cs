using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petstagram.Models;

namespace Petstagram.Context
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PetPicture> PetPictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PetPicture>()
                .HasKey(pp => new {pp.PetId, pp.PictureId});

            modelBuilder.Entity<PetPicture>()
                .HasOne(pp => pp.Pet)
                .WithMany(p => p.PetPictures)
                .HasForeignKey(pp => pp.PetId);

            modelBuilder.Entity<PetPicture>()
                .HasOne(pp => pp.Picture)
                .WithMany(p => p.PetPictures)
                .HasForeignKey(pp => pp.PictureId);
        }

        public async Task CreateAdminuserAsync(IServiceProvider services)
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
