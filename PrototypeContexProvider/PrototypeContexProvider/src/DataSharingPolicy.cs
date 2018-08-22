using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class DataSharingPolicy
    {
		public string ID;
		public string Author;
		public int Proity;
		public string Decision;
		public DataConsumer DataConsumer;
		public CompositeContex CompositeContex;
		public dynamic PrivacyOblgations;
		public ResharingObligations ResharingObligations;
	}
}