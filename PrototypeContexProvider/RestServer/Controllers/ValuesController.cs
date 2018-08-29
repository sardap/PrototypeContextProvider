using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrototypeContexProvider.src;
using RestServer.Models;

namespace RestServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		//TODO Make this not bad 
		private static List<DataSharingPolciy> _dataSharingPolciys = new List<DataSharingPolciy>();

		public static void SaveDB()
		{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			DataSharingPolicyParser.ExportListToFile(_dataSharingPolciys, "Polciy");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		}

		public static void LoadDB()
		{
			_dataSharingPolciys = DataSharingPolicyParser.ParseListFromFileAsync("Polciy").GetAwaiter().GetResult();
		}

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<DataSharingPolciy>> Get()
		{		
			return _dataSharingPolciys.ToList();			
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		[HttpPost]
		public void Create(DataSharingPolciy item)
		{
			item.CompositeContex = item.JsonCompositeContex.ToCompositeContex();
			_dataSharingPolciys.Add(item);
			SaveDB();
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
