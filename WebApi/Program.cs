using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CONTEXT VE AUTOMAPPER GİBİ ŞEYLERİN BUILDER BUILD EDİLMEDEN DAHİL EDİLMESİ LAZIM.
builder.Services.AddDbContext<BookStoreDbContext>(option => option.UseInMemoryDatabase(databaseName: "BookStoreDB"));
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

app.UseAuthorization();

//Custom Middleware i ekleyelim

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();
