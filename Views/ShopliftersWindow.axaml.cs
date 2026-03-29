using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.ViewModels;

namespace Reports;

public partial class ShopliftersWindow : ReactiveWindow<ShopliftersViewModel>
{
    public ShopliftersWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            d(this.OneWayBind(ViewModel, vm => vm.Shoplifters, view => view.cmbShoplifters.ItemsSource));
        });
    }
}