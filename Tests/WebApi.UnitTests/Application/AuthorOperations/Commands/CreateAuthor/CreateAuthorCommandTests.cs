using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper=commonTestFixture.Mapper;
        }


        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()//okuyunca anlaşılabilir bir fonksiyon adı
        {

            //arrange (hazırlık)
            var author = new Author()
            {
                Name = "New Author",
                LastName = "LastName",
                BirthDate = new System.DateTime(1990, 12, 10),
             
            };
            _context.Authors.Add(author);
            _context.SaveChanges();


            CreateAuthorCommand cmd = new CreateAuthorCommand(_context, _mapper);
            cmd.Model = new CreateAuthorModel() 
                {   Name = author.Name,
                    LastName=author.LastName,
                    BirthDate=author.BirthDate 
                };

            //act & assert (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => cmd.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This author already have.");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()//happy
        {
            //Arrange
            CreateAuthorCommand cmd = new CreateAuthorCommand(_context, _mapper);
            cmd.Model = new CreateAuthorModel() { 
                Name = "Author Name",
                LastName = "Author LastName",
                BirthDate = DateTime.Now.Date.AddYears(-20)
            };

            //act
            FluentActions.Invoking(() => cmd.Handle()).Invoke(); //dönüşü kontrol etmez.

            //Assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == cmd.Model.Name);
            author.Should().NotBeNull();
            //author.Name.Should().Be(cmd.Model.Name); //Zaten bu data name e göre alındı bu şart sağlanıyor.
            author.LastName.Should().Be(cmd.Model.LastName);
            author.BirthDate.Should().Be(cmd.Model.BirthDate);

        }




    }
}