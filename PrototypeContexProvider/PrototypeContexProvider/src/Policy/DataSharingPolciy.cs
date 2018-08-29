using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class DataSharingPolciy
	{
		[Key]
		public long Id { get; set; }
		public string Author { get; set; }
		public int Proity { get; set; }
		public string Decision { get; set; } 
		public DataConsumer DataConsumer { get; set; }
		[JsonIgnore]
		public CompositeContex CompositeContex { get; set; }
		public CompositeContexJson JsonCompositeContex { get; set; }
		public PrivacyOblgations PrivacyOblgations { get; set; }
		public ResharingObligations ResharingObligations { get; set; }
	}
}
