using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CustomControlLibrary.Misc.ValidationRules
{
    internal class TypeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((PropertyType)value == PropertyType.TRANSFORM)
            {
                return new ValidationResult(false, "Cannot disable transform");
            }

            return ValidationResult.ValidResult;
        }
    }
}
