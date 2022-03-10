using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.UserOperations.Commands.CreateUser;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreateBookModel,Book>(); //CreatebookModel nesnesi Book a Maplenebilir olsun. 
            //CreateBookModel, Book'a çevirilir.
            
            CreateMap<Book,BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest=>dest.Author,opt=>opt.MapFrom(src=>src.Author.Name+" "+src.Author.LastName));
            //
            CreateMap<Book,BooksViewModel>()
                .ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.LastName));

            //
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<CreateGenreModel,Genre>();
            

            //
            CreateMap<Author,AuthorsViewModel>().ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => (src.BirthDate).ToString("dd.MM.yyyy")));
            CreateMap<Author,AuthorDetailViewModel>().ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => (src.BirthDate).ToString("dd.MM.yyyy")));
            //.ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<CreateAuthorModel,Author>();

            //
            CreateMap<CreateUserModel,User>();

        }
        
    }
}