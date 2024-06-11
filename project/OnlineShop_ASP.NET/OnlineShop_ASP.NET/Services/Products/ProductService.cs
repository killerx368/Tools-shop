using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineShop_ASP.NET.Data;
using OnlineShop_ASP.NET.Data.Models;
using OnlineShop_ASP.NET.Models;
using OnlineShop_ASP.NET.Models.Orders;
using OnlineShop_ASP.NET.Models.Products;
using OnlineShop_ASP.NET.Services.Brands;
using OnlineShop_ASP.NET.Services.Categories;
using OnlineShopCourseWork.Models;

namespace OnlineShop_ASP.NET.Services.Products
{
    public class ProductService : IProductService
    {

        private ApplicationDbContext data;
        private IBrandService brandService;
        private ICategoryService categoryService;

        public ProductService(ApplicationDbContext data, IBrandService brandService, ICategoryService categoryServices)
        {
            this.data = data;
            this.brandService = brandService;
            this.categoryService = categoryServices;
        }

        public int AddProduct(ProductInputViewModel model)
        {
            if (model.ProductName == null && model.CategoryName == null && model.BrandName == null && model.ModelName == null)
            {
                return -1;
            }

            if (model.CategoryId == null || model.CategoryId == 0)
            {
                model.CategoryId = categoryService.AddCategory(model.CategoryName);
            }

  
          
            if (model.BrandId != null || model.BrandId != 0 )
            {
                model.BrandId = brandService.AddBrand(model.BrandName, model.ModelName);
            }
        

            Product product = new Product()
            {
                BrandId = (int)model.BrandId,
                CategoryId = (int)model.CategoryId,
                ImageURL = model.ImageURL,
                ProductName = model.ProductName,
                Price = model.Price,
                Quantity = model.Quantity,
                UserId = model.UserId,
                Description = model.Description,
         

            };


            

            if (product.Price <= 0)
            {
                return -1;
            }
            if (product.Quantity <= 0)
            {
                return -1;
            }

            
            this.data.Add(product);
            this.data.SaveChanges();

            return product.Id;
        }

        //--------------------------------------
        public async Task<bool> DeleteProductAsync(int id)
        {
            Product? product = this.data.Products.Find(id);
            if (product == null)
            {
                return false;
            }

            this.data.Remove(product);
            await this.data.SaveChangesAsync();

            return true;

        }
        //----------------------------------
        public int EditProduct(ProductInputViewModel model)
        {
            Product? ProductPlaseHolder = this.data.Products.Find(model.Id);
            if (model == null)
            {
                return -1;
            }

            if (model.ProductName == null && model.CategoryName == null && model.BrandName == null && model.ModelName == null)
            {
                return -1;
            }

            if (model.CategoryId == null || model.CategoryId == 0)
            {
                model.CategoryId = categoryService.AddCategory(model.CategoryName);
            }
            if (model.BrandId == null || model.BrandId == 0)
            {
                model.BrandId = brandService.AddBrand(model.BrandName, model.ModelName);
            }

            // da se setnat neshtata 
            ProductPlaseHolder.ProductName = model.ProductName;
            ProductPlaseHolder.Quantity = model.Quantity;
            ProductPlaseHolder.ImageURL = model.ImageURL;
            ProductPlaseHolder.Price = model.Price;
            ProductPlaseHolder.CategoryId = (int)model.CategoryId;
            ProductPlaseHolder.BrandId = (int)model.BrandId;
            ProductPlaseHolder.Description = model.Description;


            this.data.Products.Update(ProductPlaseHolder);
            this.data.SaveChanges();

            return model.Id;
        }

        public List<ProductInputViewModel> GetAllProducts()
        => this.data.Products.Select(c => new ProductInputViewModel
        {
            BrandName = c.Brand.BrandName,
            BrandId = c.BrandId,
            ProductName = c.ProductName,
            Price = c.Price,
            Quantity = c.Quantity,
            UserId = c.UserId,
            ImageURL = c.ImageURL,
            Id = c.Id,
            CategoryId = c.CategoryId,

        }).ToList();


        public int GetProductId(string name, string userId, int brandId, int categoryId)
        {
            return this.data.Products
                .Where(b => b.ProductName == name & b.BrandId == brandId & b.CategoryId == categoryId & b.UserId == userId)
                .Select(b => b.Id)
                .FirstOrDefault();
        }
        public Product? GetProductbyId(int id)
        {
            return this.data.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();

        }

        public ProductInputViewModel? GetProductInfo(int id)
        {
            var product = this.data.Products.Select(c => new ProductInputViewModel
            {
                BrandName = c.Brand.BrandName,
                BrandId = c.BrandId,
                ProductName = c.ProductName,
                Price = c.Price,
                Quantity = c.Quantity,
                UserId = c.UserId,
                ImageURL = c.ImageURL,
                Id = c.Id,
                CategoryId = c.CategoryId,
            }).Where(p => p.Id == id)
                .FirstOrDefault();


            var cat = this.categoryService.CategoryById((int)product.CategoryId);
            product.CategoryName = cat.Name;

            var brand = this.brandService.GetBrandById((int)product.BrandId);
            product.ModelName = brand.ModelName;

            return product;
        }

        public Details_OrdersInputViewModel ProductDetailsById(int id)
        {

            Product? product = this.data.Products.Find(id);
            if (product == null)
            {
                return null;
            }
            DetailsInputModel details = new DetailsInputModel();
            OrdersInputViewModel Ords = new OrdersInputViewModel();
            Details_OrdersInputViewModel model = new Details_OrdersInputViewModel {
                DetailsInput = details,
                OrdersInput = Ords
            };

            model.DetailsInput.Id = product.Id;
            model.DetailsInput.ProductName = product.ProductName;
            model.DetailsInput.Quantity = product.Quantity;
            model.DetailsInput.ImageURL = product.ImageURL;
            model.DetailsInput.Price = product.Price;
            model.DetailsInput.CategoryId = product.CategoryId;
            model.DetailsInput.BrandId = product.BrandId;
            model.DetailsInput.Description = product.Description;
            

            return model;
        }
        public Product? GetProduct(int id)
        => this.data.Products.Find(id);

        public List<ProductInputViewModel> SortByBrand(int id)
        {
            Brand? brand = brandService.GetBrandById(id);


            List<ProductInputViewModel>? first =  this.data.Products.Select(c => new ProductInputViewModel
            {
                BrandId = c.BrandId,
                ProductName = c.ProductName,
                Price = c.Price,
                Quantity = c.Quantity,
                UserId = c.UserId,
                ImageURL = c.ImageURL,
                Id = c.Id,
                CategoryId = c.CategoryId,
                BrandName = c.Brand.BrandName,
                CategoryName = null
                

            }).ToList();
            List<ProductInputViewModel> model = new List<ProductInputViewModel>();
            int chaeck = first.Count();
            for (int i = 0; i != chaeck; i++)
            {
                
                if (first[i].BrandName == brand.BrandName) 
                {
                    model.Add(first[i]);
                }
            }
            return model;
        }
        public List<ProductInputViewModel> SortByCategory(int id)
        {
            Category? category = categoryService.CategoryById(id);


            List<ProductInputViewModel>? first = this.data.Products.Select(c => new ProductInputViewModel
            {
                BrandId = c.BrandId,
                ProductName = c.ProductName,
                Price = c.Price,
                Quantity = c.Quantity,
                UserId = c.UserId,
                ImageURL = c.ImageURL,
                Id = c.Id,
                CategoryId = c.CategoryId,
                CategoryName = c.Category.Name,
                BrandName = c.Brand.BrandName,
                


            }).ToList();
            List<ProductInputViewModel> model = new List<ProductInputViewModel>();
            int chaeck = first.Count();
            for (int i = 0; i != chaeck; i++)
            {

                if (first[i].CategoryName == category.Name)
                {
                    model.Add(first[i]);
                }
            }
            return model;
        }
        public List<ProductInputViewModel> SearchBarSearch(string SearchTerm)
        {

            var prodcuts = this.data.Products.Select(c => new ProductInputViewModel
                {
                    BrandId = c.BrandId,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    UserId = c.UserId,
                    ImageURL = c.ImageURL,
                    Id = c.Id,
                    CategoryId = c.CategoryId,
                    
                   
            }).ToList();


            List<ProductInputViewModel> Searchlist = new List<ProductInputViewModel>();

            if (Searchlist != null)
            {
                foreach (var item in prodcuts)
                {
                    string check = SearchTerm.ToLower();
                    if (item.ProductName != null) { string nametest = item.ProductName.ToLower();
                        
                        if (nametest.Contains(check) )
                        {
                            Searchlist.Add(item);
                        }
                    }
                    if (item.BrandName != null) { string bnametest = item.BrandName.ToLower();
                        if (bnametest.Contains(check) )
                        {
                            Searchlist.Add(item);
                        }
                    }
                   if (item.CategoryName != null) { string cnametest = item.CategoryName.ToLower();
                        if (cnametest.Contains(check) )
                        {
                            Searchlist.Add(item);
                        }
                    }
                  if (item.ModelName != null) { string mnametest = item.ModelName.ToLower();
                        if (mnametest.Contains(check))
                        {
                            Searchlist.Add(item);
                        }
                    }
                   
                }
            }
            return Searchlist;
        }

	
	}
}
