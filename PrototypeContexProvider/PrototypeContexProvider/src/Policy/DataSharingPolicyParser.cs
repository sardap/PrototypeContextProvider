using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeContexProvider.src
{
    public static class DataSharingPolicyParser
    {
		private static JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto
		};

		public static string ExportListToJsonStrings(List<DataSharingPolciy> dataSharingPolciys)
		{
			dataSharingPolciys.ForEach(i => i.JsonCompositeContex = i.CompositeContex.GenreateJsonVersion());

			string jsonString = JsonConvert.SerializeObject(dataSharingPolciys, typeof(List<DataSharingPolciy>), _settings);

			return jsonString;
		}

		public static async Task ExportListToFile(List<DataSharingPolciy> dataSharingPolciys, string fileName)
		{
			using (StreamWriter r = new StreamWriter(fileName))
			{
				await r.WriteAsync(ExportListToJsonStrings(dataSharingPolciys));
			}
		}

		public static string ExportToJsonString(DataSharingPolciy dataSharingPolciy)
		{
			if(dataSharingPolciy.JsonCompositeContex == null)
				dataSharingPolciy.JsonCompositeContex = dataSharingPolciy.CompositeContex.GenreateJsonVersion();

			string jsonString = JsonConvert.SerializeObject(dataSharingPolciy, typeof(DataSharingPolciy), _settings);

			return jsonString;
		}

		public static async Task ExportToJson(DataSharingPolciy dataSharingPolicy, string fileName)
		{
			using (StreamWriter r = new StreamWriter(fileName))
			{
				await r.WriteAsync(JToken.Parse(ExportToJsonString(dataSharingPolicy)).ToString(Formatting.Indented));
			}
		}

		public static async Task<List<DataSharingPolciy>> ParseListFromFileAsync(string fileName)
		{
			using (StreamReader r = new StreamReader(fileName))
			{
				string json = await r.ReadToEndAsync();
				return ParseListString(json);
			}
		}

		public static async Task<DataSharingPolciy> ParseFromFileAsync(string fileName)
		{
			using (StreamReader r = new StreamReader(fileName))
			{
				string json = await r.ReadToEndAsync();
				return ParseString(json);
			}
		}

		public static List<DataSharingPolciy> ParseListString(string json)
		{
			var dataSharingPolices = JsonConvert.DeserializeObject<List<DataSharingPolciy>>(json, new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto,
			});

			dataSharingPolices.ForEach(i => i.CompositeContex = i.JsonCompositeContex.ToCompositeContex());

			return dataSharingPolices;
		}

		public static DataSharingPolciy ParseString(string json)
		{
			var dataSharingPolicy = JsonConvert.DeserializeObject<DataSharingPolciy>(json, new JsonSerializerSettings
			{ 
				TypeNameHandling = TypeNameHandling.Auto,
			});


			dataSharingPolicy.CompositeContex = dataSharingPolicy.JsonCompositeContex.ToCompositeContex();

			return dataSharingPolicy;
			/*
			DataSharingPolicy dataSharingPolicy = new DataSharingPolicy();

			dynamic jsonDataSharingPolicy = jsonVersion.dataSharingPolicy;

			dataSharingPolicy.ID = jsonDataSharingPolicy.id;
			dataSharingPolicy.Author = jsonDataSharingPolicy.author;
			dataSharingPolicy.Proity = jsonDataSharingPolicy.prority;
			dataSharingPolicy.Decision = jsonDataSharingPolicy.decision;

			dataSharingPolicy.DataConsumer = new DataConsumer
			{
				Name = jsonDataSharingPolicy.dataConsumer.name,
				Value = jsonDataSharingPolicy.dataConsumer.value
			};

			dataSharingPolicy.CompositeContex = new CompositeContex();

			foreach (dynamic contex in jsonDataSharingPolicy.compositeContex)
			{
				dynamic provider;

				switch ((string)contex.provider)
				{
					case "DateTimeProvider":
						provider = new DateTimeProvider();
						break;

					default:
						throw new InvaildProviderExpection();
				}

				IContexOperator contexOperator;

				switch((string)contex.cOperator)
				{
					case "GTE":
						contexOperator = new ContexGreaterThanOrEqual();
						break;

					default:
						throw new InvaildContexOpreatorExpection();
				}

				Type test = contex.value.GetType();

				if (contex.value.Value.GetType() == typeof(long))
				{
					var temp = new Contex<long>
					{
						Name = contex.name,
						Operator = contexOperator,
						GivenValue = contex.value.Value,
						ContextProvider = provider
					};

					dataSharingPolicy.CompositeContex.Add(temp);
				}

			}

			return dataSharingPolicy;
			*/
		}
    }
}
