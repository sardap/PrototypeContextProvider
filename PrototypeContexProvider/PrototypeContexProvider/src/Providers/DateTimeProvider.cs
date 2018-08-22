using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class DateTimeProvider : IContextProvider<long>
    {
		public TimeZoneInfo SelectedTimeZone { get; set; }

		public long GetValue()
		{
			return Utils.ToUnixTime(TimeZoneInfo.ConvertTimeFromUtc(TimeZoneInfo.ConvertTimeToUtc(DateTime.Now), SelectedTimeZone));
		}
	}
}
