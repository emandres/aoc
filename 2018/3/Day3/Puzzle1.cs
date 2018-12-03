using System;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Puzzle1
    {
        public void Execute()
        {
            var lines = File.ReadAllLines("input.txt");
            var claims = lines.Select(Claim.Parse).ToList();
            var fabric = new int[2000, 2000];

            foreach (var claim in claims)
            {
                claim.Mark(fabric);
            }

            int count = 0;
            for (int r = 0; r < fabric.GetLength(0); r++)
            {
                for (int c = 0; c < fabric.GetLength(1); c++)
                {
                    if (fabric[r, c] > 1)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }
    }
}