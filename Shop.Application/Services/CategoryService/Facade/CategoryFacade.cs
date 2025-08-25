using Shop.Application.Interface;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.CategoryService.Command;
using Shop.Application.Services.CategoryService.Query;

namespace Shop.Application.Services.CategoryService.Facade
{
    public class CategoryFacade(IDataBaseContext context) : ICategoryFacade
    {

        public ICategoryManagmentServices CategoryManagmentServices =>
            new CategoryManagmentServices(context);

        public IGetCategorymanagmentServices GetCategorymanagmentServices =>
            new GetCategorymanagmentServices(context);
    }
}
