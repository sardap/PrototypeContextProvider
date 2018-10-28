using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src.ContexOperators
{
	public class LocationInRange : IContexOperator
	{
		public int Radius { get; set; }

		public bool Resolve(dynamic a, dynamic b)
		{
			var target = (Location)a;
			var center = (Location)b;

			var distanceFromCenter = center.DistanceBetween(target);

			return distanceFromCenter < Radius;
		}

	}
}
