using System;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IUserService
    {
        event Action<int> CurrencyAmountChanged;
        int Currency { get; }
        void AddCurrency(int delta);
        void ReduceCurrency(int delta);
        bool HasCurrency(int amount);
    }
}