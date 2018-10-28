using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrototypeContexProvider.src
{
	public interface IContex
	{
		[JsonProperty(nameof(IContexOperator))]
		IContexOperator Operator { get; set; }

		bool Check();
	}
}
