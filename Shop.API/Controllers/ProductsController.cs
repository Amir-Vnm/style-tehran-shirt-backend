using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.ProductService.Command.Dto;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductFacade product) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result =await product.GetProductManagmentService.GetProducts();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] AddProductDto request)
        {
            var result = await product.ProductManagmentServices.Add(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult> Get(long categoryId)
        {
            var result = await product.GetProductManagmentService.GetProducts(categoryId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(long id)
        {
            var result = await product.GetProductManagmentService.GetProduct(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("AddRange")]
        public async Task<ActionResult> AddRange([FromForm] List<AddProductDto> request)
        {
            var result = await product.ProductManagmentServices.Add(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromForm] EditProductDto request)
        {
            var result = await product.ProductManagmentServices.Edit(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Edit(long id)
        {
            var result = await product.ProductManagmentServices.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
