namespace OpenCage.Geocode
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Point
    {
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

		public Point() { }

		public Point(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}
    }
}