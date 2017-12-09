namespace OpenCage.Geocode
{
    using System;
    using System.Net;
    using ServiceStack.Text;
    using ServiceStack;

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
            var url = string.Format(Baseurl, WebUtility.UrlEncode(query), this.key, WebUtility.UrlEncode(language));

            if (bounds != null)
            {
                url += $"&bounds={bounds.NorthEast.Longitude}%2C{bounds.NorthEast.Latitude}%2C{bounds.SouthWest.Longitude}%2C{bounds.SouthWest.Latitude}";
            }
            if (!string.IsNullOrEmpty(country))
            {
                url += "&country=" + WebUtility.UrlEncode(country);
            }

            return this.GetResponse(url);
        }

        public GeocoderResponse ReverseGeocode(double latitude, double longitude, string language = "en")
        {
            var url = string.Format(Baseurl, latitude + "%2C" + longitude, this.key, WebUtility.UrlEncode(language));
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
