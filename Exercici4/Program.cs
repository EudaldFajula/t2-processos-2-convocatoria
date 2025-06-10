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

            // Crea un escriptor per desar dades al fitxer
            using (StreamWriter writer = new StreamWriter(filePath)) 
            {
                // Obtén la llista de processos en execució
                Process[] processes = Process.GetProcesses(); 

                // Guarda el nombre total de processos
                totalProcesses = processes.Length; 

                foreach (Process process in processes)
                {
                    try
                    {
                        // Mostra i escriu la informació del procés
                        Console.WriteLine($"Process: {process.ProcessName} | ID: {process.Id}");
                        writer.WriteLine($"Process: {process.ProcessName} | ID: {process.Id}");

                        // Obtén la col·lecció de fils del procés
                        ProcessThreadCollection threads = process.Threads; 

                        foreach (ProcessThread thread in threads)
                        {
                            // Mostra i escriu la informació de cada fil
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

                // Escriu el resum de l'execució al fitxer
                writer.WriteLine("\n===============================");
                writer.WriteLine($"Total de processos: {totalProcesses}");
                writer.WriteLine($"Total de fils: {totalThreads}");
            }

            // Mostra la ubicació del fitxer generat
            Console.WriteLine($"Informació desada a: {Path.GetFullPath(filePath)}");
        }
    }

}
