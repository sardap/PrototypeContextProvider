using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexGreaterThanOrEqual<T> : IContexOperator<T>
    {
		public bool Resolve(T a, T b)
		{
			return new ContexGreaterThan<T>().Resolve(a, b) || new ContexEqual<T>().Resolve(a, b);
		}
	}	
}
