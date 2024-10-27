using QuickKart.ProductService.Data;
using QuickKart.ProductService.Models;

namespace QuickKart.ProductService.Repository
{
	public class ProductRepository : IProductRepository
	{
		QuickKartProductDBContext _context;

		public ProductRepository(QuickKartProductDBContext context)
		{
			_context = context;
		}
		public List<Products> GetProductsByCategory(int categoryId)
		{
			List<Products> result = new List<Products>();
			try
			{
				result = _context.Products.Where(x => x.CategoryId == categoryId).ToList();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
		public int GetProductQuantity(string productId)
		{
			int result = 0;
			try
			{
				result = _context.Products
					.Where(x => x.ProductId == productId)
					.Select(x => x.QuantityAvailable).FirstOrDefault();
			}
			catch (Exception)
			{

				result = -99;
			}
			return result;
		}
		public bool UpdateProductQuantity(string productId, int quantitytoReduce)
		{
			bool status = false;
			try
			{
				var tempProduct = _context.Products
					.Where(x => x.ProductId == productId).FirstOrDefault();
				tempProduct.QuantityAvailable -= quantitytoReduce;
				status = _context.SaveChanges() > 0;
			}
			catch (Exception)
			{
				status = false;
			}
			return status;
		}



		public List<Products> GetProductsByName(string name)
		{
			List<Products> result = new List<Products>();
			try
			{
				result = _context.Products.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public Products GetProduct(string prodId)
		{
			Products result = new Products();
			try
			{
				result = _context.Products.Where(x => x.ProductId.ToLower() ==  prodId.ToLower()).FirstOrDefault();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
