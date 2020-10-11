using System;
using System.Collections.Generic;

namespace ShopEF.Models
{
    class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } //todo: DateTime or string?

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }
    }
}