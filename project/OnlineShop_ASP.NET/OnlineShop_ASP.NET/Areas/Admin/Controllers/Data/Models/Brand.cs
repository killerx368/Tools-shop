using System.ComponentModel.DataAnnotations;

namespace OnlineShop_ASP.NET.Data.Models
{
    public class Brand
    {
        public int Id{ get; set; }

        [StringLength(maximumLength: 50)]
        public string BrandName { get; set; } = null!;

        [StringLength(maximumLength: 50)]
        public string ModelName{ get; set; } = null!;
    }
}
