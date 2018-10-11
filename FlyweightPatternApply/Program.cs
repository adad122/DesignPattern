using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlyweightPatternApply
{
    public interface IParticle
    {
        int StartLifeTime { get; set; }
        int Duration { get; set; }
        string Name { get; set; }
        void Run();
        string GetParticleType();

        void SetProperty(string name, int startLifeTime, int duration);

        bool TimeOut();
    }

    public class Particle : IParticle
    {
        public int StartLifeTime { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public Particle(string name, int startLifeTime, int duration)
        {
            Name = name;
            StartLifeTime = startLifeTime;
            Duration = duration;
        }

        public bool TimeOut()
        {
            return Duration != -1 && StartLifeTime > Duration;
        }

        public void Run()
        {
            StartLifeTime += 1;
        }

        public string GetParticleType()
        {
            return Name;
        }

        public void SetProperty(string name, int startLifeTime, int duration)
        {
            Name = name;
            StartLifeTime = startLifeTime;
            Duration = duration;
        }
    }

    public class Snow : Particle
    {
        public Snow(int startLifeTime, int duration)
            : base("Snow", startLifeTime, duration)
        {
        }
    }

    public class Rain : Particle
    {
        public Rain(int startLifeTime, int duration)
            : base("Rain", startLifeTime, duration)
        {
        }
    }

    public class ParticleSystem
    {
        private static Dictionary<string, Queue<IParticle>> _particles = new Dictionary<string, Queue<IParticle>>();

        public static IParticle GetParticle(string name, int startLifeTime, int duration)
        {
            if (!_particles.ContainsKey(name))
            {
                _particles[name] = new Queue<IParticle>();
            }

            if (_particles[name].Count == 0)
            {
                switch (name)
                {
                    case "Rain":
                        return new Rain(startLifeTime, duration);
                    case "Snow":
                        return new Snow(startLifeTime, duration);
                }
            }

            IParticle particle = _particles[name].Dequeue();
            particle.SetProperty(name, startLifeTime, duration);
            return particle;
        }

        public static void RecycleParticle(IParticle particle)
        {
            if (_particles.ContainsKey(particle.GetParticleType()))
            {
                _particles[particle.GetParticleType()].Enqueue(particle);
            }
        }

        public static void DisPlayCount()
        {
            Console.WriteLine("ParticleSystem Pool State:");
            foreach (KeyValuePair<string, Queue<IParticle>> keyValuePair in _particles)
            {
                Console.WriteLine(keyValuePair.Key + ": " + keyValuePair.Value.Count);
            }
        }
    }
    class Program
    {
        private static int GetRandomDuration()
        {
            return new Random().Next(30, 80);
        }

        static void Main(string[] args)
        {
            List<IParticle> particles = new List<IParticle>();
            List<IParticle> tempParticles = new List<IParticle>();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Using Particles:" + particles.Count);
                foreach (IParticle particle in particles)
                {
                    Console.WriteLine(particle.Name + "-" + particle.StartLifeTime + "-" + particle.Duration);
                }
                ParticleSystem.DisPlayCount();

                int seed = new Random().Next(0, 5);

                if (seed <= 1)
                {
                    particles.Add(ParticleSystem.GetParticle("Snow", 0, GetRandomDuration()));
                }
                else if(seed >=4)
                {
                    particles.Add(ParticleSystem.GetParticle("Rain", 0, GetRandomDuration()));
                }

                foreach (IParticle particle in particles)
                {
                    particle.Run();
                    if (particle.TimeOut())
                    {
                        tempParticles.Add(particle);
                    }
                }

                foreach (IParticle particle in tempParticles)
                {
                    particles.Remove(particle);
                    ParticleSystem.RecycleParticle(particle);
                }

                tempParticles.Clear();
                Thread.Sleep(100);
            }
        }
    }
}
