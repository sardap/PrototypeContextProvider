using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class CompositeContexJson
	{
		public class Entry
		{
			public Contex Contex;
			public GlueLogicOperator Glue;
			public bool Not;
		}

		public List<Entry> Conteiexs { get; set; }

		public CompositeContexJson()
		{
			Conteiexs = new List<Entry>();
		}

		public CompositeContex ToCompositeContex()
		{
			var result = new CompositeContex();

			foreach(var entry in Conteiexs)
			{
				result.Add(entry.Contex, entry.Glue, entry.Not);
			}

			return result;
		}
	}
}
