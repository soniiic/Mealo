using System;
using Athela.Mealo.Core.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.ViewModels
{
    public class AddFoodViewModel : MvxViewModel
    {
        private readonly IMvxMessenger messenger;
        private string name;
        private int? temperature;
        private TimeSpan? time;
        private MvxCommand addFoodCommand;

        public AddFoodViewModel(IMvxMessenger messenger)
        {
            this.messenger = messenger;
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            AddFoodCommand.RaiseCanExecuteChanged();
        }

        public MvxCommand AddFoodCommand
        {
            get
            {
                addFoodCommand = addFoodCommand ?? new MvxCommand(AddFood, CanAddFood);
                return addFoodCommand;
            }
        }

        public Action CloseDialog { get; set; }

        private bool CanAddFood()
        {
            return !string.IsNullOrWhiteSpace(Name) && Temperature > 0 && Time.HasValue;
        }

        private void AddFood()
        {
            if (!FoodId.HasValue)
            {
                messenger.Publish(new FoodAddedMessage(this, Name, Temperature.Value, Time.Value));
            }
            else
            {
                messenger.Publish(new FoodEditedMessage(this, FoodId.Value, Name, Temperature.Value, Time.Value));
            }
            CloseDialog?.Invoke();
        }

        public Guid? FoodId { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        public int? Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan? Time
        {
            get => time;
            set
            {
                time = value;
                RaisePropertyChanged();
            }
        }
    }
}