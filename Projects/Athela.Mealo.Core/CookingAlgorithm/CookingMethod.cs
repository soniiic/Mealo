using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athela.Mealo.Core.Models;

namespace Athela.Mealo.Core.CookingAlgorithm
{
    internal class CookingMethod : IEnumerable<Milestone>
    {
        //public TimeSpan TotalCookingTime => TimeSpan.FromTicks(this.Sum(i => i.CookingTime.Ticks));

        readonly List<Milestone> milestones = new List<Milestone>();
        private readonly DateTime startDate;

        public CookingMethod()
        {
            startDate = DateTime.Now;
        }

        public IEnumerator<Milestone> GetEnumerator()
        {
            var allMilestones = milestones.ToList();

            var timeOfMilestone = startDate;
            for (var i = allMilestones.Count - 1; i >= 0; i--)
            {
                var milestone = allMilestones[i];
                milestone.IsFirst = i == milestones.Count - 1;
                milestone.StartTime = timeOfMilestone;
                timeOfMilestone += milestone.CookingTime;
            }

            allMilestones.Reverse();

            var lastMilestone =
                new Milestone(0, TimeSpan.Zero, new List<string> { "Done" }, new List<string>())
                {
                    IsLast = true,
                    StartTime = timeOfMilestone
                };
            allMilestones.Add(lastMilestone);

            return allMilestones.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Milestone cookingMilestone)
        {
            milestones.Add(cookingMilestone);
        }
    }
}