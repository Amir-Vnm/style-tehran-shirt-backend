using Microsoft.EntityFrameworkCore;
using Shop.Application.Interface;
using Shop.Application.Services.ProductService.Query.Dto;
using Shop.Common.Constants;
using Shop.Common.Dto;
using System.Net;

namespace Shop.Application.Services.ProductService.Query
{
    public class GetProductManagmentService(IDataBaseContext context) : BaseService(context), IGetProductManagmentService
    {
        public async Task<ApiResult<ProductDto>> GetProduct(long id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product is null)
                {
                    return new ApiResult<ProductDto>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "موردی یاقت نشد"
                    };
                }
                var result = new ProductDto
                {
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Id = product.Id,
                    ImageFile = string.Format(ImageConstants.ProductImageAddress, product.ImageUrl),
                    inventory = product.inventory,
                    Price = product.Price
                };

                return new ApiResult<ProductDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = result,
                };
            }
            catch (Exception)
            {
                return new ApiResult<ProductDto>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "محصول با موفقیت اضافه شد"
                };
            }
        }

        public async Task<ApiResult<List<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.Select(s => new ProductDto
                {
                    Id = s.Id,
                    CategoryId = s.CategoryId,
                    Description = s.Description,
                    ImageFile = string.Format(ImageConstants.ProductImageAddress, s.ImageUrl),
                    inventory = s.inventory,
                    Price = s.Price
                }).ToListAsync();
                return new ApiResult<List<ProductDto>>
                {
                    Data = products,
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ApiResult<List<ProductDto>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "محصول با موفقیت اضافه شد"
                };
            }
        }

        public async Task<ApiResult<List<ProductDto>>> GetProducts(long categoryId)
        {
            try
            {
                var result = await _context.Products.Where(p => p.CategoryId == categoryId)
                    .Select(s => new ProductDto
                    {
                        CategoryId = s.CategoryId,
                        Description = s.Description,
                        Id = s.Id,
                        ImageFile = string.Format(ImageConstants.ProductImageAddress, s.ImageUrl),
                        inventory = s.inventory,
                        Price = s.inventory
                    }).ToListAsync();

                return new ApiResult<List<ProductDto>>
                {
                    Data = result,
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ApiResult<List<ProductDto>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "محصول با موفقیت اضافه شد"
                };
            }
        }
    }
}
