namespace Application.Model.DTO;

public class SupplierDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public List<int> OfferIds { get; set; }
}