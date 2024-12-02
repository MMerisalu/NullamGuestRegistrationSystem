using App.Contracts.DAL;
using App.Contracts.DAL.IAppRepositories;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using Base.DAL;
using Microsoft.EntityFrameworkCore;
using UoN.ExpressiveAnnotations.Net8.Attributes;
using UoN.ExpressiveAnnotations.Net8.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAppUnitOfWork, AppUOW>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddExpressiveAnnotations();
RequiredIfAttribute.DefaultErrorMessage = "Väli {0} on kohustuslik!";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404)
    {
        context.Response.Clear(); // Clear any existing response
        context.Response.StatusCode = 404; // Ensure the status code is 404

        // Set the content type
        context.Response.ContentType = "text/html";

        // Serve the custom 404 HTML template
        await context.Response.SendFileAsync("wwwroot/index.html");
    }
});






app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
