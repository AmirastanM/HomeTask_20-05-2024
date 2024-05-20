using FiorelloBack.Data;
using FiorelloBack.Services;
using FiorelloBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllersWithViews();


    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<ISliderService,SliderService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();



var app = builder.Build();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();


app.Run();
