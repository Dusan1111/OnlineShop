using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<SelectListItem> Filters { get; set; }
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<Product> CartProducts { get; set; }

        public HttpPostedFileBase ImageData { get; set; }

        public double TotalCartPrice
        {
            get
            {
                double totalCartPrice = 0;
                if (CartProducts != null)
                {
                    foreach (var product in CartProducts)
                    {
                        totalCartPrice += product.TotalProductPrice;
                    }
                }
                return totalCartPrice;
            }
        }
        
        public int CartProductsCount
        {
            get
            {
                int carProductsCount = 0;
                if (CartProducts != null)
                {
                    foreach (var product in CartProducts)
                    {
                        if (product.UnitOfMeasurement == "din/kom")
                        {
                            carProductsCount += (int)product.Ammount;
                        }
                        else if (product.UnitOfMeasurement == "din/kg")
                        {
                            carProductsCount += 1;
                        }
                    }
                }
                return carProductsCount;
            }
        }

        public List<string> MeasurementsList
        {
            get
            {
                return new List<string>(2) { "din/kom", "din/kg" };
            }
        }
    }
}
