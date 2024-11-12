using App.Contracts.DAL;
using App.Contracts.DAL.IAppRepositories;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using Base.DAL;
using Microsoft.EntityFrameworkCore;
using UoN.ExpressiveAnnotations.Net8.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAppUnitOfWork, AppUOW>();
//builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddExpressiveAnnotations();


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
