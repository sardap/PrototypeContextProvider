using GuigleAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeContexProvider.src
{
	public class LocationProvider : IContextProvider<Location>
	{
		public string ApiKey { get; set; }

		public Location GetValue()
		{
			GoogleGeocodingAPI.GoogleAPIKey = ApiKey;
			var task = Task.Run(async () => await GoogleGeocodingAPI.GetCoordinatesFromAddressAsync("100 Market St, Southbank"));
			task.Wait();
			var asyncFunctionResult = task.Result;
			var latitude = asyncFunctionResult.Item1;
			var longitude = asyncFunctionResult.Item2;

			throw new NotImplementedException();
		}
	}
}
