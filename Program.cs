using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartTutionHub.Data;
using SmartTutionHub.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add EF Core + DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnectionString")
    )
);

// 2. Add Identity (ApplicationUser, ApplicationRole)
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(opts =>
    {
        opts.Password.RequiredLength = 6;
        opts.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 3. Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 4. Error pages + HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// 5. HTTPS, Static files
app.UseHttpsRedirection();
app.UseStaticFiles();

// 6. Routing
app.UseRouting();

// 7. Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 8. Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
