using MVC_New.Services;
using System;

namespace MVC_New.Services.Interface
{
    public interface IAuditService
    {
        AuditChanges AuditChanges(string changedBy, DateTime From, DateTime To, double rate, string CurrencyType);
    }
}