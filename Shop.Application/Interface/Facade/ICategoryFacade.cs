using Shop.Application.Services.CategoryService.Command;
using Shop.Application.Services.CategoryService.Query;

namespace Shop.Application.Interface.Facade
{
    public interface ICategoryFacade
    {
        ICategoryManagmentServices CategoryManagmentServices { get; }
        IGetCategorymanagmentServices GetCategorymanagmentServices { get; }
    }
}
