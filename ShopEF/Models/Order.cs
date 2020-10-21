using System;
using System.Collections.Generic;

namespace ShopEF.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int CustomerId { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        public virtual Customer Customer { get; set; }
    }
}