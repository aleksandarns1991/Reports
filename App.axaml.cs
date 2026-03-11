using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Reports.Utility;
using Reports.ViewModels;
using Reports.Views;

namespace Reports
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            _ = new AppBootStrapper();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var vm = new MainWindowViewModel();
                vm.Load();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
                
                desktop.Exit += (_, __) => vm.Save();
            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}