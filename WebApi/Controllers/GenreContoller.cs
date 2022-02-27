using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreContoller:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreContoller(BookStoreDbContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {

            GetGenresQuery query=new GetGenresQuery(_context,_mapper);
            var obj=query.Handle();
            return Ok(obj);

        }

        [HttpGet("{id}")]
        public IActionResult GetGenreDetail(int id)
        {

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId=id;
            GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);

        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel model)
        {

            CreateGenreCommand cmd=new CreateGenreCommand(_context,_mapper);
            cmd.Model=model;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(cmd);

            cmd.Handle();
            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel model)
        {

            UpdateGenreCommand cmd=new UpdateGenreCommand(_context,_mapper);
            cmd.GenreId=id;
            cmd.Model=model;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(cmd);
            cmd.Handle();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {

            DeleteGenreCommand cmd=new DeleteGenreCommand(_context);
            cmd.GenreId=id;
            DeleteGenreCommandValidator validator=new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(cmd);
            cmd.Handle();
            return Ok();
            
        }

    }
}