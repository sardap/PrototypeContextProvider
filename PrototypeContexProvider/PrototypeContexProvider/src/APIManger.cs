using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PrototypeContexProvider.src
{
	public class APIKeyManger
	{
		private static APIKeyManger _singleton;

		private Dictionary<string, string> _apiKeys = new Dictionary<string, string>();

		private APIKeyManger() { }

		public static APIKeyManger GetInstance()
		{
			if(_singleton == null)
			{
				_singleton = new APIKeyManger();
			}

			return _singleton;
		}

		public void AddKey(string key, string apiKey)
		{
			_apiKeys.Add(key, apiKey);
		}

		public string GetApiKey(string key)
		{
			return _apiKeys[key];
		}

		public void ReadIn(Newtonsoft.Json.Linq.JObject jsonObjects)
		{
			foreach (var item in jsonObjects)
			{
				AddKey(item.Key, (string)item.Value);
			}
		}
	}
}
