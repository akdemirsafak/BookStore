using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        
        public IConfiguration Configuration{get;set;}

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public Token CreateAccessToken(User user)
        {
            Token tokenModel=new Token();
            //security key in simetriğini almamız lazım.key ile token oluşturabilmek için
            SymmetricSecurityKey key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            
            SigningCredentials credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            tokenModel.Expiration=DateTime.Now.AddMinutes(15); //expiration - token süresi 15 dakka sürecek şekilde ayarlandı
            
            JwtSecurityToken securityToken=new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:tokenModel.Expiration,
                notBefore:DateTime.Now,
                signingCredentials:credentials
            ); //token ayarları yapıldı.

            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            //token yaratılıyor
            tokenModel.AccessToken=tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken=CreateRefreshToken();

            return tokenModel;
        }
        public string CreateRefreshToken(){
            return Guid.NewGuid().ToString();
        }

    }
}