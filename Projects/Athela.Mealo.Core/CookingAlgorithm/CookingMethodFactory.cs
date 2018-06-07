using System;
using System.Collections.Generic;
using System.Linq;
using Athela.Mealo.Core.Models;

namespace Athela.Mealo.Core.CookingAlgorithm
{
    class CookingMethodFactory
    {
        internal static CookingMethod GenerateCookingMethod(List<Food> items)
        {
            var itemsCooking = items.Select(i => new Item(i.Name, i.Time, i.Temperature)).ToList();
            var cookingMethod = new CookingMethod();

            while (itemsCooking.Any())
            {
                var avgTemperature = GetAverageTemperature(itemsCooking);
                var minimumTime = itemsCooking.Min(i => i.DesiredTemperature / avgTemperature * i.RemainingTime.Ticks);

                foreach (var item in itemsCooking)
                {
                    var timeToCookAtAverageTemperature = item.DesiredTemperature / avgTemperature * item.RemainingTime.Ticks;

                    var percentageCooked = minimumTime / timeToCookAtAverageTemperature;

                    item.RemainingTime = TimeSpan.FromTicks((long)(item.RemainingTime.Ticks * (1 - percentageCooked)));
                }

                cookingMethod.Add(GetCookingMilestone(avgTemperature, minimumTime, itemsCooking));

                itemsCooking.RemoveAll(i => i.RemainingTime == TimeSpan.Zero);
            }

            return cookingMethod;
        }

        private static Milestone GetCookingMilestone(double avgTemperature, double minimumTime, List<Item> itemsCooking)
        {
            return new Milestone(
                avgTemperature,
                TimeSpan.FromTicks((long)minimumTime),
                itemsCooking.Where(i => i.RemainingTime == TimeSpan.Zero).Select(i => i.Name).ToList(),
                itemsCooking.Select(i => i.Name).ToList());
        }

        private static double GetAverageTemperature(List<Item> itemsCooking)
        {
            var temperatures = itemsCooking.Select(i => i.DesiredTemperature).ToList();
            return Math.Min(temperatures.Distinct().Average(), temperatures.Average());
        }
    }
}