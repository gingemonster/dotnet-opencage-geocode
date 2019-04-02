namespace OpenCage.Geocode
{
	public class Annotations
	{
		public DMS DMS { get; set; }
		public string MGRS { get; set; }
		public string Maidenhead { get; set; }
		public Mercator Mercator { get; set; }
		public OSGB OSGB { get; set; }
		public OSM OSM { get; set; }
		public int CallingCode { get; set; }
		public Currency Currency { get; set; }
		public string Flag { get; set; }
		public string Geohash { get; set; }
		public double Qibla { get; set; }
		public Sun Sun { get; set; }
		public Timezone Timezone { get; set; }
		public What3words What3words { get; set; }
		public string Wikidata { get; set; }
	}
}
