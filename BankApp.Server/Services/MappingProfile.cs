using AutoMapper;
using BankApp.Server.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseAccount, AccountDetailsDTO>()
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Transfers, opt => opt.Ignore()); 
    }
}
