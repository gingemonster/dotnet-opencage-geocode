namespace OpenCage.Geocode
{
    public interface IGeocoder
    {
        GeocoderResponse Geocode(string query, string language = "en", string country = null, Bounds bounds = null);

        GeocoderResponse ReverseGeocode(double latitude, double longitude, string language = "en");
    }
}
