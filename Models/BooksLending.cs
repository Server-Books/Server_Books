namespace Server_Books.Models{
    public class BookLending
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}