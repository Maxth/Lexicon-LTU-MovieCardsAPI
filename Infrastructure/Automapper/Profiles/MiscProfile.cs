using AutoMapper;
using Domain.Models.Dtos.ActorDtos;
using Domain.Models.Dtos.DirectorDtos;
using Domain.Models.Dtos.GenreDtos;
using Domain.Models.Entities;

namespace Infrastructure.Automapper.Profiles
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
                            src.ContactInformation?.Email,
                            src.DateOfBirth
                        )
                );

            CreateMap<Director, DirectorForCreationDTO>().ReverseMap();

            CreateMap<DirectorDTO, Director>().ReverseMap();

            CreateMap<Actor, ActorDTO>().ConstructUsing(src => new ActorDTO(src.Id, src.Name));

            CreateMap<Genre, GenreDTO>().ConstructUsing(src => new GenreDTO(src.Id, src.Name));
        }
    }
}
