﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public interface IContexOperator
    {
		string Type { get; }

		bool Resolve(dynamic a, dynamic b);
	}
}
