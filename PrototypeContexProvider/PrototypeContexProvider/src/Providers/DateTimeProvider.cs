using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class DateTimeProvider : IContextProvider
    {
		public TimeZoneInfo SelectedTimeZone { get; set; }

		public dynamic GetValue()
		{
			return Utils.ToUnixTime(TimeZoneInfo.ConvertTimeFromUtc(TimeZoneInfo.ConvertTimeToUtc(DateTime.Now), SelectedTimeZone));
		}
	}
}
