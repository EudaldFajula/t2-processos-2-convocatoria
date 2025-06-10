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
            // Continua mentre la bateria no s'hagi esgotat
            while (!battery.IsFinished())
            {
                // Intenta carregar el dispositiu completament
                bool charged = battery.ChargeDevice(totalCapacity);
                if (!charged)
                {
                    Console.WriteLine($"[{name}] No es pot carregar completament. Aturant.");
                    break; // Si no es pot carregar, es finalitza el procés
                }

                Console.WriteLine($"[{name}] Carregat completament.");

                // Inicialitza la càrrega restant del dispositiu
                int remainingCharge = totalCapacity;
                while (remainingCharge > 0 && !battery.IsFinished())
                {
                    Thread.Sleep(1000); // Espera 1 segon per simular el consum
                    remainingCharge -= consumptionPerSecond; // Redueix la càrrega segons el consum per segon

                    if (remainingCharge < 0) remainingCharge = 0; // Evita que la càrrega sigui negativa

                    Console.WriteLine($"[{name}] Consum actual: {remainingCharge} mAh restants.");
                }
            }

            Console.WriteLine($"[{name}] Fil finalitzat."); // Indica que el fil ha acabat
        }

    }
}
