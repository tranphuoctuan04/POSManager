using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenLib {
    public partial class ViewTemplate : ITemplate {
        public ViewTemplate()
            : this("", new string[] { }) { }

        public ViewTemplate(string tableName, IList<string> headers)
            : this(tableName, headers.ToArray()) { }

        public ViewTemplate(string tableName, params string[] headers) {
            TableName = tableName;
            Headers = headers;
        }

        public string TableName { get; set; }

        public string[] Headers { get; set; }
    }
}
