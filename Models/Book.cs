using System.ComponentModel.DataAnnotations;

namespace ServerBooks.Models{
    public class Book{
        public int Id { get; set;}
        [Required(ErrorMessage = "Title es requerido")]
        public string Title { get; set;}
        [Required(ErrorMessage = "Author es requerido")]
        public string Author {get; set;}
        [Required(ErrorMessage = "PublicationDate es requerido")]
        public DateOnly PublicationDate { get; set;}
        [Required(ErrorMessage = "Copies es requerido")]
        public int CopiesAvailable {get; set;}
        
        public string Status {get; set;}

        
    }
}