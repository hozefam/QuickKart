using System.ComponentModel.DataAnnotations;

namespace QuickKart.ProductService.Models
{
	public class Products
	{
        [Key]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public byte CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }

        public virtual Categories Category { get; set; }
	}
}
