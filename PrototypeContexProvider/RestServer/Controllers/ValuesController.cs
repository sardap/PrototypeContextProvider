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
		private static Random _random = new Random();

		//TODO Make this not bad 
		private static Dictionary<long, DataSharingPolciy> _dataSharingPolciys = new Dictionary<long, DataSharingPolciy>();

		public static void SaveDB()
		{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			DataSharingPolicyParser.ExportListToFile(_dataSharingPolciys.Values.ToList(), "Polciy");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		}

		public static void LoadDB()
		{
			var polcies = DataSharingPolicyParser.ParseListFromFileAsync("Polciy").GetAwaiter().GetResult();

			polcies.ForEach(i => _dataSharingPolciys.Add(i.Id, i));
		}

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<DataSharingPolciy>> Get()
		{		
			return _dataSharingPolciys.Values;			
		}

		[HttpGet("{policyID}/{apiKey}", Name = "GetTodo")]
		public ActionResult<int> GetById(long policyID, string apiKey)
		{
			//TODO Bad need to put resouce place
			var resouceName = PolciyResouce.GetInstance().PolicyResouceMap.FirstOrDefault(i => i.Value == policyID).Key;

			if (!APIKeyChecker.GetInstance().Check(apiKey, resouceName))
				return NotFound();

			var oldPolicyID = policyID;
			policyID = PolciyResouce.GetInstance().PolicyResouceMap[resouceName];

			var item = _dataSharingPolciys[policyID];
			if (item == null)
			{
				return NotFound();
			}
			return item.CompositeContex.Evlaute() ? 1 : 0;
		}

		[HttpPost]
		public void Create(DataSharingPolciy item)
		{
			if(item.Id == 0)
			{
				item.Id = Utils.LongRandom(_random);
			}

			item.CompositeContex = item.JsonCompositeContex.ToCompositeContex();
			_dataSharingPolciys.Add(item.Id, item);
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
