using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Queries
{
    public class ShipmentsQuery
    {

        public ShipmentsQuery()
        {
            
        }



        public Shipment? GetShipmentsById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.Shipments.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Shipment> GetShipmentsByCode([Service] LibraryDbContext context, string code)
        {
            try
            {
                var shipments = context.Shipments.FromSqlRaw(
                                    "EXEC GetShipmentsByCode @Code",
                                    new SqlParameter("@Code", code)
                                ).ToList();

                return shipments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
