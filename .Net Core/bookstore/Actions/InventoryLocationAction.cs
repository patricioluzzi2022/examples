using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class InventoryLocationAction
    {
        
        public InventoryLocationAction()
        {
            
        }



        public InventoryLocation AddNewInventoryLocation([Service] LibraryDbContext context, InventoryLocation inventoryLocation)
        {
            try
            {
                context.InventoryLocations.Add(inventoryLocation);
                context.SaveChanges();
                context.Entry(inventoryLocation).Reload();

                return inventoryLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public InventoryLocation UpdateInventoryLocation([Service] LibraryDbContext context, InventoryLocation inventoryLocation)
        {
            try
            {
                context.InventoryLocations.Update(inventoryLocation);
                context.SaveChanges();
                context.Entry(inventoryLocation).Reload();

                return inventoryLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
