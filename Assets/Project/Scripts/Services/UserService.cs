using System;
using RedPanda.Project.Services.Interfaces;

namespace RedPanda.Project.Services
{
    public sealed class UserService : IUserService
    {
        public event Action<int> CurrencyAmountChanged;
        public int Currency { get; private set; }
        
        public UserService()
        {
            Currency = 1000;
        }

        void IUserService.AddCurrency(int delta)
        {
            Currency += delta;
            CurrencyAmountChanged?.Invoke(Currency);
        }

        void IUserService.ReduceCurrency(int delta)
        {
            Currency -= delta;
            CurrencyAmountChanged?.Invoke(Currency);
        }
        
        bool IUserService.HasCurrency(int amount)
        {
            return Currency >= amount;
        }
    }
}