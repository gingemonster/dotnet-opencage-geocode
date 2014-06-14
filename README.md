OpenCage Data Geocoding Library for .Net 4.5
=======================

A [.NET 4.5](http://www.microsoft.com/net) library that uses [OpenCage Geocoder](http://geocoder.opencagedata.com/)
geocoder.

## Dependencies and Requirements

* .NET 4.5
* ServiceStack ver 3.9.71 (last version of non-commercial library)

## Usage (Geocoding)

Reference the library using Nuget https://www.nuget.org/packages/OpenCageGeocoder/

Create an instance of the geocoder library, passing a valid OpenCage Data Geocoder API key
as a parameter to the geocoder library's constructor:

```C#
var gc = new Geocoder("YOUR_KEY");
```

Pass a string containing the query or address to be geocoded to the library's `Geocode` method:

```C#
var result = gc.Geocode("82 Clerkenwell Road, London");
```

You will get a strongly typed GeocoderResponse object returned.

There are optional paramaters for language, country, and bounds see http://geocoder.opencagedata.com/api.html for explanations

Putting all of this together as a console app, a complete sample would look like this:


```C#
var gc = new Geocoder("YOUR_KEY");
var result = gc.Geocode("82 Clerkenwell Road, London");

result.PrintDump(); // ServiceStack human readable object dump to console
```

## Usage (Reverse Geocoding)

Reverse geocoding is almost identical but you pass in a latitude and longitude pair:


```C#
var gc = new Geocoder("YOUR_KEY");
var reserveresult = gc.ReverseGeocode(51.4277844, -0.3336517);
            
reserveresult.PrintDump(); // ServiceStack human readable object dump to console
```

There is an optional paramaters for language see http://geocoder.opencagedata.com/api.html for explanations
