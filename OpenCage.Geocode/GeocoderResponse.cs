namespace OpenCage.Geocode
{
    using ServiceStack.Text;
    public class GeocoderResponse
    {
        public Location[] Results { get; set; }

        public RequestStatus Status { get; set; }
    }
}