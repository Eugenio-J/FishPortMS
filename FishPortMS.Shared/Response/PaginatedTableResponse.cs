namespace FishPortMS.Shared.Response
{
    public class PaginatedTableResponse<T> {
        public List<T> ResponseData  { get; set; } = new List<T>();
        public int Count { get; set; }
    }
}
