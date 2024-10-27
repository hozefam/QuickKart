using Microsoft.EntityFrameworkCore;
using QuickKart.UserService.Models;

namespace QuickKart.UserService.Data
{
	public class QuickKartUserDBContext: DbContext
	{
		public QuickKartUserDBContext(DbContextOptions<QuickKartUserDBContext> options) : base(options)
		{
		}
		public DbSet<Users> Users { get; set; }
	}
}
