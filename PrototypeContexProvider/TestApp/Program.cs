using System;
using System.IO;
using PrototypeContexProvider.src;
using System.Threading.Tasks;

namespace TestApp
{
	class Program
	{
		private static Random rnd = new Random();

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
				Id = Utils.LongRandom(rnd),
				Author = "Paul",
				Proity = 0,
				Decision = "test",
				DataConsumer = new DataConsumer
				{
					Name = "NotPaul",
					Value = "Paul"
				},
				CompositeContex = compositeContex,
				PrivacyOblgations = new PrivacyOblgations
				{
					Purpose = "Testing",
					Granularity = "Garbage",
					Anonymisation = "Garbage",
					Notifaction = "Garbage",
					Accounting = "Garbage"
				},
				ResharingObligations = new ResharingObligations
				{
					CanShare = false,
					Cardinality = 10,
					Recurring = 10
				}
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
