using MVC_New.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_New.Services
{
    public class AuditService : IAuditService
    {
        private IAuditService _serviceWrapper;

        public AuditService(IAuditService auditService)
        {
            _serviceWrapper = auditService ?? throw new NullReferenceException("auditService");
        }

        public AuditChanges AuditChanges(String changedBy, DateTime FromDate, DateTime ToDate, double Rate, String CurrencyType)
        {
            return  _serviceWrapper.AuditChanges(changedBy, FromDate, ToDate, Rate, CurrencyType);
        }

        
    }
}