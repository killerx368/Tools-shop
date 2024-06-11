using OnlineShop_ASP.NET.Data;
using OnlineShop_ASP.NET.Data.Models;
using OnlineShop_ASP.NET.Services.Brands;

namespace OnlineShop_ASP.NET.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private ApplicationDbContext data;

        public BrandService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int AddBrand(string name, string model)
        {
            var id = GetBrandId(name, model);
            if (name == null || model == null)
            {
                return -1;
            }

            if (id > 0)
            {
                return id;
            }

           
                Brand brand = new Brand()
                {

                    BrandName = name,
                    ModelName = model,
                };

            this.data.Add(brand);
            this.data.SaveChanges();

            return brand.Id;
        }

        public Brand? GetBrandById(int id)
        {
            return this.data.Brands
                .Where(b => b.Id == id)
                .FirstOrDefault();
        }

        public int GetBrandId(string name, string model)
        {
            return this.data.Brands
                   .Where(b => b.BrandName.ToLower() == name.ToLower() & b.ModelName.ToLower() == model.ToLower())
                   .Select(b => b.Id).
                   FirstOrDefault();
        }

        public List<Brand> GetBrandNames()
       
        => this.data.Brands.Select(c => c).ToList();
        
    }


}

