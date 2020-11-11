﻿namespace Migrations.DataAccess.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public double ProductsAmount { get; set; }
    }
}