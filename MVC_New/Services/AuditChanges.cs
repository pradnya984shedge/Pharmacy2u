using System;

namespace MVC_New.Services
{
    public class AuditChanges
    {        
        public string changeBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Rate { get; set; }
        public string CurrencyType { get; set; }
    }
}