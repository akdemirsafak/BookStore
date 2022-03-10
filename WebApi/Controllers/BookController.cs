using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using WebApi.DbOperations;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Microsoft.AspNetCore.Authorization;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }



        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query=new GetBooksQuery(_context,_mapper);
            var result=query.Handle();
            return Ok(result);
        }


        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
        
            GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
            query.BookId=id;

            GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result=query.Handle();
            return Ok(result);
        
        }



        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            //Burda try catch satırlarını kaldırarak Middleware ile ele aldık ve hataları console a bastık.(Loglama amaçlı)
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
           
            command.Model=newBook;
            CreateBookCommandValidator validator=new CreateBookCommandValidator();

            validator.ValidateAndThrow(command);

            // ValidationResult result= validator.Validate(command);

            // if (!result.IsValid)
            // {
            //     foreach (var item in result.Errors)
            //     {
            //         System.Console.WriteLine("Property Name : "+item.PropertyName+ " -ErrorMessage : "+item.ErrorMessage);
            //     }

            // }
            // else 
            // {

            //     command.Handle(); 
            // }
            command.Handle();
            return Ok();


        }



        
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
          
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.Model = updatedBook;
            command.BookId = id;
            UpdateBookCommandValidator validations=new UpdateBookCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle();
    
            return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooks(int id)
        {
        
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }



    }
}