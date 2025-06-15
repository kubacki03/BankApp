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
        private readonly IAccount _accountService;
        public TransferServices(IRepository repository, Mapper mapper, IAccount account)
        {
            _accountService = account;
            _repository = repository;
            _mapper = mapper;
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
