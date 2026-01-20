using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Queries
{
    public class PublishersQuery
    {

        public PublishersQuery()
        {
            
        }



        public Publisher? GetPublisherById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.Publishers.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Publisher> GetPublishersByCode([Service] LibraryDbContext context, string code, int position)
        {
            try
            {
                var publishers = context.Publishers.FromSqlRaw(
                                    "EXEC GetPublishersByCode @Code, @Position",
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

        public List<Publisher> GetPublishersByName([Service] LibraryDbContext context, string name)
        {
            try
            {
                var publishers = context.Publishers.FromSqlRaw(
                                    "EXEC GetPublishersByName @Name",
                                    new SqlParameter("@Name", name)
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
