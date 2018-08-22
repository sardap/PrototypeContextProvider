using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexLessThanOrEqual : IContexOperator
	{
		public string Type
		{
			get
			{
				return "LOE";
			}
		}

		public bool Resolve(dynamic a, dynamic b)
		{
			return new ContexLessThan().Resolve(a, b) || new ContexEqual().Resolve(a, b);
		}
	}
}
