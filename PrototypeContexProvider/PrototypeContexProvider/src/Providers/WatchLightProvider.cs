using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PrototypeContexProvider.src.Providers
{
	public class WatchLightProvider : IContextProvider
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

			return Int32.Parse(html);
		}

	}
}
