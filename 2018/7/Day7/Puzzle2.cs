using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    public class Puzzle2
    {
        const int Weight = 60;

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

            var elves = new StepInProgress[5];
            
            var time = 0;

            while (orderedSteps.Any() || elves.Any(x => x != null))
            {
                for (int i = 0; i < elves.Length; i++)
                {
                    var elf = elves[i];

                    if (elf?.Done == true)
                    {
                        result.Add(elf.Step);

                        foreach (var dependentStep in enables[elf.Step])
                        {
                            if (dependsOn[dependentStep].All(x => result.Contains(x)))
                            {
                                orderedSteps.Add(dependentStep);
                            }
                        }

                        elves[i] = null;
                    }

                    if (elves[i] == null && orderedSteps.Any())
                    {
                        elves[i] = new StepInProgress(Pop(orderedSteps));
                    }
                }

                Log(time, elves);


                foreach (var elf in elves)
                {
                    elf?.DecrementTimeLeft();
                }

                time++;
            }

            Console.WriteLine(time);
        }

        void Log(int time, StepInProgress[] elves)
        {
            var message = $"{time,8}";
            for (var i = 0; i < elves.Length; i++)
            {
                var elf = elves[i];
                message += "\t" + (elf?.Step ?? "_");
            }

            Console.WriteLine(message);
        }

        public List<string> IndependentSteps(List<(string A, string B)> rules, ILookup<string, string> dependsOn)
        {

            return rules
                .SelectMany(x => new[] { x.A, x.B })
                .Distinct()
                .Where(x => !dependsOn.Contains(x))
                .ToList();
        }

        string Pop(SortedSet<string> set)
        {
            var result = set.First();
            set.Remove(result);
            return result;
        }

        public class StepInProgress
        {
            public StepInProgress(string step)
            {
                Step = step;
                TimeLeft = Weight + (step[0] - 'A' + 1);
            }

            public string Step { get; }
            public int TimeLeft { get; private set; }

            public bool Done => TimeLeft == 0;

            public void DecrementTimeLeft()
            {
                if (TimeLeft > 0)
                {
                    TimeLeft--;
                }
            }
        }
    }

}