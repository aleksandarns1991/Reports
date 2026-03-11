using Avalonia.Controls;
using System.Threading.Tasks;

namespace Reports.Services
{ 
    public interface IDialog<T> where T : class
    {
        string? Title { get; set; }
        Task<T> ShowAsync(Window parent);
    }
}