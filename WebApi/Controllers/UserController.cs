using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;
        readonly IConfiguration _configuration; //Configuration appsettings.json altındaki verilere ulaşmamızı sağlar.
        /*appsettings i set edelim:
        "Token":{
            "Issuer":"www.test.com",
            "Audience":"www.test.com",
            "SecurityKey": "This is my custom secret key for authentication"
        }*/
        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand cmd=new CreateUserCommand(_context,_mapper);
            cmd.Model=newUser;
            
            CreateUserCommandValidator validator=new CreateUserCommandValidator();
            validator.ValidateAndThrow(cmd);
            
            cmd.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand cmd=new CreateTokenCommand(_context,_mapper,_configuration);
            cmd.Model=login;
            var token=cmd.Handle();
            return token;

        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand cmd = new RefreshTokenCommand(_context, _configuration);
            cmd.RefreshToken = token;
            var resultToken=cmd.Handle();
           
            return resultToken;

        }


    }
}