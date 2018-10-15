using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class DataSharingPolciy
	{
		[Key]
		public long? Id { get; set; }
		public string Author { get; set; }
		public int? Proity { get; set; }
		public int? Interval { get; set; }
		public string Decision { get; set; } 
		public DataConsumer DataConsumer { get; set; }
		[JsonIgnore]
		public CompositeContex CompositeContex { get; set; }
		public CompositeContexJson JsonCompositeContex { get; set; }
		public PrivacyOblgations PrivacyOblgations { get; set; }
		public ResharingObligations ResharingObligations { get; set; }

		public bool Vaild()
		{
			// Gets every property and puts them in a list
			var properties = GetType().GetProperties().ToList();
			// property Should be null
			properties.RemoveAt(5);

			// Foreach property get the value and make sure it is not null
			return properties.All(i => i.GetValue(this) != null);
		}
	}
}
