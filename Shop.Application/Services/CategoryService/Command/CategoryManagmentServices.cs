using Shop.Application.Interface;
using Shop.Application.Services.CategoryService.Command.Dto;
using Shop.Common;
using Shop.Common.Constants;
using Shop.Common.Dto;
using Shop.Domain.Entities;
using System.Net;

namespace Shop.Application.Services.CategoryService.Command
{
    public class CategoryManagmentServices(IDataBaseContext context) : BaseService(context), ICategoryManagmentServices
    {
        public async Task<ApiResult> AddCategory(AddCategoryDto request)
        {
            try
            {
                if (!request.ImageFile.FileName.IsValidImageFile())
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "فرمت فایل تصویر نامعتبر است"
                    };
                }

                var imageName = await request.ImageFile.SaveFile(ImageConstants.CategoryImageAddress);

                var category = new Category
                {
                    CreateAt = DateTime.Now,
                    ImageUrl = imageName,
                    Name = request.Name
                };

                Insert<Category>(category);
                await Save();

                return new ApiResult
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.Created,
                    Message = "دسته‌بندی با موفقیت اضافه شد"
                };
            }
            catch (Exception)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "خطا در سرور"
                };
            }
        }

        public async Task<ApiResult> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "دسته‌بندی یافت نشد"
                };
            }

            // حذف تصویر دسته‌بندی
            if (!string.IsNullOrEmpty(category.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImage", category.ImageUrl);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            // حذف محصولات مرتبط
            var products = _context.Products.Where(p => p.CategoryId == id).ToList();
            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var productImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage", product.ImageUrl);
                    if (File.Exists(productImagePath))
                    {
                        File.Delete(productImagePath);
                    }
                }
            }
            _context.Products.RemoveRange(products);

            // حذف نرم دسته‌بندی
            category.IsDelete = true;
            category.DeleteAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResult
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "دسته‌بندی و محصولات مرتبط حذف شدند"
            };
        }

        public async Task<ApiResult> EditCategory(EditCategoryDto request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category is null)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "دسته‌بندی یافت نشد"
                };
            }

            category.Name = request.Name;

            if (request.ImageFile is { Length: > 0 })
            {
                if (!request.ImageFile.FileName.IsValidImageFile())
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "فرمت فایل تصویر نامعتبر است"
                    };
                }

                // حذف تصویر قبلی
                if (!string.IsNullOrEmpty(category.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImage", category.ImageUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                // ذخیره تصویر جدید
                var newImageName = await request.ImageFile.SaveFile(ImageConstants.CategoryImageAddress);
                category.ImageUrl = newImageName;
            }

            await _context.SaveChangesAsync();

            return new ApiResult
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "دسته‌بندی با موفقیت به‌روز شد"
            };
        }
    }
}
