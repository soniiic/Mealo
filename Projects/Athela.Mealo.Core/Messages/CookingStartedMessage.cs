using System.Collections.Generic;
using Athela.Mealo.Core.Models;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.Messages
{
    public class CookingStartedMessage : MvxMessage
    {
        public CookingStartedMessage(object sender, List<Food> foods) : base(sender)
        {
            Foods = foods;
        }

        public List<Food> Foods { get; }
    }
}