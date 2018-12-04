using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pluralsight.Maybe;
using static Pluralsight.Maybe.Maybe;

namespace Day4
{
    public class LogEventParser
    {
        public IEnumerable<ILogEvent> Parse(IEnumerable<string> lines)
        {
            var timestampedMessages = lines.Select(ParseTimestampedMessage).OrderBy(msg => msg.Timestamp).ToList();
            string currentGuardId = null;
            foreach (var message in timestampedMessages)
            {
                var guardId = TryBeginShift(message.Message);
                if (guardId.HasValue)
                {
                    currentGuardId = guardId.Value;
                }
                else if (IsWakeUp(message.Message))
                {
                    yield return new WakeUp(currentGuardId, message.Timestamp);
                }
                else if (IsFallAsleep(message.Message))
                {
                    yield return new FallAsleep(currentGuardId, message.Timestamp);
                }
            }
        }

        (DateTime Timestamp, string Message) ParseTimestampedMessage(string line)
        {
            var regex = new Regex(@"\[(?<timestamp>.*)\] (?<message>.*)");
            var match = regex.Match(line);
            return (Convert.ToDateTime(match.Groups["timestamp"].Value), match.Groups["message"].Value);
        }

        Maybe<string> TryBeginShift(string line)
        {
            var matches = Regex.Matches(line, @"Guard #(?<id>\d+)");
            if (matches.Count == 1)
            {
                return Some(matches[0].Groups["id"].Value);
            }

            return Maybe<string>.None;
        }

        bool IsWakeUp(string line)
        {
            return line.Contains("wakes up");
        }

        bool IsFallAsleep(string line)
        {
            return line.Contains("falls asleep");
        }
    }
}