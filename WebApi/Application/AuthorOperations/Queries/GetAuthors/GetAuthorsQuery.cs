using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {  
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors=_context.Authors.OrderByDescending(x=>x.Id) //Son eklenen en üstte görünür.
            //.Include(x=>x.Books)
            .ToList();
            List<AuthorsViewModel> authorsViewModel = _mapper.Map<List<AuthorsViewModel>>(authors);
            return authorsViewModel;

        }
    }
    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }

        //public List<Book> Books { get; set; }
    
        
    }
}