using Avalonia;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.ViewModels;
using System;

namespace Reports;

public partial class ProductWindow : ReactiveWindow<ProductViewModel>
{
    public ProductWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            d(this.Bind(ViewModel, vm => vm.ID, view => view.txtID.Text)); 
            d(this.Bind(ViewModel,vm => vm.Title,view  => view.txtTitle.Text));
            d(this.Bind(ViewModel, vm => vm.Quantity, view => view.txtQuantity.Text));
            d(this.Bind(ViewModel,vm => vm.Price,view => view.txtPrice.Text));

            d(this.BindCommand(ViewModel, vm => vm.SaveProductCmd, view => view.btnSave));
            d(ViewModel!.SaveProductCmd.Subscribe(Close));
        });
    }
}