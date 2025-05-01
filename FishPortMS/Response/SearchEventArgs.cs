using MudBlazor;

namespace FishPortMS.Client.Response
{
    public class SearchEventArgs<T>
    {
        public string Search { get; set; }
        public MudTable<T> Table { get; set; }
    }
}
