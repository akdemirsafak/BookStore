using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; } 
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genre=_context.Genres
                //.Include(x=>x.Books)
                    .SingleOrDefault(x => x.IsActivate && x.Id.Equals(GenreId));
            if (genre is null)
            {
                throw new Exception("Kitap türü bulunamadı");
            }
            var obj=_mapper.Map<GenreDetailViewModel>(genre);
            return obj;
        }


    }

    public class GenreDetailViewModel
    {
        public string Name { get; set; }
        //public List<Book> Books{get;set;}
        
    }
}