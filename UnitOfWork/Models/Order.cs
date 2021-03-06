﻿using System;
using System.Collections.Generic;

namespace UnitOfWork.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        public virtual Customer Customer { get; set; }
    }
}