using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petstagram.Context;
using Petstagram.Models;
using Petstagram.Repositories;
using Petstagram.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DB Context
var connectionString = builder.Configuration.GetConnectionString("AzureDb");
builder.Services.AddDbContext<PetContext>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
//builder.Services.AddDbContext<PetContext>(options =>
//{
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//});


//Repository
builder.Services.AddScoped<IRepository, Repository>();

//HTML Sanitizer
builder.Services.AddScoped<HtmlSanitizerService>();
//Structure and Sort
builder.Services.AddScoped<StructureService>();

//Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PetContext>()
    .AddDefaultTokenProviders();

//Change login pagth
builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Home/Login");

//session
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "demo",
        areaName: "Demo",
        pattern: "Demo/{controller=Demo}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

await PetContext.CreateAdminuser(app.Services);

app.Run();
