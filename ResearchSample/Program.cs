using ResearchSample;
using ResearchSample.Composite;
using System.Data.Common;
using System.Diagnostics;
using System.Numerics;

namespace ResearchSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunMetrics(() => CreateCompositeInstances(10_000_000), "Composite Objects.");

            //RunMetrics(() => CreateInhertedInstances(10_000_000), "Inherited Objects.");
        }

        private static void RunMetrics(Action func, string desc)
        {

            long before = Process.GetCurrentProcess().WorkingSet64;
            Stopwatch watch = Stopwatch.StartNew();

            func();

            watch.Stop();
            long after = Process.GetCurrentProcess().WorkingSet64;

            Console.WriteLine(desc);
            Console.WriteLine($"Memory Consumed: {after - before}");
            Console.WriteLine($"Time elapsed: {watch.ElapsedMilliseconds}");
        }


        private static void CreateInhertedInstances(int numberOfEach)
        {
            // Measure Inheritance Metrics
            Console.WriteLine("Measuring Inheritance:");
            List<Inheritance.Vehicle> inheritVechiles = new List<Inheritance.Vehicle>();
            for (int i = 0; i < numberOfEach; i++)
            {
                inheritVechiles.Add(new Inheritance.MyBike());
            }
            for (int i = 0; i < numberOfEach; i++)
            {
                inheritVechiles.Add(new Inheritance.MyCars());
            }
            for (int i = 0; i < numberOfEach; i++)
            {
                inheritVechiles.Add(new Inheritance.MyCruiser());
            }

        }


        private static void CreateCompositeInstances(int numberOfEach)
        {
            Console.WriteLine("Measuring Composition:");
            List<Composite.Vehicle> compositeVehicles = new List<Composite.Vehicle>();
            Thread.Sleep(1000);
            for (int i = 0; i < numberOfEach; i++)
            {
                compositeVehicles.Add(
                    new Vehicle("Honda",
                        "CB 125E",
                        2020,
                        new Engine(100, 10),
                        [],
                        [new Wheel(), new Wheel()])
                    );
            }
            for (int i = 0; i < numberOfEach; i++)
            {
                compositeVehicles.Add(
                    new Vehicle("Ford",
                        "Falcon XT",
                        2009,
                        new Engine(230, 262),
                        [new Door(), new Door(), new Door(), new Door()],
                        [new Wheel(), new Wheel(), new Wheel(), new Wheel()])
                    );
            }
            for (int i = 0; i < numberOfEach; i++)
            {
                compositeVehicles.Add(
                    new Vehicle("Toyota",
                        "FJ Cruiser",
                        2015,
                        new Engine(180, 268),
                        [new Door(), new Door(), new Door(), new Door()],
                        [new Wheel(), new Wheel(), new Wheel(), new Wheel()])
                    );
            }
            compositeVehicles.Clear();

        }

    }
}