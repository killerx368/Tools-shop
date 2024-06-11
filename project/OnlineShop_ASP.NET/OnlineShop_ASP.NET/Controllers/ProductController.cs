using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineShop_ASP.NET.Models;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShop_ASP.NET.Services.Brands;
using OnlineShop_ASP.NET.Services.Categories;
using OnlineShop_ASP.NET.Services.Products;


namespace OnlineShop_ASP.NET.Controllers
{
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
                if (brandname != null && brandname != "")
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

        [HttpPost]
        public IActionResult Search(string SearchTerm)
        {
            

            var products = this.productService.SearchBarSearch(SearchTerm);

            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {

            Details_OrdersInputViewModel model = productService.ProductDetailsById(id);
            if (model != null)
            {
                model.DetailsInput.Brand = this.brandService.GetBrandById((int)model.DetailsInput.BrandId);

                model.DetailsInput.Category = this.categoryService.CategoryById((int)model.DetailsInput.CategoryId);

                return View(model);
            }
            return View();





        }

    }
}
