using Microsoft.IdentityModel.Tokens;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using TokenHandler = WebApi.TokenOperations.TokenHandler;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user=_context.Users.FirstOrDefault(x=>x.RefreshToken==RefreshToken && x.RefreshTokenExpiredDate>DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token=handler.CreateAccessToken(user);
                
                user.RefreshToken=token.RefreshToken;
                user.RefreshTokenExpiredDate=token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid bir Refresh Token Bulamadı.");
            }
        }

    }
}