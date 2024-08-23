using Core.MapperProfiles;
using Data.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PV212.Services;
using Microsoft.AspNetCore.Identity;
using Data.Entities;
using Core.Interfaces;
using Core.Services;
using ShopMvcApp_PV212.SeedExtensions;  
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("LocalDb");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ShopDbContext>(options => 
    options.UseSqlServer(connectionString)
);

builder.Services.AddIdentity<User, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddDefaultUI() // fix error with IEmailSender
    .AddEntityFrameworkStores<ShopDbContext>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(AppProfile));

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ------ configure custom services
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IFilesService, FilesService>();
builder.Services.AddScoped<IEmailSender, EmailService>();

var app = builder.Build();

// -------------- Seed Initial Data
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.SeedRoles().Wait();
    scope.ServiceProvider.SeedAdmin().Wait();
}

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

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
