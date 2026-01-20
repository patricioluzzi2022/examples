using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;
using HotChocolate.Authorization;

namespace Library.Queries
{
    public class AuthorsQuery
    {

        public AuthorsQuery()
        {
            
        }


        [Authorize]
        public Author? GetAuthorById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.Authors.FirstOrDefault(a => a.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Author>? GetAuthorsByPublisherId([Service] LibraryDbContext context, int publisherId)
        {
            try
            {
                var authors = context.Authors.FromSqlRaw(
                                    "EXEC GetAuthorsByPublisherId @Id",
                                    new SqlParameter("@Id", publisherId)
                                ).ToList();

                return authors;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Author>? GetAuthorsByBookId([Service] LibraryDbContext context, int bookId)
        {
            try
            {
                var authors = context.Authors.FromSqlRaw(
                                    "EXEC GetAuthorsByBookId @Id",
                                    new SqlParameter("@Id", bookId)
                                ).ToList();

                return authors;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
