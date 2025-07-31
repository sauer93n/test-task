namespace Domain.Entity;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<Offer> Offers { get; set; }
}
