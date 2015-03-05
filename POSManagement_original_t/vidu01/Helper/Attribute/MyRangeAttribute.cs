using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Data.Entity;

namespace Validation_I
{
    class MyRangeAttribute : RangeAttribute, ICheckRule
    {
        public MyRangeAttribute(int min, int max)
            : base(min, max)
        {

        }
        public MyRangeAttribute(double min, double max)
            : base(min, max)
        {

        }

        public MyRangeAttribute(Type type, string minimum, string maximum)
            : base(type, minimum, maximum)
        {

        }

        public bool isValid(object value)
        {
            return base.IsValid(value);
        }
        public string GetClientValidation()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();
            
            var min = infos.Single(x => x.Name == "Minimum").GetValue(this, null);
            var message = infos.Single(x => x.Name == "ErrorMessage").GetValue(this, null);
            var max = infos.Single(x => x.Name == "Maximum").GetValue(this, null);

            string clientValitdationString = "min: {";
            clientValitdationString += String.Format("params: {0},", min);
            clientValitdationString += String.Format("message: '{0}'}},", message);

            clientValitdationString += "max: {";
            clientValitdationString += String.Format("params: {0},", max);
            clientValitdationString += String.Format("message: '{0}'}},", message);

            return clientValitdationString;
        }

    }
}
