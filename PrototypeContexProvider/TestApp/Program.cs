using System;
using PrototypeContexProvider.src;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
			var compositeContex = new CompositeContex();
			
			compositeContex.Add(new Contex<long>
			{
				ContextProvider = new TimeContextProvider { SelectedTimeZone = TimeZoneInfo.Local },
				Operator = new ContexLessThanOrEqual<long>(),
				GivenValue = 1534702530
			});
			
			compositeContex.Add(new Contex<long>
			{
				ContextProvider = new TimeContextProvider { SelectedTimeZone = TimeZoneInfo.Local },
				Operator = new ContexGreaterThan<long>(),
				GivenValue = 1534702410
			}, GlueLogicOperator.And, false);

			if (compositeContex.Evlaute())
			{
				Console.WriteLine(new TimeContextProvider { SelectedTimeZone = TimeZoneInfo.Local }.GetValue());
			}
			else
			{
				Console.WriteLine("Shit");
			}

			Console.ReadLine();
		}
    }
}
