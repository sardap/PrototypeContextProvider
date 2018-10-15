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
	public class WatchController : ControllerBase
	{
		private static int _lastValue;

		// GET api/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			return _lastValue.ToString();
		}

		[HttpGet("setValue/{newValue}", Name = "SetWatchValue")]
		public ActionResult<bool> CheckShareTokken(string newValue)
		{
			try
			{
				Console.WriteLine("ADDING NEW VAULE {0}", newValue);
				_lastValue = int.Parse(newValue);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
