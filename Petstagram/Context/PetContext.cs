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

            modelBuilder.Entity<Pet>().HasData(
                new Pet
                {
                    Id = 1,
                    Name = "Diddy",
                    OwnerId = "Demo",
                },
                new Pet
                {
                    Id = 2,
                    Name = "Balto",
                    OwnerId = "Demo",
                },
                new Pet
                {
                    Id = 3,
                    Name = "Cat",
                    OwnerId = "Demo",
                }
             );

            modelBuilder.Entity<Picture>().HasData(
                new Picture
                {
                    Id = 1,
                    DisplayName = "A New Beginning",
                    FileName = "balto (1).jpg",
                    Story = "Balto looks back to the town he helped save with the medicine he brought back to save a small girls life. The townsfolk have finally accepted him, he is now the sled dog's leader, and he has won the heart of the love of his life. Things are finally starting to look up for him.",
                    UploadDate = DateTime.Parse("2022-8-25"),
                    LastModified = DateTime.Parse("2022-8-25")
                },
                new Picture
                {
                    Id = 2,
                    DisplayName = "Perilous Journey",
                    FileName = "balto (2).jpg",
                    Story = "Balto looks to the journey ahead, realizing it is not going to be an easy one. However, he is determined to deliver the medicine so that his love's owner can live. A storm is snow storm is coming, and the clock is ticking. Will he be able to make it in ti me?",
                    UploadDate = DateTime.Parse("2022-8-13"),
                    LastModified = DateTime.Parse("2022-8-13")
                },
                new Picture
                {
                    Id = 3,
                    DisplayName = "Unexpected Meeting",
                    FileName = "balto (3).jpg",
                    Story = "This is when Balto first met the love of his life, Jenna. Balto decided to join in a dog race being held in town and beat all the other sled dogs. However because the wown saw he was a wolf dog they shunned him and tried to run him out of town. In his escape he ran into Jenna and thus, a new love story was born.",
                    UploadDate = DateTime.Parse("2022-8-10"),
                    LastModified = DateTime.Parse("2022-8-10")
                },
                new Picture
                {
                    Id = 4,
                    DisplayName = "Real Life Legend",
                    FileName = "balto (4).jpg",
                    Story = "Balto is actually real, there is a statue dedicated to his heroics in Central Park, Manhattan. It is made of bronze by Frederick Roth. The plague at the base reads &quot;Dedicated tot he indomitable spirit of the sled dogs that relayed antitoxin six hundred miles over rough ice, across treacherous waters, through Artic blizzards from Nenana to the relief of stricken Nome in the Winter of 1925. Endurance · Fidelity · Intelligence&quot;",
                    UploadDate = DateTime.Parse("2023-6-15"),
                    LastModified = DateTime.Parse("2023-6-15")
                },
                new Picture
                {
                    Id = 5,
                    DisplayName = "Never Ending Friendship",
                    FileName = "balto (5).jpg",
                    Story = "Back when Balto was an outcast, his best friend and Father figure is this snow goose named Boris. They lived together in an abandoned boat outside of town. He is usually the one Balto turns to when he needs advice. He does his best to support Balto, and even became Uncle to his puppies.",
                    UploadDate = DateTime.Parse("2023-9-15"),
                    LastModified = DateTime.Parse("2023-9-15")
                },
                new Picture
                {
                    Id = 6,
                    DisplayName = "Wandering Around",
                    FileName = "cat (1).jpg",
                    Story = "Under the warm glow of the setting sun, an orange feline gracefully wanders through the vast expanse of a rural farm. Its fur, the color of autumn leaves, catches the last light as it meanders among the fields of swaying wheat. With each step, the cat's keen eyes survey the landscape, tracing the intricate patterns of the earth. Whiskers twitching with curiosity, it explores every corner of the farm, blending seamlessly with the rustic backdrop of old barns and green pastures. Pausing atop a weathered fence post, the feline gazes out over its domain, a silent ruler of the tranquil countryside.",
                    UploadDate = DateTime.Parse("2024-1-1"),
                    LastModified = DateTime.Parse("2024-1-1")
                },
                new Picture
                {
                    Id = 7,
                    DisplayName = "Deserved Rest",
                    FileName = "cat (2).jpg",
                    Story = "In the soft glow of daylight filtering through the windows, an orange cat cozies up on a sun-drenched patch of the living room floor, seeking refuge from the hustle and bustle of the outside world. With eyes gently closed, it succumbs to the allure of a midday siesta, the warmth of the sun's rays wrapping it in a comforting embrace. As the minutes tick by, the cat's rhythmic purrs harmonize with the quiet hum of the household, a gentle lullaby that soothes both body and soul. In this tranquil moment, it is a picture of pure contentment, nestled within the safety and warmth of its indoor sanctuary.",
                    UploadDate = DateTime.Parse("2024-2-1"),
                    LastModified = DateTime.Parse("2024-2-1")
                },
                new Picture
                {
                    Id = 8,
                    DisplayName = "Fear Him",
                    FileName = "cat (3).jpg",
                    Story = "As dawn breaks, the orange cat rouses from its slumber, shooting you a disgruntled look. Though still nestled in comfort, its eyes convey annoyance at the interruption. With a deliberate stretch and a reproachful stare, it silently demands restitution for the disturbance before settling back into its cozy cocoon, a silent reminder to tread lightly in its realm of rest.",
                    UploadDate = DateTime.Parse("2024-3-1"),
                    LastModified = DateTime.Parse("2024-3-1")
                },
                new Picture
                {
                    Id = 9,
                    DisplayName = "He Prowls Again",
                    FileName = "cat (4).jpg",
                    Story = "Beneath the afternoon sun, the orange cat prowls the backyard, eyes fixed on the grass. With a sudden burst of energy, it pounces, emerging victorious with a small bird in its jaws. With a triumphant glance, it disappears into the shadows, leaving behind the rustle of grass in its wake.",
                    UploadDate = DateTime.Parse("2024-3-2"),
                    LastModified = DateTime.Parse("2024-3-2")
                },
                new Picture
                {
                    Id = 10,
                    DisplayName = "Base Form Diddy",
                    FileName = "diddy (1).png",
                    Story = "Meet Diddy, the cute brown Chihuahua mix with a heart of gold. Despite his small size, Diddy is bursting with personality and charm. With his expressive eyes and perky ears, he's always ready to steal your heart with just a wag of his tail. Diddy loves cuddling in your lap and chasing his favorite toys with boundless energy. Fearless and affectionate, he brightens every day with his playful antics and unwavering loyalty. Life is simply better with Diddy around.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 11,
                    DisplayName = "Stare of Diddy",
                    FileName = "diddy (2).png",
                    Story = "Diddy, the adorable brown Chihuahua mix, may be small, but his stare is as serious as they come. With furrowed brows and intense eyes, he fixes his gaze on whatever captures his attention, be it a passing squirrel or a distant sound. In those moments, there's no mistaking his focus and determination. It's as if he's pondering life's deepest mysteries or standing guard over his beloved humans. Despite his playful nature, Diddy's serious stare reminds you that even the smallest of dogs can possess a big dose of gravitas.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 12,
                    DisplayName = "Stare of Diddy",
                    FileName = "diddy (3).png",
                    Story = "Imagine Diddy, the suave brown Chihuahua mix, sporting a pair of trendy glasses and a snazzy tie, exuding an air of effortless coolness. With his glasses perched just right and his tie impeccably knotted, he struts down the street, turning heads with his confident swagger. Diddy's cool demeanor is infectious, and he knows it, flashing a charming grin as he captures the attention of everyone he passes. Whether he's lounging in a cafe or basking in the sun at the park, Diddy's undeniable style and laid-back attitude make him the epitome of cool canine charm.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 13,
                    DisplayName = "Doge of War",
                    FileName = "diddy (4).png",
                    Story = "Diddy, the once cheerful brown Chihuahua mix, now bears the anger of Kratos, the god of war. His playful demeanor replaced by a fierce snarl, and his innocent gaze now burns with the intensity of ancient wrath. With each step, his paws echo the thunderous march of a warrior, and his every growl reverberates with the might of a deity. No longer content with simple games, Diddy prowls with the ferocity of a god in battle, his tiny frame a vessel for divine rage. Those who once knew him tremble in fear at the sight of his transformed form, for Diddy, now imbued with the wrath of Kratos, commands the respect and awe of mortals and gods alike.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 14,
                    DisplayName = "Legendary Super Doge",
                    FileName = "diddy (5).png",
                    Story = "Pushed to his limits, Diddy stood amidst the chaos of battle, his determination unwavering despite the odds stacked against him. As the clash intensified, a surge of energy enveloped him, filling him with an unprecedented power. With a mighty roar, he unleashed his latent strength, his fur glowing with a radiant aura. In that moment of sheer willpower, Diddy transcended his canine form, ascending to the ranks of the legendary super saiyan. With newfound power coursing through his veins, he soared into the fray, his bark echoing with the force of a thousand warriors as he faced his adversaries head-on, ready to defend his world at any cost.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 15,
                    DisplayName = "Mythical Beast Diddy",
                    FileName = "diddy (6).png",
                    Story = "In the realm of mystic wonders, Diddy stands tall as the mighty dog of magic. With a flick of his tail and a glint in his eye, he commands the very forces of enchantment. His fur glistens with the shimmer of ancient spells, and his bark echoes with the power of incantations long forgotten. As he roams through realms of mystical beauty, Diddy's presence sparks awe and wonder among all who behold him. With each step, he reaffirms his role as the guardian of magic, ensuring that its secrets remain safe and its wonders never fade.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                     Id = 16,
                     DisplayName = "Determination",
                     FileName = "diddy (7).png",
                     Story = "With unwavering resolve, Diddy stands tall, his eyes burning with determination. Despite the challenges that lay ahead, he remains steadfast in his pursuit of his goals. With each step forward, he channels his inner strength, ready to overcome any obstacle in his path. No setback can deter him, for his spirit is unbreakable and his will indomitable. With a heart full of courage and a mind set on success, Diddy presses on, confident in his ability to achieve greatness.",
                     UploadDate = DateTime.Now,
                     LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 17,
                    DisplayName = "Xmas Miracle",
                    FileName = "diddy (8).jpg",
                    Story = "In the heart of the North Pole, amidst the swirling snowflakes and twinkling lights, Santa Diddy reigns as the beloved canine companion of the holiday season. With his red coat trimmed in white fur and a jolly twinkle in his eye, he spreads cheer and goodwill to all he meets. From his sleigh pulled by a team of spirited reindeer to his jolly laugh that echoes through the frosty air, Santa Diddy embodies the magic of Christmas. With a wag of his tail and a hearty &quot;Ho, ho, ho&quot; he delivers joy to homes around the world, ensuring that the spirit of the season shines bright for all.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                },
                new Picture
                {
                    Id = 18,
                    DisplayName = "Rest Friend",
                    FileName = "diddy (9).jpg",
                    Story = "As the day draws to a close, Diddy finds solace in the soft embrace of his favorite spot—the bed. With a contented sigh, he curls up amidst the cozy blankets, his eyes heavy with the weight of the day's adventures. As he nestles into the warmth, a sense of tranquility washes over him, melting away the cares of the world. With each gentle breath, he sinks deeper into relaxation, the rhythmic rise and fall of his chest a soothing lullaby. In this moment of peaceful serenity, Diddy finds respite from the chaos of the day, basking in the simple joy of rest and relaxation.",
                    UploadDate = DateTime.Now,
                    LastModified = DateTime.Now
                }
            );

            modelBuilder.Entity<Pet>()
                .HasMany(x => x.Pictures)
                .WithMany(y => y.Pets)
                .UsingEntity(j => j.ToTable("PetPicture").HasData(new[]
                {
                    new { PetsId = 2, PicturesId = 1 },
                    new { PetsId = 2, PicturesId = 2 },
                    new { PetsId = 2, PicturesId = 3 },
                    new { PetsId = 2, PicturesId = 4 },
                    new { PetsId = 2, PicturesId = 5 },
                    new { PetsId = 3, PicturesId = 6 },
                    new { PetsId = 3, PicturesId = 7 },
                    new { PetsId = 3, PicturesId = 8 },
                    new { PetsId = 3, PicturesId = 9 },
                    new { PetsId = 1, PicturesId = 10 },
                    new { PetsId = 1, PicturesId = 11 },
                    new { PetsId = 1, PicturesId = 12 },
                    new { PetsId = 1, PicturesId = 13 },
                    new { PetsId = 1, PicturesId = 14 },
                    new { PetsId = 1, PicturesId = 15 },
                    new { PetsId = 1, PicturesId = 16 },
                    new { PetsId = 1, PicturesId = 17 },
                    new { PetsId = 1, PicturesId = 18 }
                }));
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
