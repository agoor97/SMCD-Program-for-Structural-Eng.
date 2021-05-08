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
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Configuration;

namespace Design_Concrete
{
    public partial class combined : Form
    {
        public combined()
        {
            InitializeComponent();
        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void txttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txttype.Text == "Case_1")
            {               
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;               
            }
            if (txttype.Text == "Case_2")
            {                
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;            
            }
            
        }

        private void combined_Load(object sender, EventArgs e)
        {        
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtp1.Text.Trim() == "" || txtp2.Text.Trim() == ""
               || txta1.Text.Trim() == "" || txta2.Text.Trim() == "" || txtb1.Text.Trim() == "" || txtb2.Text.Trim() == ""
               || txts.Text.Trim() == "" || txttrc.Text.Trim() == "" || txttpc.Text.Trim() == "" || txtqall.Text.Trim() == ""
               || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "" || txtfai3.Text.Trim() == "" || txtfai4.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double qall = double.Parse(txtqall.Text);
                    double Pw1 = double.Parse(txtp1.Text);
                    double Pw2 = double.Parse(txtp2.Text);
                    double b1 = double.Parse(txtb1.Text);
                    double a1 = double.Parse(txta1.Text);
                    double b2 = double.Parse(txtb2.Text);
                    double a2 = double.Parse(txta2.Text);

                    double S = double.Parse(txts.Text);
                    double tpc = double.Parse(txttpc.Text);
                    double trc = double.Parse(txttrc.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);
                    int fai3 = int.Parse(txtfai3.Text);
                    int fai4 = int.Parse(txtfai4.Text);
                    double Lpc;
                    double Lrc;
                    double Bpc;
                    double Brc;
                    double Rw;
                    double X;
                    double d = trc - 0.07;                  // Cover = 70mm  


                    if (txttype.Text == "Case_1")
                    {
                        // ordinary Footing  Interior type                                  
                        if (Pw1 < Pw2)
                        {
                            MessageBox.Show("Look Carefully at the Photo .. P1 > P2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtp1.Focus();
                            txtp1.SelectAll();
                            return;
                        }
                        else
                        {
                            Rw = Pw1 + Pw2;
                            X = (Pw1 * S) / Rw;
                            Lrc = 2 * (X + (b2 / 2) + 0.75);
                            Lpc = Lrc + 2 * tpc;
                            if (tpc >= 0.2)
                            {
                                Bpc = Rw / (qall * Lpc);
                                Brc = Bpc - 2 * tpc;

                            }
                            else
                            {

                                Brc = Rw / (qall * Lrc);
                                Bpc = Brc + 2 * tpc;
                            }

                            // Ultimate Stage
                            // Get Moments
                            double Ru = 1.5 * Rw;
                            double Pu1 = 1.5 * Pw1;
                            double Pu2 = 1.5 * Pw2;

                            double Fact = (Ru) / (Lrc * Brc);
                            double Wu = Ru / Lrc;

                            double x1 = (Lrc / 2) - (S - X) - (b1 / 2);
                            double x2 = (Lrc / 2) - (S - X) + (b1 / 2);
                            double x4 = 0.75;
                            double x5 = 0.75 + b2;
                            double x0 = (Pu1 / Wu);
                            double x3 = x0 - x1 - (b1 / 2);
                            // Longitudinal Dir.
                            double M1 = Wu * x1 * x1 * 0.5;
                            double M2 = Wu * x2 * x2 * 0.5 - Pu1 * 0.5 * b1;
                            double M4 = Wu * 0.5 * x4 * x4;
                            double M5 = Wu * x5 * x5 * 0.5 - Pu2 * 0.5 * b2;
                            double M3top = Pu1 * x3 - Wu * 0.5 * x0 * x0;
                            double Mmax1 = Math.Max(M1, M2);
                            double Mmax2 = Math.Max(M4, M5);
                            double Mbott = Math.Max(Mmax1, Mmax2);

                            /// Tranverse Dir.   as Hidden Beams
                            /// 
                            // Footing F1
                            double L1 = b1 + 2 * d;
                            double F1act = Pu1 / (L1 * Brc);
                            double Z1 = (Brc - a1) / 2;
                            double M1act = F1act * 0.5 * Z1 * Z1;          //kN.m/m'

                            // Footing f2
                            double L2 = b2 + 2 * d;
                            double F2act = Pu2 / (L2 * Brc);
                            double Z2 = (Brc - a2) / 2;
                            double M2act = F2act * 0.5 * Z2 * Z2;

                            // Forces for Shear
                            double Q1 = Wu * x1;
                            double Q2 = Math.Abs(Wu * x2 - Pu1);
                            double Q3 = Wu * x4;
                            double Q4 = Math.Abs(Wu * x5 - Pu2);
                            double Fmax1 = Math.Max(Q1, Q2);
                            double Fmax2 = Math.Max(Q3, Q4);
                            double Qmax = Math.Max(Fmax1, Fmax2);
                            double Qcr = Qmax - Wu * (d / 2);


                            // Check Shear 
                            double qsh = 0.16 * Math.Sqrt(fcu / 1.5);
                            double qact = (Qcr * 1000) / (Brc * 1000 * d * 1000);          //N/mm2

                            if (qact > qsh)
                            {
                                MessageBox.Show("UnSafe against Shear .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                lblshear.Text = "UnSafe";
                                return;
                            }

                            lblshear.Text = "Safe";

                            //// Check Punch.
                            // column 1 
                            double Q1p = Pu1 - Fact * (a1 + d) * (b1 + d);
                            double Ap1 = 2 * d * (a1 + b1 + 2 * d);
                            double qp1 = (Q1p / (1000 * Ap1));
                            // Column 2 
                            double Q2p = Pu2 - Fact * (a2 + d) * (b2 + d);
                            double Ap2 = 2 * d * (a2 + b2 + 2 * d);
                            double qp2 = (Q2p / (1000 * Ap2));

                            double qp = Math.Max(qp1, qp2);

                            double qpaLL1 = 1.60;
                            double qpaLL2 = 0.316 * Math.Sqrt(fcu / 1.5);
                            double qpaLL3 = 0.316 * (0.5 + (a2 / b2)) * Math.Sqrt(fcu / 1.5);
                            double qpmin1 = Math.Min(qpaLL1, qpaLL2);
                            double qpun = Math.Min(qpmin1, qpaLL3);
                            if (qp > qpun)
                            {
                                MessageBox.Show("UnSafe against Punching .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                lblpunch.Text = "UnSafe";
                                return;
                            }

                            lblpunch.Text = "Safe";

                            // RFT
                            double Asmin1 = 1.5 * d * 1000;
                            double Asmin2 = 565;                              // 5 fai 12 /m'  =565mm2
                            double Asmin = Math.Max(Asmin1, Asmin2);

                            ///// For Long. Dir.
                            /// Bootom RFT
                            double amax = (320 * d * 1000) / (600 + 0.87 * fy);
                            double abot = d * 1000 * (1 - Math.Sqrt(1 - ((2 * Mbott * 1000 * 1000) / (0.45 * fcu * Brc * 1000 * d * d * 1000 * 1000))));

                            if (abot > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Cbot = 1.25 * abot;
                            double Jbot = (1 / 1.15) * (1 - 0.4 * (Cbot / (d * 1000)));
                            if (Jbot > 0.826)
                            {
                                Jbot = 0.826;                  // Ductility Condition

                            }
                            double Asbot = (Mbott * 1000 * 1000) / (fy * Jbot * d * 1000 * Brc);          // على المتر الطولي


                            if (Asbot < Asmin)
                            {
                                Asbot = Asmin;
                            }
                            double num1 = Math.Ceiling(Asbot / (3.1459 * 0.25 * fai1 * fai1));



                            //////////////////////
                            ///top RFT

                            double atop = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M3top * 1000 * 1000) / (0.45 * fcu * Brc * 1000 * d * d * 1000 * 1000))));
                            if (atop > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Ctop = 1.25 * atop;
                            double Jtop = (1 / 1.15) * (1 - 0.4 * (Ctop / (d * 1000)));
                            if (Jtop > 0.826)
                            {
                                Jtop = 0.826;                  // Ductility Condition

                            }
                            double Astop = (M3top * 1000 * 1000) / (fy * Jtop * d * 1000 * Brc);
                            if (Astop < Asmin)
                            {
                                Astop = Asmin;
                            }
                            double num2 = Math.Ceiling(Astop / (3.1459 * 0.25 * fai2 * fai2));

                            ////
                            // TranVerse Dir.

                            //Footing F1
                            double ahb1 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M1act * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                            if (ahb1 > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Chb1 = 1.25 * ahb1;
                            double Jhb1 = (1 / 1.15) * (1 - 0.4 * (Chb1 / (d * 1000)));
                            if (Jhb1 > 0.826)
                            {
                                Jhb1 = 0.826;
                            }
                            double Ashb1 = (M1act * 1000 * 1000) / (fy * Jhb1 * d * 1000);
                            if (Ashb1 < Asmin)
                            {
                                Ashb1 = Asmin;
                            }
                            double num3 = Math.Ceiling(Ashb1 / (3.1459 * 0.25 * fai3 * fai3));

                            ////

                            //Footing F2
                            double ahb2 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M2act * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                            if (ahb2 > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Chb2 = 1.25 * ahb2;
                            double Jhb2 = (1 / 1.15) * (1 - 0.4 * (Chb2 / (d * 1000)));
                            if (Jhb2 > 0.826)
                            {
                                Jhb2 = 0.826;
                            }
                            double Ashb2 = (M2act * 1000 * 1000) / (fy * Jhb2 * d * 1000);
                            if (Ashb2 < Asmin)
                            {
                                Ashb2 = Asmin;
                            }
                            double num4 = Math.Ceiling(Ashb2 / (3.1459 * 0.25 * fai4 * fai4));


                            ///////
                            // Printing 
                            txttpcfinal.Text = txttpc.Text;

                            txtLpc.Text = (0.05 * Math.Ceiling(Lpc/0.05)).ToString();
                            txtB1pc.Text = (0.05 * Math.Ceiling(Bpc/0.05)).ToString();

                            txttrcfinal.Text = txttrc.Text;

                            txtLrc.Text = (0.05 * Math.Ceiling(Lrc/0.05)).ToString();
                            txtB1rc.Text = (0.05 * Math.Ceiling(Brc/0.05)).ToString();

                            txtnumbott.Text = num1.ToString();
                            txtnumtop.Text = num2.ToString();
                            txtnumhb1.Text = num3.ToString();
                            txtnumhb2.Text = num4.ToString();

                        }
                    }



                    //////
                    if (txttype.Text == "Case_2")
                    {
                        /// Edge Footing with Rectangle type  P1<p2                
                        if (Pw1 > Pw2)
                        {
                            MessageBox.Show("Look Carefully at the Photo .. P1 < P2", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtp1.Focus();
                            txtp1.SelectAll();
                            return;
                        }
                        else
                        {
                            Rw = Pw1 + Pw2;
                            X = (Pw1 * S) / Rw;
                            Lrc = 2 * ((S - X) + (b1 / 2));
                            Lpc = Lrc;
                            if (tpc >= 0.2)
                            {
                                Bpc = Rw / (qall * Lpc);
                                Brc = Bpc - 2 * tpc;
                            }
                            else
                            {
                                Brc = Rw / (qall * Lrc);
                                Bpc = Brc + 2 * tpc;
                            }
                            /// Ultimate Stage 
                            // Ultimate Stage
                            // Get Moments
                            double Ru = 1.5 * Rw;
                            double Pu1 = 1.5 * Pw1;
                            double Pu2 = 1.5 * Pw2;

                            double Fact = (Ru) / (Lrc * Brc);
                            double Wu = Ru / Lrc;

                            /// Get Moments 
                            double x0 = Pu1 / Wu;
                            double x1 = (Lrc / 2) - X - (b2 / 2);
                            double x3 = x0 - (b1 / 2);
                            double Mtop = Pu1 * x3 - Wu * 0.5 * x0 * x0;
                            double Mbott = Wu * 0.5 * x1 * x1;

                            /// Tranverse Dir.   as Hidden Beams
                            /// 
                            // Footing F1
                            double L1 = b1 + d;
                            double F1act = Pu1 / (L1 * Brc);
                            double Z1 = (Brc - a1) / 2;
                            double M1act = F1act * 0.5 * Z1 * Z1;          //kN.m/m'

                            // Footing f2
                            double L2 = b2 + 2 * d;
                            double F2act = Pu2 / (L2 * Brc);
                            double Z2 = (Brc - a2) / 2;
                            double M2act = F2act * 0.5 * Z2 * Z2;

                            // Get Forces for Shear 
                            double x2 = (Lrc / 2) - X + (b2 / 2);
                            double x4 = b1;
                            double Q1 = Math.Abs(Pu1 - Wu * x4);
                            double Q2 = Math.Abs(Pu2 - Wu * x2);
                            double Q3 = Wu * x1;
                            double Qmax1 = Math.Max(Q1, Q2);
                            double Qmax = Math.Max(Qmax1, Q3);
                            double Qcr = Qmax - Wu * (d / 2);
                            double qact = (Qcr * 1000) / (d * 1000 * Brc * 1000);
                            double qsh = 0.16 * Math.Sqrt(fcu / 1.5);
                            if (qact > qsh)
                            {
                                MessageBox.Show("UnSafe against Shear .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                lblshear.Text = "UnSafe";
                                return;
                            }
                            lblshear.Text = "Safe";





                            //// Check Punching 
                            //// Check Punch.
                            // column 1 
                            double Q1p = Pu1 - Fact * (a1 + d) * (b1 + (d / 2));
                            double Ap1 = d * (a1 + d + 2 * (b1 + (d / 2)));
                            double qp1 = (Q1p / (1000 * Ap1));
                            // Column 2 
                            double Q2p = Pu2 - Fact * (a2 + d) * (b2 + d);
                            double Ap2 = 2 * d * (a2 + b2 + 2 * d);
                            double qp2 = (Q2p / (1000 * Ap2));

                            double qp = Math.Max(qp1, qp2);

                            double qpaLL1 = 1.60;
                            double qpaLL2 = 0.316 * Math.Sqrt(fcu / 1.5);
                            double qpaLL3 = 0.316 * (0.5 + (a1 / b1)) * Math.Sqrt(fcu / 1.5);
                            double qpmin1 = Math.Min(qpaLL1, qpaLL2);
                            double qpun = Math.Min(qpmin1, qpaLL3);
                            if (qp > qpun)
                            {
                                MessageBox.Show("UnSafe against Punching .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                lblpunch.Text = "UnSafe";
                                return;
                            }

                            lblpunch.Text = "Safe";


                            //// RFT

                            double Asmin1 = 1.5 * d * 1000;
                            double Asmin2 = 565;                              // 5 fai 12 /m'  =565mm2
                            double Asmin = Math.Max(Asmin1, Asmin2);

                            ///// For Long. Dir.
                            /// Bootom RFT
                            double amax = (320 * d * 1000) / (600 + 0.87 * fy);
                            double abot = d * 1000 * (1 - Math.Sqrt(1 - ((2 * Mbott * 1000 * 1000) / (0.45 * fcu * Brc * 1000 * d * d * 1000 * 1000))));

                            if (abot > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Cbot = 1.25 * abot;
                            double Jbot = (1 / 1.15) * (1 - 0.4 * (Cbot / (d * 1000)));
                            if (Jbot > 0.826)
                            {
                                Jbot = 0.826;                  // Ductility Condition

                            }
                            double Asbot = (Mbott * 1000 * 1000) / (fy * Jbot * d * 1000 * Brc);          // على المتر الطولي


                            if (Asbot < Asmin)
                            {
                                Asbot = Asmin;
                            }
                            double num1 = Math.Ceiling(Asbot / (3.1459 * 0.25 * fai1 * fai1));



                            //////////////////////
                            ///top RFT

                            double atop = d * 1000 * (1 - Math.Sqrt(1 - ((2 * Mtop * 1000 * 1000) / (0.45 * fcu * Brc * 1000 * d * d * 1000 * 1000))));
                            if (atop > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Ctop = 1.25 * atop;
                            double Jtop = (1 / 1.15) * (1 - 0.4 * (Ctop / (d * 1000)));
                            if (Jtop > 0.826)
                            {
                                Jtop = 0.826;                  // Ductility Condition

                            }
                            double Astop = (Mtop * 1000 * 1000) / (fy * Jtop * d * 1000 * Brc);
                            if (Astop < Asmin)
                            {
                                Astop = Asmin;
                            }
                            double num2 = Math.Ceiling(Astop / (3.1459 * 0.25 * fai2 * fai2));

                            ////
                            // TranVerse Dir.

                            //Footing F1
                            double ahb1 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M1act * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                            if (ahb1 > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Chb1 = 1.25 * ahb1;
                            double Jhb1 = (1 / 1.15) * (1 - 0.4 * (Chb1 / (d * 1000)));
                            if (Jhb1 > 0.826)
                            {
                                Jhb1 = 0.826;
                            }
                            double Ashb1 = (M1act * 1000 * 1000) / (fy * Jhb1 * d * 1000);
                            if (Ashb1 < Asmin)
                            {
                                Ashb1 = Asmin;
                            }
                            double num3 = Math.Ceiling(Ashb1 / (3.1459 * 0.25 * fai3 * fai3));

                            ////

                            //Footing F2
                            double ahb2 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M2act * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                            if (ahb2 > amax)
                            {
                                MessageBox.Show("UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txttrc.Focus();
                                txttrc.SelectAll();
                                return;
                            }
                            double Chb2 = 1.25 * ahb2;
                            double Jhb2 = (1 / 1.15) * (1 - 0.4 * (Chb2 / (d * 1000)));
                            if (Jhb2 > 0.826)
                            {
                                Jhb2 = 0.826;
                            }
                            double Ashb2 = (M2act * 1000 * 1000) / (fy * Jhb2 * d * 1000);
                            if (Ashb2 < Asmin)
                            {
                                Ashb2 = Asmin;
                            }
                            double num4 = Math.Ceiling(Ashb2 / (3.1459 * 0.25 * fai4 * fai4));


                            ///////
                            // Printing

                            //PC 
                            txttpcfinal.Text = txttpc.Text;

                            txtLpc.Text = (0.05 * Math.Ceiling(Lpc/0.05)).ToString();
                            txtB1pc.Text = (0.05 * Math.Ceiling(Bpc/0.05)).ToString();

                            //RC
                            txttrcfinal.Text = txttrc.Text;

                            txtLrc.Text = (0.05 * Math.Ceiling(Lrc/0.05)).ToString();
                            txtB1rc.Text = (0.05 * Math.Ceiling(Brc/0.05)).ToString();

                            //RFT
                            txtnumbott.Text = num1.ToString();
                            txtnumtop.Text = num2.ToString();
                            txtnumhb1.Text = num3.ToString();
                            txtnumhb2.Text = num4.ToString();
                            ////
                        }
                    }

                    ////////////////////////////////////
                    // Tabel
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(dr == DialogResult.OK)
                    {
                        string serial = "";
                        string PP1 = txtp1.Text;
                        string Col1 = txtb1.Text + " * " + txta1.Text;
                        string PP2 = txtp2.Text;
                        string Col2 = txtb2.Text + " * " + txta2.Text;
                        string PC = txtLpc.Text + " * " + txtB1pc.Text + " * " + txttpcfinal.Text;
                        string RC = txtLrc.Text + " * " + txtB1rc.Text + " * " + txttrcfinal.Text;
                        string Shear = lblshear.Text;
                        string Punch = lblpunch.Text;
                        string RFTbott = txtnumbott.Text + " T " + txtfai1.Text;
                        string RFTtop = txtnumtop.Text + " T " + txtfai2.Text;
                        string RFTHB1 = txtnumhb1.Text + " T " + txtfai3.Text;
                        string RFTHB2 = txtnumhb2.Text + " T " + txtfai4.Text;
                        object[] data = { serial, PP1, Col1, PP2, Col2, PC, RC, Shear, Punch, RFTbott, RFTtop, RFTHB1, RFTHB2 };
                        DataGridView1.Rows.Add(data);
                        return;
                        
                    }
                    else
                    {
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
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Combined Footing (Screen)";
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

        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtLpc.Clear();
            txtB1pc.Clear();
            txttpcfinal.Clear();

            txtLrc.Clear();
            txtB1rc.Clear();
            txttrcfinal.Clear();

            lblpunch.Text = "";
            lblshear.Text = "";

            txtnumbott.Clear();
            txtnumtop.Clear();
            txtnumhb1.Clear();
            txtnumhb2.Clear();

        }
    }
}
