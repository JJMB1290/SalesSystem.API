using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Dtos;
using SalesSystem.Business.Interfaces;
using SalesSystem.Domain.Entities;

namespace SalesSystem.API.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoresController(IStoreService service) => _storeService = service;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var lstStores = await _storeService.GetAllAsync().ConfigureAwait(false);
            if (lstStores == null) return NotFound();
            StoresDto getStore = new StoresDto();
            List<StoresDto> lstStoresDto = new List<StoresDto>();
            foreach (var store in lstStores)
            {
                getStore = new StoresDto()
                {
                    storeId = store.StoreId,
                    address = store.Address,
                    branchName = store.BranchName
                };
                lstStoresDto.Add(getStore);
            }
            return Ok(lstStoresDto);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var store = await _storeService.GetByIdAsync(id);
            if (store == null) return NotFound();
            StoresDto dtoStore = new StoresDto()
            {
                storeId = store.StoreId,
                address = store.Address,
                branchName = store.BranchName
            };

            return Ok(dtoStore);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(StoresDto storeDto)
        {
            if (storeDto == null) return NotFound();

            Store store = new Store()
            {
                StoreId = storeDto.storeId,
                Address = storeDto.address,
                BranchName = storeDto.branchName
            };

            var created = await _storeService.AddAsync(store);
            return CreatedAtAction(nameof(GetById), new { id = created.StoreId }, created);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, StoresDto storeDto)
        {
            if (storeDto == null) return NoContent();

            Store store = new Store()
            {
                StoreId = storeDto.storeId,
                Address = storeDto.address,
                BranchName = storeDto.branchName
            };
            await _storeService.UpdateAsync(id, store);
            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _storeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
