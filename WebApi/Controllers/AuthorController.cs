using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query=new GetAuthorsQuery(_context,_mapper);
            var result=query.Handle();
            
            return Ok(result);
        }



        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            GetAuthorDetailQuery query=new GetAuthorDetailQuery(_context,_mapper);
            query.authorId=id;

            GetAuthorDetailQueryValidator validator=new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result =query.Handle();
            
            return Ok(result);

        }


        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand cmd=new CreateAuthorCommand(_context,_mapper);
            cmd.Model=newAuthor;

            CreateAuthorCommandValidator validator=new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(cmd);
            cmd.Handle();
            
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand cmd=new UpdateAuthorCommand(_context,_mapper);
            cmd.authorId=id;
            cmd.Model=updatedAuthor;

            UpdateAuthorCommandValidator validator=new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(cmd);
            cmd.Handle();
            
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand cmd=new DeleteAuthorCommand(_context,_mapper);
            cmd.authorId=id;
            
            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(cmd);
            cmd.Handle();
            
            return Ok();

        }



    }
}