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
		public static async Task Setup()
		{
			//Loaded in API keys for contex providers
			dynamic dAPIKeys = await PrototypeContexProvider.src.Utils.ReadFromJson(
				@"C:\Users\pfsar\OneDrive\Documents\GitHub\PrototypeContextProvider\PrototypeContexProvider\APIKeys.json"
			);

			APIKeyManger.GetInstance().ReadIn(dAPIKeys);
			PolciyResouce.GetInstance().LoadDB();
			//await PolciyResouce.GetInstance().SaveDB();
		}

		public static void Main(string[] args)
		{
			Setup().Wait();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
