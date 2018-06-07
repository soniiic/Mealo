using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.Messages
{
    public class CookingCanceledMessage : MvxMessage
    {
        public CookingCanceledMessage(object sender) : base(sender) { }
    }
}