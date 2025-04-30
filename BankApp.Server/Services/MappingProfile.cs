using AutoMapper;
using BankApp.Server.DTO;
using BankApp.Server.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseAccount, AccountDetailsDTO>()
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Transfers, opt => opt.Ignore());

        CreateMap<BaseTransfer, TransferModelRequest>()
          .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
          .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
          .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
          .ForMember(dest => dest.RecipientAccountNumber, opt => opt.MapFrom(src => src.Payee.AccountNumber))
          .ForMember(dest => dest.SenderAccountNumber, opt => opt.MapFrom(src => src.Sender.AccountNumber));
    }
}
