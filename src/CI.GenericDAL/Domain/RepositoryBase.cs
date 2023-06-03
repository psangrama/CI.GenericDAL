using CI.GenericDAL.Factory;
using System.Data;
using System.Data.Common;

namespace CI.GenericDAL.Domain
{
	public class RepositoryBase
	{
		private ParserFactory parserFactory;

		public RepositoryBase()
		{
			this.parserFactory = new ParserFactory();
		}

		protected IEnumerable<T> Get<T>(
			IDbConnection Connection,
			IDbCommand Command,
			Func<IDataRecord, T> Predicate,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			return Get<T>(
				Connection,
				Command,
				(IDataRecord rdr) => { return Predicate(rdr); },
				DataParameters,
				CommandTimeout,
				CommandBehavior);
		}

		protected List<T> Get<T>(
			IDbConnection Connection,
			IDbCommand Command,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			List<T> result = new List<T>();

			Connection.Open();
			Command.CommandTimeout = CommandTimeout;
			this.PrepareParameters(Command, DataParameters);
			using (var reader = Command.ExecuteReader(CommandBehavior))
			{
				var parser = parserFactory.GetParser<T>();
				result = parser.Parse<T>(reader);

				reader.Close();
			}

			if (CommandBehavior == CommandBehavior.CloseConnection)
			{
				Connection.Close();
			}

			return result;
		}

		private IEnumerable<T> Get<T>(
			IDbConnection Connection,
			IDbCommand Command,
			Func<IDataReader, T> Predicate,
			IList<IDataParameter> DataParameters = null,
			int CommandTimeout = 10000,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			Connection.Open();
			Command.CommandTimeout = CommandTimeout;
			PrepareParameters(Command, DataParameters);
			using (var reader = Command.ExecuteReader(CommandBehavior))
			{
				while (reader.Read())
				{
					yield return Predicate(reader);
				}

				reader.Close();
			}

			if (CommandBehavior == CommandBehavior.CloseConnection)
			{
				Connection.Close();
			}
		}

		protected async Task<List<T>> GetAsync<T>(
			DbConnection Connection,
			DbCommand Command,
			CancellationToken CancellationToken,
			IList<IDataParameter> DataParameters = null,
			CommandBehavior CommandBehavior = CommandBehavior.CloseConnection)
		{
			var result = new List<T>();
			await Connection.OpenAsync();
			PrepareParameters(Command, DataParameters);
			using (var reader = await Command.ExecuteReaderAsync(CancellationToken))
			{
				var parser = parserFactory.GetParser<T>();
				result = await parser.ParseAsync<T>(reader, CancellationToken);
				reader.Close();
			}

			if (CommandBehavior == CommandBehavior.CloseConnection)
			{
				Connection.Close();
			}

			return result;
		}

		protected void PrepareParameters(IDbCommand Command, IList<IDataParameter> DataParameters)
		{
			if (DataParameters != null)
			{
				foreach (IDataParameter item in DataParameters)
				{
					var param = Command.CreateParameter();
					param.DbType = item.DbType;
					param.Direction = item.Direction;
					param.ParameterName = item.ParameterName;
					param.Value = item.Value;
				}
			}
		}
	}
}
