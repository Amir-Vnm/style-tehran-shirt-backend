using Shop.Application.Services.CategoryService.Query.Dto;
using Shop.Common.Dto;

namespace Shop.Application.Services.CategoryService.Query
{
    public interface IGetCategorymanagmentServices
    {
        Task<ApiResult<List<CategoryDto>>> Get();
        Task<ApiResult<CategoryDto>> Get(long id);
    }
}
