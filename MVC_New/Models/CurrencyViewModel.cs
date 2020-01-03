using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_New.Models
{
    public class CurrencyViewModel
    {
        [Required]
        [DisplayName("Amount In GBP")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid currency Number")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Please select currency")]
        public string Currency { get; set; }
    }
}
