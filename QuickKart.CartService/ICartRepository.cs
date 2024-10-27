using Microsoft.ServiceFabric.Services.Remoting;
using QuickKart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickKart.CartService
{
	public interface ICartRepository: IService
	{
		Task<List<Product>> GetCartProducts(string emailId);
		Task<bool> DeleteProduct(string emailId, Product productObj);
		Task<bool> UpdateAddProduct(string emailId, Product productObj);

	}
}
