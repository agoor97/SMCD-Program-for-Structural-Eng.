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
    public partial class strap : Form
    {
        public strap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtfcu.Text.Trim()=="" || txtfy.Text.Trim()=="" ||txtqall.Text.Trim()==""||txtp1.Text.Trim()==""
                    ||txtp2.Text.Trim()==""||txta1.Text.Trim()==""||txta2.Text.Trim()==""||txtb1.Text.Trim()==""
                    ||txtb2.Text.Trim()==""||txttpc.Text.Trim()==""||txttrc1.Text.Trim()==""||txttrc2.Text.Trim()==""
                    ||txtbstrap.Text.Trim()==""||txttstrap.Text.Trim()==""||txtfai1.Text.Trim()==""||txtfai2.Text.Trim()==""
                    ||txtfai3.Text.Trim()==""||txtfai4.Text.Trim()==""||txtfai5.Text.Trim()==""||txts.Text.Trim()=="")
                {
                    MessageBox.Show("Missing Data ..","Information",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);                   
                    return;
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double b1 = double.Parse(txtb1.Text);
                    double b2 = double.Parse(txtb2.Text);
                    double a1 = double.Parse(txta1.Text);
                    double a2 = double.Parse(txta2.Text);
                    double qall = double.Parse(txtqall.Text);
                    double Pw1 = double.Parse(txtp1.Text);
                    double Pw2 = double.Parse(txtp2.Text);
                    double S = double.Parse(txts.Text);                  
                    double tpc = double.Parse(txttpc.Text);
                    double trc1 = double.Parse(txttrc1.Text);
                    double trc2 = double.Parse(txttrc2.Text);
                    double b = double.Parse(txtbstrap.Text);
                    double t = double.Parse(txttstrap.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);
                    int fai3 = int.Parse(txtfai3.Text);
                    int fai4 = int.Parse(txtfai4.Text);
                    int fai5 = int.Parse(txtfai5.Text);

                    //
                    double Lpc1;
                    double Bpc1;
                    double Lrc1;
                    double Brc1;

                    double Lpc2;
                    double Bpc2;
                    double Lrc2;
                    double Brc2;
                    //

                    double ecc = 0.1 + 0.2 * S;
                    double R1 = (Pw1 * S) / (S - ecc);
                    double R2 = Pw1 + Pw2 - R1;

                    // Firstly
                    if (tpc >= 0.2)
                    {
                        // Dims of footing f1
                        Lrc1 = 2 * (ecc + (b1 / 2));
                        Lpc1 = Lrc1;
                        Bpc1 = R1 / (qall * Lpc1);
                        Brc1 = Bpc1 - 2 * tpc;   

                        // dims of footing f2 
                        // Bpc^2+(b2-a2)*Bpc-Apc=0.0 معادلة تربيعية 
                        double Apc = R2 / qall;
                        double root = Math.Sqrt(((b2 - a2) * (b2 - a2)) + 4 * Apc);
                        double Bpc22 = ((-1 * (b2 - a2)) + root) / 2;
                        double Bpc222 = ((-1 * (b2 - a2)) - root) / 2;
                        Bpc2 = Math.Max(Bpc22, Bpc222);
                        Lpc2 = Apc / Bpc2;
                        Brc2 = Bpc2 - 2  * tpc;
                        Lrc2 = Lpc2 - 2  * tpc;

                        //Check validity of using strap beam
                        double x = S + (b1 / 2) - Lpc1 - (Lpc2 / 2);
                        double L = Math.Min((Lpc1 / 2), (Lpc2 / 2));
                        if (x < L)
                        {
                            MessageBox.Show("Can't be Designed as a Strap .. (X) not Satified the Condition .... Go to Combined ." ,"Information",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            return;
                        }

                    }

                    //////////////////////////////////////////////////
                    //////SECONDLY

                   else
                    {
                     
                        //// tpc < 0.2 m
                        // Dims of footing f1
                         Lrc1 = 2 * (ecc + (b1 / 2));
                         Lpc1 = Lrc1 +  tpc;           // الزيادة من ناحية واحدة
                         Brc1 = R1 / (qall * Lrc1);
                         Bpc1 = Brc1 + 2 * tpc;       // meters

                        // dims of footing f2 
                        // Bpc^2+(b2-a2)*Bpc-Apc=0.0 معادلة تربيعية 
                        double Arc = R2 / qall;
                        double root = Math.Sqrt(((b2 - a2) * (b2 - a2)) + 4 * Arc);
                        double Brc22 = ((-1 * (b2 - a2)) + root) / 2;
                        double Brc222 = ((-1 * (b2 - a2)) - root) / 2;
                        Brc2 = Math.Max(Brc22, Brc222);
                        Lrc2 = Arc / Brc2;
                        Bpc2 = Brc2 + 2 * tpc;
                        Lpc2 = Lrc2 + 2 * tpc;

                        //Check validity of using strap beam
                        double x = S + (b1 / 2) - Lrc1 - (Lrc2 / 2);
                        double L = Math.Min((Lrc1 / 2), (Lrc2 / 2));
                        if (x < L)
                        {
                            MessageBox.Show("Can't be Designed as a Strap .. (X) not Satified the Condition ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                       
                            return;
                        }
                    }


                    ////
                    //Ultimate Stage 

                    /////////////////////
                    //// Ultimate stage

                    double W1 = (R1 * 1.5) / Lrc1;
                    double W2 = (R2 * 1.5) / Lrc2;
                    // Get Moments for Strap beam
                    double x1 = (b1 / 2);
                    double M1bott = W1 * 0.5 * x1 * x1;
                    double x2 = Lrc2 / 2;
                    double M2bott = W2 * x2 * x2 * 0.5;
                    double x0 = (Pw1 * 1.5) / W1;
                    double x3 = x0 - x1;
                    double M3top = (Pw1 * 1.5 * x3) - W1 * 0.5 * x0 * x0;

                    
                    // get shear for strap beam 
                    double Qmax1 = Math.Abs(Pw1 * 1.5 - W1 * b1);
                    double d = t - 0.07;       // meters  Cover==70mm
                    double Qcr1 = Qmax1 - W1 * (d / 2);   // kN
                    double Qmax2 = Math.Abs(Pw2 * 1.5 - W2 * ((Lrc2 / 2) + (b2 / 2)));
                    double Qcr2 = Qmax2 - W2 * (d / 2);
                    double Qdesign = Math.Max(Qcr1, Qcr2);   // kN                                     
                    double qact = (Qdesign * 1000) / (b * 1000 * d * 1000);   //N/mm2
                    double qmin = 0.12 * Math.Sqrt(fcu / 1.5);
                    double qmax = 0.70 * Math.Sqrt(fcu / 1.5);
                    if (qact <= qmin)
                    {
                        // 5 fai 10 /m'    (4 Branches)
                        txtbranches.Text = "4";
                        txtfaistirr.Text = "10";
                        txtnumstirr.Text = "5";

                    }
                    else if (qact > qmax)
                    {
                        MessageBox.Show("UnSafe Shear for Strap Beam ... Increase Dimens. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtbstrap.Focus();
                        txtbstrap.SelectAll();
                        return;
                    }
                    else
                    {
                        double qsu = qact - qmin;
                        double stirr = (4 * 78.5 * 0.87 * 240) / (qsu * b * 1000);        // mm
                        double numst = Math.Ceiling(1000 / stirr);
                        double numfinal = Math.Max(numst, 5);
                        txtbranches.Text = "4";
                        txtfaistirr.Text = "10";
                        txtnumstirr.Text = numfinal.ToString();
                    }

                    //// design of strap against moment

                    // for bottom 1 
                    double amax = (320 * d * 1000) / (600 + 0.87 * fy);
                    double comp1 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M1bott * Math.Pow(10, 6)) / (0.45 * fcu * b * 1000 * d * d * 1000 * 1000))));
                    if (amax < comp1)
                    {
                        MessageBox.Show(" UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtbstrap.Focus();
                        txtbstrap.SelectAll();
                        return;
                    }
                    double c1 = 1.25 * comp1;
                    double j1 = (1 / 1.15) * (1 - 0.4 * (c1 / (d * 1000)));
                    if (j1 > 0.826)
                    {
                        j1 = 0.826;                  // Ductility Condition
                    }
                    double As1 = (M1bott * Math.Pow(10, 6)) / (fy * j1 * d * 1000);

                    // check AS min 
                    double Asmin1 = 0.225 * b * 1000 * d * 1000 * Math.Sqrt(fcu) / fy;             //mm2
                    double Asmin2 = 0.15 * b * 1000 * d * 1000 * 0.01;
                    double Asmin = Math.Max(Asmin1, Asmin2);

                    if (As1 < Asmin)
                    {
                        As1 = Asmin;
                    }
                    double numbott1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai3 * fai3));

                    ////////
                    // for bottom 2 
                    double comp2 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M2bott * Math.Pow(10, 6)) / (0.45 * fcu * b * 1000 * d * d * 1000 * 1000))));
                    if (amax < comp2)
                    {
                        MessageBox.Show(" UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtbstrap.Focus();
                        txtbstrap.SelectAll();
                        return;
                    }
                    double c2 = 1.25 * comp2;
                    double j2 = (1 / 1.15) * (1 - 0.4 * (c2 / (d * 1000)));
                    if (j2 > 0.826)
                    {
                        j2 = 0.826;
                    }

                    double As2 = (M2bott * Math.Pow(10, 6)) / (fy * j2 * d * 1000);

                    if (As2 < Asmin)
                    {
                        As2 = Asmin;
                    }
                    double numbott2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai4 * fai4));


                    // for top 
                    double comp3 = d * 1000 * (1 - Math.Sqrt(1 - ((2 * M3top * Math.Pow(10, 6)) / (0.45 * fcu * b * 1000 * d * d * 1000 * 1000))));
                    if (amax < comp3)
                    {
                        MessageBox.Show(" UnSafe Section against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtbstrap.Focus();
                        txtbstrap.SelectAll();
                        return;
                    }
                    double c3 = 1.25 * comp3;
                    double j3 = (1 / 1.15) * (1 - 0.4 * (c3 / (d * 1000)));
                    if (j3 > 0.826)
                    {
                        j3 = 0.826;
                    }

                    
                    double As3 = (M3top * Math.Pow(10, 6)) / (fy * j3 * d * 1000);

                    if (As3 < Asmin)
                    {
                        As3 = Asmin;
                    }
                    double numtop = Math.Ceiling(As3 / (3.1459 * 0.25 * fai5 * fai5));

                    /////////////////////////////////////////////////////////////

                    // design of footing f1
                    double fact1 = (R1 * 1.5) / (Lrc1 * Brc1);
                    double z1 = (Brc1 - a1) / 2;
                    double mfoot1 = fact1 * z1 * z1 * 0.5;
                    double d1 = trc1 - 0.07;

                    // check shear for footing f1 ..                                       
                    double L1 = z1 - (d1 / 2);
                    double qact1 = (fact1 * L1) / (d1 * 1000);       // N/mm2
                    double qallow = 0.16 * Math.Sqrt(fcu / 1.5);
                    if (qact1 > qallow)
                    {
                        MessageBox.Show(" UnSafe Shear for Footing (F1) .. Increase trc1 ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        lblshear1.Text = "UnSafe";
                        txttrc1.Focus();
                        txttrc1.SelectAll();
                        return;
                    }
                    lblshear1.Text = "Safe";

                    // get rft for f1 
                    double compf1 = d1 * 1000 * (1 - Math.Sqrt(1 - ((2 * mfoot1 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * d1 * d1 * 1000 * 1000))));

                    double cf1 = 1.25 * compf1;
                    double jf1 = (1 / 1.15) * (1 - 0.4 * (cf1 / (d1 * 1000)));
                    if (jf1 > 0.826)
                    {
                        jf1 = 0.826;
                    }
                    double Asf1 = (mfoot1 * Math.Pow(10, 6)) / (fy * jf1 * d1 * 1000);

                    // check min for footing
                    double Asminf1 = 1.5 * d1 * 1000;          // mm2
                    double Asmin22 = 565;                      // 5 fai 12 == 565 mm2

                    double Asminff1 = Math.Max(Asminf1, Asmin22);
                    double Asfoot1 = Math.Max(Asf1, Asminff1);
                    double numf1 = Math.Ceiling(Asfoot1 / (3.1459 * 0.25 * fai1 * fai1));




                    /////////
                    //// for footing f2
                    double fact2 = (R2 * 1.5) / (Lrc2 * Brc2);
                    double z2 = (Brc2 - a2) / 2;
                    double mfoot2 = fact2 * z2 * z2 * 0.5;
                    double d2 = trc2 - 0.07;

                    // check shear for footing f2                                          
                    double L2 = z2 - (d2 / 2);
                    double qact2 = (fact2 * L2) / (d2 * 1000);       // N/mm2        
                               
                    if (qact2 > qallow)
                    {
                        MessageBox.Show("UnSafe Shear for Footing (F2) ... Increase trc2 ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        lblshear2.Text = "UnSafe";
                        txttrc2.Focus();
                        txttrc2.SelectAll();
                        return;
                    }
                    lblshear2.Text = "Safe";

                    // get rft for f1 
                    double compf2 = d2 * 1000 * (1 - Math.Sqrt(1 - ((2 * mfoot2 * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * d2 * d2 * 1000 * 1000))));
                    double cf2 = 1.25 * compf2;
                    double jf2 = (1 / 1.15) * (1 - 0.4 * (cf2 / (d2 * 1000)));
                    if (jf2 > 0.826)
                    {
                        jf2 = 0.826;
                    }
                    double Asf2 = (mfoot2 * Math.Pow(10, 6)) / (fy * jf2 * d2 * 1000);
                    // check min for footing
                    double Asminf2 = 1.5 * d2 * 1000;
                  
                    double Asminff2 = Math.Max(Asminf2, 565);
                    double Asfoot2 = Math.Max(Asf2, Asminff2);
                    double numf2 = Math.Ceiling(Asfoot2 / (3.1459 * 0.25 * fai2 * fai2));

                    ///////



                    /// Printing ..... 
                    //F1

                    //rft
                    txtnumF1.Text = numf1.ToString();                   
                    //pc
                    txtLpc1.Text = (0.05 * Math.Ceiling(Lpc1/0.05)).ToString();
                    txtBpc1.Text = (0.05 * Math.Ceiling(Bpc1/0.05)).ToString();

                    txttpcfinal1.Text = txttpc.Text;
                    //rc
                    txtLrc1.Text = (0.05 * Math.Ceiling(Lrc1/0.05)).ToString();
                    txtBrc1.Text = (0.05 * Math.Ceiling(Brc1/0.05)).ToString();

                    txttrcfinal1.Text = txttrc1.Text;


                    //F2

                    //rft
                    txtnumF2.Text = numf2.ToString();                   
                    //pc
                    txtLpc2.Text = (0.05 * Math.Ceiling(Lpc2/0.05)).ToString();
                    txtBpc2.Text = (0.05 * Math.Ceiling(Bpc2/0.05)).ToString();

                    txttpcfinal2.Text = txttpc.Text;
                    //rc
                    txtLrc2.Text = (0.05 * Math.Ceiling(Lrc2/0.05)).ToString();
                    txtBrc2.Text = (0.05 * Math.Ceiling(Brc2/0.05)).ToString();

                    txttrcfinal2.Text = txttrc2.Text;


                    ///Strap
                    txtnumstrap1.Text = numbott1.ToString();
                    txtnumstrap2.Text = numbott2.ToString();
                    txtnumstrap3.Text = numtop.ToString();


                    ////////
                    ///////Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string PP1 = txtp1.Text;
                        string col1 = txtb1.Text + " * " + txta1.Text;
                        string pc1 = txtLpc1.Text + " * " + txtBpc1.Text + " * " + txttpcfinal1.Text;
                        string rc1 = txtLrc1.Text + " * " + txtBrc1.Text + " * " + txttrcfinal1.Text;
                        string RFT1 = txtnumF1.Text + " T " + txtfai1.Text;

                        //
                        string PP2 = txtp2.Text;
                        string col2 = txtb2.Text + " * " + txta2.Text;
                        string pc2 = txtLpc2.Text + " * " + txtBpc2.Text + " * " + txttpcfinal2.Text;
                        string rc2 = txtLrc2.Text + " * " + txtBrc2.Text + " * " + txttrcfinal2.Text;
                        string RFT2 = txtnumF2.Text + " T " + txtfai2.Text;

                        //
                        string strap = txtbstrap.Text + " * " + txttstrap.Text;
                        string Rftbot1 = txtnumstrap1.Text + " T " + txtfai3.Text;
                        string Rftbot2 = txtnumstrap2.Text + " T " + txtfai4.Text;
                        string Rfttop = txtnumstrap3.Text + " T " + txtfai5.Text;
                        string stirr = txtnumstirr.Text + " T " + txtfaistirr.Text + "/m' .. " + "( " + txtbranches.Text + " B )";

                        object[] data = { serial, PP1, col1, pc1, rc1, RFT1, PP2, col2, pc2, rc2, RFT2, strap, Rftbot1, Rftbot2, Rfttop, stirr };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Strap  (Screen)";
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
            txtLpc1.Clear();
            txtBpc1.Clear();
            txttpcfinal1.Clear();
            txtnumF1.Clear();

            /////

            txtLpc2.Clear();
            txtBpc2.Clear();
            txttpcfinal2.Clear();
            txtnumF2.Clear();


            ////
            txtLrc1.Clear();
            txtBrc1.Clear();
            txttrcfinal1.Clear();
            ////
            txtLrc2.Clear();
            txtBrc2.Clear();
            txttrcfinal2.Clear();


            lblshear1.Text = "";
            lblshear2.Text = "";

            txtnumstirr.Clear();
            txtnumstrap1.Clear();
            txtnumstrap2.Clear();
            txtnumstrap3.Clear();
            txtfaistirr.Clear();
            txtbranches.Clear();
        }
    }
}
