using Microsoft.EntityFrameworkCore;

using SalesSystem.Business.Interfaces;
using SalesSystem.Data.Context;
using SalesSystem.Domain.Entities;


namespace SalesSystem.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly SalesDbContext _context;
        public ProductService(SalesDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.Include(p => p.ProductStores).ToListAsync();

        public async Task<Product> GetByIdAsync(int id) =>
            await _context.Products.Include(p => p.ProductStores)
                                   .FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity != null)
            {
                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();
            }

        }
        public async Task AssignToStoreAsync(int productId, int storeId, DateTime date)
        {
            var exists = await _context.ProductStores.FindAsync(productId, storeId);
            if (exists == null)
            {
                _context.ProductStores.Add(new ProductStore { ProductId = productId, StoreId = storeId, AssignedDate = date });
                await _context.SaveChangesAsync();
            }
        }
    }
}
