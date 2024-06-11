using Microsoft.AspNetCore.Identity;
using OnlineShop_ASP.NET.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopCourseWork.Models
{
    public sealed class Product
    {
        public int Id { get; set; }
		[Required]
		[StringLength(maximumLength: 50)]
        public string ProductName { get; set; }

        public string ImageURL { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int Quantity{ get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public Category Category{ get; set; }

        [Required]
        public int BrandId{ get; set; }
        public Brand Brand{ get; set; }

		[Required]
		public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
