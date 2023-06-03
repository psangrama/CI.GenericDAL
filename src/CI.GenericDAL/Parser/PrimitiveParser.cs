using CI.GenericDAL.Infrastructure;
using System.Data;
using System.Data.Common;

namespace CI.GenericDAL.Parser
{
	public class PrimitiveParser : IDataReaderParser
	{
		public List<T> Parse<T>(IDataReader Reader)
		{
			List<T> result = new List<T>();
			if (Reader != null)
			{
				while (Reader.Read())
				{
					var item = Parse<T>(Reader[0]);
					result.Add(item);
				}
			}
			return result;
		}

		public async Task<List<T>> ParseAsync<T>(DbDataReader Reader, CancellationToken CancellationToken)
		{
			List<T> result = new List<T>();
			if (Reader != null)
			{
				while (await Reader.ReadAsync(CancellationToken))
				{
					var item = Parse<T>(Reader[0]);
					result.Add(item);
				}
			}

			return result;
		}

		public T? Parse<T>(object Src)
		{
			return Src == null || Src == DBNull.Value ? default(T) : (T)Src;
		}
	}
}
