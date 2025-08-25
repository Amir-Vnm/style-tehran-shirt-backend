using Shop.Application.Services.ProductService.Command.Dto;
using Shop.Common.Dto;

namespace Shop.Application.Services.ProductService.Command
{
    public interface IProductManagmentServices
    {
        Task<ApiResult> Add(AddProductDto request);
        Task<ApiResult> Add(List<AddProductDto> request);
        Task<ApiResult> Edit(EditProductDto request);
        Task<ApiResult> Delete(long id);
    }
}
