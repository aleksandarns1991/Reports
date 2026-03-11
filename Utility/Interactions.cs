using ReactiveUI;
using Reports.Models;
using Reports.Services;
using Reports.ViewModels;

namespace Reports.Utility
{
    public static class Interactions
    {
        public static readonly Interaction<GuardViewModel, Guard> GuardInteraction = new();
        public static readonly Interaction<ProductViewModel, Product> ProductInteraction = new();
        public static readonly Interaction<ReportViewModel, Report> ReportInteraction = new();
        public static readonly Interaction<OpenFileDialogService, string[]> OpenFileInteraction = new();
    }
}
