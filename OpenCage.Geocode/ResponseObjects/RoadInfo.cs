using System.Runtime.Serialization;

namespace OpenCage.Geocode.ResponseObjects
{
    public class RoadInfo
    {
        [DataMember(Name = "drive_on")]
        public string DriveOn { get; set; }

        [DataMember(Name = "speed_in")]
        public string SpeedIn { get; set; }

        [DataMember(Name = "road")]
        public string Road { get; set; }

        [DataMember(Name = "road_type")]
        public string RoadType { get; set; }

        [DataMember(Name = "road_reference")]
        public string RoadReference { get; set; }

        [DataMember(Name = "road_reference_intl")]
        public string RoadReferenceInternational { get; set; }

        [DataMember(Name = "lanes")]
        public int? Lanes { get; set; }

        [DataMember(Name = "maxheight")]
        public string MaxHeight { get; set; }

        [DataMember(Name = "maxspeed")]
        public int? MaxSpeed { get; set; }

        [DataMember(Name = "maxweight")]
        public decimal? MaxWeight { get; set; }

        [DataMember(Name = "maxwidth")]
        public decimal? MaxWidth { get; set; }

        [DataMember(Name = "oneway")]
        public string Oneway { get; set; }

        [DataMember(Name = "surface")]
        public string Surface { get; set; }

        [DataMember(Name = "toll")]
        public string Toll { get; set; }

        [DataMember(Name = "toll_details")]
        public TollDetails TollDetails { get; set; }

        [DataMember(Name = "width")]
        public decimal? Width { get; set; }
    }
}
