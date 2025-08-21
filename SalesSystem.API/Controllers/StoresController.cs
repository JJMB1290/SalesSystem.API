using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Business.Interfaces;
using SalesSystem.Domain.Entities;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _service;
        public StoresController(IStoreService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost] public async Task<IActionResult> Create(Store store)
        {
            return Ok(await _service.AddAsync(store));
        }
        [HttpPut("{id}")] public async Task<IActionResult> Update(int id, Store store) { await _service.UpdateAsync(store); return NoContent(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
    }
}
