using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Validation_I
{
    public static class GenClientModelValidation
    {
        public static string CreateValidation<T>()
        {
            Type type = typeof(T);
            PropertyInfo[] infos = type.GetProperties();
            string declare = String.Format("var {0} = function (", type.Name);
            string validate = "";
            foreach (PropertyInfo info in infos)
            {
                declare += info.Name + ", ";
                string s = String.Format("self.{0} = ko.observable({0}).extend({{", info.Name);
                var attributes = info.GetCustomAttributes(false);
                foreach (Attribute attribute in attributes.OfType<ICheckRule>())
                {
                    s += ((ICheckRule)attribute).GetClientValidation();
                }
                s += "});";
                validate += s + "\n";
            }
            declare = declare.Substring(0, declare.Length - 2);
            declare += "){\n" + "var self=this;\n" + validate + @"self.IsCreate = ko.observable(false);
                            self.IsEdit = ko.observable(false);
                            self.IsVisible = ko.observable(true);
                            self.OldValue = ko.observable({});

                            // Validate.
                            this.Errors = ko.validation.group(this);
                            this.isValid = ko.computed(function () {
                                return self.Errors().length == 0;
                            });" + "\n}";
            //Console.WriteLine(declare);
            return declare;
        }
    }
}
