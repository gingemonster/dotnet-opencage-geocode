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
			bool prettyPrint = false,
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
			AddCommonOptionalParameters(url,jsonp, limit, minConfidence, noAnnotations, noDedupe, noRecord, prettyPrint, abbrv, addRequest);

            return this.GetResponse(url);
        }
		
		private void AddCommonOptionalParameters(string url, bool jsonp, int limit, int minConfidence, bool noAnnotations, bool noDedupe, bool noRecord, bool prettyPrint, bool abbrv, bool addRequest){
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
			if(prettyPrint){
				url += "&pretty=1";
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
			bool prettyPrint = false,
			bool addRequest = false			
			)
        {
            var url = string.Format(Baseurl, latitude + "%2C" + longitude, this.key, WebUtility.UrlEncode(language));
			AddCommonOptionalParameters(url,jsonp, limit, minConfidence, noAnnotations, noDedupe, noRecord, prettyPrint, abbrv, addRequest);
			
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
						var message = "";
						switch((int)response.StatusCode){
							case 402: 
								message = "You have reached the daily free usage limit of 2,500 requests. Please visist https://geocoder.opencagedata.com/pricing for more details";
								break;
							case 403:
								message = "Invalid or banned api key";
                                break;
							default:
								message = ex.Message;
                                break;
							
						}
                        return new GeocoderResponse() { Status = new RequestStatus() { Code = (int)response.StatusCode, Message = message } };
                    }
                }
                throw;
            }

            return result.FromJson<GeocoderResponse>();
        }
    }
}
