using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Cs_Console_Text
{
    public class BaseModel
    {
        virtual public bool Validation()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                var propValue = info.GetValue(this, null);
                var attributes = info.GetCustomAttributes(false);
                foreach (Attribute att in attributes)
                {
                    if (att is RequiredAttribute) // Kiểm tra trường require
                    {
                        string s = propValue.ToString();
                        if (s.Trim() == "" || s == null)
                        {
                            return false;
                        }
                    }
                    else if (att is RangeAttribute)
                    {
                        RangeAttribute ranAtt = (RangeAttribute)att;
                        double min = Convert.ToDouble(ranAtt.Minimum);
                        double max = Convert.ToDouble(ranAtt.Maximum);
                        double value = Convert.ToDouble(propValue);
                        if (value < min || value > max)
                            return false;
                    }
                    else if (att is RegularExpressionAttribute)
                    {
                        RegularExpressionAttribute reAtt = (RegularExpressionAttribute)att;
                        string patern = reAtt.Pattern;

                        Console.WriteLine(patern);
                        Regex rgx = new Regex(patern);
                        if (!rgx.IsMatch(propValue.ToString()))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }

    public class Student : BaseModel
    {
        [Range(10,20,ErrorMessage="13413")]
        public int StudentID { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class Professor : BaseModel
    {
        public int ProfessorId { get; set; }
        public string ProfessorName { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student { Name = "asda", StudentID = 20 };
            Console.WriteLine(s.Validation());

        }
    }
}

/* 
 */
