using System;

namespace Assignment.Utils
{
	/// <summary>
	/// Contain util methods related to time and dates
	/// </summary>
	public static class TimeUtils
	{
		public static long UtcNowMs()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		}
	}
}