using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.API.Dtos;
using SalesSystem.Business.Interfaces;
using SalesSystem.Domain.Entities;

namespace SalesSystem.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService service) => _customerService = service;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var customerDto = new CustomerDto();
            var lstCustomerResult = new List<CustomerDto>();
            IEnumerable<Customer> lstCustomers = await _customerService.GetAllAsync().ConfigureAwait(false);
            foreach (Customer c in lstCustomers)
            {
                customerDto = new CustomerDto()
                {
                    customerId = c.CustomerId,
                    firstName = c.FirstName,
                    lastName = c.LastName,
                    address = c.Address
                };
                lstCustomerResult.Add(customerDto);
            }
            return Ok(lstCustomerResult);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CustomerDto customer)
        {
            var newCustomer = new Customer();
            newCustomer.CustomerId = customer.customerId;
            newCustomer.FirstName = customer.firstName;
            newCustomer.LastName = customer.lastName;
            newCustomer.Address = customer.address;


            var created = await _customerService.AddAsync(newCustomer);
            return CreatedAtAction(nameof(GetById), new { id = created.CustomerId }, created);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto updatedCustomer)
        {
            Customer customer = new Customer()
            {
                CustomerId = updatedCustomer.customerId,
                FirstName = updatedCustomer.firstName,
                LastName = updatedCustomer.lastName,
                Address = updatedCustomer.address
            };

            await _customerService.UpdateAsync(id, customer);
            return NoContent();
        }
        [HttpDelete("delete/{id}")] public async Task<IActionResult> Delete(int id) { await _customerService.DeleteAsync(id); return NoContent(); }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseProduct(PurchaseProductDto dto)
        {
            await _customerService.PurchaseProductAsync(dto.CustomerId, dto.ProductId, dto.PurchaseDate);
            return Ok();
        }
    }
}
