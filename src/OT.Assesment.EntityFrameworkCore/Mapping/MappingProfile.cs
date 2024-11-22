using AutoMapper;
using OT.Assesment.EntityFrameworkCore.Dto.Wager;
using OT.Assesment.EntityFrameworkCore.Models;

namespace OT.Assesment.EntityFrameworkCore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Wager, WagerByPlayerDto>()
            .ForMember(dest=>dest.CreatedDate , opt=>opt.MapFrom(src=>src.CreatedDateTime))
            .ForMember(dest=>dest.WagerId , opt=>opt.MapFrom(src=>src.Id))
            .ForMember(dest=>dest.Game , opt=>opt.MapFrom(src=>src.Game.Name))
            .ForMember(dest=>dest.Provider , opt=>opt.MapFrom(src=>src.Provider.ProviderName));
    }
}