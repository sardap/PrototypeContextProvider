using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeContexProvider.src
{
	// api.openweathermap.org/data/2.5/forecast?id=524901&APPID=3e0b77a7c48d0ef47d2b3ddfdc2d9bc3 
	public class TempurtreContexProvider : IContextProvider<double>
	{
		public string ApiKey { get; set; }
		public string CityID { get; set; }

		public TempurtreContexProvider()
		{
		}

		public double GetValue()
		{
			string url = "http://api.openweathermap.org/data/2.5/weather?id=" + CityID + "&APPID=" + ApiKey;

			var request = WebRequest.Create(url);
			request.Credentials = CredentialCache.DefaultCredentials;
			var response = request.GetResponse();
			var dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);
			// Read the content.  
			string json = reader.ReadToEnd();
			dynamic temperatureData = JsonConvert.DeserializeObject(json);
			// Display the content.  
			response.Close();
			reader.Close();

			return temperatureData.main.temp;
		}

	}
}
