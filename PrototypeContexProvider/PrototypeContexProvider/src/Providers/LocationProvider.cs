using GuigleAPI;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class LocationProvider : IContextProvider
	{
		public dynamic GetValue()
		{
			GoogleGeocodingAPI.GoogleAPIKey = APIKeyManger.GetInstance().GetApiKey("GoogleLocation");
			var task = Task.Run(async () => await GoogleGeocodingAPI.GetCoordinatesFromAddressAsync("100 Market St, Southbank"));
			task.Wait();
			var asyncFunctionResult = task.Result;
			var latitude = asyncFunctionResult.Item1;
			var longitude = asyncFunctionResult.Item2;

			throw new NotImplementedException();
		}
	}
}
