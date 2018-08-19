using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class TimeContextProvider : IContextProvider<long>
    {
		public TimeZoneInfo SelectedTimeZone { get; set; }

		public long GetValue()
		{
			var fuckyou = TimeZoneInfo.ConvertTimeFromUtc(TimeZoneInfo.ConvertTimeToUtc(DateTime.Now), SelectedTimeZone);
			var shithead = Utils.ToUnixTime(fuckyou);
			return Utils.ToUnixTime(fuckyou);
		}
	}
}
