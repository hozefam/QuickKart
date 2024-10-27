using QuickKart.PurchaseService.Models;

namespace QuickKart.PurchaseService.Repository
{
	public interface IPurchaseRepository
	{
		bool CardBalanceValidation(decimal cardNumber, decimal amount);
		bool CardTransaction(decimal cardNumber, decimal amount);
		bool CardValidation(CardDetails cardObj);
		bool PurchaseProduct(PurchaseDetails purchaseObj);
	}
}