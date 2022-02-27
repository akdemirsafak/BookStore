using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {


            var book = _dbContext.Books.Find(BookId);
            if (book is null)
            {
                throw new Exception("Bu kitap bulunamadı.");
            }

            //güncelleme işlemi
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;

            _dbContext.SaveChanges();


        }


        public class UpdateBookModel
        {
            
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public int AuthorId { get; set; }
           

        }
    }
}