using System;

namespace Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise with £100
            var account = new BankAccount("ACC123", 100m);

            account.Deposit(50m);
            try
            {
                account.Withdraw(100m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine(account);
        }
    }
}
