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
    public partial class longcolumn : Form
    {
        public longcolumn()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

      

        private void longcolumn_Load(object sender, EventArgs e)
        {
            txtm.Enabled = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Get ρ From I.D")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                if(txtkin.Text.Trim()==""||txtkout.Text.Trim()=="")
                {
                    MessageBox.Show(" Get End Conditions Firstly then Get  ρ ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if(checkBox1.Checked == false)
                {
                    if (txtt.Text.Trim() == "" || txtb.Text.Trim() == "" || txtfcu.Text.Trim() == ""
                    || txtfy.Text.Trim() == "" || txtc.Text.Trim() == "" || txtp.Text.Trim() == ""
                    || txthin.Text.Trim() == ""||txthin.Text.Trim()==""||txthout.Text.Trim()=="")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        getrolong rolong = new getrolong(this);
                        rolong.ShowDialog();
                        txtro.Text = rolong.txtro1.Text;
                    }
                }

                //
                if (checkBox1.Checked == true)
                {                   
                    if (txtt.Text.Trim() == "" || txtb.Text.Trim() == "" || txtfcu.Text.Trim() == ""
                    || txtfy.Text.Trim() == "" || txtc.Text.Trim() == "" || txtp.Text.Trim() == ""
                    || txthin.Text.Trim() == ""||txtm.Text.Trim()==""
                    || txthin.Text.Trim() == "" || txthout.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    

                    else
                    {

                        getrolong rolong = new getrolong(this);
                        rolong.ShowDialog();
                        txtro.Text = rolong.txtro1.Text;
                    }
                }



            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Get K (End Conditions)")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               getKlong klong = new getKlong();
                klong.ShowDialog();
                txtkin.Text = klong.txtkin1.Text;
                txtkout.Text = klong.txtkout1.Text;
                if(klong.rdiobraced.Checked == true)
                {
                    typebraced.Text = "Braced";
                }

                if (klong.rdiounbraced.Checked == true)
                {
                    typebraced.Text = "UnBraced";
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                if (txtkin.Text.Trim() == "" || txtkout.Text.Trim() == "")
                {
                    MessageBox.Show("Get End Conditions ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                else
                {
                    double kin = double.Parse(txtkin.Text);
                    double Kout = double.Parse(txtkout.Text);
                    double t = double.Parse(txtt.Text);
                    double b = double.Parse(txtb.Text);
                    double cover = double.Parse(txtc.Text);
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double P = double.Parse(txtp.Text);

                    double H0in = double.Parse(txthin.Text);
                    double H0out = double.Parse(txthout.Text);
                    double Lamdain = kin * H0in / (0.001 * t);
                    double Lamdaout = Kout * H0out / (0.001 * b);
                    double LamdaMax = Math.Max(Lamdain, Lamdaout);

                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);
                    int fai3 = int.Parse(txtfai3.Text);
                    int fai4 = int.Parse(txtfai4.Text);
                    double d = t - cover;

                    double Asmin = (0.25 + 0.052 * LamdaMax) * 0.01 * b * t;





                    if (pictureBox1.Visible == true)
                    {
                        if(txtro.Text.Trim()=="")
                        {
                            MessageBox.Show("Get ρ firstly then Click Design ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        double Roo = double.Parse(txtro.Text);
                        double mio = Roo * fcu * Math.Pow(10, -4);
                        double As3 = mio * b * t;
                        double As4 = mio * b * t;
                        double Astotal = As3 + As4;
                        if (Asmin > Astotal)
                        {
                            Astotal = Asmin;
                            As3 = 0.5 * Asmin;
                            As4 = 0.5 * Asmin;
                            double num3 = Math.Ceiling(As3 / (3.1459 * 0.25 * fai3 * fai3));
                            double num4 = Math.Ceiling(As4 / (3.1459 * 0.25 * fai4 * fai4));
                            txtnum3.Text = num3.ToString();
                            txtnum4.Text = num4.ToString();
                        }
                        else
                        {
                            double num3 = Math.Ceiling(As3 / (3.1459 * 0.25 * fai3 * fai3));
                            double num4 = Math.Ceiling(As4 / (3.1459 * 0.25 * fai4 * fai4));
                            txtnum3.Text = num3.ToString();
                            txtnum4.Text = num4.ToString();
                        }

                        /// table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sreial = "";
                            string bb = txtb.Text;
                            string tt = txtt.Text;

                            string n1 = "---";
                            string n2 = "---";
                            string n3 = txtnum3.Text + " T " + txtfai3.Text;
                            string n4 = txtnum4.Text + " T " + txtfai4.Text;

                            object[] data = { sreial, bb, tt, n1, n2, n3, n4 };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }



                    if (pictureBox2.Visible == true)
                    {
                        if (txtro.Text.Trim() == "")
                        {
                            MessageBox.Show("Get ρ firstly then Click Design ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        double Roo = double.Parse(txtro.Text);
                        /// يبقا هنا استخدمت المنحنى التفاعلي 
                        double mio = Roo * fcu * Math.Pow(10, -4);
                        double As1 = mio * b * t;
                        double As2 = mio * b * t;
                        double Astotal = As1 + As2;
                        if (Asmin > Astotal)
                        {
                            Astotal = Asmin;
                            As1 = 0.5 * Asmin;
                            As2 = 0.5 * Asmin;
                            double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                        }
                        else
                        {
                            double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                        }


                        /// table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sreial = "";
                            string bb = txtb.Text;
                            string tt = txtt.Text;

                            string n1 = txtnum1.Text + " T " + txtfai1.Text;
                            string n2 = txtnum2.Text + " T " + txtfai2.Text;
                            string n3 = "---";
                            string n4 = "---";

                            object[] data = { sreial, bb, tt, n1, n2, n3, n4 };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }



                    if (pictureBox4.Visible == true)
                    {
                        if (txtro.Text.Trim() == "")
                        {
                            MessageBox.Show("Get ρ firstly then Click Design ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        double Roo = double.Parse(txtro.Text);
                        /// يبقا هنا استخدمت المنحنى التفاعلي 
                        double mio = Roo * fcu * Math.Pow(10, -4);
                        double As1 = mio * b * t;
                        double As2 = mio * b * t;
                        double As3;
                        double As4;
                        double Astotal = As1 + As2;
                        if (Asmin > Astotal)
                        {
                            Astotal = Asmin;
                            As1 = 0.25 * Asmin;
                            As2 = 0.25 * Asmin;
                            As3 = 0.25 * Asmin;
                            As4 = 0.25 * Asmin;
                            double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));
                            double num3 = Math.Ceiling(As3 / (3.1459 * 0.25 * fai3 * fai3));
                            double num4 = Math.Ceiling(As4 / (3.1459 * 0.25 * fai4 * fai4));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            txtnum3.Text = num3.ToString();
                            txtnum4.Text = num4.ToString();
                        }
                        else
                        {
                            As1 = 0.25 * Astotal;
                            As2 = 0.25 * Astotal;
                            As3 = 0.25 * Astotal;
                            As4 = 0.25 * Astotal;
                            double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));
                            double num3 = Math.Ceiling(As3 / (3.1459 * 0.25 * fai3 * fai3));
                            double num4 = Math.Ceiling(As4 / (3.1459 * 0.25 * fai4 * fai4));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            txtnum3.Text = num3.ToString();
                            txtnum4.Text = num4.ToString();
                        }

                        /// table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sreial = "";
                            string bb = txtb.Text;
                            string tt = txtt.Text;

                            string n1 = txtnum1.Text + " T " + txtfai1.Text;
                            string n2 = txtnum2.Text + " T " + txtfai2.Text;
                            string n3 = txtnum3.Text + " T " + txtfai3.Text;
                            string n4 = txtnum4.Text + " T " + txtfai4.Text;

                            object[] data = { sreial, bb, tt, n1, n2, n3, n4 };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }

                    }

                    if (pictureBox3.Visible == true)
                    {
                        double M = double.Parse(txtm.Text);
                        double delta = Lamdain * Lamdain * (0.001 * t) / 2000;
                        double Madd = P * delta;
                        double Mtotal = M + Madd;
                        double ecc = Mtotal / P;
                        double ecc2 = ecc + (0.001 * 0.5 * t) - (0.001 * cover);
                        double Ms = P * ecc2;
                        double amax = (320 * d) / (600 + 0.87 * fy);
                        double a = d * (1 - Math.Sqrt(1 - ((2 * Ms * 1000 * 1000) / (0.45 * fcu * b * d * d))));
                        if (a > amax)
                        {
                            MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        double C = 1.25 * a;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)
                        {
                            J = 0.826;
                        }
                        double As1 = (Ms * 1000 * 1000) / (fy * J * d) - (P * 1000) / (0.87 * fy);

                        // check min 

                        if (Asmin > As1)
                        {
                            As1 = Asmin;
                            double As2 = 0.4 * As1;
                            double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();

                        }
                        else
                        {
                            double As2 = 0.4 * As1;
                            double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                        }

                        /// table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string sreial = "";
                            string bb = txtb.Text;
                            string tt = txtt.Text;

                            string n1 = txtnum1.Text + " T " + txtfai1.Text;
                            string n2 = txtnum2.Text + " T " + txtfai2.Text;
                            string n3 = "---";
                            string n4 = "---";

                            object[] data = { sreial, bb, tt, n1, n2, n3, n4 };
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

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBox1.Checked == false)
            {
                txtm.Enabled = false;
            }
            if(checkBox1.Checked == true)
            {
                txtm.Enabled = true;
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void txtm_TextChanged(object sender, EventArgs e)
        {

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

        private void button5_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ECP 203-2018")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               codelong codelong = new codelong();

                codelong.ShowDialog();

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtnum1.Clear();
            txtnum2.Clear();
            txtnum3.Clear();
            txtnum4.Clear();


            txtkin.Clear();
            txtkout.Clear();

            txtro.Clear();
            typebraced.Text = "";
        }
    }
}
