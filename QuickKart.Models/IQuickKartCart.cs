using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickKart.Models
{
	public interface IQuickKartCart: IService
	{
		Task<List<Product>> GetCartProducts(string emailId);
		Task<bool> DeleteProduct(string emailId, Product productObj);
		Task<bool> UpdateAddProduct(string emailId, Product productObj);
	}
}
