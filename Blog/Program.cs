using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IFileManager, FileManager>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    //.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Auth/Login");

builder.Services.AddMvc(d => d.EnableEndpointRouting = false);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.EnsureCreated();

    var adminRole = new IdentityRole("Admin");
    if (!context.Roles.Any())
    {
        roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
    }

    if (!context.Users.Any(x => x.UserName == "admin"))
    {
        var adminUser = new IdentityUser()
        {
            UserName = "admin",
            Email = "admin@test.com"
        };
        userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

        userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
    }
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseMvcWithDefaultRoute();

app.Run();