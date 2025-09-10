using Demo1.Api.Common;
using Demo1.Application.DTOs;
using Demo1.Application.Interfaces.IUnitOfWork;
using Demo1.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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

    }
}
