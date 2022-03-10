using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
           
            context.Books.AddRange(

                new Book { GenreId = 1, Title = "Lean Startup", PageCount = 200, PublishDate = new DateTime(2001, 06, 12), AuthorId = 1 },
                new Book { GenreId = 2, Title = "Herland", PageCount = 250, PublishDate = new DateTime(1997, 03, 19), AuthorId = 2 },
                new Book { GenreId = 2, Title = "Dune", PageCount = 540, PublishDate = new DateTime(2010, 04, 25), AuthorId = 2 }

                );
            
        }
    }
}