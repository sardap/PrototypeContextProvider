using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeContexProvider.src
{
    public static class DataSharingPolicyParser
    {
		public static async Task ExportToJson(DataSharingPolicy dataSharingPolicy, string fileName)
		{
			using (StreamWriter r = new StreamWriter(fileName))
			{
				await r.WriteAsync(JsonConvert.SerializeObject(dataSharingPolicy));
			}
		}

		public static async Task<DataSharingPolicy> ParseFromFileAsync(string fileName)
		{
			using (StreamReader r = new StreamReader(fileName))
			{
				string json = await r.ReadToEndAsync();
				return ParseString(json);
			}
		}

		public static DataSharingPolicy ParseString(string json)
		{
			var dataSharingPolicy = JsonConvert.DeserializeObject<DataSharingPolicy>(json);

			foreach (dynamic contex in dataSharingPolicy.CompositeContex.Contexies)
			{

			}


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
