using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Concrete
{
    public partial class deepbeam : Form
    {
        public deepbeam()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(txttype.Text == "Simple")
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
                pictureBox4.Visible = true;
                pictureBox3.Visible = false;
                pictureBox5.Visible = true;
                txtAs2.Enabled = false;
                txtAs3.Enabled = false;
                txty2.Enabled = false;
                txty3.Enabled = false;

                txtM2.Enabled = false;

            }
            if (txttype.Text == "Continuous")
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                txtAs2.Enabled = true;
                txtAs3.Enabled = true;
                txty2.Enabled = true;
                txty3.Enabled = true;

                txtM2.Enabled = true;

               
            }
        }

        private void deepbeam_Load(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
            pictureBox5.Visible = true;
            txtAs2.Enabled = false;
            txtAs3.Enabled = false;
            txty2.Enabled = false;
            txty3.Enabled = false;

            txtM2.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                if (txttype.Text == "Simple")
                {
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtb.Text.Trim() == ""
                    || txtt.Text.Trim() == "" || txtLn.Text.Trim() == "" || txtc1.Text.Trim() == ""
                    || txtc2.Text.Trim() == "" || txtSv.Text.Trim() == "" || txtbranH.Text.Trim() == ""
                    || txtbranV.Text.Trim() == "" || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == ""
                    || txtfai3.Text.Trim() == "" || txtfaih.Text.Trim() == "" || txtfaiv.Text.Trim() == ""
                    || txtM1.Text.Trim() == "" || txtQ.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double b = double.Parse(txtb.Text);
                    double t = double.Parse(txtt.Text);
                    double M1 = double.Parse(txtM1.Text);
                    double Q = double.Parse(txtQ.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int faih = int.Parse(txtfaih.Text);
                    int faiv = int.Parse(txtfaiv.Text);
                    double C1 = double.Parse(txtc1.Text);
                    double C2 = double.Parse(txtc2.Text);
                    double Ln = double.Parse(txtLn.Text);
                    int BranH = int.Parse(txtbranH.Text);
                    int BranV = int.Parse(txtbranV.Text);
                    double Sv = double.Parse(txtSv.Text);



                    ////// Check empirical Method

                    double L1 = 1.05 * Ln;
                    double L2 = Ln + (0.5 * C1) + (0.5 * C2);
                    double Leff = Math.Min(L1, L2);

                    ////// take cover = 100 mm = 0.1 m
                    double d = t - 0.1;
                    double Check = (Leff / d);
                    if (Check > 1.25)
                    {
                        MessageBox.Show("Leff/d > 1.25 , Can not designed Using Empirical Method", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double yct = Math.Min((0.86 * Leff), (0.87 * d));
                    double As1 = (M1 * 1000 * 1000) / (0.87 * fy * yct * 1000);
                

                    ///// check Asmin 
                    double Asmin1 = 0.225 * Math.Sqrt(fcu) * b * 1000 * d * 1000 / fy;
                    double Asmin2 = 1.3 * As1;
                    double Asmin3 = 0.15 * 0.01 * b * 1000 * d * 1000;

                    double Asfinal;
                    if(As1 < Asmin1)
                    {
                        double Asmin = Math.Min(Asmin1, Asmin2);
                        Asfinal = Math.Max(Asmin3, Asmin);

                    }
                    else
                    {
                        Asfinal = As1;
                    }

                    double num1 = Math.Ceiling(Asfinal / (3.1459 * 0.25 * fai1 * fai1));
                    double y1 = 0.15 / (0.2 * t);
                    //
                    txtAs1.Text = num1.ToString();
                    txty1.Text = Math.Floor(100 * y1).ToString();         //cm

                    //////// shear design
                    double g = Math.Min(d, Leff);
                    double qu = (Q * 1000) / (b * 1000 * g * 1000);

                    double deltad = (1 / 3) * (2 + 0.4 * (Ln / d));
                    double deltadc = 2.5;

                    double qumax = deltad * 0.7 * Math.Sqrt(fcu / 1.5);
                    double qumin = deltadc * 0.24 * Math.Sqrt(fcu / 1.5);

                    double qumaxcheck = 4 * deltad;
                    double qumincheck = 0.46 * Math.Sqrt(fcu / 1.5);

                    if (qumax > qumaxcheck)
                    {
                        MessageBox.Show("Increse Dimensons .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    if (qumin > qumincheck)
                    {
                        qumin = qumincheck;
                    }

                    if (qu < qumin)
                    {
                        double Ahmin = Math.Ceiling(0.0025 * 200 * b * 1000);
                        double Avmin = Math.Ceiling(0.0015 * 200 * b * 1000);

                        MessageBox.Show("Safe Minimum Stirrups. , Ahmin = " + Ahmin + "  ||  " + " Avmin = " + Avmin  + " mm2 ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ////////
                    double qsu = qu - (0.5 * qumin);

                    double deltaH = (11 - (Ln / d)) / 12;
                    double deltaV = 1 - deltaH;


                    double Ah = BranH * 3.1459 * 0.25 * faih * faih;
                    double Av = BranV * 3.1459 * 0.25 * faiv * faiv;

                    double value = qsu * b * 1000 * 1.15 / fy - deltaV * (Av / Sv);

                    double Sh = deltaH * Ah / value;

                    double numh;
                    double numv;

                    if (Sh >= 100 && Sh <= 200)
                    {
                        numv = Math.Ceiling(1000 / Sv);
                        numh = Math.Ceiling(1000 / Sh);
                        txtAsh.Text = numh.ToString();
                        txtAsv.Text = numv.ToString();
                    }

                    else if (Sh > 200)
                    {
                        numv = Math.Ceiling(1000 / Sv);
                        txtAsv.Text = numv.ToString();
                        txtAsh.Text = 5.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Sh < 100 mm , Try diffrent Criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSv.Focus();
                        txtSv.SelectAll();
                        return;
                    }
                }


                //////// finish type 1

                if (txttype.Text == "Continuous")
                {
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtb.Text.Trim() == ""
                    || txtt.Text.Trim() == "" || txtLn.Text.Trim() == "" || txtc1.Text.Trim() == ""
                    || txtc2.Text.Trim() == "" || txtSv.Text.Trim() == "" || txtbranH.Text.Trim() == ""
                    || txtbranV.Text.Trim() == "" || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == ""
                    || txtfai3.Text.Trim() == "" || txtfaih.Text.Trim() == "" || txtfaiv.Text.Trim() == ""
                    || txtM1.Text.Trim() == "" || txtQ.Text.Trim() == "" || txtM2.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double b = double.Parse(txtb.Text);
                    double t = double.Parse(txtt.Text);
                    double M1 = double.Parse(txtM1.Text);
                    double M2 = double.Parse(txtM2.Text);
                    double Q = double.Parse(txtQ.Text);

                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);
                    int fai3 = int.Parse(txtfai3.Text);

                    int faih = int.Parse(txtfaih.Text);
                    int faiv = int.Parse(txtfaiv.Text);
                    double C1 = double.Parse(txtc1.Text);
                    double C2 = double.Parse(txtc2.Text);
                    double Ln = double.Parse(txtLn.Text);
                    int BranH = int.Parse(txtbranH.Text);
                    int BranV = int.Parse(txtbranV.Text);
                    double Sv = double.Parse(txtSv.Text);



                    ////// Check empirical Method

                    double L1 = 1.05 * Ln;
                    double L2 = Ln + (0.5 * C1) + (0.5 * C2);
                    double Leff = Math.Min(L1, L2);

                    ////// take cover = 100 mm = 0.1 m
                    double d = t - 0.1;
                    double Check = (Leff / d);
                    if (Check > 2.5)
                    {
                        MessageBox.Show("Leff/d > 2.5 , Can not designed Using Empirical Method", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double yct1 = Math.Min((0.43 * Leff), (0.87 * d));
                    double yct2 = Math.Min((0.37 * Leff), (0.87 * d));

                    double As1 = (M1 * 1000 * 1000) / (0.87 * fy * yct1 * 1000);
                    double As23 = (M2 * 1000 * 1000) / (0.87 * fy * yct2 * 1000);                                 
                    //
                    ///// check Asmin 
                    double Asmin1 = 0.225 * Math.Sqrt(fcu) * b * 1000 * d * 1000 / fy;
                    double Asmin2 = 1.3 * As1;
                    double Asmin3 = 0.15 * 0.01 * b * 1000 * d * 1000;

                    double Asfinal1;
                    double Asfinal23;
                    

                    if (As1 < Asmin1)
                    {
                        double Asmin = Math.Min(Asmin1, Asmin2);
                        Asfinal1 = Math.Max(Asmin3, Asmin);
                    }
                    else
                    {
                        Asfinal1 = As1;                      
                    }
                    ////////////////////

                    if (As23 < Asmin1)
                    {
                        double Asmin = Math.Min(Asmin1, Asmin2);
                        Asfinal23 = Math.Max(Asmin3, Asmin);

                    }
                    else
                    {
                        Asfinal23 = As23;
                    }
                    double Asfinal2 = Asfinal23 / 2;
                    double Asfinal3 = Asfinal23 / 2;

                    double num1 = Math.Ceiling(Asfinal1 / (3.1459 * 0.25 * fai1 * fai1));
                    double num2 = Math.Ceiling(Asfinal2 / (3.1459 * 0.25 * fai2 * fai2));
                    double num3 = Math.Ceiling(Asfinal3 / (3.1459 * 0.25 * fai3 * fai3));

                    double y1 = 0.15 / (0.2 * t);     // m
                    double y2 = 0.8 * t;              // m
                    double y3 = 0.4 * t;              // m

                    if (y2 > (0.8 * Leff))
                    {
                        y2 = 0.8 * Leff;
                    }
                    //
                    if (y3 > (0.4 * Leff))
                    {
                        y3 = 0.4 * Leff;
                    }


                    //
                    txtAs1.Text = num1.ToString();
                    txtAs2.Text = num2.ToString();
                    txtAs3.Text = num3.ToString();

                    txty1.Text = Math.Floor(100 * y1).ToString();         //cm
                    txty2.Text = Math.Floor(100 * y2).ToString();         //cm
                    txty3.Text = Math.Floor(100 * y3).ToString();         //cm



                    //////// shear design
                    double g = Math.Min(d, Leff);
                    double qu = (Q * 1000) / (b * 1000 * g * 1000);

                    double deltad = (1 / 3) * (2 + 0.4 * (Ln / d));
                    double deltadc = 2.5;

                    double qumax = deltad * 0.7 * Math.Sqrt(fcu / 1.5);
                    double qumin = deltadc * 0.24 * Math.Sqrt(fcu / 1.5);

                    double qumaxcheck = 4 * deltad;
                    double qumincheck = 0.46 * Math.Sqrt(fcu / 1.5);

                    if (qumax > qumaxcheck)
                    {
                        MessageBox.Show("Increse Dimensons .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    if (qumin > qumincheck)
                    {
                        qumin = qumincheck;
                    }
                    ///////

                    if (qu < qumin)
                    {
                        double Ahmin = Math.Ceiling(0.0025 * 200 * b * 1000);
                        double Avmin = Math.Ceiling(0.0015 * 200 * b * 1000);

                        MessageBox.Show("Safe Minimum Stirrups. , Ahmin = " + Ahmin + "  ||  " + " Avmin = " + Avmin + " mm2 ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ////////
                    double qsu = qu - (0.5 * qumin);

                    double deltaH = (11 - (Ln / d)) / 12;
                    double deltaV = 1 - deltaH;


                    double Ah = BranH * 3.1459 * 0.25 * faih * faih;
                    double Av = BranV * 3.1459 * 0.25 * faiv * faiv;

                    double value = qsu * b * 1000 * 1.15 / fy - deltaV * (Av / Sv);

                    double Sh = deltaH * Ah / value;

                    double numh;
                    double numv;

                    if (Sh >= 100 && Sh <= 200)
                    {
                        numv = Math.Ceiling(1000 / Sv);
                        numh = Math.Ceiling(1000 / Sh);
                        txtAsh.Text = numh.ToString();
                        txtAsv.Text = numv.ToString();
                    }

                    else if (Sh > 200)
                    {
                        numv = Math.Ceiling(1000 / Sv);
                        txtAsv.Text = numv.ToString();
                        txtAsh.Text = 5.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Sh < 100 mm , Try diffrent Criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSv.Focus();
                        txtSv.SelectAll();
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAs1.Clear();
            txtAs2.Clear();
            txtAs3.Clear();
            txty1.Clear();
            txty2.Clear();
            txty3.Clear();
            txtAsh.Clear();
            txtAsv.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Deep Beam (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            codedeep deep = new codedeep();
            deep.ShowDialog();
        }
    }
}
