using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class DataSharingPolicySerilszer : DefaultContractResolver
	{
		public DataSharingPolicySerilszer()
		{
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

			// only serializer properties that start with the specified character
			properties = properties.Where(p => p.PropertyName != "CompositeContex").ToList();

			return properties;
		}
	}

}
