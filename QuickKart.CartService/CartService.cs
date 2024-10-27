using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

using QuickKart.Models;
using Microsoft.ServiceFabric.Services.Remoting;

namespace QuickKart.CartService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CartService : StatefulService, ICartRepository
    {

		public CartService(StatefulServiceContext context)
            : base(context)
		{
		}

		public Task<bool> DeleteProduct(string emailId, Product productObj)
		{
			var cartRepository = new CartRepository(this.StateManager);
			return cartRepository.DeleteProduct(emailId, productObj);
		}

		public Task<List<Product>> GetCartProducts(string emailId)
		{
			var cartRepository = new CartRepository(this.StateManager);
			return cartRepository.GetCartProducts(emailId);
		}

		public Task<bool> UpdateAddProduct(string emailId, Product productObj)
		{
			var cartRepository = new CartRepository(this.StateManager);
			return cartRepository.UpdateAddProduct(emailId, productObj);	
		}

		/// <summary>
		/// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
		/// </summary>
		/// <remarks>
		/// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
		/// </remarks>
		/// <returns>A collection of listeners.</returns>
		protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
		}

		/// <summary>
		/// This is the main entry point for your service replica.
		/// This method executes when this replica of your service becomes primary and has write status.
		/// </summary>
		/// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
		protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            
            var carts = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, List<Product>>>("carts");

			// Add a sample cart for the user hozefam1@gmail.com
			using (var tx = this.StateManager.CreateTransaction())
            {
                await carts.AddAsync(tx, "hozefam1@gmail.com", new List<Product> { 
					new Product { ProductId = "P102", ProductName = "BMW X1", PricePerPiece = 3000, Quantity = 1 }, 
					new Product { ProductId = "P103", ProductName = "BMW Z4", PricePerPiece = 3000, Quantity = 1 }
				});

                await tx.CommitAsync();
            }

            // Get the cart for the user hozefam1@gmail.com
            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await carts.TryGetValueAsync(tx, "hozefam1@gmail.com");

                if (result.HasValue)
                {
                    var cartProducts = result.Value;
                    foreach (var product in cartProducts)
                    {
                        ServiceEventSource.Current.ServiceMessage(this.Context, $"Product Id: {product.ProductId}, Product Name: {product.ProductName}, Price: {product.PricePerPiece}, Quantity: {product.Quantity}");
                    }
                }

                await tx.CommitAsync();
            }
        }
	}
}
