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
		private class TokenEntry
		{
			public string ResID { get; set; }
			public long PolicyID { get; set; }
		}

		private static Random _random = new Random();

		//TODO Make this not bad 
		private static HashSet<long> _dataSharingPolcies = new HashSet<long>();
		private static Dictionary<string, TokenEntry> _tokens = new Dictionary<string, TokenEntry>();
		private static Dictionary<string, string> _shareTokkens = new Dictionary<string, string>();

		public static async Task ExportToFile(long id, DataSharingPolciy polciy)
		{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			DataSharingPolicyParser.ExportToJson(polciy, "polcies\\policy" + id.ToString() + ".json");
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
			var filePath = "polcies\\policy" + id.ToString() + ".json";
			return DataSharingPolicyParser.ParseFromFileAsync(filePath).GetAwaiter().GetResult();
		}

		// GET api/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			return PolciyResouce.GetInstance().GenrateAndAddAPIKey();
		}

		[HttpGet("shareTokken/{tokken}/{resouceID}", Name = "CheckTokken")]
		public ActionResult<bool> CheckShareTokken(string tokken, string resouceID)
		{
			return _shareTokkens[tokken] == resouceID;
		}

		[HttpGet("shareTokken/{apiKey}/{resouceID}/{policyID}", Name = "GetTokken")]
		public ActionResult<string> CreateShareTokken(string apiKey, string resouceID, long policyID)
		{
			var pr = PolciyResouce.GetInstance();
			var apiKeyEntry = pr.OwnershipTable[apiKey];

			if (!apiKeyEntry.PolciesResocuce.ContainsKey(resouceID))
				return NotFound();

			var newTokken = Utils.CreateKey(10);
			_shareTokkens[newTokken] = resouceID;
			return newTokken;
		}



		[HttpGet("checkTokken/{tokken}/{resouceID}", Name = "CheckTokken")]
		public ActionResult<int> CheckTokken(string tokken, string resouceID)
		{
			return _tokens[tokken].ResID == resouceID ? 1 : 0;
		}

		[HttpGet("GetTokken/{apiKey}/{resouceID}/{policyID}", Name = "GetTokken")]
		public ActionResult<string> GetTokken(string apiKey, string resouceID, long policyID)
		{
			var pr = PolciyResouce.GetInstance();
			var apiKeyEntry = pr.OwnershipTable[apiKey];

			if (!apiKeyEntry.PolciesResocuce.ContainsKey(resouceID))
				return NotFound();

			var newTokken = Utils.CreateKey(10);
			_tokens.Add(newTokken, new TokenEntry { ResID = resouceID, PolicyID = policyID });
			return newTokken;
		}


		[HttpGet("{apiKey}/{resouceID}", Name = "GetTodo")]
		public ActionResult<int> GetById(string apiKey, string resouceID)
		{
			var pr = PolciyResouce.GetInstance();
			var apiKeyEntry = pr.OwnershipTable[apiKey];

			if (!apiKeyEntry.PolciesResocuce.ContainsKey(resouceID))
				return NotFound();

			var item = LoadFromFile(apiKeyEntry.PolciesResocuce[resouceID]);
			if (item == null)
			{
				return NotFound();
			}

			return item.CompositeContex.Check() ? 1 : 0;
		}

		[HttpGet("{apiKey}/{resouceID}/{ident}", Name = "GetResultWithName")]
		public ActionResult<bool> GetById(string apiKey, string resouceID, string ident)
		{
			var pr = PolciyResouce.GetInstance();
			var apiKeyEntry = pr.OwnershipTable[apiKey];

			if (!apiKeyEntry.PolciesResocuce.ContainsKey(resouceID))
				return NotFound();

			var item = LoadFromFile(apiKeyEntry.PolciesResocuce[resouceID]);
			if (item == null)
			{
				return NotFound();
			}

			if(item.DataConsumer.Value.ToLower() != ident.ToLower())
			{
				return false;
			}

			return item.CompositeContex.Check();
		}

		[HttpPost("{apiKey}/{resouceID}")]
		public ActionResult<int> Create(string apiKey, string resouceID, DataSharingPolciy polciy)
		{
			if(polciy.Id == 0)
			{
				//TODO make this not shit
				polciy.Id = Utils.LongRandom(_random);
			}

			var polciyResouce = PolciyResouce.GetInstance();

			if (polciyResouce.OwnershipTable.ContainsKey(apiKey))
			{
				polciyResouce.OwnershipTable[apiKey].PolciesResocuce.Add(resouceID, polciy.Id);
				_dataSharingPolcies.Add(polciy.Id);
				ExportToFile(polciy.Id, polciy);
				polciyResouce.SaveDB();
				return 1;
			}

			return 0;
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
