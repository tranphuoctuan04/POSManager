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
        public static string CreateValidation(Type t)
        {
            PropertyInfo[] infos = t.GetProperties();
            string declare = String.Format("var {0} = function (", t.Name);
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
            declare += "){\n" + "var self=this;\n" + validate;
            return declare;
        }

        public static string GetPropertiesS(Type t, string beforeString="")
        {
            PropertyInfo[] infos = t.GetProperties();
            string properties = "";
            foreach (PropertyInfo info in infos)
            {
                properties += beforeString + info.Name;
                if(info != infos[infos.Length-1])
                    properties += ", ";
            }
            return properties;
        }

        public static string GetProperty(Type t, int index)
        {
            PropertyInfo[] infos = t.GetProperties();
            return infos[index].Name;
        }

        public static string GetProperties(Type t, string s = "", string separator = "; ")
        {
            PropertyInfo[] infos = t.GetProperties();
            string properties = "";
            foreach (PropertyInfo info in infos)
            {
                string tmp = s.Replace("#", info.Name);
                properties += tmp;
                if (info != infos[infos.Length - 1])
                    properties += separator;
            }
            return properties;
        }

        public static int Count(Type t)
        {
            return t.GetProperties().Length;
        }
    }
}