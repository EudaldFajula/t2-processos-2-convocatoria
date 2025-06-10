using System.Diagnostics;

namespace Exercici4
{
    public class Program
    {
        public static void Main()
        {
            const string filePath = "../../../processes.txt";
            int totalProcesses = 0;
            int totalThreads = 0;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                Process[] processes = Process.GetProcesses();
                totalProcesses = processes.Length;

                foreach (Process process in processes)
                {
                    try
                    {
                        Console.WriteLine($"Process: {process.ProcessName} | ID: {process.Id}");
                        writer.WriteLine($"Process: {process.ProcessName} | ID: {process.Id}");
                        ProcessThreadCollection threads = process.Threads;

                        foreach (ProcessThread thread in threads)
                        {
                            Console.WriteLine($"Thread ID: {thread.Id}");
                            writer.WriteLine($"Thread ID: {thread.Id}");
                            totalThreads++;
                        }
                    }
                    catch (Exception ex)
                    {
                        writer.WriteLine(ex.Message);
                    }
                }
                writer.WriteLine("\n===============================");
                writer.WriteLine($"Total de processos: {totalProcesses}");
                writer.WriteLine($"Total de fils: {totalThreads}");
            }
            Console.WriteLine($"Informació desada a: {Path.GetFullPath(filePath)}");
        }
    }
}
