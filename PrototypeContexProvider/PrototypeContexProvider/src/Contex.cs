using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class Contex
	{
		[Key]
		public long Id { get; set; }

		public string Name { get; set; }

		public long Interval { get; set; }

		[NotMappedAttribute]
		public IContextProvider ContextProvider { get; set; }

		[NotMappedAttribute]
		public dynamic GivenValue { get; set; }

		[NotMappedAttribute]
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
