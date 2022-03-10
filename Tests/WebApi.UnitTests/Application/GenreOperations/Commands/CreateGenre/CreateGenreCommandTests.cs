using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var  genre=new Genre(){
                Name="New Genre"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand cmd = new CreateGenreCommand(_context, _mapper);
            cmd.Model=new CreateGenreModel(){Name=genre.Name};

            //act && assert
            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already have.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()//happy
        {
            //Arrange
            CreateGenreCommand cmd = new CreateGenreCommand(_context, _mapper);
            cmd.Model = new CreateGenreModel() { Name = "New Genre"};

            //act
            FluentActions.Invoking(() => cmd.Handle()).Invoke(); //dönüşü kontrol etmez.

            //Assert
            var genre = _context.Genres.SingleOrDefault(gnr => gnr.Name == cmd.Model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().NotBeEmpty();
            
            // Name min length and max length
            

        }

    }
}