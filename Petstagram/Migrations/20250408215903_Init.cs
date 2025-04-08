using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Petstagram.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetPicture",
                columns: table => new
                {
                    PetsId = table.Column<int>(type: "int", nullable: false),
                    PicturesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetPicture", x => new { x.PetsId, x.PicturesId });
                    table.ForeignKey(
                        name: "FK_PetPicture_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetPicture_Pictures_PicturesId",
                        column: x => x.PicturesId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Diddy", "Demo" },
                    { 2, "Balto", "Demo" },
                    { 3, "Cat", "Demo" }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "DisplayName", "FileName", "LastModified", "Story", "UploadDate" },
                values: new object[,]
                {
                    { 1, "A New Beginning", "balto (1).jpg", new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Balto looks back to the town he helped save with the medicine he brought back to save a small girls life. The townsfolk have finally accepted him, he is now the sled dog's leader, and he has won the heart of the love of his life. Things are finally starting to look up for him.", new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Perilous Journey", "balto (2).jpg", new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Balto looks to the journey ahead, realizing it is not going to be an easy one. However, he is determined to deliver the medicine so that his love's owner can live. A storm is snow storm is coming, and the clock is ticking. Will he be able to make it in ti me?", new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Unexpected Meeting", "balto (3).jpg", new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is when Balto first met the love of his life, Jenna. Balto decided to join in a dog race being held in town and beat all the other sled dogs. However because the wown saw he was a wolf dog they shunned him and tried to run him out of town. In his escape he ran into Jenna and thus, a new love story was born.", new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Real Life Legend", "balto (4).jpg", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Balto is actually real, there is a statue dedicated to his heroics in Central Park, Manhattan. It is made of bronze by Frederick Roth. The plague at the base reads &quot;Dedicated tot he indomitable spirit of the sled dogs that relayed antitoxin six hundred miles over rough ice, across treacherous waters, through Artic blizzards from Nenana to the relief of stricken Nome in the Winter of 1925. Endurance · Fidelity · Intelligence&quot;", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Never Ending Friendship", "balto (5).jpg", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Back when Balto was an outcast, his best friend and Father figure is this snow goose named Boris. They lived together in an abandoned boat outside of town. He is usually the one Balto turns to when he needs advice. He does his best to support Balto, and even became Uncle to his puppies.", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Wandering Around", "cat (1).jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Under the warm glow of the setting sun, an orange feline gracefully wanders through the vast expanse of a rural farm. Its fur, the color of autumn leaves, catches the last light as it meanders among the fields of swaying wheat. With each step, the cat's keen eyes survey the landscape, tracing the intricate patterns of the earth. Whiskers twitching with curiosity, it explores every corner of the farm, blending seamlessly with the rustic backdrop of old barns and green pastures. Pausing atop a weathered fence post, the feline gazes out over its domain, a silent ruler of the tranquil countryside.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Deserved Rest", "cat (2).jpg", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In the soft glow of daylight filtering through the windows, an orange cat cozies up on a sun-drenched patch of the living room floor, seeking refuge from the hustle and bustle of the outside world. With eyes gently closed, it succumbs to the allure of a midday siesta, the warmth of the sun's rays wrapping it in a comforting embrace. As the minutes tick by, the cat's rhythmic purrs harmonize with the quiet hum of the household, a gentle lullaby that soothes both body and soul. In this tranquil moment, it is a picture of pure contentment, nestled within the safety and warmth of its indoor sanctuary.", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Fear Him", "cat (3).jpg", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "As dawn breaks, the orange cat rouses from its slumber, shooting you a disgruntled look. Though still nestled in comfort, its eyes convey annoyance at the interruption. With a deliberate stretch and a reproachful stare, it silently demands restitution for the disturbance before settling back into its cozy cocoon, a silent reminder to tread lightly in its realm of rest.", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "He Prowls Again", "cat (4).jpg", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beneath the afternoon sun, the orange cat prowls the backyard, eyes fixed on the grass. With a sudden burst of energy, it pounces, emerging victorious with a small bird in its jaws. With a triumphant glance, it disappears into the shadows, leaving behind the rustle of grass in its wake.", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Base Form Diddy", "diddy (1).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9802), "Meet Diddy, the cute brown Chihuahua mix with a heart of gold. Despite his small size, Diddy is bursting with personality and charm. With his expressive eyes and perky ears, he's always ready to steal your heart with just a wag of his tail. Diddy loves cuddling in your lap and chasing his favorite toys with boundless energy. Fearless and affectionate, he brightens every day with his playful antics and unwavering loyalty. Life is simply better with Diddy around.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9748) },
                    { 11, "Stare of Diddy", "diddy (2).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9807), "Diddy, the adorable brown Chihuahua mix, may be small, but his stare is as serious as they come. With furrowed brows and intense eyes, he fixes his gaze on whatever captures his attention, be it a passing squirrel or a distant sound. In those moments, there's no mistaking his focus and determination. It's as if he's pondering life's deepest mysteries or standing guard over his beloved humans. Despite his playful nature, Diddy's serious stare reminds you that even the smallest of dogs can possess a big dose of gravitas.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9805) },
                    { 12, "Stare of Diddy", "diddy (3).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9811), "Imagine Diddy, the suave brown Chihuahua mix, sporting a pair of trendy glasses and a snazzy tie, exuding an air of effortless coolness. With his glasses perched just right and his tie impeccably knotted, he struts down the street, turning heads with his confident swagger. Diddy's cool demeanor is infectious, and he knows it, flashing a charming grin as he captures the attention of everyone he passes. Whether he's lounging in a cafe or basking in the sun at the park, Diddy's undeniable style and laid-back attitude make him the epitome of cool canine charm.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9809) },
                    { 13, "Doge of War", "diddy (4).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9815), "Diddy, the once cheerful brown Chihuahua mix, now bears the anger of Kratos, the god of war. His playful demeanor replaced by a fierce snarl, and his innocent gaze now burns with the intensity of ancient wrath. With each step, his paws echo the thunderous march of a warrior, and his every growl reverberates with the might of a deity. No longer content with simple games, Diddy prowls with the ferocity of a god in battle, his tiny frame a vessel for divine rage. Those who once knew him tremble in fear at the sight of his transformed form, for Diddy, now imbued with the wrath of Kratos, commands the respect and awe of mortals and gods alike.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9813) },
                    { 14, "Legendary Super Doge", "diddy (5).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9819), "Pushed to his limits, Diddy stood amidst the chaos of battle, his determination unwavering despite the odds stacked against him. As the clash intensified, a surge of energy enveloped him, filling him with an unprecedented power. With a mighty roar, he unleashed his latent strength, his fur glowing with a radiant aura. In that moment of sheer willpower, Diddy transcended his canine form, ascending to the ranks of the legendary super saiyan. With newfound power coursing through his veins, he soared into the fray, his bark echoing with the force of a thousand warriors as he faced his adversaries head-on, ready to defend his world at any cost.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9817) },
                    { 15, "Mythical Beast Diddy", "diddy (6).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9823), "In the realm of mystic wonders, Diddy stands tall as the mighty dog of magic. With a flick of his tail and a glint in his eye, he commands the very forces of enchantment. His fur glistens with the shimmer of ancient spells, and his bark echoes with the power of incantations long forgotten. As he roams through realms of mystical beauty, Diddy's presence sparks awe and wonder among all who behold him. With each step, he reaffirms his role as the guardian of magic, ensuring that its secrets remain safe and its wonders never fade.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9821) },
                    { 16, "Determination", "diddy (7).png", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9827), "With unwavering resolve, Diddy stands tall, his eyes burning with determination. Despite the challenges that lay ahead, he remains steadfast in his pursuit of his goals. With each step forward, he channels his inner strength, ready to overcome any obstacle in his path. No setback can deter him, for his spirit is unbreakable and his will indomitable. With a heart full of courage and a mind set on success, Diddy presses on, confident in his ability to achieve greatness.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9826) },
                    { 17, "Xmas Miracle", "diddy (8).jpg", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9831), "In the heart of the North Pole, amidst the swirling snowflakes and twinkling lights, Santa Diddy reigns as the beloved canine companion of the holiday season. With his red coat trimmed in white fur and a jolly twinkle in his eye, he spreads cheer and goodwill to all he meets. From his sleigh pulled by a team of spirited reindeer to his jolly laugh that echoes through the frosty air, Santa Diddy embodies the magic of Christmas. With a wag of his tail and a hearty &quot;Ho, ho, ho&quot; he delivers joy to homes around the world, ensuring that the spirit of the season shines bright for all.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9830) },
                    { 18, "Rest Friend", "diddy (9).jpg", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9835), "As the day draws to a close, Diddy finds solace in the soft embrace of his favorite spot—the bed. With a contented sigh, he curls up amidst the cozy blankets, his eyes heavy with the weight of the day's adventures. As he nestles into the warmth, a sense of tranquility washes over him, melting away the cares of the world. With each gentle breath, he sinks deeper into relaxation, the rhythmic rise and fall of his chest a soothing lullaby. In this moment of peaceful serenity, Diddy finds respite from the chaos of the day, basking in the simple joy of rest and relaxation.", new DateTime(2025, 4, 8, 16, 59, 3, 280, DateTimeKind.Local).AddTicks(9834) }
                });

            migrationBuilder.InsertData(
                table: "PetPicture",
                columns: new[] { "PetsId", "PicturesId" },
                values: new object[,]
                {
                    { 1, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 1, 15 },
                    { 1, 16 },
                    { 1, 17 },
                    { 1, 18 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PetPicture_PicturesId",
                table: "PetPicture",
                column: "PicturesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PetPicture");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
