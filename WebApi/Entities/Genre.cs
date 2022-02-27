using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Primary key
        public int Id { get; set; }
        public string  Name { get; set; }
        public bool IsActivate { get; set; }=true;
        
        //public List<Book> Books { get; set; }
    }
}