using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class Contex<T>
	{
		public string Name { get; set; }

		public long Interval { get; set; }

		public IContextProvider<T> ContextProvider { get; set; }

		public T GivenValue { get; set; }

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
