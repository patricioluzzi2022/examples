using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Queries
{
    public class InventoryLocationQuery
    {

        public InventoryLocationQuery()
        {
            
        }



        public InventoryLocation? GetInventoryLocationById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.InventoryLocations.FirstOrDefault(il => il.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<InventoryLocation> GetInventoryLocationByCode([Service] LibraryDbContext context, string code, int position)
        {
            try
            {
                var publishers = context.InventoryLocations.FromSqlRaw(
                                    "EXEC GetInventoryLocationByCode @Code, @Position",
                                    new SqlParameter("@Code", code),
                                    new SqlParameter("@Position", position)
                                ).ToList();

                return publishers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
