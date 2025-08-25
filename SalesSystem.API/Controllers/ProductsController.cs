using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Dtos;
using SalesSystem.Business.Interfaces;
using SalesSystem.Domain.Entities;

namespace SalesSystem.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService service) => _productService = service;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var lstProducts = await _productService.GetAllAsync().ConfigureAwait(false);

            ProductsDto productsDto = new ProductsDto();

            foreach (var product in lstProducts)
            {
                productsDto = new ProductsDto()
                {
                    productId = product.ProductId,
                    code = product.Code,
                    description = product.Description,
                    image = product.Image != null ? product.Image.ToString() : string.Empty,
                    price = product.Price,
                    stock = product.Stock
                };
            }

            return Ok(lstProducts);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            var productDto = new ProductsDto()
            {
                productId = product.ProductId,
                code = product.Code,
                description = product.Description,
                image = product.Image != null ? product.Image.ToString() : string.Empty,
                price = product.Price,
                stock = product.Stock
            };

            return Ok(productDto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductsDto productDto)
        {
            if (productDto == null) return NoContent();

            Product product = new Product()
            {
                ProductId = productDto.productId,
                Code = productDto.code,
                Description = productDto.description,
                Image = productDto.image,
                Price = productDto.price,
                Stock = productDto.stock
            };

            var created = await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.ProductId }, created);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, ProductsDto productDto)
        {
            if (productDto == null) return NoContent();

            Product product = new Product()
            {
                ProductId = productDto.productId,
                Code = productDto.code,
                Description = productDto.description,
                Image = productDto.image,
                Price = productDto.price,
                Stock = productDto.stock
            };

            await _productService.UpdateAsync(id, product);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignToStore(AssignProductToStoreDto dto)
        {
            await _productService.AssignToStoreAsync(dto.ProductId, dto.StoreId, dto.AssignedDate);
            return Ok();
        }
    }
}
