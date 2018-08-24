using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class DataSharingPolciy
	{
		public string ID;
		public string Author;
		public int Proity;
		public string Decision;
		public DataConsumer DataConsumer;
		[JsonIgnore]
		public CompositeContex CompositeContex;
		public CompositeContexJson JsonCompositeContex;
		public PrivacyOblgations PrivacyOblgations;
		public ResharingObligations ResharingObligations;
	}
}
