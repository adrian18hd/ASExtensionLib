using System.Collections.Generic;

namespace ASExtensionLib
{
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Merges 2 dictionaries.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="source">The source dictionary.</param>
		/// <param name="toMergeItems">The dictionary to merge.</param>
		public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> toMergeItems)
		{
			if (source == null || source.Count == 0) return;
			if (toMergeItems == null || toMergeItems.Count == 0) return;
			foreach (KeyValuePair<TKey, TValue> toMergeItem in toMergeItems)
			{
				if (!source.ContainsKey(toMergeItem.Key))
				{
					source.Add(toMergeItem.Key, toMergeItem.Value);
				}
			}
		}

		/// <summary>
		/// Gets the value for the given key if it exists in the dictionary, or returns a default value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
			TValue defaultValue = default(TValue))
		{
			TValue value;
			if (!dictionary.TryGetValue(key, out value))
			{
				value = defaultValue;
			}
			return value;
		}

		/// <summary>
		/// Gets the value if it exists in the dictionary or adds it to the dictionary if not exists.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public static TValue GetValueOrAddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
			TValue value)
		{
			TValue valueToReturn;
			if (!dictionary.TryGetValue(key, out valueToReturn))
			{
				dictionary.Add(key, value);
				valueToReturn = value;
			}
			return valueToReturn;
		}
	}
}