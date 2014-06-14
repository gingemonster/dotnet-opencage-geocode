namespace OpenCage.Geocode
{
    public class GeocoderResponse
    {
        public Location[] Results { get; set; }

        public RequestStatus Status { get; set; }
    }
}