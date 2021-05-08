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
using System.Diagnostics;
using System.Data.Sql;
using System.Configuration;

namespace Design_Concrete
{
    public partial class alfabeta : Form
    {
        public alfabeta()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtlong.Text.Trim() == "" || txtshort.Text.Trim() == "" || txtcase1.Text.Trim() == ""
                || txtcase2.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtlong.Focus();
                    return;
                }
                else
                {
                    double Long = double.Parse(txtlong.Text);
                    double Short = double.Parse(txtshort.Text);
                    double case1 = double.Parse(txtcase1.Text);
                    double case2 = double.Parse(txtcase2.Text);

                    double r = (case1 * Long) / (case2 * Short);

                    if (r > 2)
                    {
                        MessageBox.Show("One Way Slab .. No need to Calculate Coeff.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    double alfa1 = Math.Round((0.5 * r - 0.15), 3);
                    double beta1 = Math.Round((0.35 / (r * r)), 3);
                    double alfa2 = Math.Round(((r * r * r * r) / (1 + (r * r * r * r))), 3);
                    double beta2 = Math.Round((1 / (1 + (r * r * r * r))), 3);

                    ///////////////
                    /// Table 
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(dr == DialogResult.OK)
                    {
                        string serial = "";
                        string Longg = txtlong.Text;
                        string Case11 = txtcase1.Text;
                        string Shortt = txtshort.Text;
                        string Case22 = txtcase2.Text;
                        string alfa11 = alfa1.ToString();
                        string beta11 = beta1.ToString();
                        string alfa22 = alfa2.ToString();
                        string beta22 = beta2.ToString();

                        object[] data = { serial, Longg, Case11, Shortt, Case22, alfa11, beta11, alfa22, beta22 };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }
                                   

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
          try
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Images|*.bmp;*.jpg";
                sf.Title = "Coefficient of Slabs";
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                Graphics g = Graphics.FromImage(bmp);
                panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string path = sf.FileName;
                    bmp.Save(path);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Do you Want to Delete This Row?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        DataGridView1.Rows.Remove(DataGridView1.CurrentRow);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Do you Want to Delete All Cells?", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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
        private void btn6_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }

        }
    }
}
