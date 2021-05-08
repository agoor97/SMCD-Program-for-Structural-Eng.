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
    public partial class check : Form
    {
        public check()
        {
            InitializeComponent();
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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

        
           

    private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double t = double.Parse(txtt.Text);
                double b = double.Parse(txtb.Text);
                double fcu = double.Parse(txtfcu.Text);
                double fy = double.Parse(txtfy.Text);
                double M = double.Parse(txtm.Text);
                double P = double.Parse(txtp.Text);
                double num1 = double.Parse(txtnum1.Text);
                double num2 = double.Parse(txtnum2.Text);
                double num3 = double.Parse(txtnum3.Text);
                int fai1 = int.Parse(txtfai1.Text);
                int fai2 = int.Parse(txtfai2.Text);
                int fai3 = int.Parse(txtfai3.Text);
                double d = t - 50;          // take C=d' = 50mm
                double Ac = b * t;


                this.chart1.Series["Point"].Points.AddXY(M, P);

                ////// draw ID by 7 points


                double As1 = num1 * 3.1459 * 0.25 * fai1 * fai1;
                double As2 = num2 * 3.1459 * 0.25 * fai2 * fai2;
                double Asside = num3 * 3.1459 * 0.25 * fai3 * fai3;
                double Astotal = As1 + As2 + Asside;


                // first Point 
                double M11 = 0;
                double P11 = (0.35 * fcu * (Ac - Astotal) + 0.67 * fy * Astotal) / 1000;
                // second point 
                double P21 = (0.35 * fcu * (Ac - Astotal) + 0.67 * fy * Astotal) / 1000;
                double M21 = P21 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb1 = (600 * d) / (600 + 0.87 * fy);
                double ab1 = 0.8 * Cb1;
                double P31 = (0.45 * fcu * ab1 * b + 0.87 * fy * As2 - 0.87 * fy * As1) / 1000;
                double M31 = (0.45 * fcu * ab1 * b * ((t / 2) - (ab1 / 2)) + 0.87 * fy * As2 * ((t / 2) - 50) + 0.87 * fy * As1 * ((t / 2) - 50)) / (1000 * 1000);

                // point 4 
                double P41 = 0;
                double M41 = 0.87 * fy * As1 * (d - 50) / (1000 * 1000);

                // point 5 
                double M51 = 0;
                double P51 = 0.87 * fy * Astotal / 1000;

                // point 6 Lies in Compression Zone
                double ey = (0.87 * fy) / (2 * Math.Pow(10, 5));
                // Assume a > ab 
                double a61 = ab1 + 115;        // mm 
                double C61 = 1.25 * a61;
                double epsilon61 = (0.003 * (d - C61)) / C61;
                double Fs11;
                if (epsilon61 >= ey)
                {
                    Fs11 = 0.87 * fy;
                }
                else
                {
                    Fs11 = epsilon61 * 2 * Math.Pow(10, 5);
                }
                double P61 = (0.45 * fcu * a61 * b + 0.87 * fy * As2 - Fs11 * As1) / 1000;
                double M61 = (0.45 * fcu * a61 * b * ((t / 2) - (a61 / 2)) + 0.87 * fy * As2 * ((t / 2) - 50) + Fs11 * As1 * ((t / 2) - 50)) / (1000 * 1000);

                //// point 7 Lies in Tension Zone 
                double a71 = ab1 - 115;       //mm
                double C71 = 1.25 * a71;
                double epsilon71 = (0.003 * (C71 - 50)) / C71;
                double Fs21;
                if (epsilon71 >= ey)
                {
                    Fs21 = 0.87 * fy;
                }
                else
                {
                    Fs21 = epsilon71 * 2 * Math.Pow(10, 5);
                }
                double P71 = (0.45 * fcu * a71 * b + Fs21 * As2 - 0.87 * fy * As1) / 1000;
                double M71 = (0.45 * fcu * a71 * b * ((t / 2) - (a71 / 2)) + Fs21 * As2 * ((t / 2) - 50) + 0.87 * fy * As1 * ((t / 2) - 50)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P11fianl = P11;
                double P21fianl = P21;
                double P31fianl = P31;
                double P41fianl = P41;
                double P51fianl = P51;
                double P61fianl = P61;
                double P71fianl = P71;

                double M11fianl = M11;
                double M21fianl = M21;
                double M31fianl = M31;
                double M41fianl = M41;
                double M51fianl = M51;
                double M61fianl = M61;
                double M71fianl = M71;

                this.chart1.Series["I.D"].Points.AddXY(M11fianl, P11fianl);
                this.chart1.Series["I.D"].Points.AddXY(M21fianl, P21fianl);
                this.chart1.Series["I.D"].Points.AddXY(M61fianl, P61fianl);
                this.chart1.Series["I.D"].Points.AddXY(M31fianl, P31fianl);
                this.chart1.Series["I.D"].Points.AddXY(M71fianl, P71fianl);
                this.chart1.Series["I.D"].Points.AddXY(M41fianl, P41fianl);
                this.chart1.Series["I.D"].Points.AddXY(M51fianl, -1 * P51fianl);
                this.chart1.Series["I.D"].Points.AddXY(M11fianl, P11fianl);


                this.chart1.Series["Point1"].Points.AddXY(M11fianl, P11fianl);
                this.chart1.Series["Point1"].Points.AddXY(M21fianl, P21fianl);
                this.chart1.Series["Point1"].Points.AddXY(M61fianl, P61fianl);
                this.chart1.Series["Point1"].Points.AddXY(M31fianl, P31fianl);
                this.chart1.Series["Point1"].Points.AddXY(M71fianl, P71fianl);
                this.chart1.Series["Point1"].Points.AddXY(M41fianl, P41fianl);
                this.chart1.Series["Point1"].Points.AddXY(M51fianl, -1 * P51fianl);
                this.chart1.Series["Point1"].Points.AddXY(M11fianl, P11fianl);

                this.chart1.Series["Point0"].Points.AddXY(0, 0);

                this.chart1.Series["Line1"].Points.AddXY(0, 0);
                this.chart1.Series["Line1"].Points.AddXY(M31fianl, P31fianl);

                this.chart1.Series["Line2"].Points.AddXY(0, 0);
                this.chart1.Series["Line2"].Points.AddXY(M21fianl, P21fianl);
            }           
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
        }
    }
}
