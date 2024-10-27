using System.ComponentModel.DataAnnotations;

namespace QuickKart.ProductService.Models
{
	public class Categories
	{
        [Key]
        public byte CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }

}
