using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexLessThanOrEqual<T> : IContexOperator<T>
	{
		public bool Resolve(T a, T b)
		{
			return new ContexLessThan<T>().Resolve(a, b) || new ContexEqual<T>().Resolve(a, b);
		}
	}
}
