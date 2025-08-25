using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Domain.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }

        public ICollection<ProductStore>? ProductStores { get; set; }
    }
}
