using Shop.Application.Services.ProductService.Command;
using Shop.Application.Services.ProductService.Query;

namespace Shop.Application.Interface.Facade
{
    public interface IProductFacade
    {
        IProductManagmentServices ProductManagmentServices { get; }
        IGetProductManagmentService GetProductManagmentService { get; }
    }
}
