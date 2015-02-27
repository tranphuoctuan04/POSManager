using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Validation_I
{
    class MyRequireAttribute : RequiredAttribute, ICheckRule
    {
        public bool isValid(object value)
        {
            return base.IsValid(value);
        }
        public string GetClientValidation()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();
            string clientValitdationString = "required: {";

            clientValitdationString += String.Format("params: true,");

            var value = infos.Single(x => x.Name == "ErrorMessage").GetValue(this, null);
            clientValitdationString += String.Format("message: '{0}'}},", value);

            return clientValitdationString;
        }

    }
}
