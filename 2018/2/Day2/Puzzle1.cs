using System;
using System.IO;
using System.Linq;

namespace Day2
{
    public class Puzzle1
    {
        public void Execute()
        {
            var ids = File.ReadAllLines("input.txt");

            var charGroups = ids.Select(x => x.ToCharArray().GroupBy(y => y)).ToList();
            var twoLetterGroups = charGroups.Count(x => x.Any(y => y.Count() == 2));
            var threeLetterGroups = charGroups.Count(x => x.Any(y => y.Count() == 3));
            Console.WriteLine(twoLetterGroups * threeLetterGroups);
        }
    }
}