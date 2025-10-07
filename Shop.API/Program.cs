using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Application.Interface;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.CategoryService.Facade;
using Shop.Application.Services.ProductService.Facade;
using Shop.Persistance.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DbConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<ICategoryFacade, CategoryFacade>();
builder.Services.AddScoped<IProductFacade, ProductFacade>();

// CORS برای فرانت
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// فعال‌سازی wwwroot کلی
app.UseStaticFiles();

// 🔧 فعال‌سازی مسیر استاتیک برای CategoryImage
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CategoryImage")),
    RequestPath = "/CategoryImage"
});

// 🔧 فعال‌سازی مسیر استاتیک برای Product
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Product")),
    RequestPath = "/Product"
});

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot/images")),
    RequestPath = "/images"
});
