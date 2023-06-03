using System.Data;

namespace CI.GenericDAL.Domain
{
	public abstract class AbstractRepository : RepositoryBase
	{
		public abstract List<T> Get<T>(
			string DBConnString,
			string SQLQuery,
			IList<IDataParameter> DataParameters = null,
			int commandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection);

		public abstract IEnumerable<T> Get<T>(
			string dbConnString,
			string SQLQuery,
			Func<IDataRecord, T> Predicate,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection);

		public abstract Task<IEnumerable<T>> GetAsync<T>(
			string DBConnString,
			string SQLQuery,
			CancellationToken CancellationToken,
			IList<IDataParameter> DataParameters = null,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection);
	}
}
