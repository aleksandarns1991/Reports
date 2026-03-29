using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reports.Models;
using Reports.Services;
using Reports.Utility;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reports.ViewModels
{
    public class GuardViewModel : ReactiveObject
    {
        [Reactive]
        public string? GuardID { get; set; }
        [Reactive]
        public string? FirstName { get; set; }
        [Reactive]
        public string? LastName { get; set; }
        [Reactive]
        public string? ImagePath { get; set; }

        public ReactiveCommand<Unit, Unit> OpenFileDialogCmd { get; }
        public ReactiveCommand<Unit, Guard> SaveGuardCmd { get; }
        
        public GuardViewModel(Guard? guard) : this()
        {
            GuardID = guard!.GuardID;
            FirstName = guard.FirstName;
            LastName = guard.LastName;
            ImagePath = guard.ImagePath;
        }

        public GuardViewModel()
        {
            OpenFileDialogCmd = ReactiveCommand.CreateFromTask(OpenFileDialogAsync);
            SaveGuardCmd = ReactiveCommand.Create(SaveGuard, this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                                                                             (firstName, lastName) =>
                                                                             !string.IsNullOrEmpty(firstName)
                                                                          && !string.IsNullOrEmpty(lastName)));
        }

        private async Task OpenFileDialogAsync()
        {
            var dialog = new OpenFileDialogService
            {
                Title = "Browse images",
                Extensions = "Images:*.png,*.jpg,*.tiff,*.bmp",
                AllowMultiple = false
            };

            var filePaths = await Interactions.OpenFileInteraction.Handle(dialog);

            if (filePaths != null && filePaths.Length > 0)
            {
                ImagePath = filePaths.FirstOrDefault();
            }
        }

        private Guard SaveGuard()
        {
            return new Guard
            {
                GuardID = this.GuardID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                ImagePath = this.ImagePath  
            };
        }
    }
}