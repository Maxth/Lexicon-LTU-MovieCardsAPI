using AutoMapper;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Profiles
{
    class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();

            CreateMap<Movie, MovieDetailsDTO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actor));

            CreateMap<MovieForCreationDTO, Movie>().ReverseMap();

            CreateMap<MovieForUpdateDTO, Movie>().ReverseMap();
        }
    }
}
