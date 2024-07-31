namespace Server_Books.Models{
    public class Gender
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<GenderBook>? GenderBooks { get; set; }
    }
}