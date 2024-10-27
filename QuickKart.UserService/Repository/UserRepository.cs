using QuickKart.UserService.Data;
using QuickKart.UserService.Models;

namespace QuickKart.UserService.Repository
{
	public class UserRepository : IUserRepository
	{
		QuickKartUserDBContext _context;
		public UserRepository(QuickKartUserDBContext context)
		{
			_context = context;
		}
		public bool ValidateCredentials(string emailId, string password)
		{
			bool result = false;
			try
			{
				result = _context
					.Users
					.Any(x => x.EmailId.ToLower() == emailId.ToLower()
					&& x.UserPassword.ToLower() == password.ToLower());
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public bool CheckUser(string emailId)
		{
			bool result = false;
			try
			{
				result = _context.Users.Any(x => x.EmailId.ToLower() == emailId);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
		public bool RegisterUser(Users userObj)
		{
			bool result = false;
			try
			{
				_context.Users.Add(userObj);
				var status = _context.SaveChanges();
				result = status > 0;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
	}
}
