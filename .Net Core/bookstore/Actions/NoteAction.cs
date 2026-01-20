using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Actions
{
    public class NoteAction
    {
        
        public NoteAction()
        {
            
        }



        public Note AddNewNote([Service] LibraryDbContext context, Note note)
        {
            try
            {
                context.Notes.Add(note);
                context.SaveChanges();
                context.Entry(note).Reload();

                return note;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Note UpdateNote([Service] LibraryDbContext context, Note note)
        {
            try
            {
                context.Notes.Update(note);
                context.SaveChanges();
                context.Entry(note).Reload();

                return note;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
