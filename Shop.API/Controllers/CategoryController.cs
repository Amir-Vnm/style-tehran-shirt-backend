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
            try
            {
                var result=await categoryFacade.GetCategorymanagmentServices.Get();
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                var result = await categoryFacade.GetCategorymanagmentServices.Get(id);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] AddCategoryDto request)
        {
            try
            {
                var result = await categoryFacade.CategoryManagmentServices.AddCategory(request);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromForm] EditCategoryDto request)
        {
            try
            {
                var result = await categoryFacade.CategoryManagmentServices.EditCategory(request);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var result = await categoryFacade.CategoryManagmentServices.DeleteCategory(id);
                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }
    }
}
