namespace Library.Models;
public class Book
{
    public int? Id { get; set; }
    public int Author { get; set; }
    public string? Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Publisher { get; set; }
    public string Edition { get; set; }
    public int Stock { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}