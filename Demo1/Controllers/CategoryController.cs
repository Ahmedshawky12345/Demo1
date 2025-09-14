using Demo1.Api.Common;
using Demo1.Application.DTOs;
using Demo1.Application.Interfaces.IUnitOfWork;
using Demo1.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
          var category= mapper.Map<Category>(categoryDto);
           await unitOfWork.Categories.AddAsync(category);
            int result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return Ok(ApiResponse<CategoryDto>.SuccessResponse(true,categoryDto, "Category Added successfully "));
            }
            return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "error"}));

        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(int pagenumber=1,int pagesize=3)
        {
            var data=  unitOfWork.Categories.GetAllAsync();
            int totalcount = await data.CountAsync();
            var quary = await data.OrderBy(x => x.Id).Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
            var result = mapper.Map<List<CategoryDto>>(quary);
            var pagningdata= new PagedResult<CategoryDto>(result,pagesize,pagenumber,totalcount);

            return Ok(ApiResponse<PagedResult<CategoryDto>>.SuccessResponse(true, pagningdata));

        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {

            var existingCategory = await unitOfWork.Categories.GetById(categoryDto.Id);
            if (existingCategory == null)
            {
                return NotFound(ApiResponse<string>.FailResponse(new List<string> { "Category not found" }));
            }

            mapper.Map(categoryDto, existingCategory);
            unitOfWork.Categories.Update(existingCategory);
            int rowsAffected = await unitOfWork.CompleteAsync();
            if (rowsAffected > 0)
            {
                return Ok(ApiResponse<string>.SuccessResponse("category updated succfuly"));
            }
            return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "Category Updated falid " }));

        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await unitOfWork.Categories.GetById(id);
            if(category == null)
            {
                return NotFound(ApiResponse<List<string>>.FailResponse(new List<string> { "Category not found" }, "Delete Failed"));
            }
             unitOfWork.Categories.Delete(category);
            int rowsAffected = await unitOfWork.CompleteAsync();
            if (rowsAffected > 0)
            {
                return Ok(ApiResponse<string>.SuccessResponse("Category deleted succesfully"));
            }
            return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "Failed to delete category" }));

        }
      

    }
}
