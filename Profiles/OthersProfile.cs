using AutoMapper;
using MovieCardsAPI.DTOs;
using MovieCardsApi.Entities;

namespace MovieCardsAPI.Profiles
{
    class OthersProfile : Profile
    {
        public OthersProfile()
        {
            CreateMap<Director, DirectorDTO>()
                .ConstructUsing(src => new DirectorDTO(
                    src.Id,
                    src.Name,
                    src.ContactInformation.Email
                ));

            CreateMap<Actor, ActorDTO>().ConstructUsing(src => new ActorDTO(src.Id, src.Name));

            CreateMap<Genre, GenreDTO>().ConstructUsing(src => new GenreDTO(src.Id, src.Name));
        }
    }
}
