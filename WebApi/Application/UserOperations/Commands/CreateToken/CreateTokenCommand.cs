using AutoMapper;
using WebApi.DbOperations;
using WebApi.TokenOperations;

using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public CreateTokenModel Model { get; set; }
        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user=_context.Users
                .FirstOrDefault(x=>x.Email.Equals(Model.Email) && x.Password.Equals(Model.Password));
                if (user is not null)
                {
                    //Token yaratalım
                    TokenHandler handler=new TokenHandler(_configuration);
                    Token token=handler.CreateAccessToken(user);
                    user.RefreshToken=token.RefreshToken;
                    user.RefreshTokenExpiredDate=token.Expiration.AddMinutes(5);
                    _context.SaveChanges();
                    return token;
                    
                }
                else
                {
                    throw new InvalidOperationException("Kullanıcı Adı - Şifre hatalı.");
                }
        }
    
    }
    public class CreateTokenModel{
        public string Email { get; set; }
        public string Password { get; set; }
    }
}