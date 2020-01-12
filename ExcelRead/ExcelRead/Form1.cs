using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelRead
{
    public partial class Form1 : Form
    {
        //异或因子
        private byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击选择excel表按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_Excel_Button_Click(object sender, EventArgs e)
        {
            //-----------
            //打开窗口 ，选择所有符合条件的exccl
            //----------
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathTextBox.Text = "";
                foreach (var item in this.openFileDialog1.FileNames)
                {
                    pathTextBox.Text += item + "\r\n";
                }
            }
        }



        private void pathListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 创建Date文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void create_Data_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.pathGroupBox.Text)) return;
            string[] pathArr = this.pathTextBox.Text.Split('\r', '\n');
            for (int i = 0; i < pathArr.Length; i++)
            {
                ReadData(pathArr[i]);
            }


            MessageBox.Show("创建成功");

        }
        /// <summary>
        /// 读取excel 表中的数据
        /// </summary>
        /// <param name="path"></param>
        private void ReadData(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            //暂定
            string tableName = "Sheet1";

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            //string strConn = "Provider = Microsoft.Ace.OLEDB.12.0;Data Source = " + path + ";Password = \"\";User ID = Admin;Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";

            DataTable table = null;

            using (OleDbConnection connection = new OleDbConnection(strConn))
            {
                connection.Open();
                string strExcel = string.Format("select * from [{0}$]", "Sheet1"); ;
                OleDbDataAdapter adapter = null;
                DataSet ds = new DataSet();
                adapter = new OleDbDataAdapter(strExcel, connection);
                adapter.Fill(ds, "table1");
                table = ds.Tables[0];
                adapter.Dispose();
            }

            CreateData(path, table);

        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="table"></param>
        private void CreateData(string path, DataTable table)
        {
            //文件路径
            string filePath = path.Substring(0, path.LastIndexOf("\\") + 1);
            //文件完整名字
            string fileFullName = path.Substring(path.LastIndexOf("\\") + 1);
            //文件名
            string fileName = fileFullName.Substring(0, fileFullName.LastIndexOf("."));

            int row = table.Rows.Count;
            int columns = table.Columns.Count;
            byte[] buffer = null;
            //头三行用来做脚本创建
            string[,] titleStr = null;
            using (MMO_MemoryStream ms = new MMO_MemoryStream())
            {
                //写入行 列数
                ms.WriteInt(row);
                ms.WriteInt(columns);

                titleStr = new string[columns, 3];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        //头三行 用来脚本信息
                        if (i < 3)
                        {
                            titleStr[j, i] = table.Rows[i][j].ToString().Trim();

                        }
                        ms.WriteUTF8String(table.Rows[i][j].ToString().Trim());
                    }
                }
                buffer = ms.ToArray();
            }


            //------------------
            //第1步：xor加密
            //------------------
            int iScaleLen = xorScale.Length;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
            }

            //------------------
            ////第2步:写入文件
            ////------------------
            FileStream fs = new FileStream(string.Format("{0}{1}.bytes", filePath, fileName), FileMode.Create);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();


            CreateEntity(filePath, fileName, titleStr);
            CreateDBModel(filePath, fileName, titleStr);
        }


        private void CreateEntity(string filePath, string fileName, string[,] titleArr)
        {
            if (!Directory.Exists(string.Format("{0}/Create", filePath)))
            {
                Directory.CreateDirectory(string.Format("{0}/Create", filePath));
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");

            sb.Append("//===================================================\r\n");
            sb.Append("//作    者：肖海林 \r\n");
            sb.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("//备    注：此代码为工具生成 请勿手工修改\r\n");
            sb.Append("//===================================================\r\n");
            sb.Append("using System.Collections;\r\n");

            sb.Append("/// <summary>\r\n");
            sb.Append("/// biao01实体\r\n");
            sb.Append("/// </summary>\r\n");
            sb.AppendFormat(" public partial class {0}Entity : AbstractEntity\r\n", fileName);
            sb.Append(" {\r\n");
            int column = titleArr.Length / 3;
            for (int i = 0; i < column; i++)
            {
                if (i == 0) continue;
                sb.Append("\t/// <summary>\r\n");
                sb.AppendFormat("\t/// {0}\r\n", titleArr[i, 2]);
                sb.Append("\t/// </summary>\r\n");
                sb.AppendFormat("\tpublic {0} {1} {{ get; set; }}\r\n", titleArr[i, 1], titleArr[i, 0]);

            }

            sb.Append("  }\r\n");


            using (FileStream fs = new FileStream(string.Format("{0}/Create/{1}Entity.cs", filePath, fileName), FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());
                fs.Write(buffer, 0, buffer.Length);
            }


        }

        private void CreateDBModel(string filePath, string fileName, string[,] titleArr)
        {

            if (!Directory.Exists(string.Format("{0}/Create", filePath)))
            {
                Directory.CreateDirectory(string.Format("{0}/Create", filePath));
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.Append(" //===================================================\r\n");
            sb.Append("//作    者：肖海林\r\n");
            sb.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("//备    注：此代码为工具生成 请勿手工修改\r\n");
            sb.Append("//===================================================\r\n");
            sb.Append("using System.Collections;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System;\r\n");

            sb.Append("/// <summary>\r\n");
            sb.AppendFormat("/// {0}数据管理\r\n", fileName);
            sb.Append("/// </summary>\r\n");
            sb.AppendFormat("public partial class {0}DBModel : AbstractDBModel<{1}DBModel, {2}Entity>\r\n", fileName, fileName, fileName);
            sb.Append(" {\r\n");
            sb.Append("\t/// <summary>\r\n");
            sb.Append("\t/// 文件名称\r\n");
            sb.Append("\t/// </summary>\r\n");
            sb.AppendFormat("\tprotected override string FileName {{ get {{ return \"{0}\"; }} }}\r\n", fileName);

            sb.Append("\t/// <summary>\r\n");
            sb.Append("\t/// 创建实体\r\n");
            sb.Append("\t/// </summary>\r\n");
            sb.Append("\t/// <param name=\"parse\"></param>\r\n");
            sb.Append("\t/// <returns></returns>\r\n");
            sb.AppendFormat("\tprotected override {0}Entity MakeEntity(GameDataTableParser parse)\r\n", fileName);
            sb.Append("\t{\r\n");
            sb.AppendFormat("\t\t{0}Entity entity = new {1}Entity();\r\n", fileName, fileName);
            int column = titleArr.Length / 3;
            for (int i = 0; i < column; i++)
            {
                string type = GetParseByType(titleArr[i, 1]);
                sb.AppendFormat("\t\tentity.{0} = parse.GetFieldValue(\"{1}\"){2};\r\n", titleArr[i, 0], titleArr[i, 0], type);
            }
            sb.Append("\t\treturn entity;\r\n");
            sb.Append(" \t}\r\n");
            sb.Append("}\r\n");

            using (FileStream fs = new FileStream(string.Format("{0}/Create/{1}DBModel.cs", filePath, fileName), FileMode.Create))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());
                fs.Write(buffer, 0, buffer.Length);
            }

        }

        private string GetParseByType(string type)
        {
            string value;
            switch (type)
            {
                case "int":
                    value = ".ToInt()";
                    break;
                case "float":
                    value = ".ToFloat()";
                    break;
                case "long":
                    value = ".ToLong()";
                    break;
                case "bool":
                    value = ".ToBoolean()";
                    break;
                default:
                    value = "";
                    break;
            }
            return value;

        }










        private void testReadIput_Click(object sender, EventArgs e)
        {
            //-----------
            //打开窗口 ，选择所有符合条件的exccl
            //----------
            if (this.openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in this.openFileDialog2.FileNames)
                {
                    using (GameDataTableParser parser = new GameDataTableParser(item))
                    {
                        StringBuilder sb = new StringBuilder();
                        while (!parser.Eof)
                        {
                            for (int i = 0; i < parser.FileNames.Length; i++)
                            {
                                string appendStr = parser.GetFileValue(parser.FileNames[i]);
                                if (appendStr != null)
                                {
                                    sb.Append(appendStr);
                                }
                            }
                            sb.Append("\r\n");
                            parser.Next();


                        }
                        testTextBox.Text += sb.ToString() + "\r\n";

                    }
                }

            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
