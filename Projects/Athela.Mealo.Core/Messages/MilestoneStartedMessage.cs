using Athela.Mealo.Core.Models;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.Messages
{
    public class MilestoneStartedMessage : MvxMessage
    {
        public Milestone Milestone { get; }

        public MilestoneStartedMessage(object sender, Milestone milestone) : base(sender)
        {
            Milestone = milestone;
        }
    }
}