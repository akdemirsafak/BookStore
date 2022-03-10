using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int authorId;

        public DeleteAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author=_context.Authors
            .Include(x=>x.Books).SingleOrDefault(x=>x.Id==authorId);
            if (author is null)
            {
                throw new Exception("Bu yazar bulunamadı.");
            }
            if (author.Books.Any())
            {
                throw new Exception("Yazarı silmeden önce kitapları silmelisiniz");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        
    }
}