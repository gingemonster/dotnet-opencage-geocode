namespace GeocoderDemo
{
    using System;

    using OpenCage.Geocode;

    using ServiceStack.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var gc = new Geocoder("YOURKEYHERE");
            // simplest example with no optional parameters
            var result = gc.Geocode("newcastle");

            result.PrintDump();

            //  example with lots of optional parameters
            var result2 = gc.Geocode("newcastle", countrycode: "gb", limit: 2, minConfidence: 6, language: "en", abbrv: true, noAnnotations:true, noRecord: true, addRequest: true);

            result2.PrintDump();

            var reserveresult = gc.ReverseGeocode(51.4277844, -0.3336517);

            reserveresult.PrintDump();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
