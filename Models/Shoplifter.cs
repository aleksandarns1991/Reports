using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace Reports.Models
{
    public class Shoplifter : Person
    {
        [JsonProperty]
        [Reactive]
        public string? PersonalID { get; set; }
        [JsonProperty]
        [Reactive]
        public bool IsKnown { get; set; }

        public Shoplifter()
        {
            ImagePath = "avares://Reports/Assets/Images/user.png";
        }
    }
}
