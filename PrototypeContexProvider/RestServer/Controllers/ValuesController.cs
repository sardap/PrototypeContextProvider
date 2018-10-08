﻿using System;
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
		private class AuthTokenEntry
		{
			public string ResID { get; set; }
			public long PolicyID { get; set; }
		}

		private class ShareTokkenEntry
		{
			public string ResID { get; set; }
			public string ApiKey { get; set; }
		}


		private static Random _random = new Random();

		//TODO Make this not bad 
		private static HashSet<long> _dataSharingPolcies = new HashSet<long>();
		private static Dictionary<string, AuthTokenEntry> _tokens = new Dictionary<string, AuthTokenEntry>();
		private static Dictionary<string, ShareTokkenEntry> _shareTokkens = new Dictionary<string, ShareTokkenEntry>();

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

		[HttpGet("shareTokken/check/{tokken}/{resouceID}", Name = "CheckShareTokken")]
		public ActionResult<bool> CheckShareTokken(string tokken, string resouceID)
		{
			var entry = _shareTokkens[tokken];

			return entry.ResID == resouceID;
		}

		[HttpGet("shareTokken/{apiKey}/{resouceID}", Name = "GetShareTokken")]
		public ActionResult<string> CreateShareTokken(string apiKey, string resouceID)
		{
			var pr = PolciyResouce.GetInstance();
			var apiKeyEntry = pr.OwnershipTable[apiKey];

			if (!apiKeyEntry.PolciesResocuce.ContainsKey(resouceID))
				return NotFound();

			var newTokken = Utils.CreateKey(10);
			_shareTokkens[newTokken] = new ShareTokkenEntry { ApiKey = apiKey, ResID = resouceID };
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
			_tokens.Add(newTokken, new AuthTokenEntry { ResID = resouceID, PolicyID = policyID });
			return newTokken;
		}

		[HttpGet("CheckPolicy/{apiKey}/{tokken}/{resID}/{ident}", Name = "CheckPolicy")]
		public ActionResult<int> GetById(string apiKey, string tokken, string resID, string ident)
		{
			var pr = PolciyResouce.GetInstance();
			var apiPR = pr.OwnershipTable[apiKey];
			var polciyRes = apiPR.PolciesResocuce[resID];

			if (!polciyRes.ContainsKey(tokken))
				return NotFound();

			var item = LoadFromFile(polciyRes[tokken]);
			if (item == null)
			{
				return NotFound();
			}

			if(item.DataConsumer.Value.ToLower() != ident.ToLower())
			{
				return 0;
			}

			var comContexResult = item.CompositeContex.Check();

			return comContexResult ? 1 : 0;
		}

		[HttpPost("{shareTokken}/{resouceID}")]
		public ActionResult<string> Create(string shareTokken, string resouceID, DataSharingPolciy polciy)
		{
			if(polciy.Id == 0)
			{
				//TODO make this not shit
				polciy.Id = Utils.LongRandom(_random);
			}

			var apiKey = _shareTokkens[shareTokken].ApiKey;
			var polciyResouce = PolciyResouce.GetInstance();

			if (polciyResouce.OwnershipTable.ContainsKey(apiKey))
			{
				var newTokken = Utils.CreateKey(10);

				var policyRes = polciyResouce.OwnershipTable[apiKey].PolciesResocuce;

				if(!policyRes.ContainsKey(resouceID))
				{
					policyRes.Add(resouceID, new Dictionary<string, long>());
				}

				policyRes[resouceID].Add(newTokken, polciy.Id);
				_dataSharingPolcies.Add(polciy.Id);
				ExportToFile(polciy.Id, polciy);
				polciyResouce.SaveDB();
				return newTokken;
			}

			return "FAILLED";
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
