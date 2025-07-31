namespace Domain.Entity;

public class Offer : BaseEntity
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public DateTime RegistrationDate { get; set; }
}