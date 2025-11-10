using System;

namespace Assignment.Utils
{
	public static class TimeUtils
	{
		public static long UtcNowMs()
		{
			return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		}
	}
}