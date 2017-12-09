using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCage.Geocode
{
    public class EchoRequest
    {
        public int Abbrv { get; set; }
        public int AddRequest { get; set; }
        public int AutoComplete { get; set; }
        public string CountryCode { get; set; }
        public string Format { get; set; }
        public string Key { get; set; }
        public string Language { get; set; }
        public int Limit { get; set; }
        public int MinConfidence { get; set; }
        public int NoAnnotations { get; set; }
        public int NoDedupe { get; set; }
        public int NoRecord { get; set; }
        public int OnlyNominatim { get; set; }
        public int Pretty { get; set; }
        public string Query { get; set; }
        public string Version { get; set; }
    }
}
