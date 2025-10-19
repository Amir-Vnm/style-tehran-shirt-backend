using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Application.Interface;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.CategoryService.Facade;
using Shop.Application.Services.ProductService.Facade;
using Shop.Persistance.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = builder.Configuration.GetConnectionString("DbConnection")
    ?? throw new InvalidOperationException("Connection string 'DbConnection' not found.");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<ICategoryFacade, CategoryFacade>();
builder.Services.AddScoped<IProductFacade, ProductFacade>();

// CORS (موقتا برای دیباگ AllowAnyOrigin تا ببینیم header اضافه میشه یا نه)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            //.WithOrigins("https://sstyle-tehran-shirt-frontend.onrender.com", "http://localhost:5173")
            .AllowAnyOrigin()   // موقت برای تست — بعد از حل شدن، به origins محدود کن
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// *** ترتیب مهم است ***
app.UseRouting();

// CORS بعد از UseRouting و قبل از Authorization/MapControllers
app.UseCors("AllowFrontend");

// فعلاً HttpsRedirection را غیرفعال کن (Render خود HTTPS را مدیریت می‌کند)
 // app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();      // موقت: Swagger فعال برای همه محیط‌ها
app.UseSwaggerUI();    // موقت: برای دیباگ

app.MapControllers();

// static files بعد از MapControllers
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
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
    db.Database.EnsureCreated(); // جدول‌ها را می‌سازد اگر وجود نداشته باشند
}

app.Run();
