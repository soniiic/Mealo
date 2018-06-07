using System;
using System.Collections.Generic;
using System.Linq;
using Athela.Mealo.Core.Extensions;

namespace Athela.Mealo.Core.Models
{
    public class Milestone
    {
        public int Temperature { get; set; }

        public string FormattedCookingTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime => StartTime + CookingTime;

        public string ItemsToStartCooking { get; set; }

        public TimeSpan CookingTime { get; set;  }

        public bool IsFirst { get; set; }

        public bool IsLast { get; set; }

        public Milestone()
        {
        }

        public Milestone(double temperature, TimeSpan cookingTime, List<string> itemsToStartCooking, List<string> itemsCooking)
        {
            CookingTime = cookingTime;
            Temperature = (int)temperature;
            FormattedCookingTime = cookingTime.ToFormattedCookingTime();

            ItemsToStartCooking =
                string.Join(", ", itemsToStartCooking.TakeWhile((s, i) => i < itemsToStartCooking.Count - 1)) +
                (itemsToStartCooking.Count > 1 ? " and " : "") + itemsToStartCooking.Last();
        }
    }
}