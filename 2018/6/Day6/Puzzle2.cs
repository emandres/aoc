using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    public class Puzzle2
    {
        public void Execute()
        {

            var input = File.ReadAllLines("../../../input.txt");
            var points = input.Select(x => x.Split(",")).Select(x => new Point(int.Parse(x[0].Trim()), int.Parse(x[1].Trim()))).ToList();

            var minX = points.Min(a => a.X);
            var maxX = points.Max(a => a.X);
            var minY = points.Min(a => a.Y);
            var maxY = points.Max(a => a.Y);

            var distances = new Dictionary<Point, int>();
            for (int y = minY; y <= maxY; y++)
            for (int x = minX; x <= maxX; x++)
            {
                var currentPoint = new Point(x, y);
                distances[currentPoint] = points.Select(a => currentPoint - a).Sum();
            }

            Console.WriteLine(distances.Count(x => x.Value < 10000));
        }
    }
}