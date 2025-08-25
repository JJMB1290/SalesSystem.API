using Microsoft.EntityFrameworkCore;
using SalesSystem.Business.Interfaces;
using SalesSystem.Data.Context;
using SalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Business.Services
{
    public class StoreService : IStoreService
    {
        private readonly SalesDbContext _context;
        public StoreService(SalesDbContext context) => _context = context;

        public async Task<IEnumerable<Store>> GetAllAsync() => await _context.Stores.ToListAsync();
        public async Task<Store> GetByIdAsync(int id) => await _context.Stores.FindAsync(id);
        public async Task<Store> AddAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }
        public async Task UpdateAsync(int id, Store store)
        {
            var existing = await _context.Stores.FindAsync(id);
            if (existing == null) return;

            existing.BranchName = store.BranchName;
            existing.Address = store.Address;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Stores.FindAsync(id);
            if (entity != null)
            {
                _context.Stores.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
