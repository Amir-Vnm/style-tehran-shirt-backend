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
                        Message = "فایل ورودی فرمت نامعتبری دارد"
                    };
                }
                var imagename = await request.ImageFile.SaveFile(ImageConstants.CategoryImageAddress);
                var category = new Category
                {
                    CreateAt = DateTime.Now,
                    ImageUrl = imagename,
                    Name = request.Name,
                };
                Insert<Category>(category);
                await Save();

                return new ApiResult
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.Created,
                    Message = "دسته بندی با موفقیت اضافه شد"
                };
            }
            catch (Exception)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "سرور در دسترس نیست"
                };
            }
        }

        public async Task<ApiResult> DeleteCategory(long id)
        {
            var category =await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "موردی یاقت نشد"
                };
            }
            category.IsDelete = true;
            category.DeleteAt = DateTime.Now;
           await _context.SaveChangesAsync();
            return new ApiResult
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "دسته بندی حدف شد"
            };
        }

        public async Task<ApiResult> EditCategory(EditCategoryDto request)
        {
            var category =await _context.Categories.FindAsync(request.Id);
            if (category is null)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "موردی یافت نشد"
                };
            }
            category.Name = request.Name;

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                if (!request.ImageFile.FileName.IsValidImageFile())
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "فایل ورودی فرمت نامعتبری دارد"
                    };
                }

                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImage", category.ImageUrl);

                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }

                var newImageName = await request.ImageFile.SaveFile("wwwroot/CategoryImage");
                category.ImageUrl = newImageName;
            }
            await _context.SaveChangesAsync();
            return new ApiResult
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "دسته بندی با موقیت به روز شد"
            };
        }
    }
}
