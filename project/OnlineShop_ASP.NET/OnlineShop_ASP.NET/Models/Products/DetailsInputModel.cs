using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OnlineShop_ASP.NET.Data.Models;
using OnlineShopCourseWork.Models;

namespace OnlineShop_ASP.NET.Models.Products
{
    public class DetailsInputModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ImageURL { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Brand? Brand { get; set; }
        public Category? Category { get; set; }
    }
}
