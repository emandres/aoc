using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Puzzle1
    {
        public void Execute()
        {
            var lines = File.ReadAllLines("../../../input.txt").Select(ParseLine).ToList();
            Resolve(lines);
        }

        public (string A, string B) ParseLine(string line)
        {
            var match = Regex.Match(line, @"Step (?<a>[A-Z]) must be finished before step (?<b>[A-Z]) can begin.");
            return (match.Groups["a"].Value, match.Groups["b"].Value);
        }

        public void Resolve(List<(string A, string B)> rules)
        {
            var dependsOn = rules.ToLookup(x => x.B, x => x.A);
            var enables = rules.ToLookup(x => x.A, x => x.B);

            var independent = IndependentSteps(rules, dependsOn);

            var orderedSteps = new SortedSet<string>(independent);
            var result = new List<string>();
            while (orderedSteps.Any())
            {
                var step = orderedSteps.First();
                orderedSteps.Remove(step);
                result.Add(step);

                foreach (var dependentStep in enables[step])
                {
                    if (dependsOn[dependentStep].All(x => result.Contains(x)))
                    {
                        orderedSteps.Add(dependentStep);
                    }
                }
            }

            Console.WriteLine(string.Join("", result));
        }

        public List<string> IndependentSteps(List<(string A, string B)> rules, ILookup<string, string> dependsOn)
        {

            return rules
                .SelectMany(x => new[] { x.A, x.B })
                .Distinct()
                .Where(x => !dependsOn.Contains(x))
                .ToList();
        }
    }
}