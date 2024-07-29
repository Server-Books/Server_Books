namespace Server_Books.Models{
    public class Rol{
        public int Id { get; set;}
        public string Name_rol {get; set;}

        public List<User> Users { get; set;}

    }
}