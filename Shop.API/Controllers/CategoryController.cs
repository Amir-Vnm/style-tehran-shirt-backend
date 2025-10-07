using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interface.Facade;
using Shop.Application.Services.CategoryService.Command.Dto;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryFacade categoryFacade) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await categoryFacade.GetCategorymanagmentServices.Get();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            var result = await categoryFacade.GetCategorymanagmentServices.Get(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] AddCategoryDto request)
        {
            var result = await categoryFacade.CategoryManagmentServices.AddCategory(request);
            return StatusCode((int)result.StatusCode, result);
        }

        // ✅ اصلاح مسیر PUT با دریافت id از URL
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(long id, [FromForm] EditCategoryDto request)
        {
            request.Id = id; // تنظیم id از URL
            var result = await categoryFacade.CategoryManagmentServices.EditCategory(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await categoryFacade.CategoryManagmentServices.DeleteCategory(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
