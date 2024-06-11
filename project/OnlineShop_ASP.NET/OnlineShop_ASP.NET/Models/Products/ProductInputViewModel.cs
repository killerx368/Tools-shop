using Microsoft.AspNetCore.Identity;
using OnlineShop_ASP.NET.Data.Models;
using OnlineShopCourseWork.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop_ASP.NET.Models.Products
{
    public class ProductInputViewModel
    {
        public int Id{ get; set; }

        [StringLength(maximumLength: 50)]
        [Required]
        [DisplayName(displayName: "Product")]
        public string ProductName { get; set; }

		[Required]
		[DisplayName(displayName: "Image URL")]
		public string ImageURL { get; set; }
		[Required]
		public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
		public int? CategoryId{ get; set; }
		[Required]
		[StringLength(maximumLength: 50)]
		[DisplayName(displayName: "Category")]
		public string CategoryName { get; set; }
		
		public string Description{ get; set; }

		public int BrandId { get; set; }

		[Required]
		[StringLength(maximumLength: 50)]
		[DisplayName(displayName: "Brand")]
		public string BrandName { get; set; }
		[Required]
		[DisplayName(displayName: "Model")]
		[StringLength(maximumLength: 50)]
		public string ModelName { get; set; }
		public string? UserId { get; set; }
    }
}
