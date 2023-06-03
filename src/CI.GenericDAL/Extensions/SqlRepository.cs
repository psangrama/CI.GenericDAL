using CI.GenericDAL.Domain;
using CI.GenericDAL.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace CI.GenericDAL.Extensions
{
	public partial class SqlRepository : AbstractRepository, IRepository
	{

		public override List<T> Get<T>(
			string DBConnString,
			string SQLQuery,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			using (var conn = new SqlConnection(DBConnString))
			{
				using (var cmd = new SqlCommand(SQLQuery, conn))
				{
					return base.Get<T>(conn, cmd, DataParameters, CommandTimeout, CommandBehavior);
				}
			}
		}

		public override IEnumerable<T> Get<T>(
			string DBConnString,
			string SQLQuery,
			Func<IDataRecord, T> Predicate,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			using (var conn = new SqlConnection(DBConnString))
			{
				using (var cmd = new SqlCommand(SQLQuery, conn))
				{
					return base.Get<T>(conn, cmd, Predicate, DataParameters, CommandTimeout, CommandBehavior);
				}
			}
		}

		public override async Task<IEnumerable<T>> GetAsync<T>(
			string DBConnString,
			string SQLQuery,
			CancellationToken CancellationToken,
			IList<IDataParameter> DataParameters = null,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			using (var conn = new SqlConnection(DBConnString))
			{
				using (var command = new SqlCommand(SQLQuery, conn))
				{
					return await base.GetAsync<T>(conn, command, CancellationToken, DataParameters, CommandBehavior);
				}
			}
		}
	}
}
