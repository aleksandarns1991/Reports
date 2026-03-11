using Avalonia;
using Avalonia.Media;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.Models;
using Reports.Utility;
using Reports.ViewModels;
using System;

namespace Reports;

public partial class ReportWindow : ReactiveWindow<ReportViewModel>
{
    public ReportWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            d(Interactions.ProductInteraction.RegisterHandler(async interaction =>
            {
                var win = new ProductWindow();
                win.DataContext = interaction.Input;

                var output = await win.ShowDialog<Product>(this);
                interaction.SetOutput(output);
            }));

            d(this.Bind(ViewModel, vm => vm.Title, view => view.txtTitle.Text));
            d(this.Bind(ViewModel,vm => vm.StoreNumber,view => view.txtStore.Text));
            d(this.Bind(ViewModel, vm => vm.CreatedAt, view => view.calendarDate.SelectedDate));

            d(this.OneWayBind(ViewModel, vm => vm.Products, view => view.cmbProducts.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedProduct, view => view.cmbProducts.SelectedItem));

            d(this.BindCommand(ViewModel, vm => vm.AddProductCmd, view => view.btnAddProduct));
            d(this.BindCommand(ViewModel,vm => vm.EditProductCmd,view => view.btnEditProduct));
            d(this.BindCommand(ViewModel, vm => vm.DeleteProductCmd, view => view.btnRemoveProduct));

            d(this.Bind(ViewModel,vm => vm.Description, view => view.txtDescription.Text));
            d(this.Bind(ViewModel, vm => vm.Manager, view => view.txtManager.Text));
            d(this.Bind(ViewModel, vm => vm.IsPaid, view => view.chkPaid.IsChecked));
            d(this.Bind(ViewModel, vm => vm.IsReported, view => view.chkReported.IsChecked));

            d(this.OneWayBind(ViewModel,vm => vm.Total,view => view.txtTotal.Text,value => value.ToString($"N2") + " RSD"));
            d(this.OneWayBind(ViewModel, vm => vm.Total, view => view.txtTotal.Foreground, value => value > 5000 ? Brushes.Red : Brushes.Black));

            d(this.BindCommand(ViewModel, vm => vm.SaveReportCmd, view => view.btnSaveReport));
            d(ViewModel!.SaveReportCmd.Subscribe(Close));
        });
    }
}