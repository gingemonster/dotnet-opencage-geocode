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
            string countrycode = null,
            Bounds bounds = null,
			bool abbrv = false,
			bool jsonp = false,
			int limit = 10,
			int minConfidence = 0,
			bool noAnnotations = false,
			bool noDedupe = false,
			bool noRecord = false,
			bool addRequest = false)
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
			AddCommonOptionalParameters(ref url, jsonp, limit, minConfidence, noAnnotations, noDedupe, noRecord, abbrv, addRequest);

            return this.GetResponse(url);
        }
		
		private void AddCommonOptionalParameters(ref string url, bool jsonp, int limit, int minConfidence, bool noAnnotations, bool noDedupe, bool noRecord, bool abbrv, bool addRequest){
			if(jsonp){
				url += "&jsonp=1";
			}
			if(limit>0){
				url += "&limit=" + limit;
			}
			if(minConfidence>0){
				url += "&min_confidence=" + minConfidence;
			}
			if(noAnnotations){
				url += "&no_annotations=1";
			}
			if(noDedupe){
				url += "&no_dedupe=1";
			}	
			if(noRecord){
				url += "&no_record=1";
			}	
			if(abbrv){
				url += "&abbrv=1";
			}
			if(addRequest){
				url += "&add_request=1";
			}
		}

        public GeocoderResponse ReverseGeocode(
			double latitude, 
			double longitude, 
			string language = "en",
			bool abbrv = false,
			bool jsonp = false,
			int limit = 10,
			int minConfidence = 0,
			bool noAnnotations = false,
			bool noDedupe = false,
			bool noRecord = false,
			bool addRequest = false			
			)
        {
            var url = string.Format(Baseurl, latitude + "%2C" + longitude, this.key, WebUtility.UrlEncode(language));
			AddCommonOptionalParameters(ref url,jsonp, limit, minConfidence, noAnnotations, noDedupe, noRecord, abbrv, addRequest);
			
            return this.GetResponse(url);
        }

        private GeocoderResponse GetResponse(string url)
        {
            string result = string.Empty;

            try
            {
                result = url.GetJsonFromUrl();
                return result.FromJson<GeocoderResponse>();
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
                        return new GeocoderResponse() { Status = new RequestStatus() { Code = (int)response.StatusCode, Message = ex.Message } };
                    }
                }
                throw;
            }
        }
    }
}
