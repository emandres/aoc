using System;
using System.IO;
using System.Linq;

namespace Day1
{
    public class Puzzle1
    {
        public void Execute()
        {
            var freq = File.ReadAllLines("input.txt")
                .Select(x => int.Parse(x))
                .Sum();

            Console.WriteLine(freq);
        }
    }
}