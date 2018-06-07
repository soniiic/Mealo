using System.Collections.Generic;
using Athela.Mealo.Core.Models;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.Messages
{
    public class MilestonesCreatedMessage : MvxMessage
    {
        public MilestonesCreatedMessage(object sender) : base(sender)
        {
        }

        public List<Milestone> Milestones { get; set; }
    }
}