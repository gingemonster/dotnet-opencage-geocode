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
            var result = gc.Geocode("newcastle", countrycode: "GBR");

            result.PrintDump();

            var reserveresult = gc.ReverseGeocode(51.4277844, -0.3336517);

            reserveresult.PrintDump();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
