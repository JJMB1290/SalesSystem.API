using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Dtos;
using SalesSystem.Business.Interfaces;
using SalesSystem.Domain.Entities;

namespace SalesSystem.API.Controllers
{  
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service) => _service = service;
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Create(Product product) => Ok(await _service.AddAsync(product));
        [HttpPut("{id}")] public async Task<IActionResult> Update(int id, Product product) { await _service.UpdateAsync(product); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignToStore(AssignProductToStoreDto dto)
        {
            await _service.AssignToStoreAsync(dto.ProductId, dto.StoreId, dto.AssignedDate);
            return Ok();
        }
    }
}
