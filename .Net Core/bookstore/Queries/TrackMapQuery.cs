using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Queries
{
    public class TrackMapQuery
    {

        public TrackMapQuery()
        {
            
        }



        public TrackMap? GetTrackMapById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.TrackMap.FirstOrDefault(tm => tm.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public TrackMap? GetTrackMapByCode([Service] LibraryDbContext context, string code)
        {
            try
            {
                return context.TrackMap.FirstOrDefault(tm => tm.TrackingCode == code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public TrackMap? GetTrackMapByShipmentId([Service] LibraryDbContext context, int shipmentId)
        {
            try
            {
                var trackMap = context.TrackMap.FirstOrDefault(tm => tm.ShipmentId == shipmentId);

                return trackMap;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public TrackMap? GetTrackMapByReceptionDate([Service] LibraryDbContext context, DateTime receptiontDate)
        {
            try
            {
                var trackMap = context.TrackMap.FirstOrDefault(tm => tm.ReceptionDate == receptiontDate);

                return trackMap;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
