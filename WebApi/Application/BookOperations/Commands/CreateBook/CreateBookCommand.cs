using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {

        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.Where(x => x.Title == Model.Title).FirstOrDefault();
            if (book is not null) //eğer booklistte bu veri varsa eklenmesin.
            {
               throw new InvalidOperationException("Book already have.");
            }
            book=_mapper.Map<Book>(Model); //CreateBookModel türündeki nesneyi Book a çevirir.
            
      
            _dbContext.Books.Add(book); //Burada ekleme işlemi
            _dbContext.SaveChanges();
        }


    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }

    }

}