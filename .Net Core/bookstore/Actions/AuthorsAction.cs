using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class AuthorsAction
    {

        public AuthorsAction() {
            
        }



        public Author AddNewAuthor([Service] LibraryDbContext context, Author author)
        {
            try
            {
                context.Authors.Add(author);
                context.SaveChanges();
                context.Entry(author).Reload();

                return author;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Author UpdateAuthor([Service] LibraryDbContext context, Author author)
        {
            try
            {
                context.Authors.Update(author);
                context.SaveChanges();
                context.Entry(author).Reload();

                return author;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
