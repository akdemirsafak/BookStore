using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
           var books=_dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x => x.Title).ToList();

           List<BooksViewModel> booksViewModel=_mapper.Map<List<BooksViewModel>>(books);
    
           return booksViewModel;
        }
    }

    public class BooksViewModel{
        
        public string Title { get; set; } 
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}