using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop_ASP.NET.Models.Brands
{
	public class BrandFormInputModel
	{
		[Required]
		[StringLength(maximumLength: 50)]
		public string BrandName { get; set; }
		[Required]
		[StringLength(maximumLength: 50)]
		public string ModelName { get; set; }
	}
}
