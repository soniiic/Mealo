using System;
using MvvmCross.Core.ViewModels;

namespace Athela.Mealo.Core.Models
{
    public class Food : MvxNotifyPropertyChanged
    {
        private string name;
        private int temperature;
        private TimeSpan time;

        public Food(string name, int temperature, TimeSpan time)
        {
            this.Id = Guid.NewGuid();
            this.name = name;
            this.temperature = temperature;
            this.time = time;
        }

        public Guid Id { get; }

        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                RaisePropertyChanged();
            }
        }

        public int Temperature
        {
            get => this.temperature;
            set
            {
                this.temperature = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan Time
        {
            get => this.time;
            set
            {
                this.time = value;
                RaisePropertyChanged();
            }
        }
    }
}