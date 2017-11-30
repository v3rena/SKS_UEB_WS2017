using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Helpers
{
	public static class DistanceCalculator
	{
		/// <summary>
		/// Gets the distance between two points.
		/// </summary>
		/// <param name="latitude1">The latitude1.</param>
		/// <param name="longitude1">The longitude1.</param>
		/// <param name="latitude2">The latitude2.</param>
		/// <param name="longitude2">The longitude2.</param>
		/// <returns></returns>
		public static double GetDistanceBetweenTwoPoints(double latitude1, double longitude1, double latitude2, double longitude2)
		{
			double theta = longitude1 - longitude2;
			double distance = Math.Sin(ConvertDecimalDegreesToRadians(latitude1)) *
							  Math.Sin(ConvertDecimalDegreesToRadians(latitude2)) +
							  Math.Cos(ConvertDecimalDegreesToRadians(latitude1)) *
							  Math.Cos(ConvertDecimalDegreesToRadians(latitude2)) *
							  Math.Cos(ConvertDecimalDegreesToRadians(theta));

			distance = Math.Acos(distance);
			distance = ConvertRadiansToDecimalDegrees(distance);
			distance = distance * 60 * 1.1515;
			// convert to kilometers
			return distance * 1.609344 * 0.001;
		}

		/// <summary>
		/// Converts the decimal degrees to radians.
		/// </summary>
		/// <param name="degree">The degree.</param>
		/// <returns></returns>
		private static double ConvertDecimalDegreesToRadians(double degree)
		{
			return (degree * Math.PI / 180.0);
		}

		/// <summary>
		/// Converts the radians to decimal degrees.
		/// </summary>
		/// <param name="radian">The radian.</param>
		/// <returns></returns>
		private static double ConvertRadiansToDecimalDegrees(double radian)
		{
			return (radian / Math.PI * 180.0);
		}
	}
}
