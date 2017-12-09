namespace OpenCage.Geocode
{
    public class Location
    {
        public Annotations Annotations { get; set; }

        public string Formatted { get; set; }

        public AddressComponent Components { get; set; }

        public Point Geometry { get; set; }

        public Bounds Bounds { get; set; }

        public int Confidence { get; set; }
    }
}