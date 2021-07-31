using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using OnlineShop.Dtos;
using OnlineShop.Models;

namespace OnlineShop.Controllers.Api
{
    public class ProductController : ApiController
    {
        private ApplicationDbContext context;

        public ProductController()
        {
            context = new ApplicationDbContext();
        }

        //GET /Api/product
        public IHttpActionResult GetProducts()
        {
            var customersDTOs = context.Products
                .ToList()
                .Select(Mapper.Map<Product, ProductDto>);

            return Ok(customersDTOs);
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = context.Products.FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        //[HttpPost]
        ////POST /API/customers
        //public IHttpActionResult CreateCustomer(ProductDto productDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var customer = Mapper.Map<ProductDto, Product>(productDto);
        //    context.Customers.Add(customer);
        //    context.SaveChanges();

        //        productDto. = customer.Id;

        //    return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        //}

        //[HttpPut]
        //public void UpdateCustomer(int id, CustomerDTO customerDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }

        //    var customerInDb = context.Customers.FirstOrDefault(x => x.Id == id);

        //    if (customerInDb == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    Mapper.Map(customerDto, customerInDb);

        //    context.SaveChanges();
        //}

        ////DELETE /API/customers/1
        //[HttpDelete]
        //public void DeleteCustomer(int id)
        //{
        //    var customerInDb = context.Customers.FirstOrDefault(x => x.Id == id);

        //    if (customerInDb == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    context.Customers.Remove(customerInDb);
        //    context.SaveChanges();
        //}
    }
}