using OnlineShop_ASP.NET.Data.Models;

namespace OnlineShop_ASP.NET.Services.Brands
{
    public interface IBrandService
    {
        Brand? GetBrandById(int id); 
        int GetBrandId(string name, string model);
        int AddBrand(string name, string model);
        List<Brand> GetBrandNames();
    }
}
