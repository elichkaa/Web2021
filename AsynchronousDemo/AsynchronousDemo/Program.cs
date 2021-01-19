using System;

namespace AsynchronousDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseChronometer();
        }

        private static void UseChronometer()
        {
            var chronometer = new Chronometer();
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "lap":
                        chronometer.Lap();
                        break;
                    case "laps":
                        chronometer.AllLaps();
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "exit":
                        return;
                }
            }
        }
    }
}
