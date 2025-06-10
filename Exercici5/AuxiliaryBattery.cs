using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercici5
{
    public class AuxiliaryBattery
    {
        private readonly object lockObj = new object();

        private int maxCapacity;
        private int currentCapacity;

        private Stopwatch stopwatch = new Stopwatch();

        public int TotalCharges { get; private set; }
        public int TotalTimeSeconds => (int)(stopwatch.ElapsedMilliseconds / 1000);

        private bool finished = false;

        public AuxiliaryBattery(int capacity)
        {
            maxCapacity = capacity;
            currentCapacity = capacity;
            stopwatch.Start();
        }

        public bool ChargeDevice(int requiredAmount)
        {
            // Garanteix exclusió mútua per evitar condicions de carrera
            lock (lockObj) 
            {
                // Si la bateria ja està esgotada, retorna fals
                if (finished) { return false; }

                // Comprova si hi ha suficient capacitat per carregar el dispositiu
                if (currentCapacity >= requiredAmount) 
                {
                    // Redueix la capacitat de la bateria
                    currentCapacity -= requiredAmount; 

                    // Augmenta el comptador de càrregues completes
                    TotalCharges++; 

                    // Si la bateria s'ha esgotat, marca com a finalitzada
                    if (currentCapacity == 0) 
                    {
                        finished = true;
                        // Atura el cronòmetre de temps d'ús
                        stopwatch.Stop(); 
                    }
                    // Indica que la càrrega ha estat satisfactòria
                    return true; 
                }
                // Si no hi ha suficient energia per carregar el dispositiu
                else 
                {
                    // Marca la bateria com esgotada
                    finished = true; 
                    // Atura el cronòmetre
                    stopwatch.Stop();
                    // Retorna fals indicant que la càrrega ha fallat
                    return false; 
                }
            }
        }

        public bool IsFinished()
        {
            lock (lockObj)
            {
                return finished;
            }
        }
    }
}
