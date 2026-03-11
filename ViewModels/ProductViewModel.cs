using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reports.Models;
using System.Reactive;

namespace Reports.ViewModels
{
    public class ProductViewModel : ReactiveObject
    {
        [Reactive]
        public string? ID { get; set; }
        [Reactive]
        public string? Title { get; set; }
        [Reactive]
        public int Quantity { get; set; }
        [Reactive]
        public decimal Price { get; set; }

        public ReactiveCommand<Unit, Product> SaveProductCmd { get; }

        public ProductViewModel(Product? product) : this()
        {
            ID = product!.ID;
            Title = product.Title;
            Quantity = product.Quantity;
            Price = product.Price;
        }

        public ProductViewModel()
        {
            SaveProductCmd = ReactiveCommand.Create(SaveProduct, this.WhenAnyValue(x => x.ID, x => x.Title, x => x.Quantity, x => x.Price,
                                                                                 (id, title, quantity, price) =>
                                                                                 int.TryParse(id, out int _)
                                                                              && !string.IsNullOrEmpty(title)
                                                                              && quantity > 0
                                                                              && price > 0));
        }

        private Product SaveProduct()
        {
            return new Product
            {
                ID = this.ID,
                Title = this.Title,
                Quantity = this.Quantity,
                Price = this.Price
            };
        }
    }
}