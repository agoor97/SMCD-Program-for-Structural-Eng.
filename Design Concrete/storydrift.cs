using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Configuration;

namespace Design_Concrete
{
    public partial class storydrift : Form
    {
        public storydrift()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Do you Want to Delete all Rows ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        DataGridView1.Rows.Clear();
                    }
                }
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            codestorydrift drift = new codestorydrift();
            drift.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string s = Clipboard.GetText();

                string[] lines = s.Replace("\n", "").Split('\r');

                DataGridView1.Rows.Add(lines.Length - 1);
                string[] fields;
                int row = 0;
                int col = 0;

                foreach (string item in lines)
                {
                    fields = item.Split('\t');
                    foreach (string f in fields)
                    {
                        Console.WriteLine(f);
                        DataGridView1[col, row].Value = f;
                        col++;
                    }
                    row++;
                    col = 0;
                }
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

                double R = double.Parse(txtR.Text);
                double v = double.Parse(txtv.Text);
                double ALL = double.Parse(txtall.Text);

               
                for (int i = 0; i < DataGridView1.Rows.Count - 1; i++)
                {

                    double dx1 = Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value);
                    double dy1 = Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);

                    ////// convet to Elastic 
                    double dxs1 = 0.7 * R * dx1;
                    double dys1 = 0.7 * R * dy1;

                    /// convert to S.L.S
                    double drvx = dxs1 * v;
                    double drvy = dys1 * v;

                    DataGridView1.Rows[i].Cells[3].Value = Math.Round(drvx, 4);
                    DataGridView1.Rows[i].Cells[4].Value = Math.Round(drvy, 4);

                    if (drvx <= ALL)
                    {
                        DataGridView1.Rows[i].Cells[5].Value = "Safe";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[5].Value = "UnSafe";
                    }

                    /////
                    if (drvy <= ALL)
                    {
                        DataGridView1.Rows[i].Cells[6].Value = "Safe";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[6].Value = "UnSafe";
                    }

                }              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            driftLink Link2 = new driftLink();
            Link2.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
