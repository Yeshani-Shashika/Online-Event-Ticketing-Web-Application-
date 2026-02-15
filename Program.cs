using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using StarEvents.Data;
using StarEvents.Models;
using StarEvents.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure MySQL with Pomelo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add Cookie Authentication (simple, no Identity)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

// Register custom services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = services.GetRequiredService<IPasswordHasher>();
        await SeedDatabase(context, passwordHasher);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();

// Seed database method
static async Task SeedDatabase(ApplicationDbContext context, IPasswordHasher passwordHasher)
{
    // Ensure database is created
    await context.Database.EnsureCreatedAsync();

    // Check if admin user exists
    if (!await context.Users.AnyAsync(u => u.Email == "admin@starevents.lk"))
    {
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
        if (adminRole != null)
        {
            var admin = new User
            {
                FirstName = "System",
                LastName = "Administrator",
                Email = "admin@starevents.lk",
                PasswordHash = passwordHasher.HashPassword("Admin@123"),
                PhoneNumber = "0771234567",
                RoleId = adminRole.RoleId,
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }

    // Seed sample promotional codes
    if (!await context.Promotions.AnyAsync())
    {
        var promotions = new List<Promotion>
        {
            new Promotion
            {
                PromotionCode = "WELCOME10",
                Description = "Welcome discount - 10% off",
                DiscountPercentage = 10.00m,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(30),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Promotion
            {
                PromotionCode = "SUMMER25",
                Description = "Summer special - 25% off",
                DiscountPercentage = 25.00m,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(60),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Promotion
            {
                PromotionCode = "EARLYBIRD15",
                Description = "Early bird discount - 15% off",
                DiscountPercentage = 15.00m,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(90),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Promotion
            {
                PromotionCode = "VIP20",
                Description = "VIP discount - 20% off",
                DiscountPercentage = 20.00m,
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(365),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Promotions.AddRange(promotions);
        await context.SaveChangesAsync();
    }
}
