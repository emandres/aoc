using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    public class Puzzle2
    {
        public void Execute()
        {
            var numbers = new LinkedList<int>(File.ReadAllText("../../../input.txt").Split().Select(int.Parse));
            var tree = Tree.Parse(numbers);
            Console.WriteLine(GetValue(tree));
        }

        public int GetValue(Tree.Node node)
        {
            if (node == null)
            {
                return 0;
            }

            if (!node.Children.Any())
            {
                return node.Metadata.Sum();
            }

            var sum = 0;
            foreach (var m in node.Metadata)
            {
                if (m <= node.Children.Count)
                {
                    sum += GetValue(node.Children[m - 1]);
                }
            }

            return sum;
        }
    }
}