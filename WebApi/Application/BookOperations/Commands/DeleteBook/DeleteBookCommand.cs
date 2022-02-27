using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId{get;set;}
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Handle()
        {
            var book = _dbContext.Books.Find(BookId);

            if (book is null) //book nesnesi boş değilse 
            {
                throw new Exception("Bu kitap bulunamadı");   
            }
            else{
                //silme işlemi
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }


        }
    }
}