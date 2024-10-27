using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Remoting;
using QuickKart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickKart.CartService
{
	public class CartRepository : ICartRepository, IService
	{
		private readonly IReliableStateManager _stateManager;

		public CartRepository(IReliableStateManager stateManager)
		{
			_stateManager = stateManager;
		}

		private async Task<bool> AlterCartProducts(string emailId, List<Product> cartProducts)
		{
			try
			{
				var carts = await _stateManager.GetOrAddAsync<IReliableDictionary<string, List<Product>>>("carts");

				using (var tx = _stateManager.CreateTransaction())
				{
					await carts.AddOrUpdateAsync(tx, emailId, cartProducts, (key, value) => cartProducts);
					await tx.CommitAsync();
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> DeleteProduct(string emailId, Product productObj)
		{
			var cartProducts = await GetCartProducts(emailId);
			var productToRemove = cartProducts.FirstOrDefault(p => p.ProductId == productObj.ProductId);

			if (productToRemove != null)
			{
				cartProducts.Remove(productToRemove);
				return await AlterCartProducts(emailId, cartProducts);
			}

			return false;
		}

		public async Task<List<Product>> GetCartProducts(string emailId)
		{
			var carts = await _stateManager.GetOrAddAsync<IReliableDictionary<string, List<Product>>>("carts");

			using (var tx = _stateManager.CreateTransaction())
			{
				var result = await carts.TryGetValueAsync(tx, emailId);
				return result.HasValue ? result.Value : new List<Product>();
			}
		}

		public async Task<bool> UpdateAddProduct(string emailId, Product productObj)
		{
			var cartProducts = await GetCartProducts(emailId);
			var existingProduct = cartProducts.FirstOrDefault(p => p.ProductId == productObj.ProductId);

			if (existingProduct != null)
			{
				existingProduct.ProductName = productObj.ProductName;
				existingProduct.Quantity = productObj.Quantity;
				existingProduct.PricePerPiece = productObj.PricePerPiece;
			}
			else
			{
				cartProducts.Add(productObj);
			}

			return await AlterCartProducts(emailId, cartProducts);
		}
	}
}
