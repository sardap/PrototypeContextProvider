using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using PrototypeContexProvider.src;

namespace RestServer
{
	public class Program
	{
		public static async Task SetupPCP()
		{
			dynamic dAPIKeys = await PrototypeContexProvider.src.Utils.ReadFromJson(
				@"C:\Users\pfsar\OneDrive\Documents\GitHub\PrototypeContextProvider\PrototypeContexProvider\APIKeys.json"
			);

			var apiKey = "BD47FFBF33D2AB936499267B902C868C7311294D7399BDF266A75D888DA7648099F61288723D1146310A0BA9FB2FDD52A6D311C6140DE939D2945991846EF87A970D3D1912E391CB34C309F0AE083F31825379D5B29B";

			APIKeyManger.GetInstance().ReadIn(dAPIKeys);
			PolciyResouce.GetInstance().LoadDB();
			//await PolciyResouce.GetInstance().SaveDB();
		}

		public static void Main(string[] args)
		{
			SetupPCP().Wait();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
