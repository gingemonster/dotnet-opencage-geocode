namespace OpenCage.Geocode
{
    using System;
    using System.Net;

    using ServiceStack.Text;

    public class Geocoder : IGeocoder
    {
        private const string Baseurl =
            "https://api.opencagedata.com/geocode/v1/json?q={0}&key={1}&language={2}";

        private readonly string key;

        public Geocoder(string key)
        {
            this.key = key;
        }

        public GeocoderResponse Geocode(
            string query,
            string language = "en",
            string country = null,
            Bounds bounds = null)
        {
            var url = string.Format(Baseurl, query.UrlEncode(), this.key, language.UrlEncode());

            if (bounds != null)
            {
                url += "&bounds="
                       + new double[]
                             {
                                 bounds.NorthEast.Longitude, bounds.NorthEast.Latitude, bounds.SouthWest.Longitude,
                                 bounds.SouthWest.Latitude
                             }.Join().UrlEncode();
            }
            if (!string.IsNullOrEmpty(country))
            {
                url += "&country=" + country.UrlEncode();
            }

            return this.GetResponse(url);
        }

        public GeocoderResponse ReverseGeocode(double latitude, double longitude, string language = "en")
        {
            var url = string.Format(Baseurl, new double[] { latitude, longitude }.Join().UrlEncode(), this.key, language.UrlEncode());
            return this.GetResponse(url);
        }

        private GeocoderResponse GetResponse(string url)
        {
            string result = string.Empty;

            try
            {
                result = url.GetJsonFromUrl();
            }
            catch (WebException webex)
            {
                var response = webex.Response as HttpWebResponse;

                // check if error can be returned as a http status
                if (response != null)
                {
                    var body = webex.GetResponseBody();
                    try
                    {
                        return body.FromJson<GeocoderResponse>();
                    }
                    catch (Exception ex)
                    {

                        return new GeocoderResponse() { Status = new RequestStatus() { Code = (int)response.StatusCode, Message = ex.Message } };
                    }
                }
                throw;
            }

            return result.FromJson<GeocoderResponse>();
        }
    }
}
