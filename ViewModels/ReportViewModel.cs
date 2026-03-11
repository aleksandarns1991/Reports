using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reports.Models;
using Reports.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reports.ViewModels
{
    public class ReportViewModel : ReactiveObject
    {
        [Reactive]
        public string? Title { get; set; }
        [Reactive]
        public int StoreNumber { get; set; }
        [Reactive]
        public DateTime? CreatedAt { get; set; }
        [Reactive]
        public string? Description { get; set; }
        [Reactive]
        public string? Manager { get; set; }
        [Reactive]
        public bool IsPaid { get; set; }
        [Reactive]
        public bool IsReported { get; set; }

        public ObservableCollection<Product> Products { get; } = new();
        [Reactive]
        public Product? SelectedProduct { get; set; }

        private readonly ObservableAsPropertyHelper<decimal> total;
        public decimal Total => total.Value;

        public ReactiveCommand<Unit, Unit> AddProductCmd { get; }
        public ReactiveCommand<Unit, Unit> EditProductCmd { get; }
        public ReactiveCommand<Unit, Unit> DeleteProductCmd { get; }
        public ReactiveCommand<Unit, Report> SaveReportCmd { get; }

        public ReportViewModel(Report? report) : this()
        {
            Title = report!.Title;
            StoreNumber = report.StoreNumber; 
            CreatedAt = report.CreatedAt;
            Description = report.Description;
            Manager = report.Manager;
            IsPaid = report.IsPaid;
            IsReported = report.IsReported;
            
            foreach(var product in report.Products)
            {
                Products.Add(product);
            }

            SelectedProduct = Products.FirstOrDefault();
        }

        public ReportViewModel()
        {
            var canModifyProduct = this.WhenAnyValue(x => x.SelectedProduct).Select(x => x != null);
            var canSaveReport = this.WhenAnyValue(x => x.Title, x => x.StoreNumber, x => x.CreatedAt, x => x.Manager,x => x.Description,x => x.Products.Count,
                                                 (title,store,created,manager,description,count) => 
                                                 !string.IsNullOrEmpty(title)
                                              && store > 0 
                                              && created != null
                                              && !string.IsNullOrEmpty(manager)
                                              && !string.IsNullOrEmpty(description)
                                              && count > 0);

            AddProductCmd = ReactiveCommand.CreateFromTask(AddProductAsync);
            EditProductCmd = ReactiveCommand.CreateFromTask(EditProductAsync, canModifyProduct);
            DeleteProductCmd = ReactiveCommand.Create(DeleteProduct, canModifyProduct);
            SaveReportCmd = ReactiveCommand.Create(SaveReport,canSaveReport);

            total = this.WhenAnyValue(x => x.SelectedProduct, x => x.SelectedProduct!.Price).Select(_ => Products.Sum(p => p.Price)).ToProperty(this, vm => vm.Total);
        }

        private async Task AddProductAsync()
        {
            var vm = new ProductViewModel();
            var product = await Interactions.ProductInteraction.Handle(vm);

            if (product != null)
            {
                Products.Add(product);
                SelectedProduct = product;
            }
        }

        private async Task EditProductAsync()
        {
            var vm = new ProductViewModel(SelectedProduct);
            var product = await Interactions.ProductInteraction.Handle(vm);

            if (product != null)
            {
                SelectedProduct!.ID = product.ID;
                SelectedProduct.Title = product.Title; 
                SelectedProduct.Quantity = product.Quantity;
                SelectedProduct.Price = product.Price;
            }
        }

        private void DeleteProduct()
        {
            Products.Remove(SelectedProduct!);
            SelectedProduct = Products.FirstOrDefault();
        }

        private Report SaveReport()
        {
            return new Report
            {
                Title = this.Title,
                StoreNumber = this.StoreNumber,
                CreatedAt = this.CreatedAt,
                Description = this.Description,
                Manager = this.Manager,
                IsPaid = this.IsPaid,
                IsReported = this.IsReported,   
                Products = new ObservableCollection<Product>(Products)
            };
        }
    }
}