using System;
using System.IO;
using PrototypeContexProvider.src;
using System.Threading.Tasks;

namespace TestApp
{
	class Program
	{
		// ChildrenTokens
		public static async Task<int> AsyncMain()
		{
			dynamic dAPIKeys = await Utils.ReadFromJson(
				@"C:\Users\pfsar\OneDrive\Documents\GitHub\PrototypeContextProvider\PrototypeContexProvider\APIKeys.json"
			);

			APIKeyManger.GetInstance().ReadIn(dAPIKeys);

			var tempurtreContexProvider = new TempurtreContexProvider
			{
				CityID = "6952201",
				SelectedMessurement = TempurtreContexProvider.Messurement.Celius
			};

			tempurtreContexProvider.GetValue();

			var compositeContex = new CompositeContex();

			compositeContex.Add(new Contex
			{
				Name = "TempTest",
				ContextProvider = tempurtreContexProvider,
				Operator = new ContexGreaterThan(),
				GivenValue = 15
			});

			var polciy = new DataSharingPolciy
			{
				Author = "Paul",
				CompositeContex = compositeContex,
				DataConsumer = new DataConsumer()
			};

			await DataSharingPolicyParser.ExportToJson(polciy, "test.json");

			var recoveredPolicy = await DataSharingPolicyParser.ParseFromFileAsync("test.json");

			if (recoveredPolicy.CompositeContex.Evlaute())
			{
				Console.WriteLine(new DateTimeProvider { SelectedTimeZone = TimeZoneInfo.Local }.GetValue());
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
