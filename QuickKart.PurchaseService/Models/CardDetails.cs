using System.ComponentModel.DataAnnotations;

namespace QuickKart.PurchaseService.Models
{
	public class CardDetails
	{
        [Key]
        public long CardNumber { get; set; }
        [Required]
        public string NameOnCard { get; set; }
		[Required]
		public string CardType { get; set; }
		[Required]
		public int Cvvnumber { get; set; }
		[Required]
		public DateTime ExpiryDate { get; set; }
		public decimal Balance { get; set; }
    }
}
