using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;
        private ProductViewModel vm;
        
        public HomeController()
        {
            context = new ApplicationDbContext();
            vm = new ProductViewModel();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<ProductCategory> productCategories = context.ProductCategories.ToList();
            List<Product> products = new List<Product>();

            foreach (var productCategory in productCategories)
            {
                items.Add(new SelectListItem
                {
                    Text = productCategory.Name,
                    Value = productCategory.Id.ToString()
                });
            }
            vm.Filters = items;
            vm.Products = products;
            vm.CartProducts = new List<Product>();
            TempData["vm"] = vm;

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}