using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace Reports.Models
{
    public class Guard : Person
    {
        [JsonProperty]
        [Reactive]
        public string? GuardID { get; set; }
        [JsonProperty]
        public ObservableCollection<Report> Reports { get; } = new();
    }
}