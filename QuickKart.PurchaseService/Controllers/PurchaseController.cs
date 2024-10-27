using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using QuickKart.CartService;
using QuickKart.PurchaseService.Models;
using QuickKart.PurchaseService.Repository;

namespace QuickKart.PurchaseService.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PurchaseController : ControllerBase, IService
	{
		private readonly IPurchaseRepository purchaseRepository;
		private readonly ICartRepository cartServiceProxy;


		public PurchaseController(IPurchaseRepository purchaseRepository)
        {
			this.purchaseRepository = purchaseRepository;
			this.cartServiceProxy = ServiceProxy.Create<ICartRepository>(new Uri("fabric:/QuickKart/QuickKart.CartService"), new ServicePartitionKey(0));
		}

		[HttpPost]
		[Route("PurchaseProduct")]
		public async Task<string> PurchaseProduct([FromBody] UserCard userCardObj)
		{
			// Check if the user is valid
			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync($"http://localhost:5000/api/user/ValidateEmail?emailId={userCardObj.EmailId}");
				if (response.IsSuccessStatusCode)
				{
					var isValidEmail = await response.Content.ReadAsStringAsync();
					if (bool.TryParse(isValidEmail, out bool result) && result)
					{
						if (purchaseRepository.CardValidation(userCardObj.CardObject))
						{
							var cartProducts = await cartServiceProxy.GetCartProducts(userCardObj.EmailId);

							if (cartProducts?.Count > 0)
							{
								foreach (var product in cartProducts)
								{
									decimal totalPrice = 0;

									var quantityResponse = await httpClient.GetAsync($"http://localhost:5001/api/product/GetProductQuantity?productId={product.ProductId}");

									if (quantityResponse.IsSuccessStatusCode)
									{
										var availableQuantityStr = await quantityResponse.Content.ReadAsStringAsync();
										if (int.TryParse(availableQuantityStr, out int availableQuantity))
										{
											if (product.Quantity > availableQuantity)
											{
												return $"Product is not available.";
											}
											else
											{
												totalPrice += (decimal)(product.PricePerPiece * product.Quantity);
											}
										}
										else
										{
											return "Some error occurred";
										}

										if (purchaseRepository.CardBalanceValidation(userCardObj.CardObject.CardNumber, totalPrice))
										{
											if (purchaseRepository.CardTransaction(userCardObj.CardObject.CardNumber, totalPrice))
											{
												PurchaseDetails purchaseObj = new PurchaseDetails
												{
													EmailId = userCardObj.EmailId,
													ProductId = product.ProductId,
													QuantityPurchased = product.Quantity,
													DateOfPurchase = DateTime.Now
												};

												if (!purchaseRepository.PurchaseProduct(purchaseObj))
												{
													return "Some error occurred";
												}

												var updateProductQuantity = await httpClient.GetAsync($"http://localhost:5001/api/product/UpdateProductQuantity?productId={product.ProductId}&quantitytoReduce={product.Quantity}");

												if (!updateProductQuantity.IsSuccessStatusCode)
												{
													return "Some error occurred";
												}

												if (!await cartServiceProxy.DeleteProduct(userCardObj.EmailId, product))
												{
													return "Some error occurred";
												}

												return "Successfully purchased";
											}
										}
										else
										{
											return "Insufficient balance";
										}
									}
								}
							}
						}
						else
						{
							return "Invalid card details";

						}
					}
					else
					{
						return "Invalid user";
					}
				}
				else
				{
					return "Some error occurred";
				}
			}

			return "Some error occurred"; ;
		}
	}
}
