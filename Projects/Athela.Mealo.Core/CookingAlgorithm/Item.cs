using System;

namespace Athela.Mealo.Core.CookingAlgorithm
{
    internal class Item
    {
        public Item(string name, TimeSpan time, int temperature)
        {
            Name = name;
            RemainingTime = time;
            DesiredTemperature = temperature;
        }

        public string Name { get; }

        public TimeSpan RemainingTime { get; set; }

        public int DesiredTemperature { get; }
    }
}