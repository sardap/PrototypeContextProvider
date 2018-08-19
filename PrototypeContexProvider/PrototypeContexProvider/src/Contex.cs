using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class Contex<T>
	{
		private string _name;

		public IContextProvider<T> ContextProvider { get; set; }
		public T GivenValue { get; set; }
		public IContexOperator<T> Operator { get; set; }

		public Contex()
		{
		}

		public bool Check()
		{
			return Operator.Resolve(ContextProvider.GetValue(), GivenValue);
		}
	}
}
