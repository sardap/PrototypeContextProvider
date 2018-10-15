using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class NumberContexProvider : IContextProvider
	{
		public long Number { get; set; }

		public dynamic GetValue()
		{
			return Number;
		}
	}
}
