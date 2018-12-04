using System;

namespace Day4
{
    public class FallAsleep : ILogEvent
    {
        readonly string guardId;
        readonly DateTime timestamp;

        public FallAsleep(string guardId, DateTime timestamp)
        {
            this.guardId = guardId;
            this.timestamp = timestamp;
        }

        public void Execute(Schedule schedule)
        {
            var guardSchedule = schedule[guardId];
            var minute = timestamp.Minute;

            for (int i = minute; i < 60; i++)
            {
                guardSchedule[i]++;
            }
        }
    }
}