namespace Library.Actions
{
    public class RootAction
    {
        public AuthorsAction Authors { get; set; }
        public BooksAction Books { get; set; }
        public PublisherAction Publishers { get; set; }
        public NoteAction Notes { get; set; }
        public InventoryLocationAction InventoryLocations { get; set; }
        public ShipmentAction Shipments { get; set; }
        public TrackMapAction TrackMap { get; set; }

        public RootAction(AuthorsAction authorsAction, 
                          BooksAction booksAction,
                          PublisherAction publisherAction,
                          NoteAction noteAction,
                          InventoryLocationAction inventoryLocationAction,
                          ShipmentAction shipmentAction,
                          TrackMapAction trackMapAction)
        {
            Authors = authorsAction;
            Books = booksAction;
            Publishers = publisherAction;
            Notes = noteAction;
            InventoryLocations = inventoryLocationAction;
            Shipments = shipmentAction;
            TrackMap = trackMapAction;
        }
    }
}
