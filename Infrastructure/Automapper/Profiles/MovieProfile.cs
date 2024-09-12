using AutoMapper;
using Domain.Models.Dtos.MovieDtos;
using Domain.Models.Entities;

namespace Infrastructure.Automapper.Profiles
{
    class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actor))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genre))
                .ReverseMap();

            CreateMap<Movie, MovieDetailsDTO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actor));

            CreateMap<MovieForCreationDTO, Movie>().ReverseMap();

            CreateMap<MovieForUpdateDTO, Movie>().ReverseMap();
            CreateMap<MovieForPatchDTO, Movie>().ReverseMap();
        }
    }
}
