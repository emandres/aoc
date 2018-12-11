using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ImageMagick;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            new Puzzle1().Execute();
        }
    }

    public class Puzzle1
    {
        public void Execute()
        {
            var lines = File.ReadAllLines("../../../input.txt");
            var points = lines.Select(Point.Parse).ToList();
            var display = new Display(points);


            for (int i = 10227; i < 10228; i++)
            {
                display.At(i);
            }
        }
    }

    public class Display
    {
        readonly List<Point> points;
        const int GraphSize = 500;
        const int ShiftAmount = GraphSize / 2;

        public Display(List<Point> points)
        {
            this.points = points;
        }

        public void At(int seconds)
        {
            using (var image = new MagickImage(new MagickColor("#000000"), GraphSize, GraphSize))
            {
                image.Format = MagickFormat.Png;
                var d = new Drawables();
                d.FillColor(MagickColors.Blue);

                bool any = false;

                foreach (var point in points)
                {
                    int x = Shift(point.X + point.Vx * seconds);
                    int y = Shift(point.Y + point.Vy * seconds);

                    if (x >= 0 && y >= 0 && x <= GraphSize && y <= GraphSize)
                    {
                        any = true;
                       d.Rectangle(x, y, x + 1, y + 1);
                    }
                }

                if (any)
                {
                    image.Draw(d);
                    image.Write($@"C:\dev\aoc\2018\10\output\{seconds}.png");
                }
            }
        }
        

        public int Shift(int n)
        {
            return n + ShiftAmount;
        }
    }

    public class Point  
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }

        public static Point Parse(string line)
        {
            var regex = new Regex(@"position=<\s*(?<x>-?\d+)\s*,\s*(?<y>-?\d+)\s*> velocity=<\s*(?<vx>-?\d+)\s*,\s*(?<vy>-?\d+)");
            var match = regex.Match(line);
            return new Point
            {
                X = int.Parse(match.Groups["x"].Value),
                Y = int.Parse(match.Groups["y"].Value),
                Vx = int.Parse(match.Groups["vx"].Value),
                Vy = int.Parse(match.Groups["vy"].Value),
            };
        }
    }

}
