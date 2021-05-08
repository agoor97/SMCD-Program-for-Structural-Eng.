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
    public partial class HeavyStory : Form
    {
        public HeavyStory()
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
            codeheavy heavy = new codeheavy();
            heavy.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

               


                for (int i = 2; i < DataGridView1.Rows.Count - 1; i++)
                {

                    ///////firstly for x
                    double mi = Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value);
                    double mii = Convert.ToDouble(DataGridView1.Rows[i-1].Cells[1].Value);

                    double ratiox1 = Math.Round((mi / mii), 2);

                    DataGridView1.Rows[i].Cells[3].Value = ratiox1;

                    if (ratiox1 <= 1.5)
                    {
                        DataGridView1.Rows[i].Cells[4].Value = "Regular";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[4].Value = "IrrRegular";
                    }

                   

                }


                /////

              
                for (int i = 1; i < DataGridView1.Rows.Count - 2 ; i++)
                {

                    ///////firstly for x
                    double mi = Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value);
                    double mii = Convert.ToDouble(DataGridView1.Rows[i+1].Cells[1].Value);
               

                    double ratiox2 = Math.Round((mi / mii), 2);

                    DataGridView1.Rows[i].Cells[5].Value = ratiox2;

                    if (ratiox2 <=  1.5)
                    {
                        DataGridView1.Rows[i].Cells[6].Value = "Regular";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[6].Value = "IrrRegular";
                    }

                  

                }

                //////
                /////////////// for y
    
               for (int i = 2; i < DataGridView1.Rows.Count - 1; i++)
                {

                    //////secondly for y 
                    double mi = Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);
                    double mii = Convert.ToDouble(DataGridView1.Rows[i-1].Cells[2].Value);

                    double ratioy1 = Math.Round((mi / mii), 2);

                    DataGridView1.Rows[i].Cells[7].Value = ratioy1;

                    if (ratioy1 <= 1.5)
                    {
                        DataGridView1.Rows[i].Cells[8].Value = "Regular";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[8].Value = "IrrRegular";
                    }

                
                }


                /////

                for (int i = 1; i < DataGridView1.Rows.Count -2; i++)
                {

                    ///////secondly for y
                    double mi = Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);                  
                    double mii = Convert.ToDouble(DataGridView1.Rows[i+1].Cells[2].Value);

              


                    double ratioy2 = Math.Round((mi / mii), 2);

                    DataGridView1.Rows[i].Cells[9].Value = ratioy2;

                    if (ratioy2 <= 1.5)
                    {
                        DataGridView1.Rows[i].Cells[10].Value = "Regular";
                    }
                    else
                    {
                        DataGridView1.Rows[i].Cells[10].Value = "IrrRegular";
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
            HeavyLink Link4 = new HeavyLink();
            Link4.ShowDialog();
        }
    }
}
