using System;
using PrototypeContexProvider.src;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
			var contexTest = new Contex<long>
			{
				ContextProvider = new TimeContextProvider { SelectedTimeZone = TimeZoneInfo.Local },
				Operator = new ContexEqual<long>(),
				Value = 1534506720
			};

			while(!contexTest.Check())
			{
				Console.WriteLine(new TimeContextProvider { SelectedTimeZone = TimeZoneInfo.Local }.GetValue());

			}

			Console.WriteLine("Shit");

			Console.ReadLine();
		}
    }
}
