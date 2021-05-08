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
    public partial class stairThreeFlight : Form
    {
        public stairThreeFlight()
        {
            InitializeComponent();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtLL.Text.Trim() == "" || txtFc.Text.Trim() == ""
              || txtL1.Text.Trim() == "" || txtL2.Text.Trim() == "" || txtL3.Text.Trim() == "" || txtL4.Text.Trim() == ""
              || txtL5.Text.Trim() == "" || txtts.Text.Trim() == "" || txtc.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                double fcu = double.Parse(txtfcu.Text);
                double fy = double.Parse(txtfy.Text);
                double LL = double.Parse(txtLL.Text);
                double FC = double.Parse(txtFc.Text);
                double L1 = double.Parse(txtL1.Text);
                double L2 = double.Parse(txtL2.Text);
                double L3 = double.Parse(txtL3.Text);
                double L4 = double.Parse(txtL4.Text);
                double L5 = double.Parse(txtL5.Text);
                double ts = double.Parse(txtts.Text);
                double cover = double.Parse(txtc.Text);
                int fai1 = int.Parse(txtfai1.Text);
                int fai2 = int.Parse(txtfai2.Text);
                int fai3 = int.Parse(txtfai3.Text);

                double tsav = ts + 0.07;          // m

                /// get Loads
                /// 

                double num = Math.PI * 26.56 / 180.0;
                double angle = Math.Cos(num);

                double Wsh = 1.4 * (ts * 25 + FC) + 1.6 * LL;
                double Wsi = 1.4 * (tsav * 25 + FC) + 1.6 * LL * angle;

                double L5incl = L5 / angle;
                double L2incl = L2 / angle;

                double R1 = (Wsh * L5 * 0.5);
                double M1 = R1 * 0.5 * L4 + Wsh * L5 * L5 / 8;

                double R2 = Wsi * L5incl * 0.5;
                double M3 = R2 * L4 * 0.5 + Wsi * L5 * L5incl / 8;
                double Ws1 = Wsh + R2 / L4;
                double Ws2 = Wsh + R1 / L4;
                double R41 = Ws2 * 0.5 * L1 * L1 + Wsi * L2incl * (L1 + (L2 / 2)) + Ws1 * L1 * (L1 + L2 + (L3 / 2));
                double R4 = R41 / (L1 + L2 + L3);
                double R3 = (Ws2 * L1 + Wsi * L2incl + Ws1 * L3) - R4;
                double M2 = Wsi * L2 * L2incl / 8 + (R4 * L3 - Wsi * 0.5 * L3 * L3);



                double d = 1000 * (ts - cover);

                double amax = (320 * d) / (600 + 0.87 * fy);
                double a1 = d * (1 - Math.Sqrt(1 - ((2 * M1 * 1000 * 1000) / (0.45 * fcu * 1000 * d * d))));
                if (a1 > amax)
                {
                    MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtts.Focus();
                    txtts.SelectAll();
                    return;
                }
                double C1 = 1.25 * a1;
                double J1 = (1 / 1.15) * (1 - 0.4 * (C1 / d));
                if (J1 > 0.826)
                {
                    J1 = 0.826;
                }
                double As3 = (M1 * 1000 * 1000) / (fy * J1 * d);
                double num3 = Math.Ceiling(As3 / (3.1459 * 0.25 * fai3 * fai3));


                /////
                double a2 = d * (1 - Math.Sqrt(1 - ((2 * M2 * 1000 * 1000) / (0.45 * fcu * 1000 * d * d))));
                if (a2 > amax)
                {
                    MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtts.Focus();
                    txtts.SelectAll();
                    return;
                }
                double C2 = 1.25 * a2;
                double J2 = (1 / 1.15) * (1 - 0.4 * (C2 / d));
                if (J2 > 0.826)
                {
                    J2 = 0.826;
                }
                double As1 = (M2 * 1000 * 1000) / (fy * J2 * d);
                double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));

                ////////
                double a3 = d * (1 - Math.Sqrt(1 - ((2 * M3 * 1000 * 1000) / (0.45 * fcu * 1000 * d * d))));
                if (a3 > amax)
                {
                    MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtts.Focus();
                    txtts.SelectAll();
                    return;
                }
                double C3 = 1.25 * a3;
                double J3 = (1 / 1.15) * (1 - 0.4 * (C3 / d));
                if (J3 > 0.826)
                {
                    J3 = 0.826;
                }
                double As2 = (M3 * 1000 * 1000) / (fy * J3 * d);
                double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));


                ////// get ts ideal 
                double Leff = L1 + L2incl + L3;
                double tsideal = Math.Round((Leff / 30), 2);

                ///Printing
                txttsideal.Text = tsideal.ToString();
                txtnum1.Text = num1.ToString();
                txtnum2.Text = num2.ToString();
                txtnum3.Text = num3.ToString();
                txtRb1.Text = Math.Round(R3, 2).ToString();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Three Flight Srairs (Screen)";
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
            txttsideal.Clear();
            txtnum1.Clear();
            txtnum2.Clear();
            txtnum3.Clear();

            txtRb1.Clear();
        }
    }
}
