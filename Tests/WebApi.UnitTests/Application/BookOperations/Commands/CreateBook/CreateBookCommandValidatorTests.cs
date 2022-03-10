using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestsSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
     
        [Theory]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lor", 0, 0)]
        [InlineData("Lord",0,1)]
        [InlineData(" ", 100, 1)]
        //[InlineData("Lord Of The Rings", 100, 1)] Hata olmaması durumu
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pageCount,int genreId)
        {
            //arrange
            CreateBookCommand cmd=new CreateBookCommand(null,null); //burada context ve mapper ile ilgilenmediğimiz için null yolladık
            //Yukarıdaki nesneyi oluşturma sebebimiz validator'un command'i alıp valide etmesi.
            
            cmd.Model=new CreateBookModel() //hata üretecek değerler verdik.PageCount un 0 dan büyük olması gerekiyor ama 0 gibi.
            {
                Title=title,
                PageCount=pageCount,
                GenreId=genreId,
                PublishDate=System.DateTime.Now.Date.AddYears(-1)
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result=validator.Validate(cmd);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0); //hata sayısı 0 dan büyükse test kırılır. 
        }
        
        [Fact]
        public void WhenDateTimeEqualNoewIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateBookCommand cmd = new CreateBookCommand(null, null); //burada context ve mapper ile ilgilenmediğimiz için null yolladık
                                                                       //Yukarıdaki nesneyi oluşturma sebebimiz validator'un command'i alıp valide etmesi.

            cmd.Model = new CreateBookModel() 
            {
                Title = "Hatasız Deger",
                PageCount = 100,
                GenreId = 2,
                PublishDate = System.DateTime.Now.Date
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(cmd);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0); //hata sayısı 0 dan büyükse test kırılır.
        }



        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError() //Happy
        {
        
            CreateBookCommand cmd = new CreateBookCommand(null, null); 

            cmd.Model = new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 100,
                GenreId = 2,
                PublishDate = System.DateTime.Now.Date.AddYears(-2)
            };
    
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(cmd);
            
            result.Errors.Count.Should().Be(0); //hata sayısı 0 ise
        }
    }
}