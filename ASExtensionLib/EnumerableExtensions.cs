using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace ASExtensionLib
{
	public static class EnumerableExtensions
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
		{
			DataTable tableToReturn = new DataTable { Locale = CultureInfo.InvariantCulture };
			if (collection == null) return tableToReturn;
			//column names
			PropertyInfo[] props = null;
			foreach (T record in collection)
			{
				//use reflection to get property names
				if (props == null)
				{
					props = record.GetType().GetProperties();
					foreach (PropertyInfo pInfo in props)
					{
						Type colType = pInfo.PropertyType;
						if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
						{
							colType = colType.GetGenericArguments()[0];
						}

						tableToReturn.Columns.Add(new DataColumn(pInfo.Name, colType));
					}
				}
				DataRow row = tableToReturn.NewRow();
				foreach (PropertyInfo pInfo in props)
				{
					object value = pInfo.GetValue(record, null);
					row[pInfo.Name] = value ?? DBNull.Value;
				}

				tableToReturn.Rows.Add(row);
			}

			return tableToReturn;
		}
	}
}