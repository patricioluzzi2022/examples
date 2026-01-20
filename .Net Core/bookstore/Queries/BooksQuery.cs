using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Queries
{
    public class BooksQuery
    {

        public BooksQuery()
        {
            
        }



        public Book? GetBookById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.Books.FirstOrDefault(b => b.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Book> GetBooksByPublisherId([Service] LibraryDbContext context, int publisherId)
        {
            try
            {
                var books = context.Books.FromSqlRaw(
                                    "EXEC GetBooksByPublisherId @Id",
                                    new SqlParameter("@Id", publisherId)
                                ).ToList();

                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Book> GetBookByAuthorId([Service] LibraryDbContext context, int authorId)
        {
            try
            {
                var books = context.Books.FromSqlRaw(
                                    "EXEC GetBookByAuthorId @Id",
                                    new SqlParameter("@Id", authorId)
                                ).ToList();

                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
