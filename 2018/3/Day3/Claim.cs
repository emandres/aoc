using System;
using System.Text.RegularExpressions;

namespace Day3
{
    public class Claim
    {
        public string Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public void Mark(int[,] fabric)
        {
            for (int r = 0; r < Height; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    fabric[Top + r, Left + c]++;
                }
            }
        }

        public bool Overlaps(int[,] fabric)
        {
            for (int r = 0; r < Height; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    if (fabric[Top + r, Left + c] != 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Claim Parse(string line)
        {
            var regex = new Regex(@"#(?<Id>\d+) @ (?<Left>\d+),(?<Top>\d+): (?<Width>\d+)x(?<Height>\d+)");
            var match = regex.Match(line);
            return new Claim
            {
                Id = match.Groups["Id"].Value,
                Top = Convert.ToInt32(match.Groups["Top"].Value),
                Left = Convert.ToInt32(match.Groups["Left"].Value),
                Width = Convert.ToInt32(match.Groups["Width"].Value),
                Height = Convert.ToInt32(match.Groups["Height"].Value),
            };
        }
    }
}