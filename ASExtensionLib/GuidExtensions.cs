using System;

namespace ASExtensionLib
{
	public static class GuidExtensions
	{
		public static bool IsEmpty(this Guid value)
		{
			return value == Guid.Empty;
		}

		public static bool IsNotNullOrEmpty(this Guid? value)
		{
			return value != null && !value.Value.IsEmpty();
		}
	}
}