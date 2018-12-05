using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    public class Puzzle1
    {
        public void Execute()
        {
            var units = File.ReadAllText("../../../input.txt");
            Console.WriteLine(units.Length);
            var stack = new Stack<char>();
            foreach (var unit in units)
            {
                TryReacting(unit, stack);
            }

            Console.WriteLine(stack.Count);
        }

        void TryReacting(char unit, Stack<char> stack)
        {
            if (stack.TryPeek(out char top) && AreReactive(unit, top))
            {
                stack.Pop();
            }
            else
            {
                stack.Push(unit);
            }
        }

        bool AreReactive(char a, char b)
        {
            return a != b && char.ToLower(a) == char.ToLower(b);
        }
    }
}