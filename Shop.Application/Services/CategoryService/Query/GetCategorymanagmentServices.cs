using Microsoft.EntityFrameworkCore;
using Shop.Application.Interface;
using Shop.Application.Services.CategoryService.Query.Dto;
using Shop.Common.Constants;
using Shop.Common.Dto;
using System.Net;

namespace Shop.Application.Services.CategoryService.Query
{
    public class GetCategorymanagmentServices(IDataBaseContext context) : BaseService(context), IGetCategorymanagmentServices
    {
        public async Task<ApiResult<List<CategoryDto>>> Get()
        {
            var result= await _context.Categories.Select(s => new CategoryDto
            {
                Id = s.Id,
                ImageUrl =string.Format(ImageConstants.GetCategoryImageAddress, s.ImageUrl),
                Name = s.Name
            }).ToListAsync();

            return new ApiResult<List<CategoryDto>>
            {
                Data = result,
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public async Task<ApiResult<CategoryDto>> Get(long id)
        {
            var category =await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return new ApiResult<CategoryDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "موردی یافت نشد"
                };
            }
            var result = new CategoryDto
            {
                Id = category.Id,
                ImageUrl = string.Format(ImageConstants.GetCategoryImageAddress, category.ImageUrl),
                Name = category.Name
            };

            return new ApiResult<CategoryDto>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = result,
            };
        }
    }
}
