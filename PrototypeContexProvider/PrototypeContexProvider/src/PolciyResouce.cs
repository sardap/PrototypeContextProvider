using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeContexProvider.src
{
	public class PolciyResouce
	{
		public const int KEY_BYTES = 86;

		public class Entry
		{
			public Dictionary<string, long> PolciesResocuce { get; set; }

			public Entry()
			{
				PolciesResocuce = new Dictionary<string, long>();
			}
		}

		private static PolciyResouce _singleton;

		public Dictionary<string, Entry> OwnershipTable { get; set; }

		public string FileName { get; set; }

		private PolciyResouce()
		{
			OwnershipTable = new Dictionary<string, Entry>();
			FileName = "PolciyResouceMap.json";
		}

		public static PolciyResouce GetInstance()
		{
			if (_singleton == null)
				_singleton = new PolciyResouce();

			return _singleton;
		}

		public string GenrateAndAddAPIKey()
		{
			var newKey = Utils.CreateKey(KEY_BYTES);
			OwnershipTable.Add(newKey, new Entry());
			SaveDB();
			return newKey;
		}

		public async Task SaveDB()
		{
			using (StreamWriter r = new StreamWriter(FileName))
			{
				await r.WriteAsync(JsonConvert.SerializeObject(OwnershipTable));
			}
		}

		public void LoadDB()
		{
			using (StreamReader r = new StreamReader(FileName))
			{
				string json = r.ReadToEnd();
				OwnershipTable = JsonConvert.DeserializeObject<Dictionary<string, Entry>>(json);

				if (OwnershipTable == null)
					OwnershipTable = new Dictionary<string, Entry>();
			}
		}
	}
}
