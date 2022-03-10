using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int authorId;
        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var author=_context.Authors
                //.Include(y=>y.Books)
                    .SingleOrDefault(x=>x.Id==authorId);
            if(author is null)
            {
                throw new Exception("Yazar bulunamadı");
            }
            var authorDetailViewModel=_mapper.Map<AuthorDetailViewModel>(author);
           
            return authorDetailViewModel;

        }

        
    }
    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
       

    }
}