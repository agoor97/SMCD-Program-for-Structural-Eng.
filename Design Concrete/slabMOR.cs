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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Design_Concrete
{
    public partial class slabMOR : Form
    {
        public slabMOR()
        {
            InitializeComponent();
        }

        private void finalslab_Resize(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void finalslab_Load(object sender, EventArgs e)
        {
            txtnum2.Enabled = false;
            txtfai2.Enabled = false;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtnum2.Enabled = true;
                txtfai2.Enabled = true;
            }
            else
            {
                txtnum2.Enabled = false;
                txtfai2.Enabled = false;
            }
        }


        private void btn1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {

                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtnum1.Text.Trim() == ""
                || txtdepth.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double d = double.Parse(txtdepth.Text);
                    double num1 = double.Parse(txtnum1.Text);
                    int fai1 = int.Parse(txtfai1.Text);

                    double amin = 0.1 * d;
                    double amax = (320 * d) / (600 + (0.87 * fy));
                    double abal = (480 * d) / (600 + (0.87 * fy));
                    double As1 = num1 * 3.1459 * 0.25 * fai1 * fai1;
                    double aact = (0.87 * fy * As1) / (0.45 * 1000 * fcu);
                    if (aact <= amax && aact > amin)
                    {
                        // Under Reinforced Section 
                        double moment = 0.45 * fcu * aact * 1000 * (d - (aact / 2));
                        moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Ductile Failure ... (Under RFT Section)";
                    }

                    else if (aact > amax)
                    {
                        // over Reinforced Section
                        double moment = 0.45 * fcu * amax * 1000 * (d - (amax / 2));
                        moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Brittle Failure ... (Over RFT Section)";
                    }

                    else if (aact < amin)
                    {
                        // under rft 
                        double moment = 0.826 * fy * d * As1 * Math.Pow(10, -6);
                        moment = Math.Round(moment, 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Ductile Failure ... (Under RFT Section)";
                    }

                    else if (aact == abal)
                    {
                        // Balanced Section
                        double moment = 0.45 * fcu * amax * 1000 * (d - (amax / 2));
                        moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Brittle Failure ... (Balanced RFT Section)";
                    }
              
                }

            }


            if (checkBox1.Checked == true)
            {
                txtfai2.Enabled = true;
                txtnum2.Enabled = true;


                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtdepth.Text.Trim() == ""
                    || txtnum1.Text.Trim() == "" || txtnum2.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double d = double.Parse(txtdepth.Text);
                    double num1 = double.Parse(txtnum1.Text);
                    double num2 = double.Parse(txtnum2.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);

                    double amin = 0.1 * d;
                    double amax = (320 * d) / (600 + (0.87 * fy));
                    double abal = (480 * d) / (600 + (0.87 * fy));
                    double As1 = num1 * 3.1459 * 0.25 * fai1 * fai1;
                    double As2 = num2 * 3.1459 * 0.25 * fai2 * fai2;

                    // Assume Comp.Steel yields. ..
                    double aact = (0.87 * fy * As1 - 0.87 * fy * As2) / (0.45 * fcu * 1000);

                    if (aact > amax)
                    {
                        // Over rft Section
                        double moment = 0.45 * fcu * amax * 1000 * (d - (amax / 2)) + 0.87 * fy * As2 * (d - 20);
                        moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Brittle Failure ... (Over RFT Section)";
                    }
                    else if (aact < amin)
                    {
                        // Under rft|| neglect effect of As'
                        double moment = 0.826 * fy * As1 * d * Math.Pow(10, -6);
                        moment = Math.Round(moment, 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Ductile Failure ... (Under RFT Section)";
                    }
                    else if (aact >= amin && aact <= amax)
                    {
                        double moment = 0.45 * fcu * aact * 1000 * (d - (aact / 2)) + 0.87 * fy * As2 * (d - 20);
                        moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Ductile Failure ... (Under RFT Section)";
                    }
                    else if (aact == abal)
                    {
                        // Balanced rft Section
                        double moment = 0.45 * fcu * amax * 1000 * (d - (amax / 2)) + 0.87 * fy * As2 * (d - 20);
                        moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                        txtout.Text = moment.ToString();
                        txttype.Text = "Brittle Failure ... (Balanced RFT Section)";
                    }


                }
            }

        }


        private void btn2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = "Capacity of Slab (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Check the CheckBox for Double RFT .... Top Cover (d') = 20 mm ", "Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txttype.Text = "";
            txtout.Clear();
        }



    }
      
}
