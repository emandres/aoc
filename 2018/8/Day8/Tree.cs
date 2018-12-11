using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    public class Tree
    {
        public static Node Parse(LinkedList<int> input)
        {
            if (!input.Any())
            {
                return null;
            }

            var numChildren = input.First.Value;
            input.RemoveFirst();
            var numMetadata = input.First.Value;
            input.RemoveFirst();

            var children = new List<Node>();
            for (int i = 0; i < numChildren; i++)
            {
                children.Add(Parse(input));
            }

            var metadata = new List<int>();
            for (int i = 0; i < numMetadata; i++)
            {
                metadata.Add(input.First.Value);
                input.RemoveFirst();
            }

            return new Node
            {
                Children = children,
                Metadata = metadata
            };
        }

        public class Node
        {
            public List<Node> Children { get; set; }
            public List<int> Metadata { get; set; }
        }
    }
}