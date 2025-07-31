namespace Application.Model;

public class SearchResult<T>
{
    private int count;

    public List<T> Values { get; set; }

    public int Count { get => Values.Count; set => count = value; }

    public SearchResult(List<T> values)
    {
        Values = values;
    }
}