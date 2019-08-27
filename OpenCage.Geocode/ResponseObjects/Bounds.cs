using System;
using System.Globalization;
using System.Linq;

namespace OpenCage.Geocode
{
    public class Bounds
    {
        public Point SouthWest { get; set; }

        public Point NorthEast { get; set; }

		public Bounds() { }

		/// <summary>
		/// Initializes Bounds object from bounds-finder string
		/// https://opencagedata.com/bounds-finder
		/// </summary>
		/// <param name="raw">String from bounds-finder containing min lon, min lat, max lon, max lat</param>
		public Bounds(string raw)
		{
			if (String.IsNullOrEmpty(raw))
				throw new ArgumentException("String parameter can't be null or empty!", nameof(raw));

			var values = raw.Split(',')
							.Select(c => double.Parse(c, CultureInfo.InvariantCulture))
							.ToArray();

			if (values.Length < 4 || values.Length > 4)
				throw new ArgumentException("String contained more or less than 4 values", nameof(raw));

			NorthEast = new Point(values[3], values[2]);
			SouthWest = new Point(values[1], values[0]);
		}
	}
}