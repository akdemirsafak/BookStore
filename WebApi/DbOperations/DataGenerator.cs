using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context=new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())//eğer data varsa
                {
                 return;   
                }
                else
                {

                    context.Authors.AddRange(
                        new Author { Name = "Peyami",LastName="Safa",BirthDate=new DateTime(1900,5,28) },
                        new Author { Name = "Sabahattin",LastName="Ali", BirthDate = new DateTime(1997, 3, 19) },
                        new Author { Name = "Oğuz",LastName="Atay" , BirthDate = new DateTime(1500, 12, 17) }
                    );
                    context.Genres.AddRange(
                        new Genre{ Name="Personel Growth"},
                        new Genre { Name = "Science Fiction" },
                        new Genre { Name = "Romance" }
                    );
                    context.Books.AddRange(

                        new Book{GenreId=1,Title="Lean Startup",PageCount=200,PublishDate=new DateTime(2001,06,12),AuthorId=1},
                        new Book {GenreId = 2, Title = "Herland", PageCount = 250, PublishDate = new DateTime(1997, 03, 19), AuthorId = 2 },
                        new Book {GenreId = 2, Title = "Dune", PageCount = 540, PublishDate = new DateTime(2010, 04, 25), AuthorId = 2 }

                        );
                    context.SaveChanges();
                }
            }
        }
    }
}