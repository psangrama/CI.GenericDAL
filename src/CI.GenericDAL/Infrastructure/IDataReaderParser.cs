using System.Data;
using System.Data.Common;

namespace CI.GenericDAL.Infrastructure
{
	public interface IDataReaderParser
	{
		List<T> Parse<T>(IDataReader Reader);

		Task<List<T>> ParseAsync<T>(DbDataReader Reader, CancellationToken CancellationToken);
	}
}
