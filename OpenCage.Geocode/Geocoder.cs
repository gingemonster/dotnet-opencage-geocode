using System.Threading.Tasks;

namespace OpenCage.Geocode
{
    using System;
	using System.Globalization;
	using System.Net;
    using ServiceStack;

    public class Geocoder : IGeocoder
    {
        private const string Baseurl =
            "https://api.opencagedata.com/geocode/v1/json?q={0}&key={1}&language={2}";

        private readonly string key;

        /// <summary>
        /// </summary>
        /// <param name="key">Your opencagedata geocoder key https://geocoder.opencagedata.com</param>
        public Geocoder(string key)
        {
            this.key = key;
        }

        private void AddCommonOptionalParameters(ref string url, int limit, int minConfidence, bool noAnnotations, bool noDedupe, bool noRecord, bool abbrv, bool addRequest, bool roadInfo)
        {

            if (limit > 0)
            {
                url += "&limit=" + limit;
            }
            if (minConfidence > 0)
            {
                url += "&min_confidence=" + minConfidence;
            }
            if (noAnnotations)
            {
                url += "&no_annotations=1";
            }
            if (noDedupe)
            {
                url += "&no_dedupe=1";
            }
            if (noRecord)
            {
                url += "&no_record=1";
            }
            if (abbrv)
            {
                url += "&abbrv=1";
            }
            if (addRequest)
            {
                url += "&add_request=1";
            }
            if (roadInfo)
            {
                url += "&roadInfo=1";
            }
        }

        /// <summary>
        /// Forward geocoding given a textual location
        /// </summary>
        /// <param name="query">The query string to be geocoded; a placename or lat+long.</param>
        /// <param name="language">An IETF format language code (such as es for Spanish or pt-BR for Brazilian Portuguese).</param>
        /// <param name="countrycode">Restricts the results to the specified country or countries. The country code is a two letter code as defined by the ISO 3166-1 Alpha 2 standard.E.g.gb for the United Kingdom</param>
        /// <param name="bounds">Provides the geocoder with a hint to the region that the query resides in. This value will restrict the possible results to the supplied region. The bounds parameter should be specified as 4 coordinate points forming the south-west and north-east corners of a bounding box.</param>
        /// <param name="abbrv">When set to true we attempt to abbreviate and shorten the formatted string we return</param>
        /// <param name="limit">How many results should be returned. Default is 10. Maximum is 100.</param>
        /// <param name="minConfidence">An integer from 1-10. Only results with at least this confidence will be returned.</param>
        /// <param name="noAnnotations">When set to true results will not contain annotations.</param>
        /// <param name="noDedupe">	When set to true results will not be deduplicated.</param>
        /// <param name="noRecord">When set to true the query contents are not logged. Please use if you have concerns about privacy and want us to have no record of your query.</param>
        /// <param name="addRequest">When set to true the various request parameters are added to the response for ease of debugging.</param>
        /// <param name="roadInfo">When set to true the behaviour of the geocoder is changed to attempt to match the nearest road (as opposed to address).</param>
        /// <returns></returns>
        public GeocoderResponse Geocode(
            string query,
            string language = "en",
            string countrycode = null,
            Bounds bounds = null,
            bool abbrv = false,
            int limit = 10,
            int minConfidence = 0,
            bool noAnnotations = false,
            bool noDedupe = false,
            bool noRecord = false,
            bool addRequest = false,
            bool roadInfo = false)
        {
            var url = string.Format(Baseurl, WebUtility.UrlEncode(query), this.key, WebUtility.UrlEncode(language));

            if (bounds != null)
            {
                url += $"&bounds={bounds.NorthEast.Longitude}%2C{bounds.NorthEast.Latitude}%2C{bounds.SouthWest.Longitude}%2C{bounds.SouthWest.Latitude}";
            }
            if (!string.IsNullOrEmpty(countrycode))
            {
                url += "&countrycode=" + countrycode;
            }

            AddCommonOptionalParameters(ref url, limit, minConfidence, noAnnotations, noDedupe, noRecord, abbrv, addRequest, roadInfo);

            return GetResponse(url);
        }

        /// <summary>
        /// Forward geocoding given a textual location
        /// </summary>
        /// <param name="query">The query string to be geocoded; a placename or lat+long.</param>
        /// <param name="language">An IETF format language code (such as es for Spanish or pt-BR for Brazilian Portuguese).</param>
        /// <param name="countrycode">Restricts the results to the specified country or countries. The country code is a two letter code as defined by the ISO 3166-1 Alpha 2 standard.E.g.gb for the United Kingdom</param>
        /// <param name="bounds">Provides the geocoder with a hint to the region that the query resides in. This value will restrict the possible results to the supplied region. The bounds parameter should be specified as 4 coordinate points forming the south-west and north-east corners of a bounding box.</param>
        /// <param name="abbrv">When set to true we attempt to abbreviate and shorten the formatted string we return</param>
        /// <param name="limit">How many results should be returned. Default is 10. Maximum is 100.</param>
        /// <param name="minConfidence">An integer from 1-10. Only results with at least this confidence will be returned.</param>
        /// <param name="noAnnotations">When set to true results will not contain annotations.</param>
        /// <param name="noDedupe">	When set to true results will not be deduplicated.</param>
        /// <param name="noRecord">When set to true the query contents are not logged. Please use if you have concerns about privacy and want us to have no record of your query.</param>
        /// <param name="addRequest">When set to true the various request parameters are added to the response for ease of debugging.</param>
        /// <param name="roadInfo">When set to true the behaviour of the geocoder is changed to attempt to match the nearest road (as opposed to address).</param>
        /// <returns></returns>
        public async Task<GeocoderResponse> GeocodeAsync(
            string query,
            string language = "en",
            string countrycode = null,
            Bounds bounds = null,
            bool abbrv = false,
            int limit = 10,
            int minConfidence = 0,
            bool noAnnotations = false,
            bool noDedupe = false,
            bool noRecord = false,
            bool addRequest = false,
            bool roadInfo = false)
        {
            var url = string.Format(Baseurl, WebUtility.UrlEncode(query), this.key, WebUtility.UrlEncode(language));

            if (bounds != null)
            {
                url += $"&bounds={bounds.NorthEast.Longitude}%2C{bounds.NorthEast.Latitude}%2C{bounds.SouthWest.Longitude}%2C{bounds.SouthWest.Latitude}";
            }
            if (!string.IsNullOrEmpty(countrycode))
            {
                url += "&countrycode=" + countrycode;
            }

            AddCommonOptionalParameters(ref url, limit, minConfidence, noAnnotations, noDedupe, noRecord, abbrv, addRequest, roadInfo);

            return await GetResponseAsync(url);
        }

        /// <summary>
        /// Reverse geocoding given a latitude and longitude
        /// </summary>
        /// <param name="latitude">The latitude</param>
        /// <param name="longitude">The longitude</param>
        /// <param name="language">An IETF format language code (such as es for Spanish or pt-BR for Brazilian Portuguese).</param>
        /// <param name="abbrv">When set to true we attempt to abbreviate and shorten the formatted string we return</param>
        /// <param name="limit">How many results should be returned. Default is 10. Maximum is 100.</param>
        /// <param name="minConfidence">An integer from 1-10. Only results with at least this confidence will be returned.</param>
        /// <param name="noAnnotations">When set to true results will not contain annotations.</param>
        /// <param name="noDedupe">	When set to true results will not be deduplicated.</param>
        /// <param name="noRecord">When set to true the query contents are not logged. Please use if you have concerns about privacy and want us to have no record of your query.</param>
        /// <param name="addRequest">When set to true the various request parameters are added to the response for ease of debugging.</param>
        /// <param name="roadInfo">When set to true the behaviour of the geocoder is changed to attempt to match the nearest road (as opposed to address).</param>
        /// <returns></returns>
        public GeocoderResponse ReverseGeocode(
            double latitude,
            double longitude,
            string language = "en",
            bool abbrv = false,
            int limit = 10,
            int minConfidence = 0,
            bool noAnnotations = false,
            bool noDedupe = false,
            bool noRecord = false,
            bool addRequest = false,
            bool roadInfo = false
            )
        {
            var url = GetReverseGeocodeUrl(latitude, longitude, language, abbrv, limit, minConfidence, noAnnotations, noDedupe, noRecord, addRequest, roadInfo);
            return GetResponse(url);
        }

        /// <summary>
        /// Reverse geocoding given a latitude and longitude
        /// </summary>
        /// <param name="latitude">The latitude</param>
        /// <param name="longitude">The longitude</param>
        /// <param name="language">An IETF format language code (such as es for Spanish or pt-BR for Brazilian Portuguese).</param>
        /// <param name="abbrv">When set to true we attempt to abbreviate and shorten the formatted string we return</param>
        /// <param name="limit">How many results should be returned. Default is 10. Maximum is 100.</param>
        /// <param name="minConfidence">An integer from 1-10. Only results with at least this confidence will be returned.</param>
        /// <param name="noAnnotations">When set to true results will not contain annotations.</param>
        /// <param name="noDedupe">	When set to true results will not be deduplicated.</param>
        /// <param name="noRecord">When set to true the query contents are not logged. Please use if you have concerns about privacy and want us to have no record of your query.</param>
        /// <param name="addRequest">When set to true the various request parameters are added to the response for ease of debugging.</param>
        /// <param name="roadInfo">When set to true the behaviour of the geocoder is changed to attempt to match the nearest road (as opposed to address).</param>
        /// <returns></returns>
        public async Task<GeocoderResponse> ReverseGeocodeAsync(
            double latitude,
            double longitude,
            string language = "en",
            bool abbrv = false,
            int limit = 10,
            int minConfidence = 0,
            bool noAnnotations = false,
            bool noDedupe = false,
            bool noRecord = false,
            bool addRequest = false,
            bool roadInfo = false
        )
        {
            var url = GetReverseGeocodeUrl(latitude, longitude, language, abbrv, limit, minConfidence, noAnnotations, noDedupe, noRecord, addRequest, roadInfo);
            return await GetResponseAsync(url);
        }

        private string GetReverseGeocodeUrl(double latitude,
            double longitude,
            string language,
            bool abbrv,
            int limit,
            int minConfidence,
            bool noAnnotations,
            bool noDedupe,
            bool noRecord,
            bool addRequest,
            bool roadInfo)
        {
            var url = GetReverseGeocodeUrl(latitude, longitude, language);
            AddCommonOptionalParameters(ref url, limit, minConfidence, noAnnotations, noDedupe, noRecord, abbrv, addRequest, roadInfo);
            return url;
        }

        protected string GetReverseGeocodeUrl(double latitude, double longitude, string language)
        {
            return string.Format(Baseurl, ToNonScientificString(latitude) + "%2C" + ToNonScientificString(longitude), key, WebUtility.UrlEncode(language));
        }

        protected static string ToNonScientificString(double d)
        {
            var s = d.ToString(DoubleFormat, CultureInfo.InvariantCulture).TrimEnd('0');
            return s.Length == 0 ? "0.0" : s;
        }

        private static readonly string DoubleFormat = "0." + new string('#', 339);

        private async Task<GeocoderResponse> GetResponseAsync(string url)
        {
            try
            {
                var result = await url.GetJsonFromUrlAsync();
                return result.FromJson<GeocoderResponse>();
            }
            catch (WebException webex)
            {
                var gcr = ProcessExceptionGeocoderResponse(webex);

                if (gcr != null)
                {
                    return gcr;
                }

                throw;
            }
        }

        private static GeocoderResponse ProcessExceptionGeocoderResponse(WebException webex)

        {
            // check if error can be returned as a http status
            if (webex.Response is HttpWebResponse response)
            {

                var body = webex.GetResponseBody();
                try
                {
                    var gcr = body.FromJson<GeocoderResponse>();
                    switch ((int)response.StatusCode)
                    {
                        case 400:
                            gcr.Status.Message = "Invalid request (bad request; a required parameter is missing; invalid coordinates))";
                            break;
                        case 402:
                            gcr.Status.Message = "Valid request but quota exceeded (payment required)";
                            break;
                        case 403:
                            gcr.Status.Message = "Invalid or missing api key (forbidden)";
                            break;
                        case 429:
                            gcr.Status.Message = "Too many requests (too quickly, rate limiting)";
                            break;

                    }
                    return gcr;
                }
                catch (Exception ex)
                {
                    return new GeocoderResponse { Status = new RequestStatus { Code = (int)response.StatusCode, Message = ex.Message } };
                }
            }

            return null;
        }

        private GeocoderResponse GetResponse(string url)
        {
            try
            {
                var result = url.GetJsonFromUrl();
                return result.FromJson<GeocoderResponse>();
            }
            catch (WebException webex)
            {
                var gcr = ProcessExceptionGeocoderResponse(webex);

                if (gcr != null)
                {
                    return gcr;
                }

                throw;
            }
        }
    }
}
