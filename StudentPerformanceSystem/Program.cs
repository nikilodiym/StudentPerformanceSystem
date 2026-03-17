using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceSystem.Data;
using StudentPerformanceSystem.Features.Users.Models;
// using StudentPerformanceSystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.RootDirectory = "/";
    options.Conventions.AddPageRoute("/Pages/Index", "/");
    options.Conventions.AddPageRoute("/Features/Students/Pages/Index", "/Students");
    options.Conventions.AddPageRoute("/Features/Courses/Index", "/Courses");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.UseStaticFiles();      
app.UseRouting();          

app.UseAuthentication();
app.UseAuthorization();

// app.UseMiddleware<LoginRequiredMiddleware>();
// app.UseMiddleware<RoleAccessMiddleware>();

app.MapRazorPages();

app.Run();