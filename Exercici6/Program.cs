namespace Exercici6
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Iniciant simulació seqüencial...");
            var seqTime = RunSequentialSimulation();
            Console.WriteLine($"Simulació seqüencial finalitzada en {seqTime.TotalSeconds} segons.\n");

            Console.WriteLine("Iniciant simulació en paral·lel...");
            var parTime = await RunParallelSimulationAsync();
            Console.WriteLine($"Simulació en paral·lel finalitzada en {parTime.TotalSeconds} segons.\n");
        }


        // Simulació Seqüencial
        public static TimeSpan RunSequentialSimulation()
        {
            Stopwatch sw = Stopwatch.StartNew();

            // 1. Batre la massa
            WaitSeconds(8);
            Console.WriteLine("Massa batuda!");

            // 2. Pre-escalfar forn
            WaitSeconds(10);
            Console.WriteLine("Forn preescalfat!");

            // 3. Enfornar (depèn de 1 i 2)
            WaitSeconds(15);
            Console.WriteLine("Base enfornada!");

            // 4. Preparar cobertura (es podria fer paral·lel però seqüencial simulació no)
            WaitSeconds(5);
            Console.WriteLine("Cobertura preparada!");

            // 5. Refredar base (depèn de 3)
            WaitSeconds(4);
            Console.WriteLine("Base refredada!");

            // 6. Glassejar (depèn de 4 i 5)
            WaitSeconds(3);
            Console.WriteLine("Pastís glassejat!");

            // 7. Decorar (depèn de 6)
            WaitSeconds(2);
            Console.WriteLine("Pastís decorat!");

            sw.Stop();
            return sw.Elapsed;
        }

        // Simulació Paral·lela 
        public static async Task<TimeSpan> RunParallelSimulationAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Tasques 1 i 2 es poden fer paral·lelament
            Task task1 = WaitSecondsAsync(8).ContinueWith(_ => Console.WriteLine("1. Massa batuda (8s)"));
            Task task2 = WaitSecondsAsync(10).ContinueWith(_ => Console.WriteLine("2. Forn preescalfat (10s)"));

            await Task.WhenAll(task1, task2);

            // 3. Enfornar (depèn de 1 i 2)
            await WaitSecondsAsync(15);
            Console.WriteLine("3. Enfornat (15s)");

            // 4. Preparar cobertura (es pot fer paral·lel al forn, ara que ja ha acabat 1 i 2)
            Task task4 = WaitSecondsAsync(5).ContinueWith(_ => Console.WriteLine("4. Cobertura preparada (5s)"));

            // 5. Refredar base (depèn de 3)
            await WaitSecondsAsync(4);
            Console.WriteLine("5. Base refredada (4s)");

            //assegurem que cobertura ha acabat abans de glassejar
            await task4; 

            // 6. Glassejar (depèn de 4 i 5)
            await WaitSecondsAsync(3);
            Console.WriteLine("6. Glassejat (3s)");

            // 7. Decorar (depèn de 6)
            await WaitSecondsAsync(2);
            Console.WriteLine("7. Decoració feta (2s)");

            sw.Stop();
            Console.WriteLine($"Temps total: {sw.Elapsed.TotalSeconds:F1} segons");
            return sw.Elapsed;
        }

        //Helpers
        public static void WaitSeconds(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        public static Task WaitSecondsAsync(int seconds)
        {
            return Task.Delay(seconds * 1000);
        }
    }

}
