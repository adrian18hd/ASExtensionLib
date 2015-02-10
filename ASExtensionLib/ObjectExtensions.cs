using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ASExtensionLib
{
	public static class ObjectExtensions
	{
		public static bool ToBool(this object input, bool defaultValue = false)
		{
			if (input == null)
			{
				return defaultValue;
			}
			return input.ToString().ToBoolean();
		}

		/// <summary>
		///     Converts the given object to decimal. If the conversion fails, defaultValue is returned
		/// </summary>
		public static Decimal ToDecimal(this object input, Decimal defaultValue = 0)
		{
			if (input == null)
			{
				return defaultValue;
			}
			Decimal val;
			if (Decimal.TryParse(input.ToString(), out val))
			{
				return val;
			}
			return defaultValue;
		}

		/// <summary>
		///     Converts the given object to float. If the conversion fails, defaultValue is returned
		/// </summary>
		public static float ToFloat(this object input, float defaultValue = 0)
		{
			if (input == null)
			{
				return defaultValue;
			}
			float val;
			if (float.TryParse(input.ToString(), out val))
			{
				return val;
			}
			return defaultValue;
		}

		/// <summary>
		///     Converts the given object to double. If the conversion fails, defaultValue is returned
		/// </summary>
		/// <param name="input"></param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static Double ToDouble(this object input, Double defaultValue = 0)
		{
			if (input == null)
			{
				return defaultValue;
			}
			Double val;
			if (Double.TryParse(input.ToString(), out val))
			{
				return val;
			}
			return defaultValue;
		}

		public static DateTime ToEasternStandardDateTime(this object input)
		{
			if (input == null)
			{
				return DateTime.MinValue;
			}
			DateTime dateTime;

			if (DateTime.TryParse(input.ToString(), out dateTime))
			{
				return dateTime.ToUniversalTime().AddHours(-5);
			}

			return DateTime.MinValue;
		}

		public static Guid ToGuid(this object input)
		{
			if (input == null)
			{
				return Guid.Empty;
			}
			return input.ToString().ToGuid();
		}

		/// <summary>
		///     Converts the given object to int32. If the conversion fails, defaultValue is returned
		/// </summary>
		public static int ToInt(this object input, int defaultValue = 0)
		{
			if (input == null)
			{
				return defaultValue;
			}
			int val;
			if (int.TryParse(input.ToString(), out val))
			{
				return val;
			}
			//try and see if it's not a decimal number
			decimal valD;
			if (decimal.TryParse(input.ToString(), out valD))
			{
				return int.Parse(valD.ToString("#", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
			}
			return defaultValue;
		}

		/// <summary>
		///     Converts the given object to int32. If the conversion fails, defaultValue is returned
		/// </summary>
		public static long ToLong(this object input, long defaultValue = 0)
		{
			if (input == null)
			{
				return defaultValue;
			}
			long val;
			if (long.TryParse(input.ToString(), out val))
			{
				return val;
			}
			//try and see if it's not a decimal number
			decimal valD;
			if (decimal.TryParse(input.ToString(), out valD))
			{
				return long.Parse(valD.ToString("#", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
			}
			return defaultValue;
		}

		public static DateTime ToPacificStandardDateTime(this object input)
		{
			if (input == null)
			{
				return DateTime.MinValue;
			}
			DateTime dateTime;

			if (DateTime.TryParse(input.ToString(), out dateTime))
			{
				return dateTime.ToUniversalTime().AddHours(-8);
			}

			return DateTime.MinValue;
		}

		public static string SerializeToXmlString<T>(this T toSerialize) where T : new()
		{
			if (!typeof (T).IsSerializable && !(typeof (T) is ISerializable))
			{
				throw new InvalidOperationException("The type of the object is not serializable");
			}
			var xmlSerializer = new XmlSerializer(toSerialize.GetType());
			using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				xmlSerializer.Serialize(textWriter, toSerialize);
				return textWriter.ToString();
			}
		}

		public static byte[] SerializeToByteArray(this object input)
		{
			if (input == null) return null;
			var binaryFormatter = new BinaryFormatter();
			using (var memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, input);
				return memoryStream.ToArray();
			}
		}

		public static T DeserializeByteArrayToObject<T>(this byte[] bytes) where T : class
		{
			var binaryFormatter = new BinaryFormatter();
			using (var memoryStream = new MemoryStream(bytes))
			{
				object o = binaryFormatter.Deserialize(memoryStream);
				if (o is T)
				{
					return (T) o;
				}
				return null;
			}
		}
	}
}