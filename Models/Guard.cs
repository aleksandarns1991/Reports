using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Reports.Models
{
    public class Guard : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public string? GuardID { get; set; }
        [JsonProperty]
        [Reactive]
        public string? FirstName { get; set; }
        [JsonProperty]
        [Reactive]
        public string? LastName { get; set; }
        [JsonProperty]
        [Reactive]
        public string? ImagePath { get; set; }
        [JsonProperty]
        public ObservableCollection<Report> Reports { get; } = new();

        private readonly ObservableAsPropertyHelper<string?> fullName; 
        public string? FullName => fullName.Value;

        public Guard()
        {
            fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName).Select(item => $"{item.Item1} {item.Item2}").ToProperty(this, vm => vm.FullName);
        }
    }
}