using System.Globalization;
using AutoMapper;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;



namespace BankApp.Server.Services
{
    public class TransferServices : ITransfer
    {
        private readonly IRepository _repository;
        private readonly Mapper _mapper;
        private readonly IAccount _accountService;
        public TransferServices(IRepository repository, Mapper mapper, IAccount account)
        {
            _accountService = account;
            _repository = repository;
            _mapper = mapper;
        }

        public byte[] GenerateConfirmation(int transferId)
        {
            var transfer = _repository.GetTransferById(transferId);
          
            var culture = new CultureInfo("pl-PL");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Potwierdzenie Przelewu")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Data przelewu: {transfer.Date.ToString("dd.MM.yyyy HH:mm")}");
                        col.Item().Text($"Tytuł: {transfer.Title}");
                        col.Item().Text($"Kwota: {transfer.Amount.ToString("C", culture)}");

                        col.Item().PaddingTop(15).Text("Dane nadawcy:").SemiBold();
                        col.Item().Text($"Imię i nazwisko: {transfer.Sender?.User.Name} {transfer.Sender?.User.LastName}");
                        col.Item().Text($"IBAN: {transfer.Sender?.Iban}");

                        col.Item().PaddingTop(10).Text("Dane odbiorcy:").SemiBold();
                        col.Item().Text($"Imię i nazwisko: {transfer.Payee?.User.Name} {transfer.Payee?.User.LastName}");
                        col.Item().Text($"IBAN: {transfer.Payee?.Iban}");
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Wygenerowano automatycznie przez system BankApp – ").FontSize(10);
                        text.Span(DateTime.Now.ToString("dd.MM.yyyy HH:mm")).FontSize(10);
                    });
                });
            });

            
            return document.GeneratePdf();
        }
        

        public void SendTransfer(TransferModelRequest request)
        {
            var amount = request.GetAmount();
            var senderNumber = request.GetSenderAccountNumber();
            var recipientNumber = request.GetRecipientAccountNumber();
            if (senderNumber == recipientNumber)
            {
                throw new Exception("Numery kont są identyczne");
            }
            var date = request.GetDate();
            var title = request.GetTitle();

            var recipient = _repository.GetAccountByNumber(recipientNumber);
            var sender = _repository.GetAccountByNumber(senderNumber);
            if (sender.Balance < amount) {
                throw new Exception("No funds");
            }
            if (_accountService.GetAccountByAccountNumber(recipientNumber) == null) {
                throw new Exception("Recipient not found");
            }
            
            var transfer = new BaseTransfer(amount,date,recipient,sender,title);
            _repository.IncreaceBalance(amount, recipientNumber);
            _repository.DecreaseBalance(amount, senderNumber);
            _repository.SaveTransfer(transfer );
        }

      
    }
}
