using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestServer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RestServer.Controllers;

namespace RestServer
{
	public class Startup
	{

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DataSharingPolciyContext>(opt =>
				opt.UseInMemoryDatabase("TodoList"));
			services.AddMvc()
					.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
			});

			ValuesController.LoadDB();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
		}
	}
}
