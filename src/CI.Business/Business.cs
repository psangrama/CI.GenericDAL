using CI.Business.Entity;
using CI.GenericDAL.Domain;
using CI.GenericDAL.Extensions;
using CI.GenericDAL.Infrastructure;
using System.Data.SqlClient;

namespace CI.Business
{
	public class Business
	{
		DALConnectionString appConString = new DALConnectionString("SQL", ".", "CIGenericDALDB");

		IRepository su = new SqlRepository();

		public List<UserInfo> GetAllUsers()
		{
			return su.Get<UserInfo>(appConString.ConnectionString, @"select * from UserInfo");
		}
	}
}