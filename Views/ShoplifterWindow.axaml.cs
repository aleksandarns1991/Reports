using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.Utility;
using Reports.ViewModels;
using System;

namespace Reports;

public partial class ShoplifterWindow : ReactiveWindow<ShoplifterViewModel>
{
    public ShoplifterWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            d(Interactions.OpenFileInteraction.RegisterHandler(async interaction =>
            {
                var output = await interaction.Input.ShowAsync(this);
                interaction.SetOutput(output);
            }));

            d(this.Bind(ViewModel, vm => vm.FirstName, view => view.txtFirstName.Text));
            d(this.Bind(ViewModel, vm => vm.LastName,view => view.txtLastName.Text));
            d(this.Bind(ViewModel, vm => vm.PersonalID, view => view.txtID.Text));
            d(this.Bind(ViewModel, vm => vm.IsKnown, view => view.chkKnown.IsChecked));

            d(this.OneWayBind(ViewModel, vm => vm.ImagePath, view => view.imgPhoto.Source, value => !string.IsNullOrEmpty(value) ? new Bitmap(value) : new Bitmap(AssetLoader.Open(new Uri("avares://Reports/Assets/Images/user.png")))));

            d(this.BindCommand(ViewModel, vm => vm.OpenFileDialogCmd, view => view.btnBrowse));
            d(this.BindCommand(ViewModel, vm => vm.SaveShoplifterCmd, view => view.btnSave));

            d(ViewModel!.SaveShoplifterCmd.Subscribe(Close));
        });
    }
}