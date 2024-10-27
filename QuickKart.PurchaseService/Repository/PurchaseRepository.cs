using QuickKart.PurchaseService.Data;
using QuickKart.PurchaseService.Models;

namespace QuickKart.PurchaseService.Repository
{
	public class PurchaseRepository : IPurchaseRepository
	{
		QuickKartPurchaseDBContext _context;
		public PurchaseRepository(QuickKartPurchaseDBContext context)
		{
			_context = context;
		}

		public bool PurchaseProduct(PurchaseDetails purchaseObj)
		{
			bool result = false;
			try
			{
				_context.PurchaseDetails.Add(purchaseObj);

				result = _context.SaveChanges() > 0;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public bool CardValidation(CardDetails cardObj)
		{
			bool result = false;
			try
			{
				result = _context.CardDetails.Any(
					x => x.CardNumber == cardObj.CardNumber
					&& x.ExpiryDate > DateTime.Now
					&& x.NameOnCard.ToLower() == cardObj.NameOnCard.ToLower()
					&& x.Cvvnumber == cardObj.Cvvnumber
					&& x.CardType == cardObj.CardType);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public bool CardBalanceValidation(decimal cardNumber, decimal amount)
		{
			bool result = false;
			try
			{
				result = _context.CardDetails.Any(x => x.CardNumber == cardNumber
				&& x.Balance >= amount);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public bool CardTransaction(decimal cardNumber, decimal amount)
		{
			bool result = false;
			try
			{
				var cardObj = _context.CardDetails
					.Where(x => x.CardNumber == cardNumber)
					.FirstOrDefault();
				if (cardObj.Balance >= amount)
				{
					cardObj.Balance -= amount;
				}
				result = _context.SaveChanges() > 0;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
	}

}
