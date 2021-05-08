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
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Design_Concrete
{
    public partial class TwoHB : Form
    {
        
        private mainForm1 form11;

        public TwoHB()
        {
            InitializeComponent();
           
            
        }

        public TwoHB(mainForm1 form11)
        {
            this.form11 = form11;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txta.Text.Trim() == "" || txtb.Text.Trim() == "" || txtc.Text.Trim() == "" || txte.Text.Trim() == ""
              || txtfai1main.Text.Trim() == "" || txtfai2main.Text.Trim() == "" || txtfai3main.Text.Trim() == ""
              || txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtLx.Text.Trim() == "" || txtLy.Text.Trim() == ""
              || txtm1main.Text.Trim() == "" || txtm2main.Text.Trim() == "" || txtm3main.Text.Trim() == ""
              || txtm1sec.Text.Trim() == "" || txtm2sec.Text.Trim() == "" || txtm3sec.Text.Trim() == ""
              || txtt.Text.Trim() == "" || txtw.Text.Trim() == "" || txtfai1sec.Text.Trim() == "" || txtfai2sec.Text.Trim() == ""
              || txtfai3sec.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtt.Focus();
                    return;
                }
                else
                {

                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double t = double.Parse(txtt.Text);
                    double cov = double.Parse(txtc.Text);
                    double W = double.Parse(txtw.Text);
                    double m1main = double.Parse(txtm1main.Text);
                    double m2main = double.Parse(txtm2main.Text);
                    double m3main = double.Parse(txtm3main.Text);
                    double m1sec = double.Parse(txtm1sec.Text);
                    double m2sec = double.Parse(txtm2sec.Text);
                    double m3sec = double.Parse(txtm3sec.Text);
                    double ee = double.Parse(txte.Text);
                    double aa = double.Parse(txta.Text);
                    double bb = double.Parse(txtb.Text);
                    int fai1main = int.Parse(txtfai1main.Text);
                    int fai2main = int.Parse(txtfai2main.Text);
                    int fai3main = int.Parse(txtfai3main.Text);
                    int fai1sec = int.Parse(txtfai1sec.Text);
                    int fai2sec = int.Parse(txtfai2sec.Text);
                    int fai3sec = int.Parse(txtfai3sec.Text);
                    double Ly = double.Parse(txtLy.Text);
                    double Lx = double.Parse(txtLx.Text);
                    double alfa;
                    double beta;
                    double r;
                    double walfa;
                    double wbeta;
                    double X1;
                    double X2;
                    double X3;
                    double X4;

                    double d = t - cov;
                    double Mr;
                    if (fy == 360 || fy == 350)
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

                    //////////////////////////
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double B = ee + bb;
                    double a1main = d * (1 - Math.Sqrt(1 - ((2 * m1main * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                    if (a1main > amax)
                    {
                        MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double C1main = 1.25 * a1main;
                    double J1main = (1 / 1.15) * (1 - 0.4 * (C1main / d));
                    if (J1main > 0.826)
                    {
                        J1main = 0.826;            /////"Ducyility Condition:"
                    }

                    double AS1main = (m1main * Math.Pow(10, 6)) / (fy * J1main * d);
                    double num1main = Math.Ceiling((AS1main / (3.1459 * 0.25 * fai1main * fai1main)));
                    if (num1main < 2)
                    {
                        num1main = 2;
                    }
                    txtnum1main.Text = num1main.ToString();

                    /////////////////

                    double a2main = d * (1 - Math.Sqrt(1 - ((2 * m2main * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                    if (a2main > amax)
                    {
                        MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double C2main = 1.25 * a2main;
                    double J2main = (1 / 1.15) * (1 - 0.4 * (C2main / d));
                    if (J2main > 0.826)
                    {
                        J2main = 0.826;            /////"Ducyility Condition:"
                    }

                    double AS2main = (m2main * Math.Pow(10, 6)) / (fy * J2main * d);
                    double num2main = Math.Ceiling((AS2main / (3.1459 * 0.25 * fai2main * fai2main)));
                    if (num2main < 2)
                    {
                        num2main = 2;
                    }
                    txtnum2main.Text = num2main.ToString();


                    ////////

                    double a3main = d * (1 - Math.Sqrt(1 - ((2 * m3main * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                    if (a3main > amax)
                    {
                        MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double C3main = 1.25 * a3main;
                    double J3main = (1 / 1.15) * (1 - 0.4 * (C3main / d));
                    if (J3main > 0.826)
                    {
                        J3main = 0.826;            /////"Ducyility Condition:"
                    }

                    double AS3main = (m3main * Math.Pow(10, 6)) / (fy * J3main * d);
                    double num3main = Math.Ceiling((AS3main / (3.1459 * 0.25 * fai3main * fai3main)));
                    if (num3main < 2)
                    {
                        num3main = 2;
                    }
                    txtnum3main.Text = num3main.ToString();


                    ////////secondary direction 
                    double a1sec = d * (1 - Math.Sqrt(1 - ((2 * m1sec * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                    if (a1sec > amax)
                    {
                        MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double C1sec = 1.25 * a1sec;
                    double J1sec = (1 / 1.15) * (1 - 0.4 * (C1sec / d));
                    if (J1sec > 0.826)
                    {
                        J1sec = 0.826;            /////"Ducyility Condition:"
                    }

                    double AS1sec = (m1sec * Math.Pow(10, 6)) / (fy * J1sec * d);
                    double num1sec = Math.Ceiling((AS1sec / (3.1459 * 0.25 * fai1sec * fai1sec)));
                    if (num1sec < 2)
                    {
                        num1sec = 2;
                    }
                    txtnum1sec.Text = num1sec.ToString();

                    /////////////////

                    double a2sec = d * (1 - Math.Sqrt(1 - ((2 * m2sec * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                    if (a2sec > amax)
                    {
                        MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double C2sec = 1.25 * a2sec;
                    double J2sec = (1 / 1.15) * (1 - 0.4 * (C2sec / d));
                    if (J2sec > 0.826)
                    {
                        J2sec = 0.826;            /////"Ducyility Condition:"
                    }

                    double AS2sec = (m2sec * Math.Pow(10, 6)) / (fy * J2sec * d);
                    double num2sec = Math.Ceiling((AS2sec / (3.1459 * 0.25 * fai2sec * fai2sec)));
                    if (num2sec < 2)
                    {
                        num2sec = 2;
                    }
                    txtnum2sec.Text = num2sec.ToString();


                    ////////

                    double a3sec = d * (1 - Math.Sqrt(1 - ((2 * m3sec * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * B * d * d))));
                    if (a3sec > amax)
                    {
                        MessageBox.Show("UNSafe Section for RFT ... iNCREASE Dims.");
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double C3sec = 1.25 * a3sec;
                    double J3sec = (1 / 1.15) * (1 - 0.4 * (C3sec / d));
                    if (J3sec > 0.826)
                    {
                        J3sec = 0.826;            /////"Ducyility Condition:"
                    }

                    double AS3sec = (m3sec * Math.Pow(10, 6)) / (fy * J3sec * d);
                    double num3sec = Math.Ceiling((AS3sec / (3.1459 * 0.25 * fai3sec * fai3sec)));
                    if (num3sec < 2)
                    {
                        num3sec = 2;
                    }
                    txtnum3sec.Text = num3sec.ToString();


                    if (txttypemain.Text == "Case_1" && txttypesec.Text == "Case_1")
                    {

                        r = Lx / Ly;
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 1 Simple beam x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * ee - (n1 - 1) * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        ////// here it is case 1 Simple beam x1=x2
                        double Xfinal2 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx3.Text = Xfinal2.ToString();
                        txtx4.Text = Xfinal2.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 2 * n2 * 2;
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
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return ;
                        }
                        else
                        {
                            return;
                        }
                           

                    }

                    if (txttypemain.Text == "Case_1" && txttypesec.Text == "Case_2")
                    {
                        r = (0.87 * Lx) / Ly;
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 1 Simple beam x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * ee - (n1 - 1) * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 2  cont on side  x3>x4
                        double Xfinal4 = Math.Round(X4, 2);
                        double Xfinal3 = (Lx - n2 * ee - (n2 - 1) * bb - Xfinal4);
                        Xfinal3 = Math.Round(Xfinal3, 2);
                        txtx3.Text = Xfinal3.ToString();
                        txtx4.Text = Xfinal4.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 2 * 2 * n2;
                        txtntotal.Text = ntotal.ToString();


                        /////// table
                        /////// table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string thick = txtt.Text;
                            string L = txtLx.Text;
                            string Ls = txtLy.Text;
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }
                          

                    }

                    if (txttypemain.Text == "Case_1" && txttypesec.Text == "Case_3")
                    {

                        r = 0.76 * Lx / Ly;
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 1 Simple beam x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * ee - (n1 - 1) * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 3 cont two sides    let x3=x4
                        double Xfinal2 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx3.Text = Xfinal2.ToString();
                        txtx4.Text = Xfinal2.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
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
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;

                        }
                        else
                        {
                            return;
                        }
                            

                    }
                    /////////////////////////////////////
                    ///////// ألحالة الثانية 

                    if (txttypemain.Text == "Case_2" && txttypesec.Text == "Case_1")
                    {

                        r = Lx / (0.87 * Ly);
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 2 cont 1 side let  x1<x2
                        double Xfinal1 = Math.Round(X1, 2);
                        double Xfinal2 = (Ly - n1 * ee - (n1 - 1) * bb - Xfinal1);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal2.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 1 Simple beam x3=x4
                        double Xfinal22 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal22 = Math.Round(Xfinal22, 2);
                        txtx3.Text = Xfinal22.ToString();
                        txtx4.Text = Xfinal22.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
                        txtntotal.Text = ntotal.ToString();


                        /////// table

                        /////// table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string thick = txtt.Text;
                            string L = txtLx.Text;
                            string Ls = txtLy.Text;
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }

                    }

                    if (txttypemain.Text == "Case_2" && txttypesec.Text == "Case_2")
                    {

                        r = Lx / Ly;
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 2 cont 1 side  x1<x2
                        double Xfinal1 = Math.Round(X1, 2);
                        double Xfinal2 = (Ly - n1 * ee - (n1 - 1) * bb - Xfinal1);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal2.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 2  cont on side  x3>x4
                        double Xfinal4 = Math.Round(X4, 2);
                        double Xfinal3 = (Lx - n2 * ee - (n2 - 1) * bb - Xfinal4);
                        Xfinal3 = Math.Round(Xfinal3, 2);
                        txtx3.Text = Xfinal3.ToString();
                        txtx4.Text = Xfinal4.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
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
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;

                        }
                        else
                        {
                            return;
                        }

                    }

                    if (txttypemain.Text == "Case_2" && txttypesec.Text == "Case_3")
                    {

                        r = (0.76 * Lx) / (0.87 * Ly);
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 2 cont 1 side  x1<x2
                        double Xfinal1 = Math.Round(X1, 2);
                        double Xfinal2 = (Ly - n1 * ee - (n1 - 1) * bb - Xfinal1);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal2.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 3 cont two sides    let x3=x4
                        double Xfinal22 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal22 = Math.Round(Xfinal22, 2);
                        txtx3.Text = Xfinal22.ToString();
                        txtx4.Text = Xfinal22.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
                        txtntotal.Text = ntotal.ToString();


                        /////// table
                        /////// table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string thick = txtt.Text;
                            string L = txtLx.Text;
                            string Ls = txtLy.Text;
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }

                    }

                    /////////////////////// 
                    ///////////////////// ألحالة الثالثة 
                    if (txttypemain.Text == "Case_3" && txttypesec.Text == "Case_1")
                    {

                        r = Lx / (0.76 * Ly);
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 3 cont 2 side let x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * ee - (n1 - 1) * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is simple Let x3=x4
                        double Xfinal2 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx3.Text = Xfinal2.ToString();
                        txtx4.Text = Xfinal2.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
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
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;

                        }
                        else
                        {
                            return;
                        }
                           

                    }

                    if (txttypemain.Text == "Case_3" && txttypesec.Text == "Case_2")
                    {
                        r = (0.87 * Lx) / (0.76 * Ly);
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 3 cont 2 side let x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * ee - (n1 - 1) * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 2  cont one side  x3>x4
                        double Xfinal4 = Math.Round(X4, 2);
                        double Xfinal3 = (Lx - n2 * ee - (n2 - 1) * bb - Xfinal4);
                        Xfinal3 = Math.Round(Xfinal3, 2);
                        txtx3.Text = Xfinal3.ToString();
                        txtx4.Text = Xfinal4.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
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
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
                            DataGridView1.Rows.Add(data);
                            return;

                        }
                        else
                        {
                            return;
                        }

                    }

                    if (txttypemain.Text == "Case_3" && txttypesec.Text == "Case_3")
                    {

                        r = Lx / Ly;
                        alfa = (r * r * r * r) / (1 + (r * r * r * r));
                        beta = 1 - alfa;
                        ///main direction
                        walfa = alfa * W;
                        wbeta = beta * W;
                        double R1 = (walfa * 0.5 * Ly * Ly + m1main - m3main) / Ly;
                        double R2 = (walfa * 0.5 * Ly * Ly + m3main - m1main) / Ly;
                        if (m1main > Mr)
                        {
                            double root1 = Math.Sqrt(4 * R1 * R1 - 4 * walfa * (2 * m1main - 2 * Mr));
                            double X11 = Math.Abs((2 * R1 + root1) / (2 * walfa));
                            double X12 = Math.Abs((2 * R1 - root1) / (2 * walfa));
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
                        if (m3main > Mr)
                        {
                            double root2 = Math.Sqrt(4 * R2 * R2 - 4 * walfa * (2 * m3main - 2 * Mr));
                            double X21 = Math.Abs((2 * R2 + root2) / (2 * walfa));
                            double X22 = Math.Abs((2 * R2 - root2) / (2 * walfa));
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
                        //// secondary direction 
                        double R3 = (wbeta * 0.5 * Lx * Lx + m1sec - m3sec) / Lx;
                        double R4 = (wbeta * 0.5 * Lx * Lx + m3sec - m1sec) / Lx;
                        if (m1sec > Mr)
                        {
                            double root3 = Math.Sqrt(4 * R3 * R3 - 4 * wbeta * (2 * m1sec - 2 * Mr));
                            double X41 = Math.Abs((2 * R3 + root3) / (2 * wbeta));
                            double X42 = Math.Abs((2 * R3 - root3) / (2 * wbeta));
                            X4 = Math.Min(X41, X42);
                            if (X4 < 0.25)
                            {
                                X4 = 0.25;
                            }
                        }
                        else
                        {
                            X4 = 0.25;
                        }

                        //////
                        if (m3sec > Mr)
                        {
                            double root4 = Math.Sqrt(4 * R4 * R4 - 4 * wbeta * (2 * m3sec - 2 * Mr));
                            double X31 = Math.Abs((2 * R4 + root4) / (2 * wbeta));
                            double X32 = Math.Abs((2 * R4 - root4) / (2 * wbeta));
                            X3 = Math.Max(X31, X32);
                            if (X3 < 0.25)
                            {
                                X3 = 0.25;
                            }
                        }
                        else
                        {
                            X3 = 0.25;
                        }

                        //    //Arrangement of Blocks ; 
                        //    // Short dir.
                        double n1 = (Ly - X1 - X2 + bb) / (bb + ee);
                        n1 = Math.Floor(n1);
                        ///// here it is case 3 let  x1=x2
                        double Xfinal1 = 0.5 * (Ly - n1 * ee - (n1 - 1) * bb);
                        Xfinal1 = Math.Round(Xfinal1, 2);
                        txtx1.Text = Xfinal1.ToString();
                        txtx2.Text = Xfinal1.ToString();
                        txtn1.Text = (n1 * 2).ToString();
                        ////long dir.
                        double n2 = (Lx - X3 - X4 + bb) / (bb + ee);
                        n2 = Math.Floor(n2);
                        //    ///// here it is case 3 cont two sides    let x3=x4
                        double Xfinal2 = 0.5 * (Lx - n2 * ee - (n2 - 1) * bb);
                        Xfinal2 = Math.Round(Xfinal2, 2);
                        txtx3.Text = Xfinal2.ToString();
                        txtx4.Text = Xfinal2.ToString();
                        txtn2.Text = (n2 * 2).ToString();
                        double ntotal = n1 * 4 * n2;
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
                            string As2main = txtnum2main.Text + " T " + txtfai2main.Text;
                            string As1main = txtnum1main.Text + " T " + txtfai1main.Text;
                            string As3main = txtnum3main.Text + " T " + txtfai3main.Text;
                            string As2sec = txtnum2sec.Text + " T " + txtfai2sec.Text;
                            string As1sec = txtnum1sec.Text + " T " + txtfai1sec.Text;
                            string As3sec = txtnum3sec.Text + " T " + txtfai3sec.Text;

                            string X111 = txtx1.Text;
                            string X222 = txtx2.Text;
                            string n111 = txtn1.Text;
                            string X333 = txtx3.Text;
                            string X444 = txtx4.Text;
                            string n222 = txtn2.Text;
                            object[] data = { serial, thick, L, Ls, As2main, As1main, As3main, As2sec, As1sec, As3sec, X111, X222, n111, X333, X444, n222 };
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
 }           
        private void txttypemain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txttypemain.Text == "Case_1")
            {
                pictureBox4.Visible = true;
                pictureBox3.Visible = false;
                pictureBox2.Visible = false;

            }
            if (txttypemain.Text == "Case_2")
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
                pictureBox2.Visible = false;

            }
            if (txttypemain.Text == "Case_3")
            {
                pictureBox2.Visible = true;
                pictureBox4.Visible = false;
                pictureBox3.Visible = false;

            }
        }

        private void txttypesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txttypesec.Text == "Case_1")
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;

            }
            if (txttypesec.Text == "Case_2")
            {
                pictureBox6.Visible = true;
                pictureBox5.Visible = false;
                pictureBox7.Visible = false;

            }
            if (txttypesec.Text == "Case_3")
            {
                pictureBox7.Visible = true;
                pictureBox6.Visible = false;
                pictureBox5.Visible = false;

            }
        }

        private void twowayHB_Load(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Look from Right side ,, then it is main strip and main B.M.D ....... Look from Bottom ,, then it is Sec. strip and Sec. B.M.D ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtx1.Clear();
            txtx2.Clear();
            txtx3.Clear();
            txtx4.Clear();

            txtn1.Clear();
            txtn2.Clear();

            txtntotal.Clear();

            txtnum1main.Clear();
            txtnum1sec.Clear();
            txtnum2main.Clear();
            txtnum2sec.Clear();
            txtnum3main.Clear();
            txtnum3sec.Clear();
        }
    }
}
