using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Dtos
{
    public class ProductDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public decimal Ammount { get; set; }

        public string UnitOfMeasurement { get; set; }

        public string Description { get; set; }

        public ProductCategoryDto ProductCategory { get; set; }

        public byte ProductCategoryId { get; set; }
    }
}