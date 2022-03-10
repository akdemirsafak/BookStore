using System;
using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context=commonTestFixture.Context;
            _mapper=commonTestFixture.Mapper;
        } 
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()//okuyunca anlaşılabilir bir fonksiyon adı
        {

            //arrange (hazırlık)
            var book=new Book(){
                Title="WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",PageCount=100,PublishDate=new System.DateTime(1990,12,10),GenreId=1
            };
            _context.Books.Add(book);
            _context.SaveChanges();


            CreateBookCommand cmd=new CreateBookCommand(_context,_mapper);
            cmd.Model=new CreateBookModel(){Title=book.Title};

            //act & assert (Çalıştırma ve Doğrulama)
                FluentActions
                    .Invoking(()=>cmd.Handle())
                        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already have.");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()//happy
        {
            //Arrange
            CreateBookCommand cmd = new CreateBookCommand(_context, _mapper);
            cmd.Model = new CreateBookModel() { Title = "Hobbit",PageCount=100,PublishDate=DateTime.Now.Date.AddYears(-10),GenreId=1 };

            //act
            FluentActions.Invoking(()=>cmd.Handle()).Invoke(); //dönüşü kontrol etmez.
            
            //Assert
            var book=_context.Books.SingleOrDefault(book=>book.Title==cmd.Model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(cmd.Model.PageCount);
            book.PublishDate.Should().Be(cmd.Model.PublishDate);
            //book.Title.Should().Be(cmd.Model.Title);  //book u alırken title ile eşleştirip çektiğimiz için bu kurala ihtiyaç yok.
            book.GenreId.Should().Be(cmd.Model.GenreId);

        }
    }
}