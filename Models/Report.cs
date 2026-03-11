using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reports.Models
{
    public class Report : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public string? Title { get; set; }
        [JsonProperty]
        [Reactive]
        public int StoreNumber { get; set; }
        [JsonProperty]
        [Reactive]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [JsonProperty]
        [Reactive]
        public string? Description { get; set; }
        [JsonProperty]
        [Reactive]
        public string? Manager { get; set; }
        [JsonProperty]
        [Reactive]
        public bool IsPaid { get; set; }
        [JsonProperty]
        [Reactive]
        public bool IsReported { get; set; }
        [JsonProperty]
        public ObservableCollection<Product> Products { get; set; }
        public decimal Total => Products.Sum(p => p.Price * p.Quantity);
    }
}