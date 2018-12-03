using System;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Puzzle2
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

            Console.WriteLine(claims.First(x => !x.Overlaps(fabric)).Id);
        }
    }
}