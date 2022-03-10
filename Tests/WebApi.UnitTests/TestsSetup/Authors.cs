using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Authors
    {
        
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { Name = "Peyami", LastName = "Safa", BirthDate = new DateTime(1900, 5, 28) },
                new Author { Name = "Sabahattin", LastName = "Ali", BirthDate = new DateTime(1997, 3, 19) },
                new Author { Name = "Oğuz", LastName = "Atay", BirthDate = new DateTime(1500, 12, 17) }
            );
        }
    }
    
}