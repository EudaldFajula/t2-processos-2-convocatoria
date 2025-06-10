namespace Exercici5
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Introdueix la capacitat de la bateria en mAh: ");
            int batteryCapacity = ReadPositiveInt();

            Console.Write("Introdueix el nombre de dispositius a carregar: ");
            int deviceCount = ReadPositiveInt();

            Console.Write("Introdueix la capacitat total necessària per a cada dispositiu (mAh): ");
            int totalCapacity = ReadPositiveInt();

            Console.Write("Introdueix el consum per segon de cada dispositiu (mAh/s): ");
            int consumptionPerSecond = ReadPositiveInt();

            AuxiliaryBattery battery = new AuxiliaryBattery(batteryCapacity);

            List<Device> devices = new List<Device>();

            for (int i = 0; i < deviceCount; i++)
            {
                string name = $"Dispositiu {i + 1}";
                devices.Add(new Device(name, totalCapacity, consumptionPerSecond, battery));
            }

            List<Thread> threads = new List<Thread>();
            foreach (var device in devices)
            {
                Thread thread = new Thread(device.StartCharging);
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Simulació finalitzada.");
            Console.WriteLine($"Temps total: {battery.TotalTimeSeconds} segons");
            Console.WriteLine($"Nombre total de càrregues completades: {battery.TotalCharges}");
        }

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
