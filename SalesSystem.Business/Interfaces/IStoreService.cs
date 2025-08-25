using SalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Business.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store> GetByIdAsync(int id);
        Task<Store> AddAsync(Store store);
        Task UpdateAsync(int id, Store store);
        Task DeleteAsync(int id);
    }
}
