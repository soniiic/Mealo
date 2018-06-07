using System;
using MvvmCross.Core.ViewModels;

namespace Athela.Mealo.Core.ViewModels
{
    public class PagerViewModel : MvxViewModel
    {
        public Action ShowTimerAction { get; set; }
    }
}