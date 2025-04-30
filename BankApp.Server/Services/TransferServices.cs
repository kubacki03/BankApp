using AutoMapper;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;

namespace BankApp.Server.Services
{
    public class TransferServices : ITransfer
    {
        private readonly IRepository _repository;
        private readonly Mapper _mapper;
        public TransferServices(IRepository repository, Mapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ICollection<TransferModelRequest> GetAccountTransfers(string accountNumber)
        {
         var baseTransfers = _repository.GetUserTransfers(accountNumber);

        var transfers = new List<TransferModelRequest>();

            foreach (var transfer in baseTransfers)
            {
                var model = _mapper.Map<TransferModelRequest>(transfer);
                transfers.Add(model);

            }
            return transfers;

        }

        public void SendTransfer(TransferModelRequest request)
        {
            var amount = request.GetAmount();
            var senderNumber = request.GetSenderAccountNumber();
            var recipientNumber = request.GetRecipientAccountNumber();
            var date = request.GetDate();
            var title = request.GetTitle();

            var recipient = _repository.GetAccountByNumber(recipientNumber);
            var sender = _repository.GetAccountByNumber(senderNumber);

           

            var transfer = new BaseTransfer(amount,date,recipient,sender,title);
            _repository.IncreaceBalance(amount, recipientNumber);
            _repository.DecreaseBalance(amount, senderNumber);
            _repository.SaveTransfer(transfer);
        }

      
    }
}
