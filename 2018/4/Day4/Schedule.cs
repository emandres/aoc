using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    public class Schedule
    {
        Dictionary<string, int[]> schedule;

        public Schedule()
        {
            schedule = new Dictionary<string, int[]>();
        }

        public int[] this[string guardId]
        {
            get
            {
                if (!schedule.ContainsKey(guardId))
                {
                    schedule.Add(guardId, new int[60]);
                }

                return schedule[guardId];
            }
        }

        public string SleepiestGuard() => 
            schedule
                .OrderByDescending(x => x.Value.Sum())
                .Select(x => x.Key)
                .First();

        public int SleepiestMinute(string guardId)
        {
            return schedule[guardId]
                .Select((x, index) => (minute: index, value: x))
                .OrderByDescending(x => x.value)
                .Select(x => x.minute)
                .First();
        }

        public (string GuardsId, int Minute) SleepiestOverall()
        {
            return schedule
                .Select(x => (GuardId: x.Key, Minute: SleepiestMinute(x.Key)))
                .Select(x => (GuardId: x.GuardId, Minute: x.Minute, Value: schedule[x.GuardId][x.Minute]))
                .OrderByDescending(x => x.Value)
                .Select(x => (GuardId: x.GuardId, Minute: x.Minute))
                .First();
        }
    }
}