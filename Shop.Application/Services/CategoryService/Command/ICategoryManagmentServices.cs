using Shop.Application.Services.CategoryService.Command.Dto;
using Shop.Common.Dto;

namespace Shop.Application.Services.CategoryService.Command
{
    public interface ICategoryManagmentServices
    {
        Task<ApiResult> AddCategory(AddCategoryDto request);
        Task<ApiResult> EditCategory(EditCategoryDto request);
        Task<ApiResult> DeleteCategory(long id);
    }
}
