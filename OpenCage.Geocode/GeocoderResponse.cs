namespace OpenCage.Geocode
{
    using ServiceStack.Text;
    public class GeocoderResponse
    {
        public string Documentation { get; set; }
        public License[] Licenses { get; set; }
        public Rate Rate { get; set; }
        public Location[] Results { get; set; }
        public RequestStatus Status { get; set; }
        public Timestamp Timestamp { get; set; }
        public int TotalResults { get; set; }
        public EchoRequest Request { get; set; }
    }
}
