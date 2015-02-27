using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation_I
{
    interface ICheckRule
    {
        bool isValid(object value);
        string GetClientValidation();
    }
}
