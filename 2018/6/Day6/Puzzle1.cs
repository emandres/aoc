using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    public class Puzzle1
    {
        public void Execute()
        {
            var input = File.ReadAllLines("../../../input.txt");
            var points = input.Select(x => x.Split(",")).Select(x => new Point(int.Parse(x[0].Trim()), int.Parse(x[1].Trim()))).ToList();

            var minX = points.Min(a => a.X);
            var maxX = points.Max(a => a.X);
            var minY = points.Min(a => a.Y);
            var maxY = points.Max(a => a.Y);

            var closest = new Dictionary<Point, Point>();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var currentPoint = new Point(x, y);
                    closest[currentPoint] = FindClosestPoint(currentPoint, points);
                }
            }

            var closestLookup = FilterInfinite(closest, minX, minY, maxX, maxY);
            var mostHits =  closestLookup
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .First();

            foreach (var kv in closestLookup)
            {
                Console.WriteLine($"{kv.Key} {kv.Count()}");
            }

            Console.WriteLine($"{mostHits} has the largest area");
            Console.WriteLine($"It is the closest to {closestLookup[mostHits].Count()}");
        }

        public ILookup<Point, Point> FilterInfinite(Dictionary<Point, Point> closest, int minX, int minY, int maxX, int maxY)
        {
            var infinitePoints = new HashSet<Point>();
            for (int x = minX; x <= maxX; x++)
            {
                infinitePoints.Add(closest[new Point(x, minY)]);
                infinitePoints.Add(closest[new Point(x, maxY)]);
            }

            for (int y = minY; y <= maxY; y++)
            {

                infinitePoints.Add(closest[new Point(minX, y)]);
                infinitePoints.Add(closest[new Point(maxX, y)]);
            }

            return closest.Where(x => !infinitePoints.Contains(x.Value)).ToLookup(x => x.Value, x => x.Key);
        }

        public Point FindClosestPoint(Point p, IList<Point> points)
        {
            var distances = points.GroupBy(a => a - p)
                .ToDictionary(a => a.Key, a => a.ToList());

            var minDist = distances.Min(a => a.Key);
            if (distances[minDist].Count > 1)
            {
                return new Point(-1, -1);
            }
            
            return distances[minDist].First();
        }
    }
}