using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenView;
using System.Diagnostics;

namespace GenTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public List<string> GetDatabaseList()
        {
            List<string> list = new List<string>();

            // Open connection to the database
            string conString = "server=.; integrated security=true";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return list;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbType.Text = cbbType.Items[0].ToString();
            foreach (string s in GetDatabaseList())
            {
                lsbDatabase.Items.Add(s);
            }
        }

        private void lsbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbTable.Items.Clear();
            //MessageBox.Show(lsbDatabase.SelectedItem.ToString());
            string conString = "server=.; database=" + lsbDatabase.SelectedItem.ToString() + "; integrated security=true";
            List<string> TableNames = new List<string>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                DataTable schema = con.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }
                con.Close();
            }
            for (int i = 0; i < TableNames.Count; i++)
            {
                lsbTable.Items.Add(TableNames[i]);
            }
        }

        private void lsbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string Notification = "";
            string[] st = getcolunms();
            lsbColunm.Items.Clear();
            foreach (string s in st)
            {
                //Notification += s + Environment.NewLine;
                lsbColunm.Items.Add(s);
            }
            //richTextBox1.Text = Notification;
        }

        private void btnGenarate_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true; ;

            if (lsbTable.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Database, table và cột để sắp xếp cần genarate code trước!");
            }
            else
            {
                if (cbbType.Text == "ModelCs")
                {
                    List<string> listString = GenerateModelCs(lsbTable.SelectedItem.ToString(), getcolunms());
                    string result = "";
                    for (int i = 0; i < listString.Count; i++)
                    {
                        result += listString[i] + "\n";
                    }
                    richTextBox1.Text = result;
                }
                else if (cbbType.Text == "ModelJs")
                {
                    List<string> listString = GenerateModelJs(lsbTable.SelectedItem.ToString(), getcolunms());
                    string result = "";
                    for (int i = 0; i < listString.Count; i++)
                    {
                        result += listString[i] + "\n";
                    }
                    richTextBox1.Text = result;
                }
                else if (cbbType.Text == "View")
                {
                    ViewTemplate vt = new ViewTemplate(lsbTable.SelectedItem.ToString(), getcolunms());
                    richTextBox1.Text = vt.TransformText();
                }
                else
                {
                    List<string> listString = GenerateViewModel(lsbTable.SelectedItem.ToString(), getcolunms(), lsbColunm.SelectedItem.ToString());
                    string result = "";
                    for (int i = 0; i < listString.Count; i++)
                    {
                        result += listString[i] + "\n";
                    }
                    richTextBox1.Text = result;
                }
            }
        }

        string[] getcolunms()
        {
            string[] colunms;
            string conString = "server=.; database=" + lsbDatabase.SelectedItem.ToString() + "; integrated security=true";
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM " + lsbTable.SelectedItem.ToString(), con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        var table = reader.GetSchemaTable();
                        int i = 0;
                        string[] temp = new string[table.Rows.Count];
                        foreach (DataRow row in table.Rows)
                        {
                            temp[i] = row[0].ToString();
                            i++;
                        }
                        colunms = temp;
                    }
                }
                con.Close();
            }
            return colunms;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            List<string> listString = new List<string>();
            if (cbbType.Text == "ModelCs")
            {
                listString = GenerateModelCs(lsbTable.SelectedItem.ToString(), getcolunms());
                saveFileDialog1.Filter = "Class|*.cs";
            }
            else if (cbbType.Text == "ModelJs")
            {
                listString = GenerateModelJs(lsbTable.SelectedItem.ToString(), getcolunms());
                saveFileDialog1.Filter = "Script|*.js";
            }
            else if (cbbType.Text == "View")
            {
                saveFileDialog1.Filter = "Razor|*.cshtml";
            }
            else
            {
                listString = GenerateViewModel(lsbTable.SelectedItem.ToString(), getcolunms(), lsbColunm.SelectedItem.ToString());
                saveFileDialog1.Filter = "Script|*.js";
            }
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                FileStream ft = File.Create(saveFileDialog1.FileName.ToString());
                StreamWriter writer = new StreamWriter(ft, Encoding.Unicode);
                if (cbbType.Text == "View")
                {
                    ViewTemplate vt = new ViewTemplate(lsbTable.SelectedItem.ToString(), getcolunms());
                    writer.Write(vt.TransformText());
                }
                else
                {
                    foreach (string s in listString)
                    {
                        writer.WriteLine(s);
                    }
                }
                writer.Close();
            }
        }

        List<string> GenerateViewModel(string tableName, string[] colsName, string SearchField)
        {
            StreamReader reader = new StreamReader("input/ViewModel.js");
            List<string> listString = new List<string>();
            while (!reader.EndOfStream)
            {
                listString.Add(reader.ReadLine());
            }
            //tạo chuỗi tham số truyền vào các hàm có tham số là các cột của bảng
            string tenCotParemeter = "";
            for (int i = 0; i < colsName.Length; i++)
            {
                if (i != 0)
                {
                    tenCotParemeter += ", item." + colsName[i];
                }
                else
                {
                    tenCotParemeter += "item." + colsName[i];
                }
            }
            //tạo các lệnh script có liên quan đến id của html và tên cột ( phần edit)
            string CotHTML = "";
            for (int i = 0; i < colsName.Length; i++)
            {
                CotHTML += "$(\"#" + colsName[i] + "Edit\")" + ".val(item." + colsName[i] + "());" + Environment.NewLine;
            }
            //thay tên bảng và các cột vào script
            for (int i = 0; i < listString.Count; i++)
            {
                string temp = listString[i];
                if (temp.Contains("#TableName#"))
                {
                    listString[i] = temp.Replace("#TableName#", tableName);
                    temp = listString[i];
                }
                if (temp.Contains("#TenCotParameter#"))
                {
                    listString[i] = temp.Replace("#TenCotParameter#", tenCotParemeter);
                    temp = listString[i];
                }
                if (temp.Contains("#CotHTML#"))
                {
                    listString[i] = temp.Replace("#CotHTML#", CotHTML);
                    temp = listString[i];
                }
                if (temp.Contains("#SearchField#"))
                {
                    listString[i] = temp.Replace("#SearchField#", SearchField);
                    temp = listString[i];
                }
            }
            return listString;
        }

        List<string> GenerateModelJs(string tableName, string[] colsName)
        {
            StreamReader reader = new StreamReader("input/Model.js");
            #region Đọc các dòng của Model.js và đưa vào list
            List<string> listString = new List<string>();
            while (!reader.EndOfStream)
            {
                listString.Add(reader.ReadLine());
            }
            reader.Close();
            #endregion
            #region Chuyển mảng thành chuổi cột
            string tenCotParemeter = "";
            for (int i = 0; i < colsName.Length; i++)
            {
                if (i != 0)
                {
                    tenCotParemeter += ", " + colsName[i];
                }
                else
                {
                    tenCotParemeter += colsName[i];
                }
            }
            #endregion
            #region Chuyển mảng cột thành dạng chuỗi declare
            string tenCotDeclare = "";
            foreach (string s in colsName)
            {
                tenCotDeclare += String.Format("self.{0} = ko.observable({0});", s) + Environment.NewLine;
            }
            #endregion

            #region Đọc và chuyển các trường.
            for (int i = 0; i < listString.Count; i++)
            {
                string s = listString[i];
                if (s.Contains("#TableName#"))
                {
                    listString[i] = s.Replace("#TableName#", tableName);
                    s = listString[i];
                }
                if (s.Contains("#TenCotParameter#"))
                {
                    listString[i] = s.Replace("#TenCotParameter#", tenCotParemeter);
                    s = listString[i];
                }
                if (s.Contains("#TenCotDeclare#"))
                {
                    listString[i] = s.Replace("#TenCotDeclare#", tenCotDeclare);
                    s = listString[i];
                }
            }
            #endregion
            return listString;
        }
        List<string> GenerateModelCs(string tableName, string[] colsName)
        {
            StreamReader reader = new StreamReader("input/Model.cs");
            #region Đọc các dòng của Model.cs và đưa vào list
            List<string> listString = new List<string>();
            while (!reader.EndOfStream)
            {
                listString.Add(reader.ReadLine());
            }
            reader.Close();
            #endregion
            #region Chuyển mảng cột thành dạng chuỗi declare
            string tenCotDeclare = "";
            foreach (string s in colsName)
            {
                tenCotDeclare += String.Format("        public Datatype {0}", s) + " {get;set;}" + Environment.NewLine;
            }
            #endregion

            #region Đọc và chuyển các trường.
            for (int i = 0; i < listString.Count; i++)
            {
                string s = listString[i];
                if (s.Contains("#TableName#"))
                {
                    listString[i] = s.Replace("#TableName#", tableName);
                    s = listString[i];
                }
                else if (s.Contains("#TenCotDeclare#"))
                {
                    listString[i] = s.Replace("#TenCotDeclare#", tenCotDeclare);
                    s = listString[i];
                }
                else if (s.Contains("#FirstCol#"))
                {
                    listString[i] = s.Replace("#FirstCol#", colsName[0]);
                    s = listString[i];
                }
            }
            #endregion
            return listString;
        }

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateAll(lsbTable.SelectedItem.ToString(), getcolunms(), lsbColunm.SelectedItem.ToString());
        }
        private void GenerateAll(string tableName, string[] cols, string searchField)
        {
            #region Tạo file ModelCS
            FileStream ft = File.Create(String.Format("output/{0}.cs", tableName));
            StreamWriter writer = new StreamWriter(ft, Encoding.Unicode);
            List<string> listString = GenerateModelCs(tableName, cols);
            string result = "";
            for (int i = 0; i < listString.Count; i++)
            {
                result += listString[i] + "\n";
            }
            writer.WriteLine(result);
            writer.Close();
            #endregion

            #region Tạo file Modeljs
            ft = File.Create(String.Format("output/{0}Model.js", tableName));
            writer = new StreamWriter(ft, Encoding.Unicode);
            listString = GenerateModelJs(tableName, cols);
            result = "";
            for (int i = 0; i < listString.Count; i++)
            {
                result += listString[i] + "\n";
            }
            writer.WriteLine(result);
            writer.Close();
            #endregion

            #region Tạo file View Model
            ft = File.Create(String.Format("output/{0}ViewModel.js", tableName));
            writer = new StreamWriter(ft, Encoding.Unicode);
            listString = listString = GenerateViewModel(tableName, cols, searchField);
            result = "";
            for (int i = 0; i < listString.Count; i++)
            {
                result += listString[i] + "\n";
            }
            writer.WriteLine(result);
            writer.Close();
            #endregion

            #region Tạo file View
            ft = File.Create(String.Format("output/Index.cshtml", tableName));
            writer = new StreamWriter(ft, Encoding.Unicode);
            ViewTemplate vt = new ViewTemplate(tableName, cols);
            result = vt.TransformText();
            writer.WriteLine(result);
            writer.Close();
            #endregion

            Process.Start(@"output");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            listTenCot.Items.Add(txtTencot.Text);
            txtTencot.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            listTenCot.Items.RemoveAt(listTenCot.SelectedIndex);
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            string[] arr = new string[listTenCot.Items.Count];
            listTenCot.Items.CopyTo(arr,0);
            GenerateAll(txtTenBang.Text, arr, arr[1]);
        }
    }
}

