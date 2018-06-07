using System;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.Messages
{
    internal class FoodAddedMessage : MvxMessage
    {
        public string Name { get; }
        public int Temperature { get; }
        public TimeSpan Time { get; }

        public FoodAddedMessage(object sender, string name, int temperature, TimeSpan time) : base(sender)
        {
            Name = name;
            Temperature = temperature;
            Time = time;
        }
    }
}