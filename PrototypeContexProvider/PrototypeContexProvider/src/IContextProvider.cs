using System;

namespace PrototypeContexProvider
{
    public interface IContextProvider<T>
    {
		T GetValue();
    }
}
