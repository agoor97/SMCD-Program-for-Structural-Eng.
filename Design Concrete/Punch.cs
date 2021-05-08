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
    public partial class Punch : Form
    {
        public Punch()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double ts = double.Parse(txtts.Text);
                if (ts < 0.25)
                {
                    MessageBox.Show("ECP 203-2018 requires ts >= 0.25 m", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtts.Focus();
                    txtts.SelectAll();
                    return;
                }
                double fcu = double.Parse(txtfcu.Text);
                double fys = double.Parse(txtfys.Text);
                double C1 = double.Parse(txtc1.Text);
                double C2 = double.Parse(txtc2.Text);
                double L1 = double.Parse(txtL1.Text);
                double L2 = double.Parse(txtL2.Text);
                double X = double.Parse(txtx.Text);
                double DL = double.Parse(txtDL.Text);
                double LL = double.Parse(txtLL.Text);
                int bran = int.Parse(txtbranch.Text);
                int Diam = int.Parse(txtfaist.Text);
                int numst = int.Parse(txtnumst.Text);
                double d = ts - 0.03;
                double fai = 3.145 * 0.25 * Diam * Diam;


                double Wsu = (DL + ts * 25) * 1.4 + 1.6 * LL;


                if(comboBox1.Text == "Interior")
                {
                    //calculations

                    double Qup = Wsu * (L1 * L2 - (C1 + d) * (C2 + d));           // kN
                    double Ap = 2 * d * (C1 + C2 + 2 * d);                        // m2

                    double qpuact = (Qup * 1.15 * 1000) / (Ap * 1000 * 1000);     //N/mm2

                    // Allowable Punching 
                    double a = Math.Min(C1, C2);
                    double b = Math.Max(C1, C2);
                    double b0 = 2 * (C1 + d) + 2 * (C2 + d);                      // m 
                    double aLL1 = 0.316 * Math.Sqrt(fcu / 1.5);
                    double aLL2 = 0.316 * (0.5 + (a / b)) * Math.Sqrt(fcu / 1.5);
                    double aLL3 = 1.70;
                    double aLL4 = 0.8 * ((4 * d / b0) + 0.2) * Math.Sqrt(fcu / 1.5);

                    double qcup1 = Math.Min(aLL1, aLL2);
                    double qcup2 = Math.Min(aLL3, aLL4);

                    double qcup = Math.Min(qcup1, qcup2);

                    if (qcup > qpuact)
                    {
                        MessageBox.Show("No Need to use Stirrups ... Safe Only by Concrete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtfcu.Focus();
                        return;
                    }

                    double S = 1000 / numst;
                    if (S > (1000 * d * 0.5))
                    {
                        MessageBox.Show("Choose .... S <= d/2 .. Increse number/m' ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtnumst.Focus();
                        txtnumst.SelectAll();
                        return;
                    }

                    ///Check on Critical Sec1 


                    double qact1 = 0.12 * Math.Sqrt(fcu / 1.5) + (bran * fai * 0.87 * fys) / (S * b0 * 1000);
                    double qmax = 0.45 * Math.Sqrt(fcu / 1.5);
                    if (qact1 > qmax)
                    {
                        MessageBox.Show("Stresses Exceed the Maximum Limit .. Try diffrent Criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    else if (qact1 < qpuact)
                    {
                        lblout.Text = "UnSafe for Section (1)..try diffrent Criteria.";
                        return;
                    }


                    /// Check on Critical sec 2 
                    /// 

                    double Area = 4 * 0.5 * (X + (d / 2) - (C1 / 2)) * (X + (d / 2) - (C2 / 2))
                        + C1 * 2 * (X + (d / 2) - (C2 / 2))
                        + C2 * 2 * (X + (d / 2) - (C1 / 2)) + C1 * C2;
                    double Primeter = 2 * C1 + 2 * C2 + 4 * Math.Sqrt((X + (d / 2) - (C1 / 2)) * (X + (d / 2) - (C1 / 2)) + (X + (d / 2) - (C2 / 2)) * (X + (d / 2) - (C2 / 2)));

                    double Qpu2 = Wsu * (L1 * L2 - Area);
                    double Ap2 = Primeter * d;
                    double qact2 = (Qpu2 * 1000 * 1.15) / (Ap2 * 1000 * 1000);   //N/mm2


                    if (qact2 > qcup)
                    {
                        lblout.Text = "UnSafe for Section (2)..try diffrent Criteria.";

                    }
                    else
                    {
                        lblout.Text = " OK .. it is Safe ";
                    }
                }
               
                //////////////////////////////// finish of Interoior Column

                if(comboBox1.Text == "Edge")
                {
                    //calculations

                    double Qup = Wsu * (L1 * L2 - (C1 + (0.5 * d)) * (C2 + d));           // kN
                    double Ap = (2 * (C1 + (d / 2)) + (C2 + d)) * d;                        // m2

                    double qpuact = (Qup * 1.30 * 1000) / (Ap * 1000 * 1000);     //N/mm2

                    // Allowable Punching 
                    double a = Math.Min(C1, C2);
                    double b = Math.Max(C1, C2);
                    double b0 = 2 * (C1 + (d/2)) + (C2 + d);                      // m 
                    double aLL1 = 0.316 * Math.Sqrt(fcu / 1.5);
                    double aLL2 = 0.316 * (0.5 + (a / b)) * Math.Sqrt(fcu / 1.5);
                    double aLL3 = 1.70;
                    double aLL4 = 0.8 * ((4 * d / b0) + 0.2) * Math.Sqrt(fcu / 1.5);

                    double qcup1 = Math.Min(aLL1, aLL2);
                    double qcup2 = Math.Min(aLL3, aLL4);

                    double qcup = Math.Min(qcup1, qcup2);

                    if (qcup > qpuact)
                    {
                        MessageBox.Show("No Need to use Stirrups ... Safe Only by Concrete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtfcu.Focus();
                        return;
                    }

                    double S = 1000 / numst;
                    if (S > (1000 * d * 0.5))
                    {
                        MessageBox.Show("Choose .... S <= d/2 .. Increse number/m' ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtnumst.Focus();
                        txtnumst.SelectAll();
                        return;
                    }

                    ///Check on Critical Sec1 


                    double qact1 = 0.12 * Math.Sqrt(fcu / 1.5) + (bran * fai * 0.87 * fys) / (S * b0 * 1000);
                    double qmax = 0.45 * Math.Sqrt(fcu / 1.5);
                    if (qact1 > qmax)
                    {
                        MessageBox.Show("Stresses Exceed the Maximum Limit .. Try diffrent Criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    else if (qact1 < qpuact)
                    {
                        lblout.Text = "UnSafe for Section (1)..try diffrent Criteria.";
                        return;
                    }


                    /// Check on Critical sec 2 
                    /// 

                    double Area = 2 * 0.5 * (X + (d / 2) - (C1 / 2)) * (X + (d / 2) - (C2 / 2))
                        + C1 * 2 * (X + (d / 2) - (C2 / 2))
                        + C2 * (X + (d / 2) - (C1 / 2)) + C1 * C2;
                    double Primeter = 2 * C1 + C2 + 2* Math.Sqrt((X + (d / 2) - (C1 / 2)) * (X + (d / 2) - (C1 / 2)) + (X + (d / 2) - (C2 / 2)) * (X + (d / 2) - (C2 / 2)));

                    double Qpu2 = Wsu * (L1 * L2 - Area);
                    double Ap2 = Primeter * d;
                    double qact2 = (Qpu2 * 1000 * 1.30) / (Ap2 * 1000 * 1000);   //N/mm2


                    if (qact2 > qcup)
                    {
                        lblout.Text = "UnSafe for Section (2)..try diffrent Criteria.";

                    }
                    else
                    {
                        lblout.Text = " OK .. it is Safe ";


                    }
                }

                ///////////////////////////////// finish of Edge Col.
                if (comboBox1.Text == "Corner")
                {
                    //calculations

                    double Qup = Wsu * (L1 * L2 - (C1 + (d/2)) * (C2 + (d/2)));           // kN
                    double Ap = (C1 + (d / 2) + C2 + (d / 2)) * d;                        // m2

                    double qpuact = (Qup * 1.50 * 1000) / (Ap * 1000 * 1000);     //N/mm2

                    // Allowable Punching 
                    double a = Math.Min(C1, C2);
                    double b = Math.Max(C1, C2);
                    double b0 =  (C1 + (d/2)) + (C2 + (d/2));                      // m 
                    double aLL1 = 0.316 * Math.Sqrt(fcu / 1.5);
                    double aLL2 = 0.316 * (0.5 + (a / b)) * Math.Sqrt(fcu / 1.5);
                    double aLL3 = 1.70;
                    double aLL4 = 0.8 * ((4 * d / b0) + 0.2) * Math.Sqrt(fcu / 1.5);

                    double qcup1 = Math.Min(aLL1, aLL2);
                    double qcup2 = Math.Min(aLL3, aLL4);

                    double qcup = Math.Min(qcup1, qcup2);

                    if (qcup > qpuact)
                    {
                        MessageBox.Show("No Need to use Stirrups ... Safe Only by Concrete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtfcu.Focus();
                        return;
                    }

                    double S = 1000 / numst;
                    if (S > (1000 * d * 0.5))
                    {
                        MessageBox.Show("Choose .... S <= d/2 .. Increse number/m' ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtnumst.Focus();
                        txtnumst.SelectAll();
                        return;
                    }

                    ///Check on Critical Sec1 


                    double qact1 = 0.12 * Math.Sqrt(fcu / 1.5) + (bran * fai * 0.87 * fys) / (S * b0 * 1000);
                    double qmax = 0.45 * Math.Sqrt(fcu / 1.5);
                    if (qact1 > qmax)
                    {
                        MessageBox.Show("Stresses Exceed the Maximum Limit .. Try diffrent Criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    else if (qact1 < qpuact)
                    {
                        lblout.Text = "UnSafe for Section (1)..try diffrent Criteria.";
                        return;
                    }


                    /// Check on Critical sec 2 
                    /// 

                   

                    double Area =  0.5 * (X + (d / 2) - (C1 / 2)) * (X + (d / 2) - (C2 / 2))
                        + C1 * (X + (d / 2) - (C2 / 2))
                        + C2 * (X + (d / 2) - (C1 / 2)) + C1 * C2;
                    double Primeter = C1 + C2 + Math.Sqrt((X + (d / 2) - (C1 / 2)) * (X + (d / 2) - (C1 / 2)) + (X + (d / 2) - (C2 / 2)) * (X + (d / 2) - (C2 / 2)));

                    double Qpu2 = Wsu * (L1 * L2 - Area);
                    double Ap2 = Primeter * d;
                    double qact2 = (Qpu2 * 1000 * 1.50) / (Ap2 * 1000 * 1000);   //N/mm2


                    if (qact2 > qcup)
                    {
                        lblout.Text = "UnSafe for Section (2)..try diffrent Criteria.";

                    }
                    else
                    {
                        lblout.Text = " OK .. it is Safe ";
                    }
                }




            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {            
            MessageBox.Show("Choose X from This Range .. ( max L1/6 to min L2/4 ).", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(8 or 16 for Interior Col.),(6 or 12 for Edge Col.),(4 or 8 for Corner Col.).", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Punching (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                punchcode code = new punchcode();
                code.ShowDialog();
            }

        }

        private void Punch_Load(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Interior")
            {
                pictureBox3.Visible = true;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }
            if (comboBox1.Text == "Corner")
            {
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
                pictureBox3.Visible = false;
            }
            if (comboBox1.Text == "Edge")
            {
                pictureBox1.Visible = true;
                pictureBox3.Visible = false;
                pictureBox2.Visible = false;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Service area For Column")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                PunchHelp Punch = new PunchHelp();
                Punch.ShowDialog();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            lblout.Text = "";
        }
    }
}
