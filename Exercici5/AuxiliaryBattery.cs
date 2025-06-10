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
            lock (lockObj)
            {
                if (finished) { return false; }

                if (currentCapacity >= requiredAmount)
                {
                    currentCapacity -= requiredAmount;
                    TotalCharges++;
                    if (currentCapacity == 0)
                    {
                        finished = true;
                        stopwatch.Stop();
                    }
                    return true;
                }
                else
                {
                    finished = true;
                    stopwatch.Stop();
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
