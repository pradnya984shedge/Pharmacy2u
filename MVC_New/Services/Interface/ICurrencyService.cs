using MVC_New.Models;

namespace MVC_New.Services.Interface
{
    public interface ICurrencyService
    {
        double CalculateCurrency(CurrencyViewModel currency);
    }
}