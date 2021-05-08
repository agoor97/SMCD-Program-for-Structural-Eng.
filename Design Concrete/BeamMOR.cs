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
    public partial class BeamMOR : Form
    {
        public BeamMOR()
        {
            InitializeComponent();
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void BeamMOR_Load(object sender, EventArgs e)
        {
            txtbb.Enabled = false;
            txtts.Enabled = false;
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txttype.Text == "R_Section")
            {
                pictureBox3.Visible = true;
                pictureBox2.Visible = false;
                pictureBox1.Visible = false;
                txtbb.Enabled = false;
                txtts.Enabled = false;
            }
            if (txttype.Text == "T_Section")
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                pictureBox1.Visible = false;
                txtbb.Enabled = true;
                txtts.Enabled = true;
            }
            if (txttype.Text == "L_Section")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                txtbb.Enabled = true;
                txtts.Enabled = true;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
           try
            {
                if (txttype.Text == "R_Section")
                {
                    if (txtb.Text.Trim() == "" || txtd.Text.Trim() == "" || txtdcover.Text.Trim() == ""
                  || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "" || txtfaisti.Text.Trim() == ""
                  || txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtnum1.Text.Trim() == ""
                  || txtnum2.Text.Trim() == "" || txtnumsti.Text.Trim() == "" || txtbran.Text.Trim() == "")

                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfcu.Focus();
                        return;
                    }
                    else
                    {
                      
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double d = double.Parse(txtd.Text);
                        double bb = double.Parse(txtb.Text);
                        double cover = double.Parse(txtdcover.Text);
                        double fai1 = double.Parse(txtfai1.Text);
                        double fai2 = double.Parse(txtfai2.Text);
                        double faistirr = double.Parse(txtfaisti.Text);
                        double num1 = double.Parse(txtnum1.Text);
                        double num2 = double.Parse(txtnum2.Text);
                        double numstirr = double.Parse(txtnumsti.Text);
                        double branch = double.Parse(txtbran.Text);
                        double S = 1000 / numstirr;
                        double Astir = 3.1459 * faistirr * faistirr * 0.25;

                        // against moment firstly 
                        double amax = (320 * d) / (600 + 0.87 * fy);
                    

                        double abal = (600 * d) / (600 + 0.87 * fy);
                        double amin = 0.1 * d;

                        double As1 = num1 * (3.1459 * 0.25 * fai1 * fai1);
                        double As2 = num2 * (3.1459 * 0.25 * fai2 * fai2);

                        if ((As2 / As1) > 0.2)
                        {
                            // we Can not Neglect As'   (Double RFT)
                            //// get aact
                            double aact = (0.87 * fy * As1 - 0.87 * fy * As2) / (0.45 * fcu * bb);
                         

                            if (aact < amin)
                            {
                                double moment = (0.826 * fy * As1 * d);
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = "Ductile Failure ... (Under RFT Section)";
                            }
                           else if (aact > amax)
                            {
                               
                                double moment = 0.45 * fcu * amax * bb * (d - (amax / 2)) + 0.87 * fy * As2 * (d - cover);
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();                     
                                txtfailure.Text = "Brittle Failure ... (Over RFT Section)";
                            }
                            else if (aact == abal)
                            {
                                double moment = 0.45 * fcu * amax * bb * (d - (amax / 2)) + 0.87 * fy * As2 * (d - cover);
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = " Brittle Failure ... (Balanced RFT Section) ";
                            }

                            else
                            {
                                double moment = 0.45 * fcu * aact * bb * (d - (aact / 2)) + 0.87 * fy * As2 * (d - cover);
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = " Ductile Failure ... (Under RFT Section) ";
                            }

                        }
                        ///

                        if ((As2 / As1) <= 0.2)
                        {
                            double aact = (0.87 * fy * As1) / (0.45 * fcu * bb);
                            if (aact < amin)
                            {
                                double moment = (0.826 * fy * As1 * d);
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = " Ductile Failure ... (Under RFT Section) ";
                            }
                            else if (aact > amax)
                            {
                                double moment = 0.45 * fcu * amax * bb * (d - (amax / 2));
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = " Brittle Failure ... (Over RFT Section) ";
                            }
                            else if (aact == abal)
                            {
                                double moment = 0.45 * fcu * amax * bb * (d - (amax / 2));
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = " Brittle Failure ... (Balanced RFT Section) ";
                            }

                            else
                            {
                                double moment = 0.45 * fcu * aact * bb * (d - (aact / 2));
                                moment = Math.Round((moment * Math.Pow(10, -6)), 2);
                                txtmoment.Text = moment.ToString();
                                txtfailure.Text = " Ductile Failure ... (Under RFT Section) ";
                            }

                        }

                        //// agiant Shear for R section
                        double qsu = (branch * Astir * 0.87 * 240) / (S * bb);
                        double qcumin = 0.12 * Math.Sqrt(fcu / 1.5);
                        double qu = qsu + qcumin;          //N/mm2
                        double Qu = Math.Round((qu * bb * d * 0.001), 2);       //kN
                        txtshear.Text = Qu.ToString();
                    }


                }

                ////////////////// T OR L Section 


                if (txttype.Text == "T_Section" || txttype.Text == "L_Section")
                {
                    if (txtb.Text.Trim() == "" || txtd.Text.Trim() == "" || txtdcover.Text.Trim() == ""
                  || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "" || txtfaisti.Text.Trim() == ""
                  || txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtnum1.Text.Trim() == ""
                  || txtnum2.Text.Trim() == "" || txtnumsti.Text.Trim() == "" || txtts.Text.Trim() == ""
                  || txtbb.Text.Trim() == "" || txtbran.Text.Trim() == "")

                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfcu.Focus();
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double d = double.Parse(txtd.Text);
                        double bb = double.Parse(txtb.Text);
                        double B = double.Parse(txtbb.Text);
                        double ts = double.Parse(txtts.Text);
                        double cover = double.Parse(txtdcover.Text);
                        double fai1 = double.Parse(txtfai1.Text);
                        double fai2 = double.Parse(txtfai2.Text);
                        double faistirr = double.Parse(txtfaisti.Text);
                        double num1 = double.Parse(txtnum1.Text);
                        double num2 = double.Parse(txtnum2.Text);
                        double numstirr = double.Parse(txtnumsti.Text);
                        double branch = double.Parse(txtbran.Text);
                        double S = 1000 / numstirr;      //mm
                        double Astir = 3.1459 * faistirr * faistirr * 0.25;

                        // against moment firstly 
                        double amax = (320 * d) / (600 + 0.87 * fy);
                        double abal = (600 * d) / (600 + 0.87 * fy);
                        double amin = 0.1 * d;

                        double As1 = num1 * (3.1459 * 0.25 * fai1 * fai1);
                        double As2 = num2 * (3.1459 * 0.25 * fai2 * fai2);
                        if ((As2 / As1) > 0.2)         ///Double RFT
                        {
                            double aact = (0.87 * fy * As1 - 0.87 * fy * As2) / (0.45 * fcu * B);
                            // firstly assume RC section (All comp. in the Flange)
                            if (aact <= ts)         ///Right Assumption
                            {
                                if (aact < amin)
                                {
                                    double moment = Math.Round((0.826 * fy * d * As1 * Math.Pow(10, -6)), 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                               else if (aact > amax)
                                {
                                    /// مازال برضوا كدا الضغط يقع باكمله داخل الفلانجة 
                                    double moment = 0.45 * fcu * B * amax * (d - (amax / 2)) + 0.87 * fy * As2 * (d - cover);
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Brittle Failure ... (Over RFT Section) ";
                                }
                                else
                                {
                                    double moment = 0.45 * fcu * B * aact * (d - (aact / 2)) + 0.87 * fy * As2 * (d - cover);
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                            }

                            ///// if Wrong Assumption 

                            if (aact > ts)
                            {
                                ////// there are comp in (Flange && Web) Do not Neglect Web (Perfect)
                                if (aact < amin)
                                {
                                    double moment = Math.Round((0.826 * fy * d * As1 * Math.Pow(10, -6)), 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                               else if (aact > amax && amax <= ts)  //R-Section
                                {
                                    /// مازال برضوا كدا الضغط يقع باكمله داخل الفلانجة 
                                    double moment = 0.45 * fcu * B * amax * (d - (amax / 2)) + 0.87 * fy * As2 * (d - cover);
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Brittle Failure ... (Over RFT Section) ";
                                }
                               else if (aact > amax && amax > ts)
                                {
                                    // comp in both web & flange
                                    double moment = 0.87 * fy * As2 * (d - cover) + 0.45 * fcu * B * ts * (d - (ts / 2)) + 0.45 * fcu * bb * (amax - ts) * (d - ts - ((amax - ts) / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Brittle Failure ... (Over RFT Section) ";

                                }
                                else if (aact >= amin && aact <= amax && aact <= ts) /// R_Section
                                {
                                    // انت هنا كدا برضوا كلوا في الفلنجة 
                                    double moment = 0.45 * fcu * aact * B * (d - (aact / 2)) + 0.87 * fy * As2 * (d - cover);
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                               else if (aact >= amin && aact <= amax && aact > ts)
                                {
                                    // comp in both web & flange
                                    double moment = 0.87 * fy * As2 * (d - cover) + 0.45 * fcu * B * ts * (d - (ts / 2)) + 0.45 * fcu * bb * (aact - ts) * (d - ts - ((aact - ts) / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";

                                }
                            }
                        }
                        ////////////////////////////////////////////////////////////////////
                        ////////// Neglect As' Here 
                        if ((As2 / As1) <= 0.2)         ///Single RFT
                        {
                            double aact = (0.87 * fy * As1) / (0.45 * fcu * B);
                            // firstly assume RC section (All comp. in the Flange)
                            if (aact <= ts)         ///Right Assumption
                            {
                                if (aact < amin)
                                {
                                    double moment = Math.Round((0.826 * fy * d * As1 * Math.Pow(10, -6)), 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                                else if (aact > amax)
                                {
                                    /// مازال برضوا كدا الضغط يقع باكمله داخل الفلانجة 
                                    double moment = 0.45 * fcu * B * amax * (d - (amax / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Brittle Failure ... (Over RFT Section) ";
                                }
                                else
                                {
                                    double moment = 0.45 * fcu * B * aact * (d - (aact / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                            }

                            ///// if Wrong Assumption 

                            if (aact > ts)
                            {
                                ////// there are comp in (Flange && Web) Do not Neglect Web (Perfect)
                                if (aact < amin)
                                {
                                    double moment = Math.Round((0.826 * fy * d * As1 * Math.Pow(10, -6)), 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                                else if (aact > amax && amax <= ts)  //R-Section
                                {
                                    /// مازال برضوا كدا الضغط يقع باكمله داخل الفلانجة 
                                    double moment = 0.45 * fcu * B * amax * (d - (amax / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Brittle Failure ... (Over RFT Section) ";
                                }
                                else if (aact > amax && amax > ts)
                                {
                                    // comp in both web & flange
                                    double moment = 0.45 * fcu * B * ts * (d - (ts / 2)) + 0.45 * fcu * bb * (amax - ts) * (d - ts - ((amax - ts) / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Brittle Failure ... (Over RFT Section) ";

                                }
                               else if (aact >= amin && aact <= amax && aact <= ts) /// R_Section
                                {
                                    // انت هنا كدا برضوا كلوا في الفلنجة 
                                    double moment = 0.45 * fcu * aact * B * (d - (aact / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";
                                }
                                else if (aact >= amin && aact <= amax && aact > ts)
                                {
                                    // comp in both web & flange
                                    double moment = 0.45 * fcu * B * ts * (d - (ts / 2)) + 0.45 * fcu * bb * (aact - ts) * (d - ts - ((aact - ts) / 2));
                                    moment = moment * Math.Pow(10, -6);
                                    moment = Math.Round(moment, 2);
                                    txtmoment.Text = moment.ToString();
                                    txtfailure.Text = "Ductile Failure ... (Under RFT Section) ";

                                }
                            }
                        }

                        //// agiant Shear for R section
                        double qsu = (branch * Astir * 0.87 * 240) / (S * bb);
                        double qcumin = 0.12 * Math.Sqrt(fcu / 1.5);
                        double qu = qsu + qcumin;          //N/mm2
                        double Qu = Math.Round((qu * bb * d * 0.001), 2);       //kN
                        txtshear.Text = Qu.ToString();

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            //////////////////// هنا يتم أخد الاسكرينة 
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
            txtmoment.Clear();
            txtshear.Clear();
            txtfailure.Text = "";
        }
    }
}