using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Validation_I
{
    class MyRegularExpression : RegularExpressionAttribute, ICheckRule
    {
        public MyRegularExpression(string pattern)
            : base(pattern)
        {

        }

        public bool isValid(object value)
        {
            return base.IsValid(value);
        }
        public string GetClientValidation()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();
            string clientValitdationString = "pattern: {";

            var value = infos.Single(x => x.Name == "Pattern").GetValue(this, null);
            clientValitdationString += String.Format("params: \"{0}\",", value);

            value = infos.Single(x => x.Name == "ErrorMessage").GetValue(this, null);
            clientValitdationString += String.Format("message: '{0}'}},", value);

            return clientValitdationString;
        }

    }
}
