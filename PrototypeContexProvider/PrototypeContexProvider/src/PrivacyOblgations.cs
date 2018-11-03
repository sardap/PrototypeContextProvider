using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class PrivacyOblgations
	{
		public string Purpose { get; set; }
		public string Granularity{get; set;}
		public string Anonymisation{get; set;}
		public string Notifaction{get; set;}
		public string Accounting{ get; set; }
	}
}
