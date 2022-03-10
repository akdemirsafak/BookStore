using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int GenreId;
        public UpdateGenreModel Model{get;set;}

        public UpdateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if (genre is null)
            {
                throw new Exception("Kitap türü bulunamadı.");
            }
            if (_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id!=GenreId))
            {
                throw new Exception("Aynı isimli bir kitap türü zaten mevcut");
            }
            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActivate=Model.IsActive;
            _context.SaveChanges();

        }

    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

}