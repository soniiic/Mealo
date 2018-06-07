using Athela.Mealo.Core.Messages;
using Athela.Mealo.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.MessageHandlers
{
    class FoodEditedMessageHandler : MessageHandler<FoodEditedMessage>
    {
        private readonly QuickMealViewModel quickMealViewModel;

        public FoodEditedMessageHandler(IMvxMessenger messenger, QuickMealViewModel quickMealViewModel) : base(messenger)
        {
            this.quickMealViewModel = quickMealViewModel;
        }

        protected override void Handle(FoodEditedMessage message)
        {
            quickMealViewModel.FoodEdited(message);
        }
    }
}
