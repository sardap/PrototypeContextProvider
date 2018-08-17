using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexEqual<T> : IContexOperator<T>
    {
		public bool Resolve(T a, T b)
		{
			dynamic da = a;
			dynamic db = b;
			return da == db;
		}
	}
}
