namespace AsynchronousDemo
{
    using System.Collections.Generic;

    public interface IChronometer
    {
        string GetTime { get; }
        List<string> Laps { get; }

        void Start();
        void Stop();
        void Lap();
        void Reset();
    }
}
