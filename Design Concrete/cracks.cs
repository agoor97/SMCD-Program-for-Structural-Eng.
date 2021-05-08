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
    public partial class cracks : Form
    {
        public cracks()
        {
            InitializeComponent();
        }

        private void cracks_Load(object sender, EventArgs e)
        {
           
            txtN.Enabled = false;
            rdiocomp.Enabled = false;
            rditens.Enabled = false;
         
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == false)
            {            
                txtN.Enabled = false;
                rdiocomp.Enabled = false;
                rditens.Enabled = false;
                
            }
            if(checkBox1.Checked == true)
            {
                txtN.Enabled = true;
                rdiocomp.Enabled = true;
                rditens.Enabled = true;
               
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(txtcat.Text == "Category One")
            {
                txtwkmax.Text = "0.30";
            }
            if (txtcat.Text == "Category Two")
            {
                txtwkmax.Text = "0.20";
            }
            if (txtcat.Text == "Category Three")
            {
                txtwkmax.Text = "0.15";
            }
            if (txtcat.Text == "Category Four")
            {
                txtwkmax.Text = "0.10";
            }

            ////////////
            if(txtfcu.Text.Trim()==""||txtm.Text.Trim()==""||txtt.Text.Trim()==""
                ||txtc.Text.Trim()==""||txtb.Text.Trim()==""||txtfai1.Text.Trim()==""
                ||txtnum1.Text.Trim()==""||txtcat.Text.Trim()=="")
            {
                MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            double fcu = double.Parse(txtfcu.Text);
            double Mu = double.Parse(txtm.Text);
            double t = double.Parse(txtt.Text);
            double cover = double.Parse(txtc.Text);
            double b = double.Parse(txtb.Text);
            double fai1 = double.Parse(txtfai1.Text);
            double num1 = double.Parse(txtnum1.Text);
            double d = t - cover;  //mm
            double K1;
            double B1;
            double Astension = 3.1459 * 0.25 * num1 * fai1 * fai1;
            if (txtfytype.Text == "24/35")
            {
                K1 = 1.60;
                B1 = 0.50;
            }
            else
            {
                K1 = 0.80;
                B1 = 0.80;
            }

            if (checkBox1.Checked == false)
            {
                //// Only Bending Moment 
              

                double K2 = 0.50;        // Bending Only
                double B2 = 0.50;        // for Long Term Deflection

               
                /// there is No Normal Force .. OnLy Bending Moment
                double roo = (Astension / (2.5 * cover * b));
                double srm = 50 + 0.25 * K1 * K2 * (fai1 / roo);

               
                double fctr = 0.6 * Math.Sqrt(fcu);
                double Ig = (b * t * t * t) / 12;
                double Mcr = fctr * Ig / (t / 2);              // N/mm2

                /// Get Z  عمق محور التعادل
                double root = Math.Sqrt((30 * Astension) * (30 * Astension) + 4 * b * 30 * Astension * d);
                double Z1 = ((-30 * Astension) + root) / (2 * b);
                double Z2 = ((-30 * Astension) - root) / (2 * b);
                double Z = Math.Max(Z1, Z2);

                double Inv = (b * Z * Z * Z) / 3 + 15 * Astension * (d - Z) * (d - Z);

                double Mact = (Mu / 1.5) * Math.Pow(10, 6);       // N/mm2
                double Fs = 15 * Mact * (d - Z) / Inv;
                double Fsr = 15 * Mcr * (d - Z) / Inv;
                double esm = (Fs / (2.0 * Math.Pow(10, 5))) * (1 - B1 * B2 * (Fsr / Fs) * (Fsr / Fs));

               
                double Wk = Math.Round((1.70 * esm * srm),4);

                txtwk.Text = Wk.ToString();

                double act = Wk;
                double aLL = double.Parse(txtwkmax.Text);

                if(act>aLL)
                {
                    lblcompare.Text = "UnAcceptable";
                }
                else if (act <= aLL)
                {
                    lblcompare.Text = "Acceptable";
                }
            }

            //////////////////////////////////////// 

            if (checkBox1.Checked == true)
            {
                //// Only Bending Moment 
                
   
                /// Both Normal && Moment
             
                if (rdiocomp.Checked == true)
                {
                

                    /// Comp. force ++ Moment
                    /// 
                    if (txtN.Text.Trim()=="")
                    {
                        MessageBox.Show(" Enter Nu .. " , "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    /// Normal Force is Comp.
                    double Nu = double.Parse(txtN.Text);
                    double ecc = Mu / Nu;
                    double ratio = ecc / (0.001 * t);

                    if(ratio <=0.5)
                    {
                        //Small eccentricity 
                        /// All Section is Subjected to Comp. stress
                        MessageBox.Show("All Section is Subjected to Compression stress,No need to Check Cracks", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;   
                    }

                    else
                    {
                        /// Section is Subjected to Bending an Normal /// تشتغل زب بالظبط لما يكون مومنت لوحده
                        double K2 = 0.50;          // for Bending and Comp. force with Big eccentric.
                        double B2 = 0.50;        // for Long Term Deflection

                        double roo = (Astension / (2.5 * cover * b));
                        double srm = 50 + 0.25 * K1 * K2 * (fai1 / roo);


                        double fctr = 0.6 * Math.Sqrt(fcu);
                        double Ig = (b * t * t * t) / 12;
                        double Mcr = fctr * Ig / (t / 2);              // N/mm2

                        /// Get Z  عمق محور التعادل
                        double root = Math.Sqrt((30 * Astension) * (30 * Astension) + 4 * b * 30 * Astension * d);
                        double Z1 = ((-30 * Astension) + root) / (2 * b);
                        double Z2 = ((-30 * Astension) - root) / (2 * b);
                        double Z = Math.Max(Z1, Z2);

                        double Inv = (b * Z * Z * Z) / 3 + 15 * Astension * (d - Z) * (d - Z);

                        double Mact = (Mu / 1.5) * Math.Pow(10, 6);       // N/mm2
                        double Fs = 15 * Mact * (d - Z) / Inv;
                        double Fsr = 15 * Mcr * (d - Z) / Inv;
                        double esm = (Fs / (2.0 * Math.Pow(10, 5))) * (1 - B1 * B2 * (Fsr / Fs) * (Fsr / Fs));


                        double Wk = Math.Round((1.70 * esm * srm), 4);

                        txtwk.Text = Wk.ToString();

                        double act = Wk;
                        double aLL = double.Parse(txtwkmax.Text);

                        if (act > aLL)
                        {
                            lblcompare.Text = "UnAcceptable";
                        }
                        else if (act <= aLL)
                        {
                            lblcompare.Text = "Acceptable";
                        }
                    }
                }

                ////////////////////////////


                if (rditens.Checked == true)
                {
                    ///Tens. force ++ Moment
                    /// 
                    if (txtN.Text.Trim() == "")
                    {
                        MessageBox.Show(" Enter Nu .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    /// Normal Force is tens
                    double Nu = double.Parse(txtN.Text);
                    double ecc = Mu / Nu;
                    double ratio = ecc / (0.001 * t);

                    if (ratio <= 0.5)
                    {
                        /// القطاع كله عليه شد
                      
                        double fy;

                        if(txtfytype.Text == "24/35")
                        {
                            fy = 240;
                        }
                        else if (txtfytype.Text == "36/52")
                        {
                            fy = 360;
                        }
                        else if ( txtfytype.Text == "40/60")
                        {
                            fy = 400;
                        }
                        else
                        {
                            fy = 360;
                        }

                        //
                        double ecc1 = (t / 2) - ecc - cover;
                        double ecc2 = (t / 2) + ecc - cover;

                        double e2 = 0.87 * fy / (2 * Math.Pow(10, 5));
                        double e1 = (ecc2 / ecc1) * e2;
                        double K2 = (e1 + e2) / (2 * e1);      //Both Tension ++ Moment
                        double B2 = 0.50;        // for Long Term Deflection

                        double Fs1 = (Nu/1.5) * 1000 * ecc2 / (Astension * (d - 40));
                        

                        double fctr = 0.6 * Math.Sqrt(fcu);
                        double Ncr = 0.001 * fctr / ((1 / (b * t)) + (6 * ecc / (b * t * t)));
                        double Fsr1 = Ncr * ecc2 / (Astension * (d - 50));
                        MessageBox.Show(Fsr1.ToString());

                        double roo = (Astension / (2.5 * cover * b));
                        double srm = 50 + 0.25 * K1 * K2 * (fai1 / roo);                     
                        double esm = (Fs1 / (2.0 * Math.Pow(10, 5))) * (1 - B1 * B2 * (Fsr1 / Fs1) * (Fsr1 / Fs1));


                        double Wk = Math.Round((1.70 * esm * srm), 4);

                        txtwk.Text = Wk.ToString();

                        double act = Wk;
                        double aLL = double.Parse(txtwkmax.Text);

                        if (act > aLL)
                        {
                            lblcompare.Text = "UnAcceptable";
                        }
                        else if (act <= aLL)
                        {
                            lblcompare.Text = "Acceptable";
                        }
                    }


                    else
                    {
                        /// تعامل كالحالة العادية
                     

                        /// Section is Subjected to Bending an Normal /// تشتغل زب بالظبط لما يكون مومنت لوحده
                        double K2 = 0.50;          // for Bending and Comp. force with Big eccentric.
                        double B2 = 0.50;        // for Long Term Deflection

                        double roo = (Astension / (2.5 * cover * b));
                        double srm = 50 + 0.25 * K1 * K2 * (fai1 / roo);


                        double fctr = 0.6 * Math.Sqrt(fcu);
                        double Ig = (b * t * t * t) / 12;
                        double Mcr = fctr * Ig / (t / 2);              // N/mm2

                        /// Get Z  عمق محور التعادل
                        double root = Math.Sqrt((30 * Astension) * (30 * Astension) + 4 * b * 30 * Astension * d);
                        double Z1 = ((-30 * Astension) + root) / (2 * b);
                        double Z2 = ((-30 * Astension) - root) / (2 * b);
                        double Z = Math.Max(Z1, Z2);

                        double Inv = (b * Z * Z * Z) / 3 + 15 * Astension * (d - Z) * (d - Z);

                        double Mact = (Mu / 1.5) * Math.Pow(10, 6);       // N/mm2
                        double Fs = 15 * Mact * (d - Z) / Inv;
                        double Fsr = 15 * Mcr * (d - Z) / Inv;
                        double esm = (Fs / (2.0 * Math.Pow(10, 5))) * (1 - B1 * B2 * (Fsr / Fs) * (Fsr / Fs));


                        double Wk = Math.Round((1.70 * esm * srm), 4);

                        txtwk.Text = Wk.ToString();

                        double act = Wk;
                        double aLL = double.Parse(txtwkmax.Text);

                        if (act > aLL)
                        {
                            lblcompare.Text = "UnAcceptable";
                        }
                        else if (act <= aLL)
                        {
                            lblcompare.Text = "Acceptable";
                        }
                    }
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtwk.Clear();
            txtwkmax.Clear();
        }
    }
}
