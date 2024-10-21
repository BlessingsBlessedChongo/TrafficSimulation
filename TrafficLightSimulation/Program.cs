using System;
using System.Threading;
using System.Timers;

namespace TrafficLightSimulation
{
    class TrafficLight
    {
        public enum Light { Red, Yellow, Green }
        private Light currentLight;
        private int redDuration = 5000; //5 seconds
        private int yellowDuration = 2000; //2 seconds
        private int greenDuration = 5000; //5 seconds
        private int trafficVolume = 10;
        private System.Timers.Timer timer;
        private Random random = new Random();
        private int currentTime = 0;
        public TrafficLight()
        {
            currentLight = Light.Red;
            timer = new System.Timers.Timer();
            timer.Interval = 1000; //1 second
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            timer.Start();
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //update traffic volume
            trafficVolume += random.Next(-5, 5);
            //Adjust traffic light duration based on traffic volume
            if(trafficVolume > 20)
            {
                redDuration = 7000;
                greenDuration = 3000;

            }else if (trafficVolume < 5)
            {
                redDuration = 3000;
                greenDuration = 5000;
            }
            else
            {
                redDuration = 5000;
                greenDuration = 5000;
            }
            //traffic light simulation
            currentTime++;
            switch (currentLight)
            {
                case Light.Red:
                    Console.WriteLine("Red Light");
                    if(currentTime >= redDuration / 1000)
                    {
                        currentLight = Light.Green;
                        currentTime = 0;
                    }
                    break;
                case Light.Green:
                    Console.WriteLine("Green Light");
                    if (currentTime >= greenDuration / 1000)
                    {
                        currentLight = Light.Yellow;
                        currentTime = 0;
                    }
                    break;
                case Light.Yellow:
                    Console.WriteLine("Yellow Light");
                    if(currentTime >= yellowDuration / 1000)
                    {
                        currentLight = Light.Red;
                        currentTime = 0;
                    }
                    break;
            }
            //display traffic volume
            Console.WriteLine($"Traffic Volume: {trafficVolume}");
        }
        public void Simulate()
        {
            timer.Start();
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void Pause()
        {
            timer.Stop();
            Console.WriteLine("Simulation Paused");
        }
        public void Resume()
        {
            timer.Start();
            Console.WriteLine("Simulation Resumed");
        }

        public void Exit()
        {
            timer.Stop();
            Environment.Exit(0);
        }
    }
    class Pedestrian
    {
        private System.Timers.Timer pedestrianTimer;
        private int pedestrianDuration = 5000;

        public Pedestrian()
        {
            pedestrianTimer = new System.Timers.Timer();
            pedestrianTimer.Interval = 1000;
            pedestrianTimer.Elapsed += new System.Timers.ElapsedEventHandler(PedestrianTimer_Elapsed);
            pedestrianTimer.Start();

        }
        private void PedestrianTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (e.SignalTime.Millisecond % pedestrianDuration == 0)
            {
                Console.WriteLine("Pedestrian Crossing");
            }
        }
        /*public void Start()
        {
            pedestrianTimer.Start();
        }*/
        public void stop()
        {
            pedestrianTimer.Stop();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TrafficLight trafficLight = new TrafficLight();
            Pedestrian pedestrian = new Pedestrian();

            Console.WriteLine("Traffic Light Simulation");
            Console.WriteLine("Press 'P' to pause, 'R' to resume, 'E' to exit");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'P':
                        trafficLight.Pause();
                        break;
                    case 'R':
                        trafficLight.Resume();
                        break;
                    case 'E':
                        trafficLight.Exit();
                        break;
                }
            }
        }
    }
}



