using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    public class Puzzle1
    {
        public void Execute()
        {
            var numbers = new LinkedList<int>(File.ReadAllText("../../../input.txt").Split().Select(int.Parse));
            var tree = Tree.Parse(numbers);
            Console.WriteLine(SumMetadata(tree));
        }

        public int SumMetadata(Tree.Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Metadata.Sum() + node.Children.Select(SumMetadata).Sum();
        }
    }
}