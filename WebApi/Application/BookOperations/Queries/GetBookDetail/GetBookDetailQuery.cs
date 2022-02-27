using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetBookDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper=mapper;
        }

        public int BookId{ get; set; }
        public BookDetailViewModel Handle()
        {

            var book = _dbContext.Books
                .Include(x=>x.Genre)
                .Include(x=>x.Author)
                    .SingleOrDefault(y=>y.BookId==BookId);
            if (book is null)
            {
                throw new Exception("Bu kitap bulunamadı.");
            }

            var book_Dvm=_mapper.Map<BookDetailViewModel>(book);
            

            return book_Dvm;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
            public string Author { get; set; }
        }


    }
}
