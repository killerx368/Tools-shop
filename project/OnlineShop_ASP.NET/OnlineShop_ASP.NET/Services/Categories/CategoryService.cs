using OnlineShop_ASP.NET.Data;
using OnlineShop_ASP.NET.Data.Models;
using OnlineShopCourseWork.Models;

namespace OnlineShop_ASP.NET.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext data;

        public CategoryService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int AddCategory(string name)
        {

            if (name == null)
            {
                return -1;
            }

            if (GetCategoryId(name) > 0)
            {
                return GetCategoryId(name);
            }

            Category cat = new Category()
            {
                Name = name
            };

            this.data.Add(cat);
            this.data.SaveChanges();

            return cat.Id;
        }

		public Category? CategoryById(int id)
		{
			return this.data.Categories
			   .Where(b => b.Id == id)
			   .FirstOrDefault();
		}

		public int GetCategoryId(string name)
        {
            return this.data.Categories
                   .Where(b => b.Name.ToLower() == name.ToLower())
                   .Select(b => b.Id).
                   FirstOrDefault();
        }
        public List<Category> GetCatNames()

     => this.data.Categories.Select(c => c).ToList();

    }
    

    }
