using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KeyOwners = System.Collections.Generic.Dictionary<string, System.Collections.Generic.ISet<string>>;

namespace RestServer
{
	public class APIKeyChecker
	{
		public const int KEY_BYTES = 128;

		private static APIKeyChecker _singleton;

		private KeyOwners _keyOwners = new KeyOwners();

		public string FileName { get; set; }

		private APIKeyChecker()
		{
			FileName = "APIKeys.json";
		}

		public static APIKeyChecker GetInstance()
		{
			if (_singleton == null)
				_singleton = new APIKeyChecker();

			return _singleton;
		}

		public void SaveDB()
		{
			using (StreamWriter r = new StreamWriter(FileName))
			{
				r.Write(JsonConvert.SerializeObject(_keyOwners));
			}
		}

		public void LoadDB()
		{
			using (StreamReader r = new StreamReader(FileName))
			{
				string json = r.ReadToEnd();
				_keyOwners = JsonConvert.DeserializeObject<KeyOwners>(json);
			}

		}

		public bool Check(string apiKey, string resouceName)
		{
			var keysResouces = _keyOwners[apiKey];

			if (keysResouces == null)
				return false;

			return keysResouces.Contains(resouceName);
		}

		public string GenrateAndAdd()
		{
			var keyCreator = new KeyCreator();
			var newKey = keyCreator.CreateKey(KEY_BYTES);
			_keyOwners.Add(newKey, new HashSet<string>());
			return newKey;
		}

		public void AddResouce(string apiKey, string ResouceName)
		{
			_keyOwners[apiKey].Add(ResouceName);
		}
	}
}
