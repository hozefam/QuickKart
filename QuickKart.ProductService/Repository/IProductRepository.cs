using QuickKart.ProductService.Models;

namespace QuickKart.ProductService.Repository
{
	public interface IProductRepository
	{
		Products GetProduct(string prodId);
		int GetProductQuantity(string productId);
		List<Products> GetProductsByCategory(int categoryId);
		List<Products> GetProductsByName(string name);
		bool UpdateProductQuantity(string productId, int quantitytoReduce);
	}
}