using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
(opt=>
    {
        opt.TokenValidationParameters=new TokenValidationParameters
        {
            ValidateAudience=true,
            ValidateIssuer=true,
            ValidateLifetime=true,
            ValidateIssuerSigningKey=true,
            ValidIssuer=builder.Configuration["Token:Issuer"],
            ValidAudience=builder.Configuration["Token:Audience"],
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew=TimeSpan.Zero

        };
    }
);

//CONTEXT VE AUTOMAPPER GİBİ ŞEYLERİN BUILDER BUILD EDİLMEDEN DAHİL EDİLMESİ LAZIM.
builder.Services.AddDbContext<BookStoreDbContext>(option => option.UseInMemoryDatabase(databaseName: "BookStoreDB"));
builder.Services.AddScoped<IBookStoreDbContext>(provider=>provider.GetService<BookStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<ILoggerService,DBLogger>(); //interface i ve implemente edilen ConsoleLogger(tercihimize göre,burada tanımladık.)


var app = builder.Build();


//InMemory e özel uygulama her çalıştığında örnek veriler eklemek için.
using (var scope=app.Services.CreateScope())
{
    var Services=scope.ServiceProvider;
   
    DataGenerator.Initialize(Services);    
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //JWT BEARAER

app.UseAuthorization();

//Custom Middleware i ekleyelim

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();
