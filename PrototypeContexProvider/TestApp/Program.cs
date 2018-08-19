using System;
using System.IO;
using PrototypeContexProvider.src;
using System.Threading.Tasks;

namespace TestApp
{
	class Program
	{
		public static async Task<int> AsyncMain()
		{
			dynamic dAPIKeys = await Utils.ReadFromJson(
				@"C:\Users\pfsar\OneDrive\Documents\GitHub\PrototypeContextProvider\PrototypeContexProvider\APIKeys.json"
			);

			var tempurtreContexProvider = new TempurtreContexProvider
			{
				CityID = "6952201",
				ApiKey = dAPIKeys.OpenWeather
			};
			tempurtreContexProvider.GetValue();

			var compositeContex = new CompositeContex();

			compositeContex.Add(new Contex<double>
			{
				ContextProvider = tempurtreContexProvider,
				Operator = new ContexGreaterThan<double>(),
				GivenValue = 270
			});

			if (compositeContex.Evlaute())
			{
				Console.WriteLine(new TimeContextProvider { SelectedTimeZone = TimeZoneInfo.Local }.GetValue());
			}
			else
			{
				Console.WriteLine("Shit");
			}

			Console.ReadLine();

			return 0;
		}

        static void Main(string[] args)
        {
			AsyncMain().Wait();
		}
    }
}


/*
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
*/
