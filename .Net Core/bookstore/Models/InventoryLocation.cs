namespace Library.Models;
public class InventoryLocation
{
    public int? Id { get; set; }
    public int Book { get; set; }
    public string Shelf { get; set; }
    public string Code { get; set; }
    public decimal Row { get; set; }
    public decimal Column { get; set; }
    public string? Branch { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}