using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model;
        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author=_context.Authors.FirstOrDefault(x=>x.Name.Trim()==Model.Name.Trim() && x.LastName.Trim() ==Model.LastName.Trim());
            if (author is not null)
            {
                throw new InvalidOperationException("This author already have.");
            }
            author=_mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}