using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Design_Concrete
{
    public partial class onewayHb : Form
    {
        public onewayHb()
        {
            InitializeComponent();
        }

        private void btn3_Click(object sender, EventArgs e)
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

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtc.Text.Trim() == "" || txtt.Text.Trim() == "" || txtw.Text.Trim() == "" ||
                txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "" || txtfai3.Text.Trim() == ""
                || txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtLx.Text.Trim() == ""
                || txtLy.Text.Trim() == "" || txtm1.Text.Trim() == "" || txtm2.Text.Trim() == ""
                || txtm3.Text.Trim() == "" || txttype.Text.Trim() == "" || txte.Text.Trim() == ""
                || txta.Text.Trim() == "" || txtb.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtfcu.Focus();
                    return;
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double t = double.Parse(txtt.Text);
                    double Cov = double.Parse(txtc.Text);
                    double ee = double.Parse(txte.Text);
                    double aa = double.Parse(txta.Text);
                    double bb = double.Parse(txtb.Text);
                    double m1 = double.Parse(txtm1.Text);
                    double m2 = double.Parse(txtm2.Text);
                    double m3 = double.Parse(txtm3.Text);
                    double W = double.Parse(txtw.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);
                    int fai3 = int.Parse(txtfai3.Text);
                    double Lx = double.Parse(txtLx.Text);
                    double Ly = double.Parse(txtLy.Text);
                    double d = t - Cov;         //// mm
                    double B = ee + bb;          //m
                    double Mr;
                    double X1;
                    double X2;
                    double X3;
                    double X4;
                    double amax = (320 * d) / (600 + 0.87 * fy);

                    /// Check Number of x-ribs required 
                    if (checkBox1.Checked == false)
                    {
                        if (Ly <= 5)
                        {
                            txtrib.Text = "0";
                        }
                        if (Ly > 5)
                        {
                            txtrib.Text = "1";
                        }

                    }


                    if (checkBox1.Checked == true)
                    {
                        if (Ly >= 4 && Ly <= 7)
                        {
                            txtrib.Text = "1";
                        }
                        if (Ly > 7)
                        {
                            txtrib.Text = "3";
                        }
                    }
                    int Numbrib = int.Parse(txtrib.Text);     //// to use it in the arrangement of blocks

                    // Get M/rib
                    if (fy == 360 || fy == 350)      // Old Code= 360 , New = 350;
                    {
                        Mr = 0.194 * (fcu / 1.5) * bb * 1000 * d * d * Math.Pow(10, -6);
                    }
                    if (fy == 400)
                    {
                        Mr = 0.187 * (fcu / 1.5) * bb * 1000 * d * d * Math.Pow(10, -6);
                    }
                    if (fy == 240)
                    {
                        Mr = 0.214 * (fcu / 1.5) * bb * 1000 * d * d * Math.Pow(10, -6);
                    }
                    if (fy == 280)
                    {
                        Mr = 0.208 * (fcu / 1.5) * bb * 1000 * d * d * Math.Pow(10, -6);
                    }
                    else
                    {
                        Mr = 0.194 * (fcu / 1.5) * bb * 1000 * d * d * Math.Pow(10, -6);
                    }



                    if (txttype.Text == "Case_1" || txttype.Text == "Case_3")
                    {

                        double a1 = d * (1 - Math.Sqrt(1 - ((2 * m1 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                        if (a1 > amax)
                        {
                            MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C1 = 1.25 * a1;
                        double J1 = (1 / 1.15) * (1 - 0.4 * (C1 / d));
                        if (J1 > 0.826)
                        {
                            J1 = 0.826;            /////"Ducyility Condition:"
                        }

                        double AS1 = (m1 * Math.Pow(10, 6)) / (fy * J1 * d);
                        double num1 = Math.Ceiling((AS1 / (3.1459 * 0.25 * fai1 * fai1)));
                        if (num1 < 2)
                        {
                            num1 = 2;
                        }
                        txtnum1.Text = num1.ToString();

                        /////////////////

                        double a2 = d * (1 - Math.Sqrt(1 - ((2 * m2 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                        if (a2 > amax)
                        {
                            MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C2 = 1.25 * a2;
                        double J2 = (1 / 1.15) * (1 - 0.4 * (C2 / d));
                        if (J2 > 0.826)
                        {
                            J2 = 0.826;            /////"Ducyility Condition:"
                        }

                        double AS2 = (m2 * Math.Pow(10, 6)) / (fy * J2 * d);
                        double num2 = Math.Ceiling((AS2 / (3.1459 * 0.25 * fai2 * fai2)));
                        if (num2 < 2)
                        {
                            num2 = 2;
                        }
                        txtnum2.Text = num2.ToString();


                        ////////

                        double a3 = d * (1 - Math.Sqrt(1 - ((2 * m3 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                        if (a3 > amax)
                        {
                            MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C3 = 1.25 * a3;
                        double J3 = (1 / 1.15) * (1 - 0.4 * (C3 / d));
                        if (J3 > 0.826)
                        {
                            J3 = 0.826;            /////"Ducyility Condition:"
                        }

                        double AS3 = (m3 * Math.Pow(10, 6)) / (fy * J3 * d);
                        double num3 = Math.Ceiling((AS3 / (3.1459 * 0.25 * fai3 * fai3)));
                        if (num3 < 2)
                        {
                            num3 = 2;
                        }
                        txtnum3.Text = num3.ToString();


                        //// Calculation of Solid Part 
                        double R1 = (W * 0.5 * Ly * Ly + m1 - m3) / Ly;
                        double R2 = (W * 0.5 * Ly * Ly + m3 - m1) / Ly;
                        if (m1 > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * W * (2 * m1 - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * W));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * W));
                            X1 = Math.Min(X11, X12);
                            if (X1 < 0.25)
                            {
                                X1 = 0.25;
                            }
                        }
                        else
                        {
                            X1 = 0.25;
                        }

                        //////
                        if (m3 > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * W * (2 * m3 - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * W));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * W));
                            X2 = Math.Max(X21, X22);
                            if (X2 < 0.25)
                            {
                                X2 = 0.25;
                            }
                        }
                        else
                        {
                            X2 = 0.25;
                        }
                        // before printing >>>> get n1 && n2 firsyly 
                        //Arrangement of Blocks ; 
                        // Short dir.
                        double n1 = (Ly - X1 - X2 - Numbrib * bb) / aa;
                        n1 = Math.Floor(n1);
                        ///// here it is case 1 Simple beam x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * aa - Numbrib * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);

                        //Long dir.
                        X3 = 0.25;
                        X4 = 0.25;
                        double n2 = (Lx - X3 - X4 + bb) / (ee + bb);
                        n2 = Math.Floor(n2);
                        ///// here it is case 1 Simple beam x1=x2
                        double Xfinal2 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal2 = Math.Round(Xfinal2, 2);

                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();

                        txtx3.Text = Xfinal2.ToString();
                        txtx4.Text = Xfinal2.ToString();

                        txtn1.Text = n1.ToString();
                        txtn2.Text = n2.ToString();
                        double ntotal = n1 * n2;
                        txtntotal.Text = ntotal.ToString();




                        /////// table
                        ///////////////
                        /// Table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string thick = txtt.Text;
                            string L = txtLx.Text;
                            string Ls = txtLy.Text;
                            string Ass2 = txtnum2.Text + " T " + txtfai2.Text;
                            string Ass1 = txtnum1.Text + " T " + txtfai1.Text;
                            string Ass3 = txtnum3.Text + " T " + txtfai3.Text;
                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            string croos = txtrib.Text;

                            object[] data = { serial, thick, L, Ls, Ass2, Ass1, Ass3, X111, X222, n111, X333, X444, n222, croos };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }  


                    }

                    ////////////////////////////////////////////




                    if (txttype.Text == "Case_2")
                    {

                        double a1 = d * (1 - Math.Sqrt(1 - ((2 * m1 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                        if (a1 > amax)
                        {
                            MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C1 = 1.25 * a1;
                        double J1 = (1 / 1.15) * (1 - 0.4 * (C1 / d));
                        if (J1 > 0.826)
                        {
                            J1 = 0.826;            /////"Ducyility Condition:"
                        }

                        double AS1 = (m1 * Math.Pow(10, 6)) / (fy * J1 * d);
                        double num1 = Math.Ceiling((AS1 / (3.1459 * 0.25 * fai1 * fai1)));
                        if (num1 < 2)
                        {
                            num1 = 2;
                        }
                        txtnum1.Text = num1.ToString();

                        /////////////////

                        double a2 = d * (1 - Math.Sqrt(1 - ((2 * m2 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                        if (a2 > amax)
                        {
                            MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C2 = 1.25 * a2;
                        double J2 = (1 / 1.15) * (1 - 0.4 * (C2 / d));
                        if (J2 > 0.826)
                        {
                            J2 = 0.826;            /////"Ducyility Condition:"
                        }

                        double AS2 = (m2 * Math.Pow(10, 6)) / (fy * J2 * d);
                        double num2 = Math.Ceiling((AS2 / (3.1459 * 0.25 * fai2 * fai2)));
                        if (num2 < 2)
                        {
                            num2 = 2;
                        }
                        txtnum2.Text = num2.ToString();


                        ////////

                        double a3 = d * (1 - Math.Sqrt(1 - ((2 * m3 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                        if (a3 > amax)
                        {
                            MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C3 = 1.25 * a3;
                        double J3 = (1 / 1.15) * (1 - 0.4 * (C3 / d));
                        if (J3 > 0.826)
                        {
                            J3 = 0.826;            /////"Ducyility Condition:"
                        }

                        double AS3 = (m3 * Math.Pow(10, 6)) / (fy * J3 * d);
                        double num3 = Math.Ceiling((AS3 / (3.1459 * 0.25 * fai3 * fai3)));
                        if (num3 < 2)
                        {
                            num3 = 2;
                        }
                        txtnum3.Text = num3.ToString();


                        //// Calculation of Solid Part 
                        double R1 = (W * 0.5 * Ly * Ly + m1 - m3) / Ly;
                        double R2 = (W * 0.5 * Ly * Ly + m3 - m1) / Ly;
                        if (m1 > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * W * (2 * m1 - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * W));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * W));
                            X1 = Math.Min(X11, X12);
                            if (X1 < 0.25)
                            {
                                X1 = 0.25;
                            }
                        }
                        else
                        {
                            X1 = 0.25;
                        }

                        //////
                        if (m3 > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * W * (2 * m3 - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * W));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * W));
                            X2 = Math.Min(X21, X22);
                            if (X2 < 0.25)
                            {
                                X2 = 0.25;
                            }
                        }
                        else
                        {
                            X2 = 0.25;
                        }
                        // before printing >>>> get n1 && n2 firsyly 
                        //Arrangement of Blocks ; 
                        // Short dir.
                        double n1 = (Ly - X1 - X2 - Numbrib * bb) / aa;
                        n1 = Math.Floor(n1);
                        ///// here it is case 2 cont. one side  x1 <= x2
                        double X1final1 = Math.Round(X1, 2);
                        double X2final1 = (Ly - n1 * aa - Numbrib * bb - X1final1);
                        X2final1 = Math.Round(X2final1, 2);

                        //Long dir.
                        X3 = 0.25;
                        X4 = 0.25;
                        double n2 = (Lx - X3 - X4 + bb) / (ee + bb);
                        n2 = Math.Floor(n2);
                        ///// here it is case 1 Simple beam x1=x2
                        double Xfinal2 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal2 = Math.Round(Xfinal2, 2);

                        txtx1.Text = X1final1.ToString();
                        txtx2.Text = X2final1.ToString();

                        txtx3.Text = Xfinal2.ToString();
                        txtx4.Text = Xfinal2.ToString();

                        txtn1.Text = n1.ToString();
                        txtn2.Text = n2.ToString();
                        double ntotal = n1 * n2;
                        txtntotal.Text = ntotal.ToString();


                        /////// table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string thick = txtt.Text;
                            string L = txtLx.Text;
                            string Ls = txtLy.Text;
                            string Ass2 = txtnum2.Text + " T " + txtfai2.Text;
                            string Ass1 = txtnum1.Text + " T " + txtfai1.Text;
                            string Ass3 = txtnum3.Text + " T " + txtfai3.Text;
                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            string croos = txtrib.Text;

                            object[] data = { serial, thick, L, Ls, Ass2, Ass1, Ass3, X111, X222, n111, X333, X444, n222, croos };
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
       

        private void button2_Click(object sender, EventArgs e)
        {           
        }

        private void onewayHb_Load(object sender, EventArgs e)
        {
            pictureBox4.Enabled = true;
            pictureBox3.Enabled = false;
            pictureBox2.Enabled = false;
        }

        private void txttype_SelectedIndexChanged(object sender, EventArgs e)
        {
          try
            {
                if (txttype.Text == "Case_1")
                {
                    pictureBox4.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox2.Visible = false;
                }

                if (txttype.Text == "Case_2")
                {

                    pictureBox3.Visible = true;
                    pictureBox4.Visible = false;
                    pictureBox2.Visible = false;
                }

                if (txttype.Text == "Case_3")
                {
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Look at the Strip from Right Side .. then it is the below B.M.D  ","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btn6_Click(object sender, EventArgs e)
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

        private void btn7_Click(object sender, EventArgs e)
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

        private void btn5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ISOPEN = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ECP 203-2018 One Way H.B.")
                {
                    ISOPEN = true;
                    f.Focus();
                    break;
                }
            }
            if (ISOPEN == false)
            {
              codeoneway code = new codeoneway();

                code.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtx1.Clear();
            txtx2.Clear();
            txtx3.Clear();
            txtx4.Clear();

            txtn1.Clear();
            txtn2.Clear();
            txtntotal.Clear();

            txtnum1.Clear();
            txtnum2.Clear();
            txtnum3.Clear();
            txtrib.Clear();
          

        }
    }
}
