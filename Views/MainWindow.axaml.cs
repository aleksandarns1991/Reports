using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.Models;
using Reports.Utility;
using Reports.ViewModels;
using System.Reactive;
using System.Reactive.Linq;

namespace Reports.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(Interactions.GuardInteraction.RegisterHandler(async interaction =>
                {
                    var win = new GuardWindow();
                    win.DataContext = interaction.Input;

                    var output = await win.ShowDialog<Guard>(this);
                    interaction.SetOutput(output);
                }));

                d(Interactions.ReportInteraction.RegisterHandler(async interaction =>
                {
                    var win = new ReportWindow(); 
                    win.DataContext = interaction.Input;

                    var output = await win.ShowDialog<Report>(this); 
                    interaction.SetOutput(output);
                }));

                d(Interactions.StatisticInteraction.RegisterHandler(interaction =>
                {
                    var win = new StatisticWindow(); 
                    win.DataContext = interaction.Input;
                    win.Show();

                    interaction.SetOutput(Unit.Default);
                }));

                d(Interactions.ShopliftersInteraction.RegisterHandler(interaction =>
                {
                    var win = new ShopliftersWindow();
                    win.DataContext = interaction.Input;
                    win.Show();

                    interaction.SetOutput(Unit.Default);
                }));

                d(this.OneWayBind(ViewModel, vm => vm.Guards, view => view.cmbGuards.ItemsSource));
                d(this.Bind(ViewModel, vm => vm.SelectedGuard, view => view.cmbGuards.SelectedItem));

                d(this.BindCommand(ViewModel, vm => vm.AddGuardCmd, view => view.btnAddGuard));
                d(this.BindCommand(ViewModel,vm => vm.EditGuardCmd, view => view.btnEditGuard));
                d(this.BindCommand(ViewModel,vm => vm.DeleteGuardCmd, view => view.btnDeleteGuard));
                
                d(this.WhenAnyValue(x => x.ViewModel!.SelectedGuard).Select(guard => guard != null ? guard.WhenAnyValue(g => g.FirstName) : Observable.Return(string.Empty)).Switch().BindTo(this, v => v.txtFirstName.Text));
                d(this.WhenAnyValue(x => x.ViewModel!.SelectedGuard).Select(guard => guard != null ? guard.WhenAnyValue(g => g.LastName) : Observable.Return(string.Empty)).Switch().BindTo(this, v => v.txtLastName.Text));
                
                d(this.OneWayBind(ViewModel, vm => vm.SelectedGuard!.Reports, view => view.cmbReports.ItemsSource));
                d(this.Bind(ViewModel, vm => vm.SelectedReport, view => view.cmbReports.SelectedItem));
                
                d(this.BindCommand(ViewModel,vm => vm.AddReportCmd,view => view.btnAddReport));
                d(this.BindCommand(ViewModel,vm => vm.EditReportCmd,view => view.btnEditReport));
                d(this.BindCommand(ViewModel,vm => vm.DeleteReportCmd,view => view.btnDeleteReport));

                d(this.BindCommand(ViewModel, vm => vm.ShowStatisticCmd, view => view.btnStats));
                d(this.BindCommand(ViewModel, vm => vm.ShowShopliftersCmd,view => view.btnShoplifters));
            });
        }
    }
}