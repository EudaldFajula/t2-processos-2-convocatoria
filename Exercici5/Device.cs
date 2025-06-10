using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercici5
{
    public class Device
    {
        private readonly string name;
        private readonly int totalCapacity;
        private readonly int consumptionPerSecond;
        private readonly AuxiliaryBattery battery;

        public Device(string name, int totalCapacity, int consumptionPerSecond, AuxiliaryBattery battery)
        {
            this.name = name;
            this.totalCapacity = totalCapacity;
            this.consumptionPerSecond = consumptionPerSecond;
            this.battery = battery;
        }
        public void StartCharging()
        {
            while (!battery.IsFinished())
            {
                bool charged = battery.ChargeDevice(totalCapacity);
                if (!charged)
                {
                    Console.WriteLine($"[{name}] No es pot carregar completament. Aturant.");
                    break;
                }

                Console.WriteLine($"[{name}] Carregat completament.");

                int remainingCharge = totalCapacity;
                while (remainingCharge > 0 && !battery.IsFinished())
                {
                    Thread.Sleep(1000);
                    remainingCharge -= consumptionPerSecond;
                    if (remainingCharge < 0) remainingCharge = 0;

                    Console.WriteLine($"[{name}] Consum actual: {remainingCharge} mAh restants.");
                }
            }

            Console.WriteLine($"[{name}] Fil finalitzat.");
        }
    }
}
