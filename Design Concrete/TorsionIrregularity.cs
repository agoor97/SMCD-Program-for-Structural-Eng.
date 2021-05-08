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
    public partial class TorsionIrregularity : Form
    {
        public TorsionIrregularity()
        {
            InitializeComponent();
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


                for (int i = 0; i < DataGridView1.Rows.Count - 1; i++)
                {
                    double D = Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value);

                    double max = Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);
                    double avg = Convert.ToDouble(DataGridView1.Rows[i].Cells[3].Value);
                    double ratio = Convert.ToDouble(DataGridView1.Rows[i].Cells[4].Value);

                    //// Ax
                    double Ax = Math.Round((max / (1.2 * avg)) * (max / (1.2 * avg)),3);

                    double e1 = 0.05 * Ax;
                    double e2 = e1 * D;

                    if(ratio < 1.2)
                    {
                        DataGridView1.Rows[i].Cells[5].Value = "Regular";
                        DataGridView1.Rows[i].Cells[6].Value = "--";
                        DataGridView1.Rows[i].Cells[7].Value = "--";
                        DataGridView1.Rows[i].Cells[8].Value = "--";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[5].Value = "T.Irregularity";
                        DataGridView1.Rows[i].Cells[6].Value = Ax;
                        DataGridView1.Rows[i].Cells[7].Value = e1;
                        DataGridView1.Rows[i].Cells[8].Value = e2;
                    }

  
               
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            codetorsionirre reff = new codetorsionirre();
            reff.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            codetorsionirre code = new codetorsionirre();
            code.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            torsionalLink Link1 = new torsionalLink();
            Link1.ShowDialog();
        }
    }
}
