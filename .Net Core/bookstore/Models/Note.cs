namespace Library.Models;
public class Note
{
    public int? Id { get; set; }
    public int Book { get; set; }
    public string TheMessage { get; set; }
    public string Nickname { get; set; }
    public int Page { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}