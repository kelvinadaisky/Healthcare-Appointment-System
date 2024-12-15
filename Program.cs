using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Data;
using Web_prog_Project.Models;
using Web_prog_Project.Services;
using Web_prog_Project.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Modifier les exigences de mot de passe
    options.Password.RequireDigit = false; // Ne nécessite pas de chiffres
    options.Password.RequiredLength = 6; // Longueur minimale
    options.Password.RequireNonAlphanumeric = false; // Ne nécessite pas de caractères non alphanumériques
    options.Password.RequireLowercase = false; // Ne nécessite pas de minuscules
    options.Password.RequireUppercase = false; // Ne nécessite pas de majuscules

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register your custom service
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// Register the EmailSender service
builder.Services.Configure<MailjetOptions>(builder.Configuration.GetSection("Mailjet")); // Bind Mailjet options
builder.Services.AddScoped<IEmailSender, EmailSender>(); // Register EmailSender

// Add session services
builder.Services.AddDistributedMemoryCache(); // Required for session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Prevent JavaScript access
    options.Cookie.IsEssential = true; // For GDPR compliance
});

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable Authentication Middleware
app.UseAuthorization();

app.UseSession(); // Add Session Middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
