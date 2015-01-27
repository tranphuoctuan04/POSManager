using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenView {
    public partial class ViewTemplate {
        public ViewTemplate()
            : this("", new string[] { }) { }

        public ViewTemplate(string tableName, IList<string> headers)
            : this(tableName, headers.ToArray()) { }

        public ViewTemplate(string tableName, string[] headers) {
            TableName = tableName;
            Headers = headers;
        }

        public string TableName { get; set; }

        public string[] Headers { get; set; }

        public static string GenView(string tableName, IList<string> headers) {
            return GenView(tableName, headers.ToArray<string>());
        }

        public static string GenView(string tableName, string[] headers) {
            ViewTemplate vt = new ViewTemplate(tableName, headers);
            return vt.TransformText();
        }
    }
}
