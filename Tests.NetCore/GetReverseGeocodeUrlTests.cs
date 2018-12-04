using NUnit.Framework;
using OpenCage.Geocode;

namespace Tests.NetCore
{
    public class GetReverseGeocodeUrlTests
    {
        private TestGeocoder geocoder;

        [SetUp]
        public void SetUp()
        {
            geocoder = new TestGeocoder("the-key");
        }

        private class TestGeocoder : Geocoder
        {
            public string GetReverseGeocodeUrlPublic(double latitude, double longitude, string language)
            {
                return GetReverseGeocodeUrl(latitude, longitude, language);
            }

            public static string ToNonScientificStringPublic(double d)
            {
                return ToNonScientificString(d);
            }

            public TestGeocoder(string key) : base(key)
            {
            }
        }

        [Test]
        public void WhenNearZero_ExpectCorrectUrl()
        {
            // Longitude of 0.000009 is converted to 9E-06 using Invariant ToString, but we need 0.000009
            Assert.AreEqual("https://api.opencagedata.com/geocode/v1/json?q=57.231%2C0.000009&key=the-key&language=en", geocoder.GetReverseGeocodeUrlPublic(57.231d, 0.000009d, "en"));
        }

        [Test]
        public void WhenNearZero_ExpectCorrectToString()
        {
            // Longitude of 0.000009 is converted to 9E-06 using Invariant ToString, but we need 0.000009
            Assert.AreEqual("0.000009", TestGeocoder.ToNonScientificStringPublic(0.000009d));
        }

        [Test]
        public void WhenZero_ExpectCorrectToString()
        {
            Assert.AreEqual("0.0", TestGeocoder.ToNonScientificStringPublic(0.0d));
        }
    }
}