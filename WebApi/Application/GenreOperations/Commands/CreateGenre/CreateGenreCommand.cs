using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    
    
    public class CreateGenreCommand
    {
        public CreateGenreModel Model{ get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=>x.Name.Equals(Model.Name));
            if (genre is not null)
            {
                throw new Exception("Kitap türü zaten mevcut.");
            }

            genre=new Genre();
            genre.Name=Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();

        }


    }
    
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }


}