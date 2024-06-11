
using OnlineShop_ASP.NET.Models;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShopCourseWork.Models;
using System.Net;

namespace OnlineShop_ASP.NET.Services.Products
{
    public interface IProductService
    {
		List<ProductInputViewModel> GetAllProducts();
        ProductInputViewModel? GetProductInfo(int id);
        int AddProduct(ProductInputViewModel model);
        int GetProductId(string name, string userId, int brandId, int categoryId);
        int EditProduct(ProductInputViewModel model);
        Details_OrdersInputViewModel ProductDetailsById(int id);

        Task<bool> DeleteProductAsync(int id);
		Product GetProductbyId(int id); 
        Product GetProduct(int id);
        List<ProductInputViewModel> SortByBrand(int id);
         List<ProductInputViewModel> SearchBarSearch(string SearchTerm);
        List<ProductInputViewModel> SortByCategory(int id);

    }
}
