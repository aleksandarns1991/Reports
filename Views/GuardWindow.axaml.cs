using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Reports.Models;
using Reports.Utility;
using Reports.ViewModels;
using System;

namespace Reports;

public partial class GuardWindow : ReactiveWindow<GuardViewModel>
{
    public GuardWindow()
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

            d(Interactions.OpenFileInteraction.RegisterHandler(async interaction =>
            {
                var output = await interaction.Input.ShowAsync(this);
                interaction.SetOutput(output);  
            }));

            d(this.Bind(ViewModel, vm => vm.FirstName, view => view.txtFirstName.Text));
            d(this.Bind(ViewModel, vm => vm.LastName, view => view.txtLastName.Text));
            d(this.Bind(ViewModel, vm => vm.GuardID, view => view.txtID.Text));

            d(this.OneWayBind(ViewModel, vm => vm.ImagePath, view => view.imgFile.Source, value => !string.IsNullOrEmpty(value) ? new Bitmap(value) : new Bitmap(AssetLoader.Open(new Uri("avares://Reports/Assets/Images/user.png")))));
            d(this.BindCommand(ViewModel, vm => vm.OpenFileDialogCmd, view => view.btnBrowse));
            d(this.BindCommand(ViewModel, vm => vm.SaveGuardCmd, view => view.btnSave));

            d(ViewModel!.SaveGuardCmd.Subscribe(Close));
        });
    }
}