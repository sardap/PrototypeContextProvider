using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class CompositeContex  : IContex
	{
		public long Id { get; set; }

		private class Entry
		{
			public IContex Contex;
			public GlueLogicOperator Glue;
			public bool Not;
		}

		private List<Entry> Contexies { get; set; }

		public IContexOperator Operator { get; set; }

		public CompositeContex()
		{
			Contexies = new List<Entry>();
		}

		public void Add(IContex contex, GlueLogicOperator glueLogicOperator = GlueLogicOperator.And, bool not = false)
		{
			if(Contexies.Count == 0)
			{
				glueLogicOperator = GlueLogicOperator.And;
			}

			Contexies.Add(CreateEntry(contex, glueLogicOperator, not));
		}

		public bool Check()
		{
			bool result = true;

			foreach(Entry entry in Contexies)
			{
				dynamic daContex = entry.Contex;

				switch (entry.Glue)
				{
					case GlueLogicOperator.And:
						result = result && daContex.Check();
						break;

					case GlueLogicOperator.Or:
						result = result || daContex.Check();
						break;

					case GlueLogicOperator.Xor:
						result = Utils.Xor(result, daContex.Check());
						break;

				}
			}

			return result;
		}

		public CompositeContexJson GenreateJsonVersion()
		{
			var result = new CompositeContexJson();

			foreach(var entry in Contexies)
			{
				result.Conteiexs.Add
				(
					new CompositeContexJson.Entry
					{
						Contex = entry.Contex,
						Glue = entry.Glue,
						Not = entry.Not
					}
				);
			}

			return result;
		}

		private Entry CreateEntry(IContex contex, GlueLogicOperator glueLogicOperator, bool not)
		{
			return new Entry { Contex = contex, Glue = glueLogicOperator, Not = not };
		}
    }
}
