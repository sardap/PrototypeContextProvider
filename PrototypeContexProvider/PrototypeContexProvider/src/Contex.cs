using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class Contex<T> where T : ValueType
	{
		private string _name;
		private T _value;
		private ContexOperator _operator;

		public IContextProvider<T> ContextProvider	{ get; set; }

		public Contex()
		{
		}

		public bool Check()
		{
			return _value == ContextProvider.GetValue();
		}
	}
}
