using System.Collections.Generic;

namespace ASExtensionLib
{
    public static class DictionaryExtensions
    {
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
    }
}