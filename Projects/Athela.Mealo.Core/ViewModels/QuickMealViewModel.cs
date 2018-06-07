using System;
using System.Collections.Generic;
using System.Linq;
using Athela.Mealo.Core.CookingAlgorithm;
using Athela.Mealo.Core.Messages;
using Athela.Mealo.Core.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Athela.Mealo.Core.ViewModels
{
    public class QuickMealViewModel : MvxViewModel
    {
        private readonly IMvxMessenger messenger;
        private MvxObservableCollection<Food> foods = new MvxObservableCollection<Food>();
        private int? preheatTemperature;
        private MvxCommand startCookingCommand;
        private MvxCommand<Food> editFoodCommand;
        private MvxCommand addFoodCommand;

        public QuickMealViewModel(IMvxMessenger messenger)
        {
            this.messenger = messenger;
        }

        private void FoodsChanged()
        {
            StartCookingCommand.RaiseCanExecuteChanged();
            UpdatePreheatTemperature();
            RaisePropertyChanged(() => ShowPreheatTemperature);
        }

        private void UpdatePreheatTemperature()
        {
            if (foods.Any())
            {
                var cookingMethod = CookingMethodFactory.GenerateCookingMethod(Foods.ToList());
                PreheatTemperature = cookingMethod.First().Temperature;
            }
            else
            {
                PreheatTemperature = null;
            }
        }

        internal void FoodAdded(Food food)
        {
            Foods.Add(food);
            FoodsChanged();
        }

        internal void FoodEdited(FoodEditedMessage message)
        {
            var food = Foods.Single(f => f.Id == message.Id);

            food.Name = message.Name;
            food.Temperature = message.Temperature;
            food.Time = message.Time;
            FoodsChanged();
        }

        public MvxCommand AddFoodCommand
        {
            get
            {
                addFoodCommand = addFoodCommand ?? new MvxCommand(() => ShowAddFoodDialogAction?.Invoke(null));
                return addFoodCommand;
            }
        }

        public MvxCommand<Food> EditFoodCommand
        {
            get
            {
                editFoodCommand = editFoodCommand ?? new MvxCommand<Food>(f => ShowAddFoodDialogAction?.Invoke(f));
                return editFoodCommand;
            }
        }

        public MvxCommand StartCookingCommand
        {
            get
            {
                startCookingCommand = startCookingCommand ?? new MvxCommand(StartCooking, CanStartCooking);
                return startCookingCommand;
            }
        }

        public Action<Food> ShowAddFoodDialogAction { get; set; }

        public int? PreheatTemperature
        {
            get => preheatTemperature;
            set
            {
                preheatTemperature = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowPreheatTemperature => Foods.Any();

        private bool CanStartCooking()
        {
            return Foods.Any();
        }

        public MvxCommand<Food> RemoveFoodCommand => new MvxCommand<Food>(RemoveFood);

        private void RemoveFood(Food food)
        {
            Foods.Remove(food);
            FoodsChanged();
        }

        private void StartCooking()
        {
            messenger.Publish(new CookingStartedMessage(this, Foods.ToList()));
        }

        public MvxObservableCollection<Food> Foods
        {
            get => this.foods;
            set
            {
                this.foods = value;
                RaisePropertyChanged();
            }
        }
    }
}