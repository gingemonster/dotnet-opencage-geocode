using System.Runtime.Serialization;

namespace OpenCage.Geocode
{
    [DataContract]
    public class AddressComponent
    {
        [DataMember(Name = "_type")]
        public string Type { get; set; }

        [DataMember(Name = "Country")]
        public string Country { get; set; }

        [DataMember(Name = "StateDistrict")]
        public string StateDistrict { get; set; }

        [DataMember(Name = "CountryCode")]
        public string CountryCode { get; set; }

        [DataMember(Name = "State")]
        public string State { get; set; }

        [DataMember(Name = "Suburb")]
        public string Suburb { get; set; }

        [DataMember(Name = "City")]
        public string City { get; set; }

        [DataMember(Name = "BusStop")]
        public string BusStop { get; set; }

        [DataMember(Name = "County")]
        public string County { get; set; }

        [DataMember(Name = "Road")]
        public string Road { get; set; }

        [DataMember(Name = "Postcode")]
        public string Postcode { get; set; }
    }
}