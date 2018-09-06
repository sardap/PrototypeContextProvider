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
		private static HashSet<long> _dataSharingPolcies = new HashSet<long>();

		public static void ExportToFile(long id, DataSharingPolciy polciy)
		{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			DataSharingPolicyParser.ExportToJson(polciy, "policy" + id.ToString() + ".json");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		}

		public static void LoadDB()
		{
			foreach (string file in Directory.EnumerateFiles("polcies", "*", SearchOption.AllDirectories))
			{
				string fileName = file.Split(new string[] { "\\" }, StringSplitOptions.None)[1];
				if (fileName.Contains("policy"))
				{
					string numString = fileName.Substring(6, ((fileName.Length - 1) - 10));

					_dataSharingPolcies.Add(long.Parse(numString));
				}
			}

		}

		private DataSharingPolciy LoadFromFile(long id)
		{
			return DataSharingPolicyParser.ParseFromFileAsync("polcies\\policy" + id.ToString() + ".json").GetAwaiter().GetResult();
		}

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<DataSharingPolciy>> Get()
		{
			var result = new List<DataSharingPolciy>();
			_dataSharingPolcies.ToList().ForEach(i => result.Add(LoadFromFile(i)));
			return result;			
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

			var item = LoadFromFile(policyID);
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
				//TODO make this not shit
				item.Id = Utils.LongRandom(_random);
			}

			item.CompositeContex = item.JsonCompositeContex.ToCompositeContex();
			ExportToFile(item.Id, item);
			_dataSharingPolcies.Add(item.Id);
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
