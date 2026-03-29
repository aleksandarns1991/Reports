using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reports.DataAccess;
using Reports.Models;
using Reports.Utility;
using Splat;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reports.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ObservableCollection<Guard> Guards { get; } = new();
        [Reactive]
        public Guard? SelectedGuard { get; set; }
        [Reactive]
        public Report? SelectedReport { get; set; }

        public ReactiveCommand<Unit, Unit> AddGuardCmd { get; }
        public ReactiveCommand<Unit, Unit> EditGuardCmd { get; }
        public ReactiveCommand<Unit, Unit> DeleteGuardCmd { get; }
        public ReactiveCommand<Unit,Unit> AddReportCmd { get; }
        public ReactiveCommand<Unit, Unit> EditReportCmd { get; }
        public ReactiveCommand<Unit, Unit> DeleteReportCmd { get; }
        public ReactiveCommand<Unit, Unit> ShowStatisticCmd { get; }
        public ReactiveCommand<Unit, Unit> ShowShopliftersCmd { get; }

        private readonly IDataPersistence<Guard> persistence;

        public MainWindowViewModel(IDataPersistence<Guard>? persistence = null)
        {
            this.persistence = persistence ?? Locator.Current.GetService<IDataPersistence<Guard>>()!;

            var canModifyGuard = this.WhenAnyValue(x => x.SelectedGuard).Select(x => x != null);
            var canModifyReport = this.WhenAnyValue(x => x.SelectedReport).Select(x => x != null);
            var canAddReport = this.WhenAnyValue(x => x.SelectedGuard).Select(x => x != null);

            AddGuardCmd = ReactiveCommand.CreateFromTask(AddGuardAsync);
            EditGuardCmd = ReactiveCommand.CreateFromTask(EditGuardAsync,canModifyGuard);
            DeleteGuardCmd = ReactiveCommand.Create(DeleteGuard,canModifyGuard);
            AddReportCmd = ReactiveCommand.CreateFromTask(AddReportAsync,canAddReport); 
            EditReportCmd = ReactiveCommand.CreateFromTask(EditReportAsync,canModifyReport);    
            DeleteReportCmd = ReactiveCommand.Create(DeleteReport,canModifyReport);
            ShowStatisticCmd = ReactiveCommand.CreateFromTask(ShowStatisticAsync, canModifyGuard);
            ShowShopliftersCmd = ReactiveCommand.CreateFromTask(ShowShopliftersAsync,canModifyGuard);
        }

        #region Load data

        public void Load()
        {
            persistence.Load(Guards);
        }

        public void Save()
        {
            persistence.Save(Guards);
        }

        #endregion

        #region Guard 

        private async Task AddGuardAsync()
        {
            var vm = new GuardViewModel();
            var guard = await Interactions.GuardInteraction.Handle(vm);

            if (guard != null)
            {
                Guards.Add(guard);
                SelectedGuard = Guards.FirstOrDefault();
            }
        }

        private async Task EditGuardAsync()
        {
            var vm = new GuardViewModel(SelectedGuard);
            var guard = await Interactions.GuardInteraction.Handle(vm); 

            if (guard != null)
            {
                SelectedGuard!.GuardID = guard.GuardID;
                SelectedGuard.FirstName = guard.FirstName; 
                SelectedGuard.LastName = guard.LastName;
                SelectedGuard.ImagePath = guard.ImagePath;
            }
        }

        private void DeleteGuard()
        {
            Guards?.Remove(SelectedGuard!);
            SelectedGuard = Guards?.FirstOrDefault();
        }

        #endregion

        #region Report 

        private async Task AddReportAsync()
        {
            var vm = new ReportViewModel(); 
            var report = await Interactions.ReportInteraction.Handle(vm);

            if (report != null)
            {
                SelectedGuard?.Reports.Add(report);
                SelectedReport = SelectedGuard?.Reports.FirstOrDefault();
            }
        }

        private async Task EditReportAsync()
        {
            var vm = new ReportViewModel(SelectedReport);
            var report = await Interactions.ReportInteraction.Handle(vm); 

            if (report != null)
            {
                SelectedReport!.Title = report.Title;
                SelectedReport.StoreNumber = report.StoreNumber;
                SelectedReport.CreatedAt = report.CreatedAt;
                SelectedReport.Description = report.Description;
                SelectedReport.Manager = report.Manager;
                SelectedReport.IsPaid = report.IsPaid;
                SelectedReport.IsReported = report.IsReported;
                SelectedReport.Total = report.Total;
                SelectedReport.Products = new ObservableCollection<Product>(report.Products);
                SelectedReport.Shoplifters = new ObservableCollection<Shoplifter>(report.Shoplifters);  
            }
        }

        private void DeleteReport()
        {
            SelectedGuard?.Reports.Remove(SelectedReport!);
            SelectedReport = SelectedGuard?.Reports.FirstOrDefault();
        }

        #endregion

        #region Statistics
        
        private async Task ShowStatisticAsync()
        {
            var vm = new StatisticViewModel(SelectedGuard!);
            await Interactions.StatisticInteraction.Handle(vm);
        }

        private async Task ShowShopliftersAsync()
        {
            var shoplifters = Guards.SelectMany(g => g.Reports).SelectMany(r => r.Shoplifters).DistinctBy(s => s.ImagePath);

            var vm = new ShopliftersViewModel(shoplifters);
            await Interactions.ShopliftersInteraction.Handle(vm);
        }

        #endregion
    }
}