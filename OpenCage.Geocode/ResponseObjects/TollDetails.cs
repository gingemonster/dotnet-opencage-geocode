using System.Runtime.Serialization;

namespace OpenCage.Geocode.ResponseObjects
{
    public class TollDetails
    {
        [DataMember(Name = "excluded")]
        public string[] Excluded {  get; set; }
        [DataMember(Name = "included")]
        public string[] Included { get; set; }
        [DataMember(Name = "operator ")]
        public string Operator { get; set; }
    }
}