using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexGreaterThanOrEqual : IContexOperator
    {
		public bool Resolve(dynamic a, dynamic b)
		{
			return new ContexGreaterThan().Resolve(a, b) || new ContexEqual().Resolve(a, b);
		}
	}	
}
