using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class ShipmentAction
    {
        
        public ShipmentAction()
        {
            
        }



        public Shipment AddNewShipment([Service] LibraryDbContext context, Shipment shipment)
        {
            try
            {
                context.Shipments.Add(shipment);
                context.SaveChanges();
                context.Entry(shipment).Reload();

                return shipment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Shipment UpdateShipment([Service] LibraryDbContext context, Shipment shipment)
        {
            try
            {
                context.Shipments.Update(shipment);
                context.SaveChanges();
                context.Entry(shipment).Reload();

                return shipment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
