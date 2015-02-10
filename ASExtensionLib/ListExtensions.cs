using System;
using System.Collections.Generic;
using System.Linq;

namespace ASExtensionLib
{
	public static class ListExtensions
	{
		public static int[] GetRandomIndexes<T>(this IList<T> originalList, int numberOfItems)
		{
			return GetRandomIndexes(originalList, numberOfItems, false);
		}

		public static int[] GetRandomIndexes<T>(this IList<T> originalList, int numberOfItems, bool allowDuplicates)
		{
			List<int> indexes = new List<int>();
			if (originalList == null) return indexes.ToArray();
			if (numberOfItems > 0 && numberOfItems < originalList.Count)
			{
				for (int i = 0; i < numberOfItems; i++)
				{
					Random random = new Random();
					if (!allowDuplicates)
					{
						int index;
						do
						{
							index = originalList.IndexOf(originalList.ElementAt(random.Next(0, originalList.Count - 1)));
						} while (indexes.Contains(index));

						indexes.Add(index);
					}
					else
					{
						indexes.Add(originalList.IndexOf(originalList.ElementAt(random.Next(0, originalList.Count - 1))));
					}
				}
			}
			else //return all indexes
			{
				for (int i = 0; i < originalList.Count; i++)
				{
					indexes.Add(i);
				}
			}
			return indexes.ToArray();
		}

		public static List<T> GetRandomValues<T>(this IList<T> originalList, int numberOfItems)
		{
			return GetRandomValues(originalList, numberOfItems, false);
		}

		public static List<T> GetRandomValues<T>(this IList<T> originalList, int numberOfItems, bool allowDuplicates)
		{
			if (originalList == null || originalList.Count == 0) return new List<T>();
			List<T> returnList = new List<T>();
			int[] indexes = originalList.GetRandomIndexes(numberOfItems, allowDuplicates);
			foreach (int index in indexes)
			{
				returnList.Add(originalList[index]);
			}
			return returnList;
		}
	}
}
