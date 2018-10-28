using System;
using System.IO;
using PrototypeContexProvider.src;
using System.Threading.Tasks;
using PrototypeContexProvider.src.Providers;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
	class Program
	{
		private static Random rnd = new Random();

		public static async Task<int> AsyncMain()
		{
			/*
			dynamic dAPIKeys = await Utils.ReadFromJson(
				@"C:\Users\pfsar\OneDrive\Documents\GitHub\PrototypeContextProvider\PrototypeContexProvider\APIKeys.json"
			);

			APIKeyManger.GetInstance().ReadIn(dAPIKeys);

			var watch = new WatchLightProvider
			{
				URL = @"https://localhost:44320/api/watch"
			};

			var compositeContex = new CompositeContex();

			compositeContex.Add(new Contex
			{
				Name = "TempTest",
				ContextProvider = new NumberContexProvider(){ Number = 3 },
				Operator = new ContexGreaterThan(),
				GivenValue = 10
			});

			compositeContex.Add(new Contex
			{
				Name = "TempTest",
				ContextProvider = new NumberContexProvider() { Number = 10 },
				Operator = new ContexLessThan(),
				GivenValue = 5
			});

			compositeContex.Add(new Contex
			{
				Name = "TempTest",
				ContextProvider = new NumberContexProvider() { Number = 15 },
				Operator = new ContexEqual(),
				GivenValue = 15
			});

			var id = Utils.LongRandom(rnd);

			var polciy = new DataSharingPolciy
			{
				Id = id,
				Author = "Paul",
				Proity = 0,
				Interval = -1,
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

			await DataSharingPolicyParser.ExportToJson(polciy, "ThreeProviders.json");
			*/

			var recoveredPolicys = 
				new Dictionary<string, string>()
				{
					{"none", "NoneProviders.json"},
					{"one", "OneProviders.json"},
					{"two", "TwoProviders.json"},
					{"three", "ThreeProviders.json"}
				};

			await BenchUnLoadedReuslt(10, recoveredPolicys["none"]);
			await BenchUnLoadedReuslt(10, recoveredPolicys["one"]);
			await BenchUnLoadedReuslt(10, recoveredPolicys["two"]);
			await BenchUnLoadedReuslt(10, recoveredPolicys["three"]);

			//before your loop
			var csv = new StringBuilder();
			var newLine = string.Format("Name, 10, 100, 1000, 10000, 100000 ");
			csv.AppendLine(newLine);

			var vaules = new List<double>();
			var lengthsToCheck = new List<int>() { 10, 100, 1000, 10000, 100000 };

			foreach(var policy in recoveredPolicys)
			{
				string curCSV = csv.ToString();

				foreach (var length in lengthsToCheck)
				{
					vaules.Add(await BenchUnLoadedReuslt(length, policy.Value));
				}

				csv.AppendLine(string.Format("{0}, {1}", policy.Key, string.Join(",", vaules.Select(x => x.ToString()).ToArray())));
				vaules.Clear();
			}

			using (StreamWriter streamWriter = new StreamWriter("UnloadedBenchmark.csv"))
			{
				await streamWriter.WriteAsync(csv.ToString());
			}


			return 0;
		}

        static void Main(string[] args)
        {
			AsyncMain().Wait();
		}

		private async static Task<double> BenchUnLoadedReuslt(int n, string fileName)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			for (int i = 0; i < n; i++)
			{
				var policy = await DataSharingPolicyParser.ParseFromFileAsync(fileName);
				policy.Check("NotPaul");
			}
			stopwatch.Stop();

			double result = stopwatch.ElapsedTicks * 1000000 / Stopwatch.Frequency;

			return result;
		}


		private static double BenchLoadedReuslt(int n, DataSharingPolciy policy)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			for(int i = 0; i < n; i++)
			{
				policy.Check("NotPaul");
			}
			stopwatch.Stop();

			double result = stopwatch.ElapsedTicks * 1000000 / Stopwatch.Frequency;

			return result;
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
