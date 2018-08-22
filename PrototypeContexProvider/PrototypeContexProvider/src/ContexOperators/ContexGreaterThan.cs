using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexGreaterThan : IContexOperator
	{
		public string Type
		{
			get
			{
				return "G";
			}
		}

		public bool Resolve(dynamic a, dynamic b)
		{
			return a > b;
		}
    }
}
