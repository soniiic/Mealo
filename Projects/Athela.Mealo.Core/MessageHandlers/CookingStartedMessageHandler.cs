using Athela.Mealo.Core.Messages;
using Athela.Mealo.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.MessageHandlers
{
    class CookingStartedMessageHandler : MessageHandler<CookingStartedMessage>
    {
        private readonly CookingViewModel cookingViewModel;
        private readonly PagerViewModel pagerViewModel;

        public CookingStartedMessageHandler(IMvxMessenger messenger, CookingViewModel cookingViewModel, PagerViewModel pagerViewModel) : base(messenger)
        {
            this.cookingViewModel = cookingViewModel;
            this.pagerViewModel = pagerViewModel;
        }

        protected override void Handle(CookingStartedMessage message)
        {
            cookingViewModel.StartCooking(message.Foods);
            pagerViewModel.ShowTimerAction?.Invoke();
        }
    }
}
