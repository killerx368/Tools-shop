using OnlineShop_ASP.NET.Data.Models;
using OnlineShopCourseWork.Models;

namespace OnlineShop_ASP.NET.Services.Categories
{
    public interface ICategoryService
    {
		Category? CategoryById(int id);
		int GetCategoryId(string name);

        int AddCategory(string name);
        List<Category> GetCatNames();
    }
}
