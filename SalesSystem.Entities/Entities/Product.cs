using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }

        public ICollection<ProductStore> ProductStores { get; set; }
    }
}
