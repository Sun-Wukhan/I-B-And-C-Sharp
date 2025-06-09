using System.Collections.Generic; 
using Fundamentals; 

namespace AccountService.Services
{
    public interface IAccountRepository
    {
        IEnumerable<BankAccount> GetAll();
        BankAccount?       Get(string id);
        void               Create(BankAccount acct);
        bool               Update(BankAccount acct);
        bool               Delete(string id);
    }
}