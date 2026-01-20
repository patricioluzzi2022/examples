using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Context
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<InventoryLocation> InventoryLocations { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<TrackMap> TrackMap { get; set; }

        public LibraryDbContext() { }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Authors");
            modelBuilder.Entity<Author>().ToTable(tb => tb.HasTrigger("AuthorsTriggerAferInsert"));
            modelBuilder.Entity<Author>().ToTable(tb => tb.HasTrigger("AuthorsTriggerAferUpdate"));
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(54);
                entity.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(54);
                entity.Property(e => e.Nationality).HasColumnName("Nationality").HasMaxLength(54);
                entity.Property(e => e.BirthDate).HasColumnName("BirthDate").IsRequired();
                entity.Property(e => e.CreationDate).HasColumnName("CreationDate");
                entity.Property(e => e.ModificationDate).HasColumnName("ModificationDate");
            });

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().ToTable(tb => tb.HasTrigger("BooksTriggerAferInsert"));
            modelBuilder.Entity<Book>().ToTable(tb => tb.HasTrigger("BooksTriggerAferUpdate"));
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Author).HasColumnName("Author").IsRequired();
                entity.Property(e => e.Code).HasColumnName("Code").HasMaxLength(21);
                entity.Property(e => e.Title).HasColumnName("Title").IsRequired().HasMaxLength(365);
                entity.Property(e => e.Description).HasColumnName("Description").IsRequired(); //
                entity.Property(e => e.Publisher).HasColumnName("Publisher").IsRequired();
                entity.Property(e => e.Edition).HasColumnName("Edition").HasMaxLength(221);
                entity.Property(e => e.Stock).HasColumnName("Stock").IsRequired();
                entity.Property(e => e.CreationDate).HasColumnName("CreationDate");
                entity.Property(e => e.ModificationDate).HasColumnName("ModificationDate");
            });

            modelBuilder.Entity<Publisher>().ToTable("Publishers");
            modelBuilder.Entity<Publisher>().ToTable(tb => tb.HasTrigger("PublishersTriggerAferInsert"));
            modelBuilder.Entity<Publisher>().ToTable(tb => tb.HasTrigger("PublishersTriggerAferUpdate"));
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(700);
                entity.Property(e => e.CreationDate);
                entity.Property(e => e.ModificationDate);
            });

            modelBuilder.Entity<Note>().ToTable("Notes");
            modelBuilder.Entity<Note>().ToTable(tb => tb.HasTrigger("NotesTriggerAferInsert"));
            modelBuilder.Entity<Note>().ToTable(tb => tb.HasTrigger("NotesTriggerAferUpdate"));
            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Book).IsRequired();
                entity.Property(e => e.TheMessage).IsRequired(); 
                entity.Property(e => e.Nickname).IsRequired().HasMaxLength(365);
                entity.Property(e => e.Page).IsRequired();
                entity.Property(e => e.CreationDate);
                entity.Property(e => e.ModificationDate);
            });

            modelBuilder.Entity<InventoryLocation>().ToTable("InventoryLocation");
            modelBuilder.Entity<InventoryLocation>().ToTable(tb => tb.HasTrigger("InventoryLocationTriggerAferInsert"));
            modelBuilder.Entity<InventoryLocation>().ToTable(tb => tb.HasTrigger("InventoryLocationTriggerAferUpdate"));
            modelBuilder.Entity<InventoryLocation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Book).HasColumnName("Book").IsRequired();
                entity.Property(e => e.Shelf).HasColumnName("Shelf").IsRequired();
                entity.Property(e => e.Code).HasColumnName("Code").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Row).HasColumnName("RowNumber").IsRequired();
                entity.Property(e => e.Column).HasColumnName("ColumnNumber").IsRequired();
                entity.Property(e => e.Branch).HasColumnName("Branch").IsRequired();
                entity.Property(e => e.CreationDate).HasColumnName("CreationDate");
                entity.Property(e => e.ModificationDate).HasColumnName("ModificationDate");
            });

            modelBuilder.Entity<Shipment>().ToTable("Shipments");
            modelBuilder.Entity<Shipment>().ToTable(tb => tb.HasTrigger("ShipmentsTriggerAferInsert"));
            modelBuilder.Entity<Shipment>().ToTable(tb => tb.HasTrigger("ShipmentsTriggerAferUpdate"));
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OriginLocations).IsRequired().HasMaxLength(36554);
                entity.Property(e => e.DestinationLocations).IsRequired().HasMaxLength(36554);
                entity.Property(e => e.Notes).IsRequired();
                entity.Property(e => e.Budget).IsRequired(); //
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.DeliveryDate);
                entity.Property(e => e.ReservationDate);
                entity.Property(e => e.Confirmation);
                entity.Property(e => e.CreationDate);
                entity.Property(e => e.ModificationDate);
            });

            modelBuilder.Entity<TrackMap>().ToTable("TrackMap");
            modelBuilder.Entity<TrackMap>().ToTable(tb => tb.HasTrigger("TrackMapTriggerAferInsert"));
            modelBuilder.Entity<TrackMap>().ToTable(tb => tb.HasTrigger("TrackMapTriggerAferUpdate"));
            modelBuilder.Entity<TrackMap>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TrackingCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ShipmentId).IsRequired();
                entity.Property(e => e.ReceptionDate).IsRequired();
                entity.Property(e => e.Notes).IsRequired();
                entity.Property(e => e.DocumentPath).IsRequired();
                entity.Property(e => e.CreationDate);
                entity.Property(e => e.ModificationDate);
            });
        }
    }
}
