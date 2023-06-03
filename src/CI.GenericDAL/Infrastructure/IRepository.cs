using System.Data;

namespace CI.GenericDAL.Infrastructure
{
	public interface IRepository
	{
		/// <summary>
		/// Retrieve data synchronously.
		/// Convert to actual type by C# reflective mechanism.
		/// </summary>
		/// <typeparam name="T">Generic type which the result list will contain.</typeparam>
		/// <param name="DBConnString">Database connection string</param>
		/// <param name="SQLQuery">Given sql to retrieve data from database</param>
		/// <param name="DataParameters">Given parameters which will be passed into sql.</param>
		/// <param name="CommandTimeout">Maximum sql execution time.</param>
		/// <param name="CommandBehavior">Default is closing connection.</param>
		/// <returns></returns>
		List<T> Get<T>(
			string DBConnString,
			string SQLQuery,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection);

		/// <summary>
		/// Retrieve data synchronously.
		/// Custom converter is used to convert to correct generic type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="DBConnString"></param>
		/// <param name="SQLQuery"></param>
		/// <param name="Predicate">Custom converter to transfer IDataRecord into correct type.</param>
		/// <param name="DataParameters"></param>
		/// <param name="commandTimeout"></param>
		/// <param name="commandBehavior"></param>
		/// <returns></returns>
		IEnumerable<T> Get<T>(
			string DBConnString,
			string SQLQuery,
			Func<IDataRecord, T> Predicate,
			IList<IDataParameter> DataParameters = null,
			int commandTimeout = 10000,
			CommandBehavior commandBehavior = CommandBehavior.CloseConnection);

		/// <summary>
		/// Retrieve data asynchronously.
		/// Automatically convert to correct type by C# reflective mechanism.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="DBConnString"></param>
		/// <param name="SQLQuery"></param>
		/// <param name="CancellationToken"></param>
		/// <param name="DataParameters"></param>
		/// <param name="CommandBehavior"></param>
		/// <returns></returns>
		Task<IEnumerable<T>> GetAsync<T>(
			string DBConnString,
			string SQLQuery,
			CancellationToken CancellationToken,
			IList<IDataParameter> DataParameters = null,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection);
	}
}
