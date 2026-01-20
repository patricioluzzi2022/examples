using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class BooksAction
    {
        
        public BooksAction()
        {
            
        }



        public Book AddNewBook([Service] LibraryDbContext context, Book book)
        {
            try
            {
                context.Books.Add(book);
                context.SaveChanges();
                context.Entry(book).Reload();

                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Book UpdateBook([Service] LibraryDbContext context, Book book)
        {
            try
            {
                context.Books.Update(book);
                context.SaveChanges();
                context.Entry(book).Reload();

                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
