using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.ViewModels;

namespace Reports;

public partial class StatisticWindow : ReactiveWindow<StatisticViewModel>
{
    public StatisticWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            d(this.OneWayBind(ViewModel, vm => vm.FullName, view => view.txtFullName.Text));
            d(this.OneWayBind(ViewModel, vm => vm.GuardID,view => view.txtGuardID.Text));
            d(this.OneWayBind(ViewModel,vm => vm.Count,view => view.txtCount.Text));
            d(this.OneWayBind(ViewModel, vm => vm.Reported, view => view.txtReported.Text,value => $"{value} time(s)"));
            d(this.OneWayBind(ViewModel, vm => vm.Paid, view => view.txtPaid.Text,value => $"{value} time(s)"));
            d(this.OneWayBind(ViewModel, vm => vm.Total, view => view.txtTotal.Text,value => value.ToString($"N2") + " RSD"));
        });
    }
}