using Shop.Application.Services.ProductService.Query.Dto;
using Shop.Common.Dto;

namespace Shop.Application.Services.ProductService.Query
{
    public interface IGetProductManagmentService
    {
        Task<ApiResult<List<ProductDto>>> GetProducts();
        Task<ApiResult<List<ProductDto>>> GetProducts(long categoryId);
        Task<ApiResult<ProductDto>> GetProduct(long id);
    }
}
