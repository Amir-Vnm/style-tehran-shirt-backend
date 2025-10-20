using Shop.Application.Interface;
using Shop.Application.Services.ProductService.Command.Dto;
using Shop.Common;
using Shop.Common.Constants;
using Shop.Common.Dto;
using Shop.Domain.Entities;
using System.Net;

namespace Shop.Application.Services.ProductService.Command
{
    public class ProductManagmentServices(IDataBaseContext context) : BaseService(context), IProductManagmentServices
    {
        public async Task<ApiResult> Add(AddProductDto request, CloudinaryService cloudinary)
        {
            try
            {
                if (request.Picture == null || request.Picture.Length == 0)
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "عکس محصول ارسال نشده است"
                    };
                }

                var imageUrl = await cloudinary.UploadImageAsync(request.Picture);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "آپلود عکس به Cloudinary ناموفق بود"
                    };
                }

                var product = new Product()
                {
                    CategoryId = request.CategoryId,
                    CreateAt = DateTime.Now,
                    Description = request.Description,
                    Price = request.Price,
                    inventory = request.inventory,
                    ImageUrl = imageUrl
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return new ApiResult
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "محصول با موفقیت اضافه شد"
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

        // سایر متدها (AddRange, Edit, Delete) فعلاً بدون تغییر باقی می‌مانند
    }
}
