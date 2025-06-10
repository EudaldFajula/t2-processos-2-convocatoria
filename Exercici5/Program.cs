namespace Exercici5
{
    public class Program
    {
        public static void Main()
        {
            // Demana a l'usuari la capacitat de la bateria en mAh
            Console.Write("Introdueix la capacitat de la bateria en mAh: ");
            int batteryCapacity = ReadPositiveInt();

            // Demana a l'usuari el nombre de dispositius a carregar
            Console.Write("Introdueix el nombre de dispositius a carregar: ");
            int deviceCount = ReadPositiveInt();

            // Demana a l'usuari la capacitat total necessària per a cada dispositiu
            Console.Write("Introdueix la capacitat total necessària per a cada dispositiu (mAh): ");
            int totalCapacity = ReadPositiveInt();

            // Demana a l'usuari el consum per segon de cada dispositiu
            Console.Write("Introdueix el consum per segon de cada dispositiu (mAh/s): ");
            int consumptionPerSecond = ReadPositiveInt();

            // Crea una instància de la bateria auxiliar
            AuxiliaryBattery battery = new AuxiliaryBattery(batteryCapacity);

            // Llista per emmagatzemar els dispositius
            List<Device> devices = new List<Device>();

            // Inicialitza els dispositius amb els valors proporcionats per l'usuari
            for (int i = 0; i < deviceCount; i++)
            {
                string name = $"Dispositiu {i + 1}";
                devices.Add(new Device(name, totalCapacity, consumptionPerSecond, battery));
            }

            // Llista per gestionar els fils dels dispositius
            List<Thread> threads = new List<Thread>();
            foreach (var device in devices)
            {
                // Crea un fil per a cada dispositiu i inicia la càrrega
                Thread thread = new Thread(device.StartCharging);
                threads.Add(thread);
                thread.Start();
            }

            // Espera que tots els fils finalitzin abans de continuar
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Mostra el resum de la simulació
            Console.WriteLine("Simulació finalitzada.");
            Console.WriteLine($"Temps total: {battery.TotalTimeSeconds} segons");
            Console.WriteLine($"Nombre total de càrregues completades: {battery.TotalCharges}");
        }

        // Funció per llegir un enter positiu de l'entrada de l'usuari
        private static int ReadPositiveInt()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result > 0)
                    return result;
                Console.Write("Entrada no vàlida. Introdueix un número enter positiu: ");
            }
        }
    }


}
