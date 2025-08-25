using Microsoft.EntityFrameworkCore;

using SalesSystem.Business.Interfaces;
using SalesSystem.Data.Context;
using SalesSystem.Domain.Entities;


namespace SalesSystem.Business.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly SalesDbContext _context;
        public CustomerService(SalesDbContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await _context.Customers.Include(c => c.CustomerProducts).ToListAsync();

        public async Task<Customer> GetByIdAsync(int id) =>
            await _context.Customers.Include(c => c.CustomerProducts)
                                    .FirstOrDefaultAsync(c => c.CustomerId == id);

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateAsync(int id, Customer customer)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null) return;

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Address = customer.Address;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task PurchaseProductAsync(int customerId, int productId, DateTime date)
        {
            var exists = await _context.CustomerProducts
                                       .FirstOrDefaultAsync(cp => cp.CustomerId == customerId &&
                                                                  cp.ProductId == productId &&
                                                                  cp.PurchaseDate == date);
            if (exists == null)
            {
                _context.CustomerProducts.Add(new CustomerProduct
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    PurchaseDate = date
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
