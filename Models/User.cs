using System.Text.Json.Serialization;

namespace Server_Books.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }

        public Role? Role { get; set; }
        [JsonIgnore]
        public ICollection<BookLending>? BookLendings { get; set; }
    }
}

