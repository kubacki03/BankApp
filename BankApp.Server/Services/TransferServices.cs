using System.Globalization;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace BankApp.Server.Services
{
    public class TransferServices : ITransfer
    {
        private const int MaxTitleLength = 140;

        private readonly IRepository _repository;
        private readonly ILogger<TransferServices> _logger;

        public TransferServices(IRepository repository, ILogger<TransferServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<byte[]?> GenerateConfirmationAsync(int transferId, string email, CancellationToken cancellationToken = default)
        {
            var transfer = await _repository.GetTransferByIdAsync(transferId, cancellationToken);
            var userId = await _repository.GetUserByAccountEmailAsync(email, cancellationToken);

            if (transfer == null || userId == 0 || (transfer.Sender.UserId != userId && transfer.Payee.UserId != userId))
            {
                _logger.LogWarning("Confirmation for transfer {TransferId} not available for user {UserId}", transferId, userId);
                return null;
            }

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

            return await Task.Run(() => document.GeneratePdf(), cancellationToken);
        }

        public async Task SendTransferAsync(TransferModelRequest request, CancellationToken cancellationToken = default)
        {
            var amount = request.GetAmount();
            var senderNumber = request.GetSenderAccountNumber();
            var recipientNumber = request.GetRecipientAccountNumber();
            var title = request.GetTitle()?.Trim();

            if (amount <= 0 || decimal.Round(amount, 2) != amount)
            {
                throw new TransferException("Nieprawidłowa kwota przelewu");
            }

            if (string.IsNullOrWhiteSpace(recipientNumber))
            {
                throw new TransferException("Brak numeru konta odbiorcy");
            }

            if (string.IsNullOrWhiteSpace(title) || title.Length > MaxTitleLength)
            {
                throw new TransferException("Nieprawidłowy tytuł przelewu");
            }

            if (senderNumber == recipientNumber)
            {
                throw new TransferException("Numery kont są identyczne");
            }

            var sender = await _repository.FindAccountByNumberAsync(senderNumber, cancellationToken);
            if (sender == null || !sender.IsActive)
            {
                throw new TransferException("Konto nadawcy jest niedostępne");
            }

            var recipient = await _repository.FindAccountByNumberAsync(recipientNumber, cancellationToken);
            if (recipient == null || !recipient.IsActive)
            {
                throw new TransferException("Nie znaleziono konta odbiorcy");
            }

            var transfer = new BaseTransfer
            {
                Amount = amount,
                Date = request.GetDate(),
                PayeeId = recipient.Id,
                SenderId = sender.Id,
                Title = title
            };

            if (!await _repository.TryExecuteTransferAsync(transfer, cancellationToken))
            {
                _logger.LogWarning("Transfer from account {SenderId} to {PayeeId} rejected: insufficient funds", sender.Id, recipient.Id);
                throw new TransferException("Brak wystarczających środków na koncie");
            }

            _logger.LogInformation("Transfer {TransferId} from account {SenderId} to {PayeeId} completed", transfer.Id, sender.Id, recipient.Id);
        }
    }
}
