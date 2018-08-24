using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class Contex
	{
		public string Name { get; set; }

		public long Interval { get; set; }

		public IContextProvider ContextProvider { get; set; }

		public dynamic GivenValue { get; set; }

		[JsonProperty(nameof(IContexOperator))]
		public IContexOperator Operator { get; set; }

		public Contex()
		{
		}

		public bool Check()
		{
			return Operator.Resolve(ContextProvider.GetValue(), GivenValue);
		}
	}
}
