using AutoMapper;
using BankApp.Server.DTO;
using BankApp.Server.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseAccount, AccountDetailsDTO>()
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Iban))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Transfers, opt => opt.Ignore());

        CreateMap<BaseTransfer, TransferModelRequest>()
          .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
          .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
          .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
          .ForMember(dest => dest.RecipientAccountNumber, opt => opt.MapFrom(src => src.Payee.Iban))
          .ForMember(dest => dest.SenderAccountNumber, opt => opt.MapFrom(src => src.Sender.Iban));

        CreateMap<RegisterModelRequest, User>()
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.lastName))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Pesel, opt => opt.MapFrom(src => src.pesel))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.birthDay));

    }
}
