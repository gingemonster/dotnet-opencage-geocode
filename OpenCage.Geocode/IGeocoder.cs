﻿namespace OpenCage.Geocode
{
    public interface IGeocoder
    {
        GeocoderResponse Geocode(
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
            bool roadInfo = false
        );

        GeocoderResponse ReverseGeocode(
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
        );
    }
}
