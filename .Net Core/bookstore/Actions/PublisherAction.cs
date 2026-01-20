using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class PublisherAction
    {
        
        public PublisherAction()
        {
            
        }



        public Publisher AddNewPublisher([Service] LibraryDbContext context, Publisher publisher)
        {
            try
            {
                context.Publishers.Add(publisher);
                context.SaveChanges();
                context.Entry(publisher).Reload();

                return publisher;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Publisher UpdatePublisher([Service] LibraryDbContext context, Publisher publisher)
        {
            try
            {
                context.Publishers.Update(publisher);
                context.SaveChanges();
                context.Entry(publisher).Reload();

                return publisher;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
