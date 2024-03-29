﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrototypeContexProvider.src;
using RestServer.Models;

namespace RestServer.Controllers
{
	/// <summary>
	/// AM api endpoints
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private class SecTokenEntry
		{
			public string ResID { get; set; }
			public long PolicyID { get; set; }
		}

		private class AuthTokenEntry
		{
			public string ResID { get; set; }
			public bool Result { get; set; }
		}

		private class ShareTokkenEntry
		{
			public string ResID { get; set; }
			public string ApiKey { get; set; }
		}

		private class ConCheckEntry
		{
			public long PolicyID;
			private Dictionary<Contex, TimeSpan> _updateIntervals = new Dictionary<Contex, TimeSpan>();
			private Dictionary<Contex, bool> _results = new Dictionary<Contex, bool>();

			public ConCheckEntry(long policyID, DataSharingPolciy polciy)
			{
				PolicyID = policyID;
				
				foreach(var entry in polciy.JsonCompositeContex.Conteiexs)
				{
					if(entry.Contex.Interval > 0)
					{
						_updateIntervals.Add(entry.Contex, DateTime.Now.TimeOfDay);
						_results.Add(entry.Contex, false);
					}
				}
			}

			public bool Resolve()
			{
				var policy = LoadFromFile(PolicyID);

				foreach (var key in _updateIntervals.Keys.ToArray())
				{
					if(DateTime.Now.TimeOfDay > _updateIntervals[key])
					{
						_updateIntervals[key] = DateTime.Now.TimeOfDay + new TimeSpan(0, 0, 0, 0, (int)key.Interval);
						_results[key] = key.Check();
					}
				}

				return _results.All(i => i.Value);
			}

			public long GetLowestInteveral()
			{
				var min = _results.Keys.Min(i => i.Interval);
				return min;
			}
		}

		private static Random _random = new Random();

		//TODO Make this not bad 
		private static HashSet<long> _dataSharingPolcies = new HashSet<long>();

		private static Dictionary<string, SecTokenEntry> _secTokken = new Dictionary<string, SecTokenEntry>();
		private static Dictionary<string, AuthTokenEntry> _authTokken = new Dictionary<string, AuthTokenEntry>();
		private static Dictionary<string, ShareTokkenEntry> _shareTokkens = new Dictionary<string, ShareTokkenEntry>();
		private static ConcurrentDictionary<string, ConCheckEntry> _conCheckTable = new ConcurrentDictionary<string, ConCheckEntry>();

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

		private static DataSharingPolciy LoadFromFile(long id)
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

		[HttpGet("CheckAuthTokken/{tokken}/{resID}", Name = "CheckAuthTokken")]
		public ActionResult<int> CheckAuthTokken(string tokken, string resID)
		{
			if (!_authTokken.ContainsKey(tokken))
				return 0;

			if (_authTokken[tokken].ResID != resID)
				return 0;

			var vaildAuth = _authTokken[tokken].Result;

			if(!vaildAuth)
			{
				return 0;
			}

			return 1;
		}

		[HttpGet("CheckSecTokken/{tokken}/{ident}", Name = "CheckSecTokken")]
		public ActionResult<string> CheckSecTokken(string tokken, string ident)
		{
			var secTokkenEntry = _secTokken[tokken];
			var item = LoadFromFile(secTokkenEntry.PolicyID);

			if (item == null)
			{
				return NotFound();
			}

			var comContexResult = item.Check(ident.ToLower());
			var authTokken = Utils.CreateKey(10);

			if (item.JsonCompositeContex.Conteiexs.Any(i => i.Contex.Interval > 0))
			{
				_conCheckTable.TryAdd(authTokken, new ConCheckEntry(secTokkenEntry.PolicyID, item));
			}

			_authTokken.Add(authTokken, new AuthTokenEntry() { ResID = _secTokken[tokken].ResID, Result = comContexResult });

			return authTokken;
		}

		[HttpPost("{shareTokken}/{resouceID}")]
		public ActionResult<string> Create(string shareTokken, string resouceID, DataSharingPolciy polciy)
		{

			if (polciy.Id == null)
			{
				//TODO make this not bad
				polciy.Id = Utils.LongRandom(_random);
			}

			if (polciy.Interval == null)
			{
				polciy.Interval = -1;
			}

			if (!polciy.Vaild())
			{
				return "ERROR:Invaild Json";
			}

			var apiKey = _shareTokkens[shareTokken].ApiKey;
			var polciyResouce = PolciyResouce.GetInstance();

			if (polciyResouce.OwnershipTable.ContainsKey(apiKey))
			{
				var newTokken = Utils.CreateKey(10);

				var id = (long)polciy.Id;

				_secTokken.Add(newTokken, new SecTokenEntry() { PolicyID = id, ResID = resouceID });

				ExportToFile(id, polciy);

				return newTokken;
			}

			return "FAILLED";
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

		[HttpGet("CheckCon/{authTokken}", Name = "CheckCon")]
		public int CheckCon(string authTokken)
		{
			var entry = _conCheckTable[authTokken];

			return entry.Resolve() ? 1 : 0;
		}

		[HttpGet("CheckCon/GetInterval/{authTokken}", Name = "GetInterval")]
		public int GetInterval(string authTokken)
		{
			var entry = _conCheckTable[authTokken];
			var lowest = entry.GetLowestInteveral();
			return (int)lowest;
		}
	}
}
