using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ASExtensionLib
{
	public static class StringExtensions
	{
		public static bool EqualsNoCase(this string input, string stringToCompare, bool trim = false)
		{
			bool inputEmpty = String.IsNullOrEmpty(input);
			bool compareEmpty = String.IsNullOrEmpty(stringToCompare);
			if (inputEmpty && compareEmpty) return true;
			if (inputEmpty || compareEmpty) return false;
			return !trim
				? input.Equals(stringToCompare, StringComparison.OrdinalIgnoreCase)
				: input.Trim().Equals(stringToCompare.Trim(), StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsNullOrEmpty(this string input)
		{
			return string.IsNullOrEmpty(input);
		}

		public static bool IsNullOrWhiteSpace(this string input)
		{
			return string.IsNullOrWhiteSpace(input);
		}

		public static bool ToBoolean(this string input)
		{
			if (String.IsNullOrWhiteSpace(input))
			{
				return false;
			}
			var trimmedString = input.Trim();

			if (trimmedString.Equals("1", StringComparison.OrdinalIgnoreCase) ||
				trimmedString.Equals("y", StringComparison.OrdinalIgnoreCase) ||
				trimmedString.Equals("yes", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}

			if (trimmedString.Equals("0", StringComparison.OrdinalIgnoreCase) ||
				trimmedString.Equals("n", StringComparison.OrdinalIgnoreCase) ||
				trimmedString.Equals("no", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			bool result;
			return bool.TryParse(input, out result) && result;
		}

		public static decimal ToDecimal(this string input, decimal defaultValue = 0)
		{
			if (input.IsNullOrWhiteSpace())
			{
				return defaultValue;
			}

			decimal result;
			return decimal.TryParse(input, out result) ? result : defaultValue;
		}

		public static double ToDouble(this string input, double defaultValue = 0)
		{
			if (input.IsNullOrWhiteSpace())
			{
				return defaultValue;
			}

			double result;
			return double.TryParse(input, out result) ? result : defaultValue;
		}

		public static DateTime ToDateTime(this string input)
		{
			DateTime defaultValue = DateTime.MinValue;
			if (input.IsNullOrWhiteSpace())
			{
				return defaultValue;
			}

			DateTime result;
			return DateTime.TryParse(input, out result) ? result : defaultValue;
		}

		public static DateTime ToDateTime(this string input, DateTime defaultValue)
		{
			if (input.IsNullOrWhiteSpace())
			{
				return defaultValue;
			}

			DateTime result;
			return DateTime.TryParse(input, out result) ? result : defaultValue;
		}

		public static float ToFloat(this string input, float defaultValue = 0)
		{
			if (input.IsNullOrWhiteSpace())
			{
				return defaultValue;
			}

			float result;
			return float.TryParse(input, out result) ? result : defaultValue;
		}

		public static Guid ToGuid(this string input)
		{
			if (input.IsNullOrEmpty())
			{
				return Guid.Empty;
			}

			Guid result;
			return Guid.TryParse(input, out result) ? result : Guid.Empty;
		}

		private static readonly Regex _regexPattern = new Regex("(?<key>.+?):(?<value>.*?)(?=(,\")|(}))", RegexOptions.Compiled);

		public static Dictionary<string, string> ParseJsonToDictionary(this string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return null;
			}

			var match = _regexPattern.Match(json);
			var dictionary = new Dictionary<string, string>();
			while (match.Success) // always get if match was succeed
			{
				// getting key and value by alias in Regex pattern (named group)
				var key = match.Groups["key"].Value;
				var value = match.Groups["value"].Value;
				dictionary.Add(key, value);
				match = match.NextMatch(); // navigate to the next match
			}
			return dictionary;
		}
	}
}