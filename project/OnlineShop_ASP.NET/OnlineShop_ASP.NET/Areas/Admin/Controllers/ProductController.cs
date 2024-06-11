using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop_ASP.NET.Data.Models;
using OnlineShop_ASP.NET.Extensions;
using OnlineShop_ASP.NET.Models;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShop_ASP.NET.Services.Brands;
using OnlineShop_ASP.NET.Services.Categories;
using OnlineShop_ASP.NET.Services.Products;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineShop_ASP.NET.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProductController : Controller
	{
		private readonly IProductService productService;
		private readonly IBrandService brandService;
		private readonly ICategoryService categoryService;

		public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService)
		{
			this.productService = productService;
			this.brandService = brandService;
			this.categoryService = categoryService;
		}

		[HttpGet]
		public IActionResult AddProducts()
		{
			var model = new ProductInputViewModel();

			return View(model);
		}

		[HttpPost]
		public IActionResult AddProducts(ProductInputViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return RedirectToAction("CustomError", "Home");
			}
			model.UserId = User.GetId();
			productService.AddProduct(model);

			return RedirectToAction(nameof(AddProducts));
		}
		[HttpPost]
		public IActionResult Search(string SearchTerm)
		{
            var products = this.productService.SearchBarSearch(SearchTerm);

            return View(products);
        }



            [HttpGet]
		public IActionResult Edit(int id)
		{
			if (id == 0)
			{
				return View();
			}

			var model = productService.GetProductInfo(id);

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(ProductInputViewModel model)
		{
			if (ModelState.IsValid == false)
			{
                return RedirectToAction("CustomError", "Home");
            }

			var result = productService.EditProduct(model);
			if (result == -1)
			{
                return RedirectToAction("CustomError", "Home");
            }

			return RedirectToAction("AllProducts");
		}
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = productService.DeleteProductAsync(id);
           // if (result.Result == false)
            //{
            //    return RedirectToAction("CustomError", "Home");
           // }

            return RedirectToAction("AllProducts");
        }
        [HttpGet]
        public IActionResult AllProducts(string Id, string brandname)
        {
            if (Id != null && Id != "")
            {
				
                int id = -1;
                try
                {
                    bool isNumber = int.TryParse(Id, out int number);


                    if (isNumber == true)
                    {
                        id = int.Parse(Id);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                if (id == -1)
                {
                    return View(productService.GetAllProducts());
                }
               if (brandname != null && brandname != "" )
				{
					var model = productService.SortByBrand(id);
                    return View(model);
				}
				else
                {
                    var model = productService.SortByCategory(id);
                    return View(model);
                }


            }



            return View(productService.GetAllProducts());
        }

        [HttpGet]
		public IActionResult Details(int id)
		{
           
            Details_OrdersInputViewModel model = productService.ProductDetailsById(id);
			model.DetailsInput.Brand = this.brandService.GetBrandById((int)model.DetailsInput.BrandId);
			model.DetailsInput.Category = this.categoryService.CategoryById((int)model.DetailsInput.CategoryId);
	        return View(model);
		}

	}
}