using System;
using System.Collections.Generic;

namespace ShopEF.Models
{
    class Order
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int CustomerId { get; set; }

        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        public Customer Customer { get; set; }
    }
}