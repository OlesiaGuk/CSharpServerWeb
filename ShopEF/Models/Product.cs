using System.Collections.Generic;

namespace ShopEF.Models
{
    class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public List<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}