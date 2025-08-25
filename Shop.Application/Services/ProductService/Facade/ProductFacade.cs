using Shop.Application.Interface;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.ProductService.Command;
using Shop.Application.Services.ProductService.Query;

namespace Shop.Application.Services.ProductService.Facade
{
    public class ProductFacade(IDataBaseContext context) : IProductFacade
    {
        public IProductManagmentServices ProductManagmentServices =>
            new ProductManagmentServices(context);

        public IGetProductManagmentService GetProductManagmentService =>
            new GetProductManagmentService(context);
    }
}
