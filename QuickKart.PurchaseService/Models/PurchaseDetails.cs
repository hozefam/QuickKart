using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace QuickKart.PurchaseService.Models
{
	public class PurchaseDetails
	{
        [Key]
        public  long PurchaseId { get; set; }
        [Required]
        public  string EmailId { get; set; }
		[Required]
		public  string ProductId { get; set; }
		[Required]
		public  int QuantityPurchased { get; set; }
		[Required]
		public  DateTime DateOfPurchase { get; set; }
    }
}
