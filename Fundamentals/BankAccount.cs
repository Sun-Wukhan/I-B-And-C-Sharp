using System;

namespace Fundamentals
{
    public class BankAccount
    {
        public string AccountNumber { get; }
        public decimal Balance      { get; private set; }

        public BankAccount(string accountNumber, decimal balance = 0m)
        {
            AccountNumber = accountNumber;
            Balance       = balance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive", nameof(amount));

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;
        }

        public override string ToString() =>
            $"Account {AccountNumber}: balance {Balance:C}";
    }
}
