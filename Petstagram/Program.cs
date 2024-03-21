using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petstagram.Context;
using Petstagram.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DB Context
var connectionString = builder.Configuration.GetConnectionString("MySqlConn");
builder.Services.AddDbContext<PetContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PetContext>()
    .AddDefaultTokenProviders();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
