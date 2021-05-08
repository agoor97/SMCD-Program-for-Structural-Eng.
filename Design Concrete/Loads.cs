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
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Diagnostics;
using System.Configuration;
namespace Design_Concrete
{
    public partial class Loads : Form
    {
        public Loads()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           try
            {
                if (txte.Text.Trim() == "" || txta.Text.Trim() == "" || txtb.Text.Trim() == "" || txtfc.Text.Trim() == ""
               || txtLL.Text.Trim() == "" || txtt.Text.Trim() == "" || txtts.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtfc.Focus();
                    return;
                }
                else
                {
                    double ee = double.Parse(txte.Text);
                    double aa = double.Parse(txta.Text);
                    double bb = double.Parse(txtb.Text);
                    double Fc = double.Parse(txtfc.Text);
                    double LL = double.Parse(txtLL.Text);
                    double t = double.Parse(txtt.Text);
                    double ts = double.Parse(txtts.Text);
                    double wblock = double.Parse(txtblock.Text);

                    double H = t - ts;

                    double S = ee + bb;               // mm

                    if (radio1.Checked == true)
                    {
                        double Numb = 1000 / aa;

                        double w = ((1.4 * ((0.001 * ts * 25) + Fc) + 1.6 * LL) * 0.001 * S) + (1.4 * 0.001 * bb * H * 0.001 * 25 * 1.0) + (wblock * 0.001 * 1.4 * Numb);
                        w = Math.Round(w, 2);


                        //// Table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if(dr == DialogResult.OK)
                        {
                            string serial = "";
                            string e1 = txte.Text;
                            string a1 = txta.Text;
                            string b1 = txtb.Text;
                            string t1 = txtt.Text;
                            string ts1 = txtts.Text;
                            string wb1 = txtblock.Text;
                            string fc = txtfc.Text;
                            string LL1 = txtLL.Text;
                            string w1 = w.ToString();
                            string type = "One Way";

                            object[] data = { serial, e1, a1, b1, t1, ts1, wb1, fc, LL1, w1, type };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }

                    }


                    ////

                    if (radio2.Checked == true)
                    {
                        double Numb = ee / aa;
                        double Lribs = 0.001 * (2 * S - bb);    //mm
                        double w = ((1.4 * ((0.001 * ts * 25) + Fc) + 1.6 * LL) * 0.001 * S * 0.001 * S) + (1.4 * 0.001 * bb * H * 0.001 * 25 * Lribs) + (wblock * 0.001 * 1.4 * Numb);
                        double W11 = w / (0.001 * S);

                        W11 = Math.Round(W11, 2);


                        //// Table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string e1 = txte.Text;
                            string a1 = txta.Text;
                            string b1 = txtb.Text;
                            string t1 = txtt.Text;
                            string ts1 = txtts.Text;
                            string wb1 = txtblock.Text;
                            string fc = txtfc.Text;
                            string LL1 = txtLL.Text;
                            string W1 = W11.ToString();
                            string type = "Two Way";

                            object[] data = { serial, e1, a1, b1, t1, ts1, wb1, fc, LL1, W1, type };
                            DataGridView1.Rows.Add(data);
                            return;

                        }
                        else
                        {
                            return;
                        }
                    
                  

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void loads_Load(object sender, EventArgs e)
        {
            radio1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = "Capacity of Slab (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Do you Want to Remove this Row ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (dr == DialogResult.OK)
                    {
                        DataGridView1.Rows.Remove(DataGridView1.CurrentRow);
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
            bool ISOPEN = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ECP 203-2018  H.B")
                {
                    ISOPEN = true;
                    f.Focus();
                    break;
                }
            }
            if (ISOPEN == false)
            {
                codeloads code = new codeloads();

                code.Show();
            }
        }
    }
}
