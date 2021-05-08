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
    public partial class PMM : Form
    {
        public PMM()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Get α")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                double fcu = double.Parse(txtfcu.Text);
                double t = double.Parse(txtt.Text);
                double b = double.Parse(txtb.Text);
                double P = double.Parse(txtp.Text);
                double Rb = (P * 1000) / (fcu * b * t);
                if (Rb > 0.4)
                {
                    MessageBox.Show("Not Suitable Using This Method... Rb > 0.4  "  +  " Rb = " + Math.Round(Rb,2) , "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                 else
                {
                    alfab alfa = new alfab(this);
                    alfa.ShowDialog();
                    txtalfa.Text = alfa.txtalfasend.Text;

                }
               

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Get ρ from I.D")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                double fcu = double.Parse(txtfcu.Text);

                double t = double.Parse(txtt.Text);
                double b = double.Parse(txtb.Text);
                double P = double.Parse(txtp.Text);
                double Rb = (P * 1000) / (fcu * b * t);
                if(radioButton2.Checked == true)
                {
                    if (txtalfa.Text.Trim() == "")
                    {
                        MessageBox.Show("Get α Firstly then Get ρ .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                 
                    else
                    {
                        getro ro = new getro(this);
                        ro.ShowDialog();
                        txtro1.Text = ro.txtro1send.Text;
                        s.Text = ro.txtro2send.Text;
                    }
                }   
                if(radioButton1.Checked == true)
                {
                    getro ro = new getro(this);
                    ro.ShowDialog();
                    txtro1.Text = ro.txtro1send.Text;
                    s.Text = ro.txtro2send.Text;
                }            
                
               
               

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Solid Slab (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void PMM_Load(object sender, EventArgs e)
        {
            groupBox9.Enabled = false;
            label25.Enabled = false;
            s.Enabled = false;
            radioButton1.Checked = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                groupBox9.Enabled = false;
                label25.Enabled = false;
                s.Enabled = false;
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            if (radioButton2.Checked == true)
            {
                label25.Enabled = true;
                s.Enabled = true;
                groupBox9.Enabled = true;
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            try
            {
                if (radioButton1.Checked == true)
                {
                    // Symmetrical 
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtt.Text.Trim() == "" || txtb.Text.Trim() == ""
                        || txtc.Text.Trim() == "" || txtp.Text.Trim() == "" || txtmx.Text.Trim() == "" || txtmy.Text.Trim() == ""
                        || txtro1.Text.Trim() == "" || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "" || txtfai3.Text.Trim() == ""
                        || txtfai4.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double t = double.Parse(txtt.Text);
                        double b = double.Parse(txtb.Text);
                        double cover = double.Parse(txtc.Text);
                        double Ro1 = double.Parse(txtro1.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int fai2 = int.Parse(txtfai2.Text);
                        int fai3 = int.Parse(txtfai3.Text);
                        int fai4 = int.Parse(txtfai4.Text);
                        double d = t - cover;            // mm

                        double mio = Ro1 * fcu * Math.Pow(10, -4);
                        double As1 = mio * b * t;
                        double AS2 = mio * b * t;
                        double Astotal = As1 + AS2;
                        double Asmin = 0.8 * 0.01 * b * t;
                        double Asdesign;
                        if (Asmin > Astotal)
                        {
                            Asdesign = Asmin;
                        }
                        else
                        {
                            Asdesign = Astotal;
                        }
                        double Asside = Asdesign / 4;
                        double num1 = Math.Ceiling(Asside / (3.1459 * 0.25 * fai1 * fai1));
                        double num2 = Math.Ceiling(Asside / (3.1459 * 0.25 * fai2 * fai2));
                        double num3 = Math.Ceiling(Asside / (3.1459 * 0.25 * fai3 * fai3));
                        double num4 = Math.Ceiling(Asside / (3.1459 * 0.25 * fai4 * fai4));

                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();
                        txtnum3.Text = num3.ToString();
                        txtnum4.Text = num4.ToString();
                    }


                    ///////////////////
                    // Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Tabe ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string tt = txtt.Text;
                        string PP = txtp.Text;
                        string MXX = txtmx.Text;
                        string MYY = txtmy.Text;
                        string ASn1 = txtnum1.Text + " T " + txtfai1.Text;
                        string ASn2 = txtnum2.Text + " T " + txtfai2.Text;
                        string ASn3 = txtnum3.Text + " T " + txtfai3.Text;
                        string ASn4 = txtnum4.Text + " T " + txtfai4.Text;
                        string type = "Symmetrical";
                        object[] data = { serial, bb, tt, PP, MXX, MYY, ASn1, ASn2, ASn3, ASn4, type };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }


                ////////////////////////////////
                if (radioButton2.Checked == true)
                {
                    if (s.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {

                        double fcu = double.Parse(txtfcu.Text);
                        double t = double.Parse(txtt.Text);
                        double b = double.Parse(txtb.Text);
                        double cover = double.Parse(txtc.Text);
                        double Ro1 = double.Parse(txtro1.Text);
                        double Ro2 = double.Parse(s.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int fai2 = int.Parse(txtfai2.Text);
                        int fai3 = int.Parse(txtfai3.Text);
                        int fai4 = int.Parse(txtfai4.Text);
                        double d = t - cover;            // mm

                        double mio1 = Ro1 * fcu * Math.Pow(10, -4);
                        double mio2 = Ro2 * fcu * Math.Pow(10, -4);
                        double Asx = mio1 * b * t;
                        double Asy = mio2 * b * t;
                        double Astotal = 2 * Asx + 2 * Asy;
                        double Asmin = 0.8 * 0.01 * b * t;
                        double Asxx;
                        double Asyy;

                        if (Asmin > Astotal)
                        {
                            Asxx = Asmin / 4;
                            Asyy = Asmin / 4;
                            double num1 = Math.Ceiling(Asxx / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(Asxx / (3.1459 * 0.25 * fai2 * fai2));
                            double num3 = Math.Ceiling(Asyy / (3.1459 * 0.25 * fai3 * fai3));
                            double num4 = Math.Ceiling(Asyy / (3.1459 * 0.25 * fai4 * fai4));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            txtnum3.Text = num3.ToString();
                            txtnum4.Text = num4.ToString();
                        }

                        else if (Asmin < Astotal)
                        {
                            Asxx = Asx;
                            Asyy = Asy;
                            double num1 = Math.Ceiling(Asxx / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(Asxx / (3.1459 * 0.25 * fai2 * fai2));
                            double num3 = Math.Ceiling(Asyy / (3.1459 * 0.25 * fai3 * fai3));
                            double num4 = Math.Ceiling(Asyy / (3.1459 * 0.25 * fai4 * fai4));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            txtnum3.Text = num3.ToString();
                            txtnum4.Text = num4.ToString();
                        }


                    }
                    ///////////////////
                    // Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Tabe ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string tt = txtt.Text;
                        string PP = txtp.Text;
                        string MXX = txtmx.Text;
                        string MYY = txtmy.Text;
                        string ASn1 = txtnum1.Text + " T " + txtfai1.Text;
                        string ASn2 = txtnum2.Text + " T " + txtfai2.Text;
                        string ASn3 = txtnum3.Text + " T " + txtfai3.Text;
                        string ASn4 = txtnum4.Text + " T " + txtfai4.Text;
                        string type = "UnSymmetrical";
                        object[] data = { serial, bb, tt, PP, MXX, MYY, ASn1, ASn2, ASn3, ASn4, type };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
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
        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                codePMM code = new codePMM();               
                code.ShowDialog();

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtnum1.Clear();
            txtnum2.Clear();
            txtnum3.Clear();
            txtnum4.Clear();


            txtalfa.Clear();
            txtro1.Clear();
            s.Clear();

        }
    }
}
