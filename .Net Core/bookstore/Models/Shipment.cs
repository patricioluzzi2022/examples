namespace Library.Models;
public class Shipment
{
    public int? Id { get; set; }
    public string OriginLocations { get; set; }
    public string DestinationLocations { get; set; }
    public string Notes { get; set; }
    public decimal Budget { get; set; }
    public string Code { get; set; }
    public int Stock { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime ReservationDate { get; set; }
    public bool Confirmation { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}