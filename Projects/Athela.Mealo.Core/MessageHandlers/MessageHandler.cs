using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.MessageHandlers
{
    public abstract class MessageHandler<TMessage> : IMessageHandler where TMessage : MvxMessage
    {
        private MvxSubscriptionToken subscriptionToken;

        protected MessageHandler(IMvxMessenger messenger)
        {
            subscriptionToken = messenger.Subscribe<TMessage>(Handle);
        }

        protected abstract void Handle(TMessage message);
    }
}