using CI.GenericDAL.Infrastructure;
using CI.GenericDAL.Parser;

namespace CI.GenericDAL.Factory
{
	public class ParserFactory
	{
		public IDataReaderParser GetParser<T>()
		{
			var objectType = typeof(T);
			if (objectType.IsPrimitive
				|| objectType == typeof(string)
				|| objectType == typeof(decimal)
				|| objectType == typeof(DateTime)
				|| objectType == typeof(Guid))
			{
				return new PrimitiveParser();
			}
			else
			{
				return new ReflectiveParser();
			}
		}
	}
}
