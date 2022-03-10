using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options=new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            Context=new BookStoreDbContext(options);
            //Burada var olan BookStoreDbContext'i yeniden ayarlamış olduk.
            Context.Database.EnsureCreated();

            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();
            

            Mapper=new MapperConfiguration(cfg=>{cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}