namespace Library.Models;
public class TrackMap
{
    public int? Id { get; set; }
    public string TrackingCode { get; set; }
    public int ShipmentId { get; set; }
    public DateTime ReceptionDate { get; set; }
    public string Notes { get; set; }
    public string DocumentPath { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}