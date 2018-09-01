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

			APIKeyManger.GetInstance().ReadIn(dAPIKeys);

			PolciyResouce.GetInstance().LoadDB();
			APIKeyChecker.GetInstance().LoadDB();
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
