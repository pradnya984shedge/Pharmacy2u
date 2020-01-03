using MVC_New.Models;
using MVC_New.Services.Interface;

namespace MVC_New.Services
{
    public class CurrencyService : ICurrencyService
    {
        public enum currencyrate
        {
            USD = 1,
            AUD = 2,
            EUR = 3
            
        }
        public double CalculateCurrency(CurrencyViewModel currency)
        {
            double calRate = 0;

            if (currency.Currency == currencyrate.USD.ToString())
            {
                calRate = (currency.Amount) * 1.29;
            }
            else if (currency.Currency == currencyrate.AUD.ToString())
            {
                calRate = (currency.Amount) * 1.87;
            }
            else
            {
                calRate = (currency.Amount) * 1.17;
            }
            
            return calRate;
        }
    }
}
