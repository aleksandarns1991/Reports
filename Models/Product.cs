using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Reports.Models
{
    public class Product : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public string? ID { get; set; }
        [JsonProperty]
        [Reactive]
        public string? Title { get; set; }
        [JsonProperty]
        [Reactive]
        public int Quantity { get; set; }
        [JsonProperty]
        [Reactive]
        public decimal Price { get; set; }
    }
}
