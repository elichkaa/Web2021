
namespace AsynchronousDemo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Chronometer : IChronometer
    {
        private Stopwatch sw = new Stopwatch();
        private bool isRunning = false;
        private List<string> laps = new List<string>();

        public string GetTime => @$"{sw.Elapsed.Minutes:D2}:{sw.Elapsed.Seconds:D2}:{sw.Elapsed.Milliseconds:D4}";

        public List<string> Laps => this.laps;

        public void Start()
        {
            this.sw.Start();
            this.isRunning = true;
        }

        public void Stop()
        {
            this.sw.Stop();
            this.isRunning = false;
        }

        public void Lap()
        {
            if (isRunning)
            {
                Console.WriteLine(this.GetTime);
                this.Laps.Add(GetTime);
            }
            else
            {
                Console.WriteLine("Chronometer is not running.");
            }
        }

        public void Reset()
        {
            this.sw.Reset();
            this.laps.Clear();
        }

        public void AllLaps()
        {
            for (int i = 0; i < this.laps.Count; i++)
            {
                Console.WriteLine($"{i}. {laps[i]}");
            }
        }
    }
}
