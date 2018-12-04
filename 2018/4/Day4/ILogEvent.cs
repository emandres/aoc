namespace Day4
{
    public interface ILogEvent
    {
        void Execute(Schedule schedule);
    }
}