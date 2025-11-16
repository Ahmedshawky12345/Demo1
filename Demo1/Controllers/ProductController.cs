using AutoMapper;
using Demo1.Api.Common;
using Demo1.Application.DTO;
using Demo1.Application.DTOs;
using Demo1.Application.Interfaces.Iservices;
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
        private readonly IimageService iimageService;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper,IimageService iimageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.iimageService = iimageService;
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
        {
            string? ImageUrl = null;
            if (productCreateDto.file != null)
            {
                 ImageUrl = await iimageService.SaveFileAsync(productCreateDto.file, "books");
            }
                
            var product = mapper.Map<Product>(productCreateDto);
            product.ImageUrl = ImageUrl;
            try
            {
                await unitOfWork.Products.AddAsync(product);
                int result = await unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    var productDto = mapper.Map<ProductDto>(product);
                    return Ok(ApiResponse<ProductDto>.SuccessResponse(true, productDto, "Product Added successfully"));
                }

                return BadRequest(ApiResponse<List<string>>.FailResponse(new List<string> { "Error while saving product" }));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
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
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var path = await iimageService.SaveFileAsync(file, "books");
            return Ok(new { imageUrl = path });
        }
        [HttpGet("GetProductByCategoryID")]
        public async Task<IActionResult> GetProductByCategoryID(int CategoryID)
        {
            var products = await unitOfWork.Products.FindAsync(p => p.CategoryId == CategoryID && !p.IsDeleted);
            var mappedProducts = mapper.Map<List<ProductDto>>(products);
            if (mappedProducts.Any())
            {
                return Ok(ApiResponse<List<ProductDto>>.SuccessResponse(true, mappedProducts, "Products fetched successfully"));
            }

            return NotFound(ApiResponse<List<string>>.FailResponse(new List<string> { "No products found for this category" }));
        }

    }
}
