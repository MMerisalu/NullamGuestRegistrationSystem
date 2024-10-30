using System.Configuration;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System;
using App.DAL.EF;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddLocalization(options => { options.ResourcesPath = ""; });

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

/*

//builder.Services.AddApiVersioning(options =>
//{
//    options.ReportApiVersions = true;
//    // in case of no explicit version
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//});
//builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SchemaFilter<ApiSchemaFilter>();
//});


//builder.Services.AddSingleton<IConfigureOptions<MvcOptions>,
//    ConfigureModelBindingLocalization>();




//builder.Services.AddAutoMapper(typeof(App.DAL.EF.AutoMapperConfig),
//    typeof(App.BLL.AutoMapperConfig),
//    typeof(WebApp.ApiControllers.v1.AutoMapperConfig));
/*builder.Services.AddAutoMapper(
    typeof(App.DAL.EF.AutoMapperConfig));*/



builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(options => { options.SlidingExpiration = true; })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });





builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsAllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});





var app = builder.Build();


//await DataHelper.SetupAppData(app, app.Environment, app.Configuration);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseCors("CorsAllowAll");


var requestLocalizationOptions = ((IApplicationBuilder)app).ApplicationServices
    .GetService<IOptions<RequestLocalizationOptions>>()?.Value;
if (requestLocalizationOptions != null)
    app.UseRequestLocalization(requestLocalizationOptions);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    "areas",
    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");


// serve from root
// options.RoutePrefix = string.Empty;

app.Run();
