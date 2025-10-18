using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Application.Interface;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.CategoryService.Facade;
using Shop.Application.Services.ProductService.Facade;
using Shop.Persistance.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// 📦 اتصال به دیتابیس
var connectionString = builder.Configuration.GetConnectionString("DbConnection")
    ?? throw new InvalidOperationException("Connection string 'DbConnection' not found.");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<ICategoryFacade, CategoryFacade>();
builder.Services.AddScoped<IProductFacade, ProductFacade>();

// ✅ تنظیم CORS برای React frontend و localhost
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "https://sstyle-tehran-shirt-frontend.onrender.com", // فرانت واقعی
            "http://localhost:5173" // برای تست لوکال
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // اگه از کوکی/توکن استفاده می‌کنی لازمه
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ ترتیب دقیق middlewareها (خیلی مهم!)
app.UseCors("AllowFrontend"); // باید قبل از بقیه باشه
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ✅ فایل‌های استاتیک در آخر
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CategoryImage")),
    RequestPath = "/CategoryImage"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Product")),
    RequestPath = "/Product"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot/images")),
    RequestPath = "/images"
});

app.Run();
