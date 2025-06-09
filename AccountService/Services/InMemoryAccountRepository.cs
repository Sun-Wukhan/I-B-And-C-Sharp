using System.Collections.Concurrent;
using Fundamentals;

namespace AccountService.Services
{
    public class InMemoryAccountRepository : IAccountRepository
{
    private readonly ConcurrentDictionary<string, BankAccount> _store = new();

    public IEnumerable<BankAccount> GetAll() => _store.Values;

    public BankAccount? Get(string id) =>
        _store.TryGetValue(id, out var acct) ? acct : null;

    public void Create(BankAccount acct) =>
        _store[acct.AccountNumber] = acct;

    public bool Update(BankAccount acct)
    {
        if (!_store.ContainsKey(acct.AccountNumber))
            return false;

        _store[acct.AccountNumber] = acct;
        return true;
    }

    public bool Delete(string id) =>
        _store.TryRemove(id, out _);
}
}
