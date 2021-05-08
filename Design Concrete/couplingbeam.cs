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
    public partial class couplingbeam : Form
    {
        public couplingbeam()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAsdiag.Clear();
            txtAshoriz.Clear();
            txtStdiag.Clear();
            txtStvert.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Spandrel Beam (Screen)";
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
            codecoupling code = new codecoupling();
            code.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtL.Text.Trim() == "" || txtb.Text.Trim() == ""
               || txtt.Text.Trim() == "" || txtMu.Text.Trim() == "" || txtQu.Text.Trim() == ""
               || txtc.Text.Trim() == "" || txtfaidiag.Text.Trim() == "" || txtfaihoriz.Text.Trim() == "")
               
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double fcu = double.Parse(txtfcu.Text);
                double fy = double.Parse(txtfy.Text);
                double Mu = double.Parse(txtMu.Text);
                double Qu = double.Parse(txtQu.Text);
                double Ln = double.Parse(txtL.Text);
                double b = double.Parse(txtb.Text);
                double t = double.Parse(txtt.Text);
                double cover = double.Parse(txtc.Text);


                int faidiag = int.Parse(txtfaidiag.Text);
                int faihoriz = int.Parse(txtfaihoriz.Text);
               



                double d = t - cover;
                //// check 
                double Check = Ln / (0.001 * t);
                if (Check >= 4.0)
                {
                    MessageBox.Show("It is Ordinary Beam (Ln/t) > 4.0 .. Go away From Here .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double qumax = 0.70 * Math.Sqrt(fcu / 1.5);

                double qu = (Qu * 1000) / (b * d);
                if (qu > qumax)
                {
                    MessageBox.Show("UnSafe against Shear .. Increase Dimensions.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtt.Focus();
                    txtt.SelectAll();
                    return;
                }

                double alfarad = Math.Atan(d / (1000 * Ln));              //// تقدير دائري
                double alfa = alfarad * 180 / 3.1459;

                double sinalfa = Math.Sin(alfarad);
                double cosalfa = Math.Cos(alfarad);

                double Asd = (Qu * 1000) / (2 * fy * 0.87 * sinalfa);

                double numd = Math.Ceiling(Asd / (3.1459 * 0.25 * faidiag * faidiag));
                txtAsdiag.Text = numd.ToString();

                ///////// 
                /////// يتم حساب المقاومة القصوى للانحناء للكمرات بمشاركة التسليح القطري

                double Mud = 2 * Asd * 0.87 * fy * cosalfa * (t - 2 * cover);

                if (Mu <= Mud)
                {
                    ///// put minimum steel 
                    double Asmin1 = 0.225 * b * d * Math.Sqrt(fcu) / fy;
                    double Asmin2 = 0.15 * 0.01 * b * d;
                    double Asmin = Math.Max(Asmin1, Asmin2);
                    double numh = Math.Ceiling(Asmin / (3.1459 * 0.25 * faihoriz * faihoriz));
                     
                    txtAshoriz.Text = numh.ToString();
                    txtStvert.Text = "5";
                    txtfaistvert.Text = "10";                       ////// مذكرات الزلازل دكتور مشهور


                }

                else
                {
                    double Mdesign = Mu - Mud;
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double aact = d * (1 - Math.Sqrt(1 - ((2 * Mdesign * 1000 * 1000) / (0.45 * fcu * b * d * d))));
                    if (aact > amax)
                    {
                        MessageBox.Show("UnSafe Section against Moment .. Increase Dimensions", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }
                    double C = 1.25 * aact;
                    double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                    double Asadd = (Mdesign * 1000 * 1000) / (fy * J * d);
                    /// check min 
                    double Asmin1 = 0.225 * b * d * Math.Sqrt(fcu) / fy;
                    double Asmin2 = 1.3 * Asadd;
                    double Asmin3 = 0.01 * 0.15 * b * d;

                    double Asfinal;
                    ///////
                    if (Asmin1 > Asadd)
                    {
                        double Asmin = Math.Min(Asmin1, Asmin2);
                        Asfinal = Math.Max(Asmin, Asmin3);

                    }
                    else
                    {
                        Asfinal = Asadd;
                    }
                    //////
                    double numh = Math.Ceiling(Asfinal / (3.1459 * 0.25 * faihoriz * faihoriz));

                    txtAshoriz.Text = numh.ToString();
                    txtStvert.Text = "5";
                    txtfaistvert.Text = "10";
                }



                /////// يتبقى كانات التسليح القطري فقط ولها اشتراطات حددها الكود

                double S1 = 8 * faidiag;
               
                double S2 = 0.5 * 0.5 * b;   //// على فرض ان التسليح القطري يكون قطاعه تقريبا نصف قطاع الكمرة
                double S3 = 150;         
                double SS1 = Math.Min(S1, S2);

                double S = Math.Min(SS1, S3);     
                double numStdiag = Math.Ceiling(1000 / S);

                txtStdiag.Text = numStdiag.ToString();
                txtfaistdiag.Text = "10";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
