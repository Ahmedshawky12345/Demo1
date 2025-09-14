using AutoMapper;
using Demo1.Api.Common;
using Demo1.Application.DTO;
using Demo1.Application.DTOs;
using Demo1.Application.Interfaces.IUnitOfWork;
using Demo1.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            await unitOfWork.Products.AddAsync(product);
            int result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return Ok(ApiResponse<ProductDto>.SuccessResponse(true, productDto, "Category Added successfully "));
            }
            return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "error" }));
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int pagenumber=1,int pagesize=3)
        {
            var products = unitOfWork.Products.GetAllAsync();
     int  totalcount = await products.CountAsync();
            var quary =await products.Skip((pagenumber - 1)
                * pagesize).Take(pagesize).ToListAsync();

            var productmaping = mapper.Map<List<ProductDto>>(quary);
            var result = new PagedResult<ProductDto>(productmaping, pagesize, pagenumber, totalcount);

            return Ok(ApiResponse<PagedResult<ProductDto>>.SuccessResponse(true, result));

        }
        [HttpPut("UpdateProudcts")]
        public async Task<IActionResult> UpdateProudcts(ProductDto productDto)
        {
            var existingProduct =await unitOfWork.Products.GetById(productDto.Id);
            if (existingProduct == null)
            {
                return NotFound(ApiResponse<string>.FailResponse(new List<string> { "Product not found" }));

            }
            mapper.Map(productDto, existingProduct);
            unitOfWork.Products.Update(existingProduct);
            int rowsAffected = await unitOfWork.CompleteAsync();
            if (rowsAffected > 0)
            {
                return Ok(ApiResponse<string>.SuccessResponse("Product updated succfuly"));
            }
            return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "Product Updated falid " }));



        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await unitOfWork.Products.GetById(id);
            if (product == null)
            {
                return NotFound(ApiResponse<List<string>>.FailResponse(new List<string> { "Product not found" }, "Delete Failed"));
            }
            unitOfWork.Products.Delete(product);
            int rowsAffected = await unitOfWork.CompleteAsync();
            if (rowsAffected > 0)
            {
                return Ok(ApiResponse<string>.SuccessResponse("Product deleted succesfully"));
            }
            return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "Failed to delete product" }));

        }

    }
}
