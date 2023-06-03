using CI.GenericDAL.Infrastructure;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace CI.GenericDAL.Parser
{
	public class ReflectiveParser : IDataReaderParser
	{
		public List<T> Parse<T>(IDataReader Reader)
		{
			List<T> result = new List<T>();

			if (Reader != null)
			{
				while (Reader.Read())
				{
					BuildResult<T>(Reader, result);
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
					BuildResult<T>(Reader, result);
				}
			}

			return result;
		}

		private void BuildResult<T>(IDataReader Reader, List<T> Result)
		{
			T t = (T)Activator.CreateInstance(typeof(T), null);
			Hashtable cacheProperties = GetProperties<T>();
			for (int idx = 0; idx < Reader.FieldCount; idx++)
			{
				PropertyInfo info = (PropertyInfo)cacheProperties[Reader.GetName(idx).ToUpper()];
				if ((info != null) && info.CanWrite)
				{
					var val = Reader.GetValue(idx) == null || Reader.GetValue(idx) == DBNull.Value
						? null : Reader.GetValue(idx);
					info.SetValue(t, val, null);
				}
			}
			Result.Add(t);
		}

		private Hashtable GetProperties<T>()
		{
			Hashtable result = new Hashtable();

			Type entityType = typeof(T);
			PropertyInfo[] properties = entityType.GetProperties();
			foreach (PropertyInfo info in properties)
			{
				result[info.Name.ToUpper()] = info;
			}

			return result;
		}
	}
}
