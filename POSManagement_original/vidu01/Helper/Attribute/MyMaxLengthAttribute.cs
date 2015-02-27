using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Validation_I
{
    class MyMaxLengthAttribute : MaxLengthAttribute, ICheckRule
    {
        public MyMaxLengthAttribute(int length)
            : base(length)
        {

        }

        public bool isValid(object value)
        {
            return base.IsValid(value);
        }
        public string GetClientValidation()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();
            string clientValitdationString = "maxLength: {";

            var value = infos.Single(x => x.Name == "Length").GetValue(this, null);
            clientValitdationString += String.Format("params: {0},", value);

            value = infos.Single(x => x.Name == "ErrorMessage").GetValue(this, null);
            clientValitdationString += String.Format("message: '{0}'}},", value);

            return clientValitdationString;
        }

    }
}
