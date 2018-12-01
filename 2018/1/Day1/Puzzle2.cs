using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    public class Puzzle2
    {
        public void Execute()
        {
            var changes = File.ReadAllLines("input.txt").Select(int.Parse).ToList();
            var visited = new HashSet<int>();
            var i = 0;
            var freq = 0;
            while (true)
            {
                freq += changes[i];
                if (visited.Contains(freq))
                {
                    Console.WriteLine(freq);
                    break;
                }

                visited.Add(freq);
                i = (i + 1) % changes.Count;
            }
        }
    }
}