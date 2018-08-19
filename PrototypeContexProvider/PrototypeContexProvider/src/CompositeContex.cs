using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class CompositeContex
    {
		private class Entry
		{
			public object Contex;
			public Type ContexType;
			public GlueLogicOperator Glue;
			public bool Not;
		}

		List<Entry> _contexies = new List<Entry>();

		public void Add<T>(Contex<T> contex, GlueLogicOperator glueLogicOperator = GlueLogicOperator.And, bool not = false)
		{
			if(_contexies.Count == 0)
			{
				glueLogicOperator = GlueLogicOperator.And;
			}

			_contexies.Add(CreateEntry(contex, glueLogicOperator, not));
		}

		public bool Evlaute()
		{
			bool result = true;

			foreach(Entry entry in _contexies)
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

		private Entry CreateEntry<T>(Contex<T> contex, GlueLogicOperator glueLogicOperator, bool not)
		{
			return new Entry { Contex = contex, Glue = glueLogicOperator, Not = not };
		}
    }
}
