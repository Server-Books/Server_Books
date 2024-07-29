namespace Server_Books.Models{
    public class Book{
        public int Id { get; set;}
        public string Title { get; set;}
        public string Author {get; set;}
        public DateOnly PublicationDate { get; set;}
        public int CopiesAvailable {get; set;}
        public string Status {get; set;}

        
    }
}