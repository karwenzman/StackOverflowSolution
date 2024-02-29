namespace SampleConverterCulture.Models;

public class ReceiptModel : ModelBase
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public DateOnly Date { get; set; }
}
