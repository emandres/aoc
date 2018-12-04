using System;
using System.IO;
using System.Linq;

namespace Day4
{
    public class Puzzle1
    {
        public void Execute()
        {
            var lines = File.ReadAllLines("../../../input.txt");
            var events = new LogEventParser().Parse(lines).ToList();
            var schedule = new Schedule();
            foreach (var logEvent in events)
            {
                logEvent.Execute(schedule);
            }

            var sleepiestGuard = schedule.SleepiestGuard();
            Console.WriteLine(sleepiestGuard);
            var sleepiestMinute = schedule.SleepiestMinute(sleepiestGuard);
            Console.WriteLine(sleepiestMinute);
        }
    }
}