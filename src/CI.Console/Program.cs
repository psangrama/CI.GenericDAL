

using CI.Business;
using CI.Business.Entity;

public class program
{
	static void Main()
	{
		Console.WriteLine("Execution Started!");

		Business mBusiness = new Business();
		List<UserInfo> mUserInfo = mBusiness.GetAllUsers();

		foreach (UserInfo userInfo in mUserInfo)
		{
			Console.WriteLine($"User - {userInfo.FullName} added on {userInfo.UserAdded} with status now set to {userInfo.IsActive}");
		}
	}
}