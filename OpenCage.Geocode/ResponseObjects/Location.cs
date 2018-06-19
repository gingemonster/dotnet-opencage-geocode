using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenCage.Geocode
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "Annotations")]
        public Annotations Annotations { get; set; }

        [DataMember(Name = "Formatted")]
        public string Formatted { get; set; }

        [DataMember(Name = "Components")]
        public Dictionary<string, string> ComponentsDictionary { get; set; }

        public AddressComponent Components
        {
            get
            {
                return new AddressComponent
                {
                    BusStop = ComponentsDictionary.GetValueOrDefault("bus_stop"),
                    City = ComponentsDictionary.GetValueOrDefault("city"),
                    Country = ComponentsDictionary.GetValueOrDefault("country"),
                    County = ComponentsDictionary.GetValueOrDefault("county"),
                    CountryCode = ComponentsDictionary.GetValueOrDefault("country_code"),
                    Postcode = ComponentsDictionary.GetValueOrDefault("postcode"),
                    Road = ComponentsDictionary.GetValueOrDefault("road"),
                    State = ComponentsDictionary.GetValueOrDefault("state"),
                    StateDistrict = ComponentsDictionary.GetValueOrDefault("state_district"),
                    Suburb = ComponentsDictionary.GetValueOrDefault("suburb"),
                    Type = ComponentsDictionary.GetValueOrDefault("_type")
                };
            }
        }

        [DataMember(Name = "Geometry")]
        public Point Geometry { get; set; }

        [DataMember(Name = "Bounds")]
        public Bounds Bounds { get; set; }

        [DataMember(Name = "Confidence")]
        public int Confidence { get; set; }
    }
}