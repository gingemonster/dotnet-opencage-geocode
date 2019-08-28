using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenCage.Geocode;

namespace Tests.NetCore
{
	public class BoundsTests
	{
		[Test]
		public void ParseBoundsFromString()
		{
			Bounds bounds = new Bounds("-102.87186,6.79417,36.10382,59.03702");

			Assert.AreEqual(59.03702, bounds.NorthEast.Latitude);
			Assert.AreEqual(36.10382, bounds.NorthEast.Longitude);
			Assert.AreEqual(6.79417, bounds.SouthWest.Latitude);
			Assert.AreEqual(-102.87186, bounds.SouthWest.Longitude);
		}

		[Test]
		public void ThrowExceptionOnMalformedString()
		{
			var exception = Assert.Catch<ArgumentException>(() => new Bounds("-102,87186.6,79417.36,10382.59,03702"));
			Assert.IsTrue(exception.Message.StartsWith("String contained more or less than 4 values"));
		}

		[Test]
		public void ThrowExceptionOnNullOrEmpty()
		{
			String[] values = new String[] { null, String.Empty };
			foreach (var value in values)
			{
				var exception = Assert.Catch<ArgumentException>(() => new Bounds(value));
				Assert.IsTrue(exception.Message.StartsWith("String parameter can't be null or empty!"));
			}
		}
	}
}
