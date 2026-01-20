using Library.Actions;

namespace Library.Queries
{
    public class RootQuery
    {
        //public AuthQuery Auth { get; set; }
        public AuthorsQuery Authors { get; set; }
        public BooksQuery Books { get; set; }
        public NotesQuery Notes { get; set; }
        public PublishersQuery Publishers { get; set; }
        public InventoryLocationQuery InventoryLocation { get; set; }
        public ShipmentsQuery Shipments { get; set; }
        public TrackMapQuery TrackMap { get; set; }

        public RootQuery(AuthQuery auth,
                         AuthorsQuery authorsQuery,
                         BooksQuery booksQuery,
                         NotesQuery notesQuery,
                         PublishersQuery publishers,
                         InventoryLocationQuery inventoryLocation,
                         ShipmentsQuery shipments,
                         TrackMapQuery trackMap)
        {
            //Auth = auth;
            Authors = authorsQuery;
            Books = booksQuery;
            Notes = notesQuery;
            Publishers = publishers;
            InventoryLocation = inventoryLocation;
            Shipments = shipments;
            TrackMap = trackMap;
        }
    }
}
