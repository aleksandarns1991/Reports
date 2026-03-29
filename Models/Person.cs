using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Linq;
using System.Reactive.Linq;

namespace Reports.Models
{
    public class Person : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public string? FirstName { get; set; }
        [JsonProperty]
        [Reactive]
        public string? LastName { get; set; }
        [JsonProperty]
        [Reactive]
        public string? ImagePath { get; set; }

        private readonly ObservableAsPropertyHelper<string?> fullName;
        public string? FullName => fullName.Value;

        public Person()
        {
            fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName)
                           .Select(item => $"{item.Item1} {item.Item2}")
                           .ToProperty(this, vm => vm.FullName);
        }
    }
}
