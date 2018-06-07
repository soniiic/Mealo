using Athela.Mealo.Core.Messages;
using Athela.Mealo.Core.Models;
using Athela.Mealo.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.MessageHandlers
{
    class FoodAddedMessageHandler : MessageHandler<FoodAddedMessage>
    {
        private readonly QuickMealViewModel quickMealViewModel;

        public FoodAddedMessageHandler(IMvxMessenger messenger, QuickMealViewModel quickMealViewModel) : base(messenger)
        {
            this.quickMealViewModel = quickMealViewModel;
        }

        protected override void Handle(FoodAddedMessage message)
        {
            var food = new Food(message.Name, message.Temperature, message.Time);   
            quickMealViewModel.FoodAdded(food);
        }
    }
}
