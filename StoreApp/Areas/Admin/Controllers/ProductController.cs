using Entities.Dtos;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index([FromQuery] ProductRequestParameters p)
        {
            ViewData["Title"]="Products";
            var products = _manager.ProductService.GetAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProduct(false).Count()

            };
            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination


            });
        }
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesSelectList();

            return View();
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryServices.GetAllCategories(false), "CategoryId", "CategoryName", "1");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //File operation
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                _manager.ProductService.CreateProduct(productDto);
                TempData["success"] = $"{productDto.ProductName} has been created.";
                return RedirectToAction("Index");

            }
            TempData["danger"] = $"{productDto.ProductName} has been created.";
            return View();

        }
        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            ViewData["Title"]= model.ProductName ;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto
         , IFormFile? file)
        {
            // var test = ModelState.va;
            if (ModelState.IsValid)
            {

                if (file is not null)
                {
                    //File operation
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file?.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    productDto.ImageUrl = String.Concat("/images/", file.FileName);
                }
                else
                {

                    var imageUrl = _manager.ProductService.GetOneProductForUpdate(productDto.ProductId, false).ImageUrl;
                    if (imageUrl is not null)
                    {
                        productDto.ImageUrl = imageUrl;
                    }
                }
                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");

            }
            return View();

        }
        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            TempData["danger"] = "Product has been removed.";
            return RedirectToAction("Index");
        }

    }
}