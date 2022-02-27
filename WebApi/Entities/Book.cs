using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string Title { get; set; }=string.Empty;
        public int PageCount { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
        
        public Author Author { get; set; }
        

    }
}