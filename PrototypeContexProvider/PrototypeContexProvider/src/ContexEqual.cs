using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ContexEqual : IContexOperator
    {
		public string Type
		{
			get
			{
				return "E";
			}
		}

		public bool Resolve(dynamic a, dynamic b)
		{
			return a == b;
		}
	}
}
