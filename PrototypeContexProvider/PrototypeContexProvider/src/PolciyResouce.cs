using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestServer
{
	public class PolciyResouce
	{
		private static PolciyResouce _singleton;

		public Dictionary<string, long> PolicyResouceMap { get; set; }

		public string FileName { get; set; }

		private PolciyResouce()
		{
			PolicyResouceMap = new Dictionary<string, long>();
			FileName = "PolciyResouceMap.json";
		}

		public static PolciyResouce GetInstance()
		{
			if (_singleton == null)
				_singleton = new PolciyResouce();

			return _singleton;
		}

		public void SaveDB()
		{
			using (StreamWriter r = new StreamWriter(FileName))
			{
				r.Write(JsonConvert.SerializeObject(PolicyResouceMap));
			}
		}

		public void LoadDB()
		{
			using (StreamReader r = new StreamReader(FileName))
			{
				string json = r.ReadToEnd();
				PolicyResouceMap = JsonConvert.DeserializeObject<Dictionary<string, long>>(json);
			}

		}
	}
}
