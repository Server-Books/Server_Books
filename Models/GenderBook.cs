namespace Server_Books.Models{
    public class GenderBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int GenderId { get; set; }

        public Gender Gender { get; set; }
        public Book Book { get; set; }
    }
}