﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace GenLib
{
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class ViewTemplate : ViewTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("@model YourNamespace.Models.");
            
            #line 3 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n@{\r\n    ViewBag.Title = \"YourTitle\";\r\n}\r\n<link href=\"~/Content/css/style.css\"" +
                    " rel=\"stylesheet\" />\r\n<!-- knockout -->\r\n<script src=\"~/Content/knockout/knockou" +
                    "t-3.1.0.js\"></script>\r\n<script src=\"~/Content/jquery/jquery.min.js\"></script>\r\n<" +
                    "script src=\"~/Content/jquery/jquery.mediaTable.min.js\"></script>\r\n<script src=\"~" +
                    "/Content/jquery/jquery.actual.min.js\"></script>\r\n<script src=\"~/Content/jquery/j" +
                    "query-migrate.min.js\"></script>\r\n<script src=\"~/Content/jquery/jquery.debouncedr" +
                    "esize.min.js\"></script>\r\n<script src=\"~/Content/jquery/jquery.wookmark.js\"></scr" +
                    "ipt>\r\n<script src=\"~/Content/jquery/jquery.peity.min.js\"></script>\r\n<script src=" +
                    "\"~/Content/jquery/jquery.easing.1.3.min.js\"></script>\r\n<script src=\"~/Content/jq" +
                    "uery/jquery_cookie.min.js\"></script>\r\n<script src=\"~/Content/jquery/jquery.image" +
                    "sloaded.min.js\"></script>\r\n<script src=\"~/Content/jQueryAlert/jquery.alerts.js\">" +
                    "</script>\r\n<script src=\"~/Content/jquery/jquery.validate.min.js\"></script>\r\n<scr" +
                    "ipt src=\"~/Content/jquery/jquery.validate.unobtrusive.min.js\"></script>\r\n\r\n\r\n<li" +
                    "nk href=\"~/Content/bootstrap/css/bootstrap.min.css\" rel=\"stylesheet\" />\r\n<link h" +
                    "ref=\"~/Content/bootstrap/css/bootstrap-responsive.min.css\" rel=\"stylesheet\" />\r\n" +
                    "<script src=\"~/Content/bootstrap/js/bootstrap.min.js\"></script>\r\n<script src=\"~/" +
                    "Content/bootstrap/js/bootstrap.plugins.min.js\"></script>\r\n<link href=\"~/Content/" +
                    "jQueryAlert/jquery.alerts.css\" rel=\"stylesheet\" />\r\n\r\n<h2>YourTitle</h2>\r\n<div c" +
                    "lass=\"row-fluid\">\r\n    <!---Left -->\r\n\r\n    <div class=\"span4\">\r\n        <div cl" +
                    "ass=\"w-box-header\">\r\n            <span class=\"glyphicon glyphicon-th-list\"></spa" +
                    "n> &nbsp;Danh sách ");
            
            #line 38 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write(@"(<span data-bind=""html: CountSearch() + '/' + List().length""></span>)
            <span class=""pull-right"" style=""align-self:stretch"">
                <a class=""btn btn-success"" id=""btnModalCreate"" data-toggle=""modal"" data-target=""#modal"" data-bind=""click: Create");
            
            #line 40 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write(@""">
                    <i class=""icon-plus icon-white""></i>
                </a>
            </span>
        </div>

        <div class=""w-box-content cnt_a"" data-bind=""fadeVisible: true"">
            <div class=""form-vertical"">
                <div class=""row-fluid"">
                    <input class=""form-control"" type=""text"" data-bind=""value: SearchName, valueUpdate: 'afterkeydown', event: { keyup: btnSearchName }"" />
                    <a class=""btn btn-success"" data-bind=""click: btnSearchName""><span class=""icon-search icon-white""></span></a>
                </div>
                <div class=""row-fluid"">
                    <div style=""height: 430px; overflow: auto;"">
                        <div class=""table-responsive"">
                            <table class=""table table-bordered table-striped"">
                                <thead>
                                    <tr>
                                        ");
            
            #line 58 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
 if(Headers != null) {
                                        for(int i = 0; i < Headers.Length; i++) {
                                        
            
            #line default
            #line hidden
            this.Write("<th class=\"span1\">@Html.LabelFor(x => x.");
            
            #line 60 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write(")</th>\r\n                                        ");
            
            #line 61 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"

                                            }
                                        }
                                        
            
            #line default
            #line hidden
            this.Write(@"<th class=""span1"">Sửa</th>
                                    </tr>
                                </thead>
                                <tbody data-bind=""template: { name: 'RowItem', foreach: List }""></tbody>
                                <tfoot data-bind=""visible: List().length == 0"">
                                    <tr>
                                        <td colspan=""4"">Không có dữ liệu</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script id=""RowItem"" type=""text/html"">
            <tr data-bind=""css: { normalrow: (($index() % 2) == 0), alternativerow: ($index() % 2 != 0) }, visible: IsVisible() == true"">
                <td style=""font-weight: bold;""><a style=""cursor: pointer"" data-bind=""click: $root.btnSetCurrent""><span data-bind=""html: ");
            
            #line 82 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[0]));
            
            #line default
            #line hidden
            this.Write("\"></span></a></td>\r\n                ");
            
            #line 83 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"

                for(int i = 1; i < Headers.Length; i++) {
                
            
            #line default
            #line hidden
            this.Write("<td data-bind=\"html: ");
            
            #line 85 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write("\"></td>\r\n                ");
            
            #line 86 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"

                }
                
            
            #line default
            #line hidden
            this.Write("                <td style=\"text-align: right;\">\r\n                    <button clas" +
                    "s=\"btn btn-warning\" title=\"Edit ");
            
            #line 90 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("\" data-bind=\"value: ");
            
            #line 90 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[0]));
            
            #line default
            #line hidden
            this.Write(", click: $root.Edit");
            
            #line 90 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write(@""" id=""btnModalCreate"" data-toggle=""modal"" data-target=""#modal"" style=""width:100%"">
                        <i class=""icon-edit icon-white""></i>
                    </button>
                </td>
            </tr>
        </script>
    </div>

    <!---End Left -->
    <!---Right -->
    <div class=""span8"" data-bind=""with: Current"">
        <div class=""w-box-header"">
            <i class=""icon-list""></i>Thông tin chi tiết
        </div>

        <div class=""w-box-content cnt_a"" data-bind=""fadeVisible: true"">
            <div class=""form-vertical"">
                <div class=""row-fluid"">
                    <div style=""height: 456px; overflow: auto;"">
                        <div class=""table-responsive"">
                            <table class=""table table-bordered table-striped"" border=""1"">
                                <tbody>
                                    ");
            
            #line 112 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"

                                    for(int i = 0; i < Headers.Length; i++) {
                                    
            
            #line default
            #line hidden
            this.Write("<tr><td>@Html.LabelFor(x => x.");
            
            #line 114 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write(")</td><td data-bind=\"html: ");
            
            #line 114 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write("\"></td></tr>\r\n                                    ");
            
            #line 115 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"<tr>
                                        <td>
                                            Chức năng   
                                        </td>
                                        <td>
                                            <form id=""formDelete"">
                                                <input data-bind=""value: ");
            
            #line 121 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[0]));
            
            #line default
            #line hidden
            this.Write("\" type=\"text\" style=\"display:none\" name=\"");
            
            #line 121 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[0]));
            
            #line default
            #line hidden
            this.Write("\" />\r\n                                                <button type=\"button\" class" +
                    "=\"btn btn-danger\" data-bind=\"click: $root.Delete");
            
            #line 122 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write(@"Submit"">Xóa</button>
                                            </form>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!---End Right -->
    <!-- Modal-->
    <div id=""modal"" class=""modal hide fade"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"" aria-hidden=""true"">
        <div class=""modal-header"">
            <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
            <h3 id=""myModalLabel"">Thêm ");
            
            #line 139 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("</h3>\r\n        </div>\r\n        <div class=\"modal-body\">\r\n            <form class=" +
                    "\"form-horizontal\" method=\"post\" action=\"");
            
            #line 142 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("/Create\" id=\"form\">\r\n                <!--Begin Form-->\r\n                @Html.Val" +
                    "idationSummary(true)\r\n            ");
            
            #line 145 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"

            if(Headers != null) {
            for(int i = 0; i < Headers.Length; i++) {
            
            
            #line default
            #line hidden
            this.Write("  <div class=\"control-group\">\r\n\t\t\t\t@Html.LabelFor(model => model.");
            
            #line 149 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write(", new { @class = \"control-label\" })\r\n                    <div class=\"controls\">\r\n" +
                    "                        @Html.TextBoxFor(model => model.");
            
            #line 151 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write(", new { @class = \"span12\", placeholder = \"");
            
            #line 151 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write("\", id = \"");
            
            #line 151 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write("Edit\", data_bind=\"value: val.");
            
            #line 151 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write("\" })\r\n                        <!--<span class=\"text-error\">@Html.ValidationMessag" +
                    "eFor(model => model.");
            
            #line 152 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Headers[i]));
            
            #line default
            #line hidden
            this.Write(")</span> -->\r\n                    </div>\r\n                </div>\r\n            ");
            
            #line 155 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
 
            } }
            
            
            #line default
            #line hidden
            this.Write("</form> <!--End Form-->\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n    " +
                    "        <button class=\"btn btncloseModal\" data-dismiss=\"modal\" aria-hidden=\"true" +
                    "\">Đóng</button>\r\n            <button class=\"btn btn-primary\" data-bind=\"click: C" +
                    "reate");
            
            #line 161 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("Submit\" id=\"btnCreate\">Tạo ");
            
            #line 161 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("</button>\r\n            <button class=\"btn btn-primary\" data-bind=\"click: Edit");
            
            #line 162 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("Submit\" id=\"btnEdit\">Sửa ");
            
            #line 162 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("</button>\r\n        </div>\r\n    </div>\r\n\r\n</div>\r\n\r\n<script src=\"~/Scripts/Insite/" +
                    "Views/");
            
            #line 168 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("/");
            
            #line 168 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("ModelValidation.js\"></script>\r\n<script src=\"~/Scripts/Insite/Views/");
            
            #line 169 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("/");
            
            #line 169 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("Model.js\"></script>\r\n<script src=\"~/Scripts/Insite/Views/");
            
            #line 170 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("/");
            
            #line 170 "D:\Proj\VSW2013\POSManager\GenTool\GenLib.cs\ViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TableName));
            
            #line default
            #line hidden
            this.Write("ViewModel.js\"></script>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class ViewTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
