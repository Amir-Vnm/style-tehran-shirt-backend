using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Shop.Application.Interface;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.CategoryService.Facade;
using Shop.Application.Services.ProductService.Facade;
using Shop.Persistance.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// 📦 Database
var connectionString = builder.Configuration.GetConnectionString("DbConnection")
    ?? throw new InvalidOperationException("Connection string 'DbConnection' not found.");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<ICategoryFacade, CategoryFacade>();
builder.Services.AddScoped<IProductFacade, ProductFacade>();

// ✅ تنظیم CORS به‌صورت مطمئن (Render + localhost)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "https://sstyle-tehran-shirt-frontend.onrender.com",
            "http://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ⚠️ ترتیب در .NET 8 خیلی مهمه
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// ✅ از اینجا شروع کن
app.UseRouting();

// ✅ CORS بعد از UseRouting ولی قبل از UseAuthorization
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

// ✅ Static files بعد از MapControllers
app.UseStaticFiles();

app.Run();
