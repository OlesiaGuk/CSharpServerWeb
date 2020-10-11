using System.Collections.Generic;

namespace ShopEF.Models
{
    class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int CategoryId { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}