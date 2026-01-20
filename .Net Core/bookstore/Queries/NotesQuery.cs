using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Context;
using Microsoft.Data.SqlClient;

namespace Library.Queries
{
    public class NotesQuery
    {

        public NotesQuery()
        {
            
        }



        public Note? GetNoteById([Service] LibraryDbContext context, int id)
        {
            try
            {
                return context.Notes.FirstOrDefault(n => n.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Note> GetNotesByBookId([Service] LibraryDbContext context, int bookId)
        {
            try
            {
                var notes = context.Notes.FromSqlRaw(
                                    "EXEC GetNotesByBookId @Id",
                                    new SqlParameter("@Id", bookId)
                                ).ToList();

                return notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Note> GetNotesByTheMessage([Service] LibraryDbContext context, string theMessage)
        {
            try
            {
                var notes = context.Notes.FromSqlRaw(
                                    "EXEC GetNotesByTheMessage @TheMessate",
                                    new SqlParameter("@TheMessate", theMessage)
                                ).ToList();

                return notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Note> GetNotesByNickname([Service] LibraryDbContext context, int? bookId, string? nickname)
        {
            try
            {
                var bookIdParam = new SqlParameter("@Id", bookId ?? (object)DBNull.Value);
                var nicknameParam = new SqlParameter("@Nickname", nickname ?? (object)DBNull.Value);

                var notes = context.Notes.FromSqlRaw(
                                    "EXEC GetNotesByNickname @Id, @nickname",
                                    bookIdParam,
                                    nicknameParam
                                ).ToList();

                return notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
