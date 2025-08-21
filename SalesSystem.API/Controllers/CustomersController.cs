using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Dtos;
using SalesSystem.Business.Interfaces;
using SalesSystem.Domain.Entities;

namespace SalesSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService service) => _customerService = service;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _customerService.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _customerService.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Create(Customer customer) => Ok(await _customerService.AddAsync(customer));
        [HttpPut("{id}")] public async Task<IActionResult> Update(int id, Customer customer) { await _customerService.UpdateAsync(customer); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _customerService.DeleteAsync(id); return NoContent(); }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseProduct(PurchaseProductDto dto)
        {
            await _customerService.PurchaseProductAsync(dto.CustomerId, dto.ProductId, dto.PurchaseDate);
            return Ok();
        }
    }
}
