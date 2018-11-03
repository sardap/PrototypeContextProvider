using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrototypeContexProvider.src
{
    public class ResharingObligations
    {
		public bool CanShare { get; set; }
		public int Cardinality { get; set; }
		public int Recurring { get; set; }
	}
}
