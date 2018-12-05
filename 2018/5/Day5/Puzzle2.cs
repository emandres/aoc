using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    public class Puzzle2
    {

        public void Execute()
        {
            var units = File.ReadAllText("../../../input.txt");
            Console.WriteLine(units.Length);
            int min = Int32.MaxValue;
            for (char bannedType = 'a'; bannedType <= 'z'; bannedType++)
            {
                var stack = new Stack<char>();
                foreach (var unit in units)
                {
                    TryReacting(unit, stack, bannedType);
                }

                if (stack.Count < min)
                {
                    min = stack.Count;
                }
            }

            Console.WriteLine(min);
        }

        void TryReacting(char unit, Stack<char> stack, char bannedType)
        {
            if (char.ToLower(unit) == bannedType)
            {
                return;
            }

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