using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public class CreditCardValidation
    {
        [Required(ErrorMessage = "Name on card is required")]
        public string displayName
        {
            get;
            set;
        }

        [RegularExpression(@"(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)", ErrorMessage = "Valid card number is required")]
        [Required(ErrorMessage = "Card number is required")]
        public string cardNumber
        {
            get;
            set;
        }

        [RegularExpression(@"^(0?[1-9]|1[0-2])$", ErrorMessage = "Valid expire month is required")]
        [Required(ErrorMessage = "Month is required")]
        public string expireMonth
        {
            get;
            set;
        }

        [RegularExpression(@"^([0-9]{4}|[0-9]{2})$", ErrorMessage = "Valid expire year is required")]
        [Required(ErrorMessage = "Year is required")]
        public string expireYear
        {
            get;
            set;
        }
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "Valid CVV is required")]
        [Required(ErrorMessage = "CVV is required")]
        public string cvv
        {
            get;
            set;
        }
    }
}
