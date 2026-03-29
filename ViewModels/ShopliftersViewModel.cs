using ReactiveUI;
using Reports.Models;
using System.Collections.Generic;

namespace Reports.ViewModels
{
    public class ShopliftersViewModel : ReactiveObject
    {
        public IEnumerable<Shoplifter> Shoplifters { get; }

        public ShopliftersViewModel(IEnumerable<Shoplifter> shoplifters)
        {
            Shoplifters = shoplifters;
        }
    }
}
