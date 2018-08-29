using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

		public static async Task<object> ReadFromJson(string filePath)
		{
			using (StreamReader r = new StreamReader(filePath))
			{
				string json = await r.ReadToEndAsync();
				return await Task.Run(() => JsonConvert.DeserializeObject(json));
			}
		}

		// Stolen https://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way
		public static long LongRandom(Random random, long min = long.MinValue, long max = long.MaxValue)
		{
			byte[] buf = new byte[8];
			random.NextBytes(buf);
			long longRand = BitConverter.ToInt64(buf, 0);

			return (Math.Abs(longRand % (max - min)) + min);
		}
	}
}
