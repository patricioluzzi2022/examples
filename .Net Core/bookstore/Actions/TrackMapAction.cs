using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class TrackMapAction
    {
        
        public TrackMapAction()
        {
            
        }



        public TrackMap AddNewTrackMap([Service] LibraryDbContext context, TrackMap trackMap)
        {
            try
            {
                context.TrackMap.Add(trackMap);
                context.SaveChanges();
                context.Entry(trackMap).Reload();

                return trackMap;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public TrackMap UpdateTrackMap([Service] LibraryDbContext context, TrackMap trackMap)
        {
            try
            {
                context.TrackMap.Update(trackMap);
                context.SaveChanges();
                context.Entry(trackMap).Reload();

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
