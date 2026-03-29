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
    public class ShoplifterViewModel : ReactiveObject
    {
        [Reactive]
        public string? PersonalID { get; set; }
        [Reactive]
        public bool IsKnown { get; set; }
        [Reactive]
        public string? FirstName { get; set; }
        [Reactive]
        public string? LastName { get; set; }
        [Reactive]
        public string? ImagePath { get; set; }
        
        public ReactiveCommand<Unit, Unit> OpenFileDialogCmd { get; }
        public ReactiveCommand<Unit, Shoplifter> SaveShoplifterCmd { get; }

        public ShoplifterViewModel(Shoplifter shoplifter) : this()
        {
            PersonalID = shoplifter.PersonalID;
            IsKnown = shoplifter.IsKnown;
            FirstName = shoplifter.FirstName;
            LastName = shoplifter.LastName;
            ImagePath = shoplifter.ImagePath;
        }

        public ShoplifterViewModel()
        {
            OpenFileDialogCmd = ReactiveCommand.CreateFromTask(OpenFileDialogAsync);
            SaveShoplifterCmd = ReactiveCommand.Create(SaveShoplifter,this.WhenAnyValue(x => x.FirstName,x => x.LastName,
                                                                                       (firstName,lastName) =>
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

        private Shoplifter SaveShoplifter()
        {
            return new Shoplifter
            {
                PersonalID = this.PersonalID,
                IsKnown = this.IsKnown, 
                FirstName = this.FirstName,
                LastName = this.LastName,
                ImagePath = this.ImagePath,
            };
        }
    }
}