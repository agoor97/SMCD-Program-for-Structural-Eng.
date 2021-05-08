using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using ExcelDataReader;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Configuration;

namespace Design_Concrete
{
    public partial class columnImport : Form
    {
        public columnImport()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Excel workbook|*.xlsx", ValidateNames = true })
                {

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        //////
                        ////////////// هنا علشان يمسح header
                        DataGridView1.Columns.Clear();
                        /////


                        /////// هنا يكتب كود الاستلام بتاع الاكسيل 
                        string path = dialog.FileName;
                        string Constr = "PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties = 'Excel 12.0;HDR=yes'";
                        OleDbConnection con = new OleDbConnection(Constr);
                        OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$] ", con);
                        con.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        DataGridView1.DataSource = dt;


                        con.Close();

                    }
                }


                ///////////
                double Rows = DataGridView1.RowCount;

                for (int i = 1; i < Rows; i++)
                {
                    string bb = "250";
                    string faifai = "16";
                    object[] data = { bb, faifai };
                    DataGridView3.Rows.Add(data);
                    DataGridView2.Rows.Add();
                    DataGridView4.Rows.Add();
                }

                /////////////////
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {         
            try
            {
                double fcu = double.Parse(txtfcu.Text);
                double fy = double.Parse(txtfy.Text);
                double mio = double.Parse(txtmio.Text);
                double Nofloors = double.Parse(txtnfloors.Text);


                for (int i = 0; i < DataGridView1.Rows.Count - 1; i++)
                {
                    double R = Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value);
                    double Ptotal = R * Nofloors * 1.1;
                    DataGridView4.Rows[i].Cells[0].Value = Math.Round(Ptotal, 2).ToString();

                    double b = Convert.ToDouble(DataGridView3.Rows[i].Cells[0].Value);
                    double fai = Convert.ToDouble(DataGridView3.Rows[i].Cells[1].Value);

                    double Ac = (Ptotal * 1000) / (0.35 * fcu - 0.35 * fcu * 0.01 * mio + 0.67 * fy * 0.01 * mio);

                    double t = Ac / b;
                   
                    //// قرب لاقرب 50 مم                
                    t = 50 * Math.Ceiling(t / 50);

                    if (t < 250)
                    {
                        t = 250;
                    }

                    DataGridView2.Rows[i].Cells[0].Value = DataGridView1.Rows[i].Cells[0].Value;
                    DataGridView2.Rows[i].Cells[1].Value = DataGridView3.Rows[i].Cells[0].Value + " * " + t.ToString();

                    double RFT = 0.01 * b * t;
                    double num = Math.Ceiling(RFT / (3.1459 * 0.25 * fai * fai));

                    DataGridView2.Rows[i].Cells[2].Value = num + " T " + DataGridView3.Rows[i].Cells[1].Value;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {              
                //DataGridView1.ColumnCount = 2;
                //DataGridView1.Columns[0].Name = "ID";
                //DataGridView1.Columns[1].Name = "Pu (kN)";
                //DataGridView1.Columns[0].Width = 73;
                //DataGridView1.Columns[1].Width = 114;


                DataGridView2.Rows.Clear();
                DataGridView3.Rows.Clear();
                DataGridView4.Rows.Clear();
                DataGridView1.Columns.Clear();
                DataGridView1.Columns.Add("column1", "ID");
                DataGridView1.Columns.Add("column2", "Pu (kN)");
                DataGridView1.Refresh();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void ToExcel(DataGridView dgv, string filename)
        {
            string stoutput = "";
            string sheader = "";
            for (int j = 0; j < dgv.Columns.Count; j++)
                sheader = sheader.ToString() + Convert.ToString(dgv.Columns[j].HeaderText) + "\t";
            stoutput += sheader + "\r\n";

            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dgv.Rows[i].Cells[j].Value) + "\t";
                stoutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stoutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length);
            bw.Flush();
            bw.Close();
            fs.Close();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView2, path);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColumnImportLink Linkk = new ColumnImportLink();
            Linkk.ShowDialog();

               
        }
    }
}
