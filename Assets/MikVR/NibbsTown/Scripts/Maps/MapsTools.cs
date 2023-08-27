using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace NibbsTown
{
    internal class MapsTools : MonoBehaviour
    {
        internal static GPSPosition CalculateCenterPosition(Station[] stations)
        {

            List<GPSPosition> positions = stations.Select(s => s.Pos).ToList();

            // Convert to Cartesian coordinates
            var cartesianLocations = positions.Select(loc =>
            {
                double lat = loc.Latitude * Math.PI / 180.0;
                double lon = loc.Longitude * Math.PI / 180.0;
                double x = Math.Cos(lat) * Math.Cos(lon);
                double y = Math.Cos(lat) * Math.Sin(lon);
                double z = Math.Sin(lat);

                return new Tuple<double, double, double>(x, y, z);
            }).ToList();

            // Calculate average x, y, z coordinates
            double avgX = cartesianLocations.Average(loc => loc.Item1);
            double avgY = cartesianLocations.Average(loc => loc.Item2);
            double avgZ = cartesianLocations.Average(loc => loc.Item3);

            // Convert average x, y, z back to latitude and longitude
            double lon = Math.Atan2(avgY, avgX);
            double hyp = Math.Sqrt(avgX * avgX + avgY * avgY);
            double lat = Math.Atan2(avgZ, hyp);

            // Convert to degrees
            double centerLat = lat * 180.0 / Math.PI;
            double centerLong = lon * 180.0 / Math.PI;

            Console.WriteLine($"Center Point: {centerLong}, {centerLat}");
            return new GPSPosition(centerLong, centerLat);
        }

        internal static float GetZoomLevel(Station[] stations)
        {
            double distance = GetMaxDistance(stations);

            const double EARTH_DIAMETER = 40075000; // Earth's diameter in meters
            const double MIN_DISTANCE = 50; // Minimum distance in meters
            const float MAX_ZOOM = 20; // Corresponds to MIN_DISTANCE
            const float MIN_ZOOM = 4; // Corresponds to EARTH_DIAMETER

            // Clamp distance to the valid range
            distance = Math.Max(distance, MIN_DISTANCE);
            distance = Math.Min(distance, EARTH_DIAMETER);

            // Map distance to zoom exponentially
            double ratio = Math.Log(EARTH_DIAMETER / distance) / Math.Log(EARTH_DIAMETER / MIN_DISTANCE);
            Debug.Log("DISTANCE IN METERS: " + distance);

            float zoom = (float)(MIN_ZOOM + ratio * (MAX_ZOOM - MIN_ZOOM));
            Debug.Log("ZOOM: " + zoom);





            //double lat1 = 47.05435524989106;
            //double lon1 = 15.868105011408847;
            //double lat2 = 47.05480067686285;
            //double lon2 = 15.868132504051324;

            //double distanceMETERS = CalculateDistance(lat1, lon1, lat2, lon2);
            //UnityEngine.Debug.Log("DIST MET: " + distanceMETERS);


            return zoom;


        }

        private static double GetMaxDistance(Station[] stations)
        {
            //var stationList = stations.ToList();
            double maxDistance = 0;

            for (int i = 0; i < stations.Length; i++)
            {
                for (int j = i + 1; j < stations.Length; j++)
                {
                    double distance = CalculateDistance(
                        stations[i].Pos.Latitude,
                        stations[i].Pos.Longitude,
                        stations[j].Pos.Latitude,
                        stations[j].Pos.Longitude);

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                    }
                }
            }

            return maxDistance;
        }

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371e3; // Earth radius in meters

            // Convert degrees to radians
            double lat1Rad = ToRadians(lat1);
            double lon1Rad = ToRadians(lon1);
            double lat2Rad = ToRadians(lat2);
            double lon2Rad = ToRadians(lon2);

            // Calculate differences
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Haversine formula
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
