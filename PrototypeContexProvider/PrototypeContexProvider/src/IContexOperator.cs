using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public interface IContexOperator<T>
    {
		bool Resolve(T a, T b);
	}
}
