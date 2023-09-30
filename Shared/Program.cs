namespace GeocoderDemo
{
    using System;
    using System.Threading.Tasks;
    using OpenCage.Geocode;

    using ServiceStack.Text;

    public class Program
    {
        private static Geocoder _geocoder;

        public static void Main(string[] args)
        {
            _geocoder = new Geocoder("YOURKEYHERE");

            // simplest example with no optional parameters
            var result = _geocoder.Geocode("newcastle");
            result.PrintDump();

            //  example with lots of optional parameters
            var result2 = _geocoder.Geocode("newcastle", countrycode: "gb", limit: 2, minConfidence: 6, language: "en", abbrv: true, noAnnotations:true, noRecord: true, addRequest: true);
            result2.PrintDump();

            var reserveresult = _geocoder.ReverseGeocode(51.4277844, -0.3336517);
            reserveresult.PrintDump();

            ExecuteQueriesAsync().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        public static async Task ExecuteQueriesAsync()
        {
            // simplest example with no optional parameters
            var result = await _geocoder.GeocodeAsync("newcastle");
            result.PrintDump();

            var reserveresult = await _geocoder.ReverseGeocodeAsync(51.4277844, -0.3336517);
            reserveresult.PrintDump();
        }
    }
}
