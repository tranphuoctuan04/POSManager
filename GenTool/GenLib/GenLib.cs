using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenLib {
    public class GenLib {
        public static string GenCode(Type type, string tableName, params string[] headers) {
            ITemplate template = null;
            if (type.Equals( typeof(ViewTemplate))) {
                template = new ViewTemplate(tableName, headers);
            }
            if (template == null)
                return "";
            else
                return template.TransformText();
        }
    }
}
