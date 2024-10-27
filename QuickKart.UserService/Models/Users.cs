using System.ComponentModel.DataAnnotations;

namespace QuickKart.UserService.Models
{
	public class Users
	{
		[Key]
		public string EmailId { get; set; }
		[Required]
		public string UserPassword { get; set; }
		[Required]
		public char Gender { get; set; }
		[Required]
		public DateOnly DateOfBirth { get; set; }
		[Required]
		public string Address { get; set; }	
	}
}
