using System.Collections.Generic;
using System.Linq;
using Athela.Mealo.Core.CookingAlgorithm;
using Athela.Mealo.Core.Messages;
using Athela.Mealo.Core.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.ViewModels
{
    public class CookingViewModel : MvxViewModel
    {
        private readonly IMvxMessenger messenger;
        private MvxCommand cancelCookingCommand;

        public MvxObservableCollection<Milestone> Milestones { get; } = new MvxObservableCollection<Milestone>();

        public MvxCommand CancelCookingCommand
        {
            get
            {
                cancelCookingCommand = cancelCookingCommand ?? new MvxCommand(CancelCookingClicked, CanCancelCooking);
                return cancelCookingCommand;
            }
        }

        public bool ShowMilestones => Milestones.Any();

        public CookingViewModel(IMvxMessenger messenger)
        {
            this.messenger = messenger;
        }

        private void CancelCookingClicked()
        {
            Milestones.Clear();
            RaisePropertyChanged(() => ShowMilestones);
            messenger.Publish(new CookingCanceledMessage(this));
        }

        private bool CanCancelCooking()
        {
            return Milestones.Any();
        }

        internal void StartCooking(List<Food> foods)
        {
            var milestones = CookingMethodFactory.GenerateCookingMethod(foods);
            Milestones.ReplaceWith(milestones);
            CancelCookingCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(() => ShowMilestones);
            messenger.Publish(new MilestonesCreatedMessage(this)
            {
                Milestones = milestones.ToList()
            });
        }
    }
}