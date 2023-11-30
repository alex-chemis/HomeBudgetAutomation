using HomeBudgetAutomation.Data;
using HomeBudgetAutomation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HomeBudgetAutomation.Controllers;
using HomeBudgetAutomation.Repositories.Contract;
using HomeBudgetAutomation.Repositories;
using HomeBudgetAutomation.Mapper;
using HomeBudgetAutomation.Services.Contract;
using HomeBudgetAutomation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));

builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services.AddScoped<IBalancesRepository, BalancesRepository>();
builder.Services.AddScoped<IOperationsRepository, OperationsRepository>();

builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IBalancesService, BalancesService>();
builder.Services.AddScoped<IOperationsService, OperationsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();
