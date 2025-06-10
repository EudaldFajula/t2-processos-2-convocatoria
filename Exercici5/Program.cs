namespace Exercici5
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Enter the battery capacity in mAh: ");
            int batteryCapacity = ReadPositiveInt();

            Console.Write("Enter the number of devices to charge: ");
            int deviceCount = ReadPositiveInt();

            Console.Write("Enter total capacity needed for each device (mAh): ");
            int totalCapacity = ReadPositiveInt();

            Console.Write("Enter consumption per second for each device (mAh/s): ");
            int consumptionPerSecond = ReadPositiveInt();

            AuxiliaryBattery battery = new AuxiliaryBattery(batteryCapacity);

            List<Device> devices = new List<Device>();

            for (int i = 0; i < deviceCount; i++)
            {
                string name = $"Device {i + 1}";
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

            Console.WriteLine("Simulation ended.");
            Console.WriteLine($"Total time: {battery.TotalTimeSeconds} seconds");
            Console.WriteLine($"Total number of charges completed: {battery.TotalCharges}");
        }

        private static int ReadPositiveInt()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result > 0)
                    return result;
                Console.Write("Invalid input. Please enter a positive integer: ");
            }
        }
    }

}
