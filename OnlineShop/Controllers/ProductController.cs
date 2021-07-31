using System.Linq;
using System.Web.Mvc;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using System.Data.Entity;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.IO;
using System;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext context;

        public ProductController()
        {
            context = new ApplicationDbContext();
        }

        // GET: get products
        [HttpGet]
        public ActionResult Index()
        {
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            if(vm == null)
            {
                vm = new ProductViewModel();
            }
            if (vm.Products == null || !vm.Products.Any())
            {
                vm.Products = context.Products.Include(p => p.ProductCategory).ToList();
            }
            if (vm.CartProducts == null)
            {
                vm.CartProducts = new List<Product>();
            }
            TempData["vm"] = vm;
            //else if (vm.pro)
            return PartialView(vm);
        }

        public ActionResult New()
        {
            var productCategories = context.ProductCategories.ToList();

            var viewModel = new ProductViewModel
            {
                Product = new Product(),
                ProductCategories = productCategories
            };

            return PartialView("ProductForm", viewModel);
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            //Ako nije validan model korisnik se vraca na istu formu da pokusa ponovni unos
            // Gledaju se validacije definisane atributima iznad property-ja u modelu
            if (!ModelState.IsValid)
            {
                var viewModel = new ProductViewModel
                {
                    Product = product,
                    ProductCategories = context.ProductCategories.ToList()
                };

                return this.PartialView("Edit", viewModel);
            }
            if (product.Id == 0)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                product.Image = ConvertImageToBytes(file);
                product.Id = context.Products.Select(obj => obj.Id).Distinct().ToList().Max();
                product.Id++;
                context.Products.Add(product);

                context.SaveChanges();
            }

            return RedirectToAction("Index", "Product");
        }
        // GET: get cart window
        [HttpGet]
        public ActionResult Cart(int? id)
        {
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            if (vm == null)
            {
                vm = new ProductViewModel();
            }
            vm.Product = context.Products.FirstOrDefault(x => x.Id == id);
            //else if (vm.pro)
            TempData["vm"] = vm;
            return PartialView(vm);
        }

        [AuthorizeSession]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new ProductViewModel()
            {
                Product = context.Products.FirstOrDefault(x => x.Id == id),
                ProductCategories = context.ProductCategories.ToList()
            };
            return PartialView(viewModel);
        }
        // GET: put product into cart
        [HttpPost]
        public ActionResult Cart(ProductViewModel vmToCart)
        {
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            var product = vmToCart.Product;

            if (product == null)
            {
                return HttpNotFound();
            }
            if(vm.CartProducts == null)
            {
                vm.CartProducts = new List<Product>();
            }
            //    List<Product> products = FillMyCart(product.UnitOfMeasurement,product.Ammount, product);
            //    vm.CartProducts.AddRange(products);
            vm.CartProducts.Add(product);
            TempData["vm"] = vm;

            return RedirectToAction("Index");
        }

        //private List<Product> FillMyCart(string unitOfMeasurement, decimal ammount, Product product)
        //{
        //    List<Product> products;

        //    if (unitOfMeasurement == "din/kom")
        //    {
        //        products = new List<Product>((int)ammount);
        //        for (int i = 0; i < products.Capacity; i++)
        //        {
        //            Product productToAdd = new Product(product);
        //            products.Add(productToAdd);
        //        }
        //    }
        //    else if(unitOfMeasurement == "din/kg")
        //    {
        //        products = new List<Product>();
        //        products.Add(product);
        //    }
        //    else
        //    {
        //        products = new List<Product>();
        //    }
           
        //    return products;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var editedProduct = context.Products.FirstOrDefault(x => x.Id == product.Id);

            if (editedProduct == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                var updatedProduct = context.Products.First(x => x.Id == product.Id);
                updatedProduct.Name = product.Name;
                updatedProduct.Price = product.Price;
                updatedProduct.Ammount = product.Ammount;
                updatedProduct.Description = product.Description;
                updatedProduct.ProductCategoryId = product.ProductCategoryId;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = new ProductViewModel()
            {
                Product = product,
                ProductCategories = context.ProductCategories.ToList()
            };

            return PartialView(viewModel);
        }

        public ActionResult GetDetails(int id)
        {
            var product = context.Products.Include(p => p.ProductCategory).FirstOrDefault(x => x.Id == id);
            var viewModel = new ProductViewModel
            {
                Product = product
            };
            return PartialView("GetDetails", viewModel);
        }

        // GET: Product/Delete/5
        [HttpGet]
        public ActionResult RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            var product = vm.CartProducts.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProductViewModel
            {
                Product = product
            };
            TempData["vm"] = vm;
            return PartialView(viewModel);
        }
  
        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int id)
        {
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            var product =  vm.CartProducts.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            vm.CartProducts.Remove(product);
            var viewModel = new ProductViewModel
            {
                Product = product
            };

            TempData["vm"] = vm;
            return RedirectToAction("GetCartItems",vm);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProductViewModel
            {
                Product = product
            };
            return PartialView(viewModel);
        }
      
        
        //public ActionResult UpdateCart(Product productToUpdate)
        //{
        //    ProductViewModel vm = (ProductViewModel)TempData["vm"];
        //    foreach(var product in vm.CartProducts)
        //    {
        //        if (productToUpdate.Id == product.Id)
        //        {
        //            product.Ammount = productToUpdate.Ammount;
        //        }
        //    }

        //    TempData["vm"] = vm;
        //    return PartialView("GetCartItems", vm);
        //}

        [HttpGet]
        public ActionResult GetCartItems()
        {
            ProductViewModel vm = (ProductViewModel)TempData["vm"];
            TempData["vm"] = vm;
            return PartialView("GetCartItems", vm);
        }
        
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        private byte[] ConvertImageToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public ActionResult RenderPhoto(int productId)
        {
            byte[] photo = context.Products.FirstOrDefault(x => x.Id == productId).Image;
            if (photo == null)
            {
                HttpPostedFileBase file = Request.Files["ImageData"];
                photo = ConvertImageToBytes(file);
            }
            return File(photo, "image/png");
        }
    }
}