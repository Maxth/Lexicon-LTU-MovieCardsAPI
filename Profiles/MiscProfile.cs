using AutoMapper;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Profiles
{
    class MiscProfile : Profile
    {
        public MiscProfile()
        {
            CreateMap<Director, DirectorForMovieDetailsDTO>()
                .ConstructUsing(
                    (src, dest) =>
                        new DirectorForMovieDetailsDTO(
                            src.Id,
                            src.Name,
                            src?.ContactInformation?.Email,
                            src!.DateOfBirth
                        )
                );

            CreateMap<Director, DirectorForCreationDTO>().ReverseMap();

            CreateMap<DirectorDTO, Director>().ReverseMap();

            CreateMap<Actor, ActorDTO>().ConstructUsing(src => new ActorDTO(src.Id, src.Name));

            CreateMap<Genre, GenreDTO>().ConstructUsing(src => new GenreDTO(src.Id, src.Name));
        }
    }
}
