namespace Server_Books.Models{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public int CopiesAvailable { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

        public ICollection<GenderBook> GenderBooks { get; set; }
        public ICollection<BookLending> BookLendings { get; set; }
    }
}
