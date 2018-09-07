using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class CompositeContexJson
	{
		[Key]
		public long Id { get; set; }

		public class Entry
		{
			public long Id { get; set; }
			public IContex Contex { get; set; }
			public GlueLogicOperator Glue { get; set; }
			public bool Not { get; set; }
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
