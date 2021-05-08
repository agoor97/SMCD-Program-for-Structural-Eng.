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
    public partial class shortcant : Form
    {
        public shortcant()
        {
            InitializeComponent();
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

        private void button5_Click(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtt.Text.Trim() == "" || txtb.Text.Trim() == ""
              || txtQ.Text.Trim() == "" || txtN.Text.Trim() == "" || txtfai1.Text.Trim() == ""
              || txta.Text.Trim() == "" || txtdelta.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double Q = double.Parse(txtQ.Text);
                    double N = double.Parse(txtN.Text);
                    double t = double.Parse(txtt.Text);
                    double b = double.Parse(txtb.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    double a = double.Parse(txta.Text);
                    double delta = double.Parse(txtdelta.Text);

                    double d = t - 50;     //mm
                                           // take d' = 50 mm
                    double An = (N * 1000) / (0.87 * fy);
                    double Asf = (Q * 1000) / (1.2 * 0.87 * fy) + (N * 1000) / (0.87 * fy);
                    double Moment = Q * a + N * (delta + 0.05);     // kN.m
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double aa = d * (1 - Math.Sqrt((1 - (2 * Moment * Math.Pow(10, 6)) / (0.45 * fcu * b * d * d))));
                    if (aa > amax)
                    {
                        // unsafe
                        MessageBox.Show("UnSafe Section against Moment .. Increase Dimen.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }
                    double C = 1.25 * aa;
                    double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                    if (J > 0.826)
                    {
                        J = 0.826;
                    }
                    double Af = (Moment * 1000 * 1000) / (fy * J * d);
                    double AS1 = An + Af;
                    double AS2 = An + (2 / 3) * Asf;
                    double AS3 = 0.03 * fcu * b * d / fy;
                    double Asmax = Math.Max(AS1, AS2);
                    double Asfinal = Math.Max(AS3, Asmax);
                    double num1 = Math.Ceiling(Asfinal / (3.1459 * 0.25 * fai1 * fai1));
                    txtnum1.Text = num1.ToString();
                    txtnum3.Text = "5";
                    txtfai3.Text = "8";

                    /// Horizontal Stirrups
                    double Ah = 0.5 * (Asfinal - An);
                    double AhFinal = (Ah * 1000) / (0.67 * d);     // Ah for (2/3)d

                    // try fai = 8 mm
                    double S = (2000 * 50.3) / AhFinal;
                    if ((S >= 100 && S <= 200) || S > 200)
                    {
                        //Safe Min St.
                        double No = 1000 / S;
                        double Nfinal = Math.Max(No, 5);          // لا يقل العدد عن 5 
                        txtnum2.Text = Nfinal.ToString();
                        txtfai2.Text = "8";
                    }
                    else if (S < 100)
                    {
                        // try fai = 10 mm
                        S = (2000 * 78.5) / AhFinal;
                        if ((S >= 100 && S <= 200) || S > 200)
                        {
                            //Safe Min St.
                            double No = 1000 / S;
                            double Nfinal = Math.Max(No, 5);          // لا يقل العدد عن 5 
                            txtnum2.Text = Nfinal.ToString();
                            txtfai2.Text = "10";
                        }
                        else if (S < 100)
                        {
                            // try fai = 12 mm
                            S = (2000 * 113) / AhFinal;
                            if ((S >= 100 && S <= 200) || S > 200)
                            {
                                //Safe Min St.
                                double No = 1000 / S;
                                double Nfinal = Math.Max(No, 5);          // لا يقل العدد عن 5 
                                txtnum2.Text = Nfinal.ToString();
                                txtfai2.Text = "12";
                            }
                        }
                    }
                    ///////////////////////
                    // check Stress
                    double Stress = (Q * 1000) / (b * d);
                    double ALL1 = 5.5;           //N/mm2
                    double ALL2 = 0.15 * fcu;
                    double ALL = Math.Min(ALL1, ALL2);
                    if (Stress <= ALL)
                    {
                        lblstress.Text = "Safe";

                    }
                    else
                    {
                        lblstress.Text = "UnSafe";
                    }

                }
            }    
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }

        private void button3_Click(object sender, EventArgs e)
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
                codecant code = new codecant();             
                code.ShowDialog();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtnum1.Clear();
            txtnum2.Clear();
            txtnum3.Clear();
            txtfai2.Clear();
            txtfai3.Clear();
            lblstress.Text = "";
        }
    }
}
