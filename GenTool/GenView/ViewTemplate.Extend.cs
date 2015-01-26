using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenView {
    public partial class ViewTemplate {
        public ViewTemplate()
            : this("", new string[] { }) { }

        public ViewTemplate(string tableName, List<string> headers)
            : this(tableName, headers.ToArray()) { }

        public ViewTemplate(string tableName, string[] headers) {
            TableName = tableName;
            Headers = headers;
        }

        public string TableName { get; set; }

        public string[] Headers { get; set; }
    }
}
