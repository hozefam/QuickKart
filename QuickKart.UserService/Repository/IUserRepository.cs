using QuickKart.UserService.Models;

namespace QuickKart.UserService.Repository
{
	public interface IUserRepository
	{
		bool CheckUser(string emailId);
		bool RegisterUser(Users userObj);
		bool ValidateCredentials(string emailId, string password);
	}
}