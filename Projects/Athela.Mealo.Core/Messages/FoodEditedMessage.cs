using System;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.Messages
{
    internal class FoodEditedMessage : MvxMessage
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Temperature { get; }
        public TimeSpan Time { get; }

        public FoodEditedMessage(object sender, Guid id, string name, int temperature, TimeSpan time) : base(sender)
        {
            Id = id;
            Name = name;
            Temperature = temperature;
            Time = time;
        }
    }
}