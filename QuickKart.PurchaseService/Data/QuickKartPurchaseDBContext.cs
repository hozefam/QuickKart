using Microsoft.EntityFrameworkCore;
using QuickKart.PurchaseService.Models;

namespace QuickKart.PurchaseService.Data
{
	public class QuickKartPurchaseDBContext: DbContext
	{
        public QuickKartPurchaseDBContext(DbContextOptions options): base(options)
        {
            
        }
		public DbSet<PurchaseDetails> PurchaseDetails { get; set; }
		public DbSet<CardDetails> CardDetails { get; set; }

	}
}
