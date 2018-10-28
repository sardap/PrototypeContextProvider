using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypeContexProvider.src
{
	public class Location
	{
		public double Lat { get; set; }
		public double Lon { get; set; }

		public Location()
		{
		}

		public Location(double lat, double lon)
		{
			Lat = lat;
			Lon = lon;
		}

		public Location(string str) : this(double.Parse(str.Split('_')[0]), double.Parse(str.Split('_')[1]))
		{
		}

		private static double Rad2deg(double rad)
		{
			return rad * (180 / Math.PI);
		}

		private static double Deg2rad(double deg)
		{
			return deg * (Math.PI / 180);
		}

		// In meters
		internal double DistanceBetween(Location b)
		{
			Location a = this;

			var lon1 = a.Lon;
			var lat1 = a.Lat;
			var lon2 = b.Lon;
			var lat2 = b.Lat;

			double theta, dist;
			theta = lon1 - lon2;
			dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
			dist = Math.Acos(dist);
			dist = Rad2deg(dist);
			dist = dist * 60 * 1.1515;
			dist = dist * 1.609344;
			dist = dist * 1000;
			return dist;
		}

		public override string ToString()
		{
			return Lat.ToString() + "_" + Lon.ToString();
		}
	}
}
