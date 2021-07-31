using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using System.Net;

namespace OnlineShop.Controllers
{
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext context;

        public ProductCategoryController()
        {
            context = new ApplicationDbContext();
        }

        // GET: get product categories
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> filterItems = new List<SelectListItem>();
            List<ProductCategory> productCategories = context.ProductCategories.ToList();

            foreach (var productCategory in productCategories)
            {
                filterItems.Add(new SelectListItem
                {
                    Text = productCategory.Name,
                    Value = productCategory.Id.ToString()
                });
            }
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            if (vm == null)
            {
                vm = new ProductViewModel();
                TempData["vm"] = vm;
            }
            vm.Filters = filterItems;
            // if user is admin
            vm.ProductCategories = productCategories;

            return PartialView(vm);
        }
        
        //Filter products by product category
        [HttpPost]
        public ActionResult Index(ProductViewModel vm)
        {
            ProductViewModel vm1 = (ProductViewModel)TempData["vm"];
            List<Product> allProducts = context.Products.ToList();
            List<Product> productsBySelectedCategory = new List<Product>();

            foreach (SelectListItem item in vm.Filters)
            {
                if (item.Selected)
                {
                   foreach (var product in allProducts)
                   {
                        int selectedCategoryId;
                        int.TryParse(item.Value, out selectedCategoryId);

                        if (product.ProductCategoryId == selectedCategoryId)
                        {
                            productsBySelectedCategory.Add(product);
                        }
                   }
                }
            }

            vm.Products = productsBySelectedCategory;
            TempData["vm"] = vm;
            return RedirectToAction("Index", "Product", vm);
            //return PartialView(vm);
        }


        public ActionResult New()
        {
            var viewModel = new ProductViewModel
            {
                ProductCategory = new ProductCategory()
            };

            return PartialView("ProductCategoryForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProductCategory productCategory)
        {
            //Ako nije validan model korisnik se vraca na istu formu da pokusa ponovni unos
            // Gledaju se validacije definisane atributima iznad property-ja u modelu
            if (!ModelState.IsValid)
            {
                var viewModel = new ProductViewModel
                {
                    ProductCategory = productCategory
                };
                return PartialView("ProductCategoryForm", viewModel);
            }
            if (productCategory.Id == 0)
            {
                context.ProductCategories.Add(productCategory);
            }
            else
            {
                var updatedProductCategory = context.ProductCategories.First(x => x.Id == productCategory.Id);
                updatedProductCategory.Name = productCategory.Name;
            }

            context.SaveChanges();

            return RedirectToAction("Index", "ProductCategory");
        }

        public ActionResult Edit(int? id)
        {
            var productCategory = context.ProductCategories.FirstOrDefault(x => x.Id == id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProductViewModel
            {
                ProductCategory = productCategory
            };
            return PartialView("ProductCategoryForm", viewModel);
        }

        public ActionResult GetDetails(int id)
        {
            var productCategory = context.ProductCategories.FirstOrDefault(x => x.Id == id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProductViewModel
            {
                ProductCategory = productCategory
            };
            return PartialView("GetDetails", viewModel);
        }

        // GET: ProductCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productCategory = context.ProductCategories.FirstOrDefault(x => x.Id == id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProductViewModel
            {
                ProductCategory = productCategory
            };
            return PartialView(viewModel);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var productCategory = context.ProductCategories.FirstOrDefault(x => x.Id == id);
            context.ProductCategories.Remove(productCategory);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
