using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexLessThan : IContexOperator
    {
		public string Type
		{
			get
			{
				return "L";
			}
		}

		public bool Resolve(dynamic a, dynamic b)
		{
			return a < b;
		}
	}
}
