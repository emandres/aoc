using System;
using System.IO;

namespace Day2
{
    public class Puzzle2
    {
        public void Execute()
        {
            var ids = File.ReadAllLines("input.txt");
            for (int i = 0; i < ids.Length; i++)
            {
                for (int j = i + 1; j < ids.Length; j++)
                {
                    if (AreMatch(ids[i], ids[j]))
                    {
                        Console.WriteLine(ids[i]);
                        Console.WriteLine(ids[j]);
                        return;
                    }
                }
            }
        }

        public bool AreMatch(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            var differences = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    differences++;
                    if (differences > 1)
                    {
                        return false;
                    }
                }
            }

            return differences == 1;
        }
    }
}