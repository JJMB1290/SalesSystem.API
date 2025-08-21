using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Domain.Entities
{
    public class CustomerProduct
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Customer? Customer { get; set; }
        public Product? ListProducts { get; set; }
    }
}
