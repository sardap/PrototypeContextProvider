using GuigleAPI;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace PrototypeContexProvider.src
{
	public class LocationProvider : IContextProvider
	{
		public string URL { get; set; }

		public dynamic GetValue()
		{
			string html = string.Empty;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
			request.AutomaticDecompression = DecompressionMethods.GZip;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				html = reader.ReadToEnd();
			}

			return new Location(html);
		}
	}
}
