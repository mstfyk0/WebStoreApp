
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repositories;
using Repositories.Contracts;
using System.Security.Cryptography;
using Services.Contracts;
using Entities.RequestParameters;
using StoreApp.Models;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {

        // private readonly RepositoryContext _context;
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p )
        {
            // var context = new RepositoryContext(
            //         new DbContextOptionsBuilder<RepositoryContext>().UseSqlite(" Data Source = C:\\Users\\mstyg\\source\\repos\\ProductDb.db")
            //         .Options);
            // var model = _manager.ProductService.GetAllProductsWithDetails(p);
            var products=_manager.ProductService.GetAllProductsWithDetails(p);
            var pagination  = new Pagination() {
                CurrentPage=p.PageNumber,
                ItemsPerPage=p.PageSize,
                TotalItems= _manager.ProductService.GetAllProduct(false).Count()

            };
            return View(new ProductListViewModel () {
                Products=products,
                Pagination=pagination


            });

        }


        public IActionResult Get([FromRoute(Name ="id") ] int id)
        {

            var model= _manager.ProductService.GetOneProduct(id,false);

            ViewData["Title"]=model.ProductName;
            return View(model);
        }
            

        // try            // {            //     // Product product = _context.Products.First(p => p.ProductId.Equals(id));            //     // return View(product);            // }            // catch (System.Exception)            // {,// }

    }

}