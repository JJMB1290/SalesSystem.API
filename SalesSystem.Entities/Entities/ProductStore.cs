using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Domain.Entities
{
    public class ProductStore
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public DateTime AssignedDate { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }
    }
}
