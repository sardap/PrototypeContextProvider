using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public static class Utils
    {
		public static long ToUnixTime(DateTime dateTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var temp = (DateTime.Now - epoch).TotalSeconds;
			return (long)temp;
		}

		public static bool Xor(bool a, bool b)
		{
			return ((!a) && b) || (a && (!b));
		}
    }
}
