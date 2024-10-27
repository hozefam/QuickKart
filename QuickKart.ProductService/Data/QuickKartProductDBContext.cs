using Microsoft.EntityFrameworkCore;
using QuickKart.ProductService.Models;

namespace QuickKart.ProductService.Data
{
	public class QuickKartProductDBContext: DbContext
	{
        public QuickKartProductDBContext(DbContextOptions options): base(options)
        {
            
        }

		public DbSet<Products> Products { get; set; }
		public DbSet<Categories> Categories { get; set; }


	}
}
