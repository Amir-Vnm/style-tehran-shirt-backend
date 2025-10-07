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
        public async Task<ApiResult> Add(AddProductDto request)
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

                var imageName = await request.ImageFile.SaveFile(ImageConstants.ProductImageAddress);

                var product = new Product()
                {
                    CategoryId = request.CategoryId,
                    CreateAt = DateTime.Now,
                    Description = request.Description,
                    Price = request.Price,
                    inventory = request.inventory,
                    ImageUrl = $"Product/{imageName}"
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

        public async Task<ApiResult> Add(List<AddProductDto> request)
        {
            try
            {
                var products = new List<Product>();

                foreach (var item in request)
                {
                    if (!item.ImageFile.FileName.IsValidImageFile())
                    {
                        return new ApiResult
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = $"فایل {item.ImageFile.FileName} فرمت نامعتبری دارد"
                        };
                    }

                    var imageName = await item.ImageFile.SaveFile(ImageConstants.ProductImageAddress);

                    products.Add(new Product
                    {
                        CategoryId = item.CategoryId,
                        CreateAt = DateTime.Now,
                        Description = item.Description,
                        Price = item.Price,
                        inventory = item.inventory,
                        ImageUrl = $"Product/{imageName}"
                    });
                }

                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();

                return new ApiResult
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = $"{products.Count} محصول با موفقیت اضافه شد"
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

        public async Task<ApiResult> Edit(EditProductDto request)
        {
            try
            {
                var product = await _context.Products.FindAsync(request.Id);
                if (product is null)
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "موردی یافت نشد"
                    };
                }

                product.UpdateAt = DateTime.Now;
                product.Description = request.Description;
                product.CategoryId = request.CategoryId;
                product.Price = request.Price;
                product.inventory = request.inventory;

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

                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }

                    var newImageName = await request.ImageFile.SaveFile(ImageConstants.ProductImageAddress);
                    product.ImageUrl = $"Product/{newImageName}";
                }

                await _context.SaveChangesAsync();

                return new ApiResult
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "محصول با موفقیت بروز رسانی شد"
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

        public async Task<ApiResult> Delete(long id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product is null)
                {
                    return new ApiResult
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "موردی یافت نشد"
                    };
                }

                // حذف فایل فیزیکی محصول
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                product.IsDelete = true;
                product.DeleteAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return new ApiResult
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "محصول حذف شد"
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
    }
}
