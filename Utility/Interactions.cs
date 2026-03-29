using ReactiveUI;
using Reports.Models;
using Reports.Services;
using Reports.ViewModels;
using System.Reactive;

namespace Reports.Utility
{
    public static class Interactions
    {
        public static readonly Interaction<GuardViewModel, Guard> GuardInteraction = new();
        public static readonly Interaction<ProductViewModel, Product> ProductInteraction = new();
        public static readonly Interaction<ReportViewModel, Report> ReportInteraction = new();
        public static readonly Interaction<ShoplifterViewModel, Shoplifter> ShoplifterInteraction = new();
        public static readonly Interaction<OpenFileDialogService, string[]> OpenFileInteraction = new();
        public static readonly Interaction<StatisticViewModel, Unit> StatisticInteraction = new();
        public static readonly Interaction<ShopliftersViewModel, Unit> ShopliftersInteraction = new();
    }
}
