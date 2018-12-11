using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            var game = new Game(476, 71657);
            game.Play();
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }

    public class Game
    {
        readonly int playerCount;
        readonly int maxMarble;
        readonly Ring ring;

        public Game(int playerCount, int maxMarble)
        {
            this.playerCount = playerCount;
            this.maxMarble = maxMarble;
            ring = new Ring();
        }

        public void Play()
        {
            var scores = new Dictionary<int, long>();
            for (int i = 1; i <= maxMarble; i++)
            {
                if (i % 23 == 0)
                {
                    var score = i + ring.Remove();
                    var player = (i - 1) % playerCount;
                    if (scores.TryGetValue(player, out var currentScore))
                    {
                        scores[player] = currentScore + score;
                    }
                    else
                    {
                        scores[player] = score;
                    }
                }
                else
                {
                    ring.Add(i);
                }
            }

            Console.WriteLine(scores.Values.Max());
        }
    }

    public class Ring
    {
        public class Node
        {
            public Node(long value)
            {
                Value = value;
            }

            public long Value { get; }
            public Node Clockwise { get; set; }
            public Node CounterClockwise { get; set; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        Node current;
        readonly Node first;

        public Ring()
        {
            first = new Node(0);
            first.Clockwise = first;
            first.CounterClockwise = first;

            current = first;
        }

        public Node Add(int value)
        {
            var newNode = new Node(value);
            var left = Clockwise(current, 1);
            var right = left.Clockwise;
            newNode.CounterClockwise = left;
            newNode.Clockwise = right;
            left.Clockwise = newNode;
            right.CounterClockwise = newNode;

            current = newNode;

            return newNode;
        }

        public long Remove()
        {
            var node = CounterClockwise(current, 7);
            var left = node.CounterClockwise;
            var right = node.Clockwise;
            left.Clockwise = right;
            right.CounterClockwise = left;
            current = right;

            return node.Value;
        }

        public Node Clockwise(Node node, int n)
        {
            var result = node;
            for (int i = 0; i < n; i++)
            {
                result = result.Clockwise;
            }

            return result;
        }
        public Node CounterClockwise(Node node, int n)
        {
            var result = node;
            for (int i = 0; i < n; i++)
            {
                result = result.CounterClockwise;
            }

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var node = first;

            do
            {
                if (node == current)
                {
                    sb.Append($"[{node.Value}] ");
                }
                else
                {
                    sb.Append($"{node.Value} ");
                }

                node = node.Clockwise;
            }
            while (node != first);

            return sb.ToString();
        }
    }
}
