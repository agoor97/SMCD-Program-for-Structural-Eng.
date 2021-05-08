using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Configuration;
using System.IO;

namespace Design_Concrete
{
    public partial class RCsection : Form
    {
        public DialogResult OK { get; private set; }

        public RCsection()
        {
            InitializeComponent();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==false)
            {
                txtmtu.Enabled = false;
                txtt2.Enabled = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                groupBox11.Enabled = false;
                txtnotorsion.Enabled = false;
            }
            if (checkBox1.Checked ==true)
            {
                txtmtu.Enabled = true;
                txtt2.Enabled = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                groupBox11.Enabled = true;
                txtnotorsion.Enabled = true;
            }
        }

        private void RCsection_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            txtmtu.Enabled = false;
            txtt2.Enabled = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            groupBox11.Enabled = false;
            txtnotorsion.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == false)
                {
                    ///// Case No torsion 
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtfys.Text.Trim() == ""
                   || txtt.Text.Trim() == "" || txtc.Text.Trim() == "" || txtfai1.Text.Trim() == ""
                   || txtfai2.Text.Trim() == "" || txtm.Text.Trim() == "" || txtQ.Text.Trim() == ""
                   || txtb.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfcu.Focus();
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double fys = double.Parse(txtfys.Text);
                        double b = double.Parse(txtb.Text);
                        double t = double.Parse(txtt.Text);
                        double cover = double.Parse(txtc.Text);
                        double M = double.Parse(txtm.Text);
                        double Q = double.Parse(txtQ.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int fai2 = int.Parse(txtfai2.Text);


                        double d = t - cover;            //mm
                                                         //// for Recommended Thickness
                        double d1 = 3.5 * Math.Sqrt((M * Math.Pow(10, 6)) / (fcu * b));
                        double t1 = Math.Round((d1 + 50), 2);
                        if (t1 < 400)
                        {
                            t1 = 400;      //// min t for Beams
                        }
                        txtt1.Text = t1.ToString();
                        txttrecom.Text = t1.ToString();

                        //// Design against Moment
                        double amax = (320 * d) / (600 + 0.87 * fy);
                        double a = d * (1 - Math.Sqrt(1 - ((2 * M * Math.Pow(10, 6)) / (0.45 * fcu * b * d * d))));
                        if (a > amax)
                        {
                            MessageBox.Show("UnSafe section against Moment .. Increase Dim.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        double C = 1.25 * a;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)             // Ductility Condition 
                        {
                            J = 0.826;
                        }
                        double ASreq = (M * Math.Pow(10, 6)) / (fy * J * d);
                        //// Get ASmin 
                        double Asmin1 = 0.225 * b * d * Math.Sqrt(fcu) / fy;
                        double Asmin2 = 1.3 * ASreq;

                        if (ASreq < Asmin1)
                        {
                            double Asmin = Math.Min(Asmin1, Asmin2);
                            double Assteel = 0.15 * 0.01 * b * d;
                            double AsDesign = Math.Max(Assteel, Asmin);
                            double Ashunger = 0.15 * AsDesign;          ////// Top RFT
                            txtnum1.Text = (Math.Ceiling(AsDesign / (3.1459 * 0.25 * fai1 * fai1))).ToString();
                            txtnum2.Text = (Math.Ceiling(Ashunger / (3.1459 * 0.25 * fai2 * fai2))).ToString();
                        }
                        else
                        {
                            double AsDesign = ASreq;
                            double Ashunger = 0.15 * AsDesign;
                            txtnum1.Text = (Math.Ceiling(AsDesign / (3.1459 * 0.25 * fai1 * fai1))).ToString();
                            txtnum2.Text = (Math.Ceiling(Ashunger / (3.1459 * 0.25 * fai2 * fai2))).ToString();
                        }

                        ////// agaianst only shear
                        //// let assume Cracked Section
                        double qcumin = 0.12 * Math.Sqrt(fcu / 1.5);
                        double qcumax = 0.70 * Math.Sqrt(fcu / 1.5);
                        double qu = (Q * 1000) / (b * d);
                        if (qu <= qcumin)
                        {
                            //// safe min. st.

                            txtbranch.Text = "2";
                            txtnumstiout.Text = "5";
                            txtfaistirrout.Text = "8";
                            txtfaistirrinn.Text = "--";
                            txtnumstiinn.Text = "--";
                        }
                        else if (qu > qcumax)
                        {
                            MessageBox.Show("UnSafe Section for Shear .. Increase Dimensions .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtb.Focus();
                            txtb.SelectAll();
                            return;
                        }
                        else
                        {
                            double qsu = qu - qcumin;
                            double S = 2 * 50.3 * 0.87 * fys / (qsu * b);
                            if ((S >= 100 && S <= 200) || S > 200)
                            {
                                txtfaistirrout.Text = "8";
                                txtbranch.Text = "2";
                                txtfaistirrinn.Text = "--";
                                txtnumstiinn.Text = "--";
                                double numst = Math.Ceiling(1000 / S);
                                double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                txtnumstiout.Text = numfinal.ToString();

                            }
                            else
                            {
                                S = 2 * 78.5 * 0.87 * fys / (qsu * b);
                                if (S >= 100 && S <= 200)
                                {
                                    txtfaistirrout.Text = "10";
                                    txtbranch.Text = "2";
                                    txtfaistirrinn.Text = "--";
                                    txtnumstiinn.Text = "--";
                                    double numst = Math.Ceiling(1000 / S);
                                    double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                    txtnumstiout.Text = numfinal.ToString();
                                }
                                else
                                {
                                    S = 4 * 50.3 * 0.87 * fys / (qsu * b);
                                    if (S >= 100 && S <= 200)
                                    {
                                        txtfaistirrout.Text = "8";
                                        txtfaistirrinn.Text = "8";
                                        txtbranch.Text = "4";
                                        double numst = Math.Ceiling(1000 / S);
                                        double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                        txtnumstiout.Text = numfinal.ToString();
                                        txtnumstiinn.Text = numfinal.ToString();
                                    }
                                    else
                                    {
                                        S = 4 * 78.5 * 0.87 * fys / (qsu * b);
                                        if (S >= 100 && S <= 200)
                                        {
                                            txtfaistirrout.Text = "10";
                                            txtfaistirrinn.Text = "10";
                                            txtbranch.Text = "4";
                                            double numst = Math.Ceiling(1000 / S);
                                            double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                            txtnumstiout.Text = numfinal.ToString();
                                            txtnumstiinn.Text = numfinal.ToString();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Shear Design Not Suitable for Four Cases (100 <= S <= 200 mm) .. you can Check Manual to be Persuaded ,,, try diffrent Criteria !! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            txtt.Focus();
                                            txtt.SelectAll();
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                        /////////////////////////////////////////////////////
                        //for Table.
                        DialogResult dr;
                        dr = MessageBox.Show(" Add to the Table ? ", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string bb = txtb.Text;
                            string dd = (t - cover).ToString();
                            string Mu = txtm.Text;
                            string Qu = txtQ.Text;
                            string Mtu = "----";
                            string ASbott = txtnum1.Text + " T " + txtfai1.Text;
                            string ASTop = txtnum2.Text + " T " + txtfai2.Text;
                            string ASside = "----";
                            string stirrout = txtnumstiout.Text + " T " + txtfaistirrout.Text;
                            string stirrin = txtnumstiinn.Text + " T " + txtfaistirrinn.Text;
                            string branch = txtbranch.Text;
                            object[] data = { serial, bb, dd, Mu, Qu, Mtu, ASbott, ASTop, ASside, stirrout, stirrin , branch };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }




                    }
                    /////////////////////////////////////////////////finish case of no torsion
                }






                ///////////////////////////////////////////////////////////////////////////////////////
                ///// Case with torsion 

                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtfys.Text.Trim() == ""
               || txtt.Text.Trim() == "" || txtc.Text.Trim() == "" || txtfai1.Text.Trim() == ""
               || txtfai2.Text.Trim() == "" || txtfai3.Text.Trim() == "" || txtm.Text.Trim() == ""
               || txtQ.Text.Trim() == "" || txtb.Text.Trim() == "" || txtmtu.Text.Trim() == ""
               || txtnotorsion.Text.Trim() == "")

                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtfcu.Focus();
                    return;
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double fys = double.Parse(txtfys.Text);
                    double b = double.Parse(txtb.Text);
                    double t = double.Parse(txtt.Text);
                    double cover = double.Parse(txtc.Text);
                    double Mu = double.Parse(txtm.Text);
                    double Mtu = double.Parse(txtmtu.Text);

                    double Q = double.Parse(txtQ.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);
                    int fai3 = int.Parse(txtfai3.Text);
                    int No = int.Parse(txtnotorsion.Text);

                    double d = t - cover;            //mm
                                                     //// for Recommended Thickness
                    double d1 = 3.5 * Math.Sqrt((Mu * Math.Pow(10, 6)) / (fcu * b));
                    double t1 = Math.Round((d1 + 50), 2);
                    double t2 = Math.Round(((3 * Mtu * Math.Pow(10, 6)) / (1.6 * b * b)), 2);
                    if (t1 < 400)
                    {
                        t1 = 400;      //// min t for Beams
                    }
                    if (t2 < 400)
                    {
                        t2 = 400;      //// min t for Beams
                    }
                    txtt1.Text = t1.ToString();
                    txtt2.Text = t2.ToString();
                    txttrecom.Text = Math.Max(t1, t2).ToString();

                    //// Design against Moment
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double a = d * (1 - Math.Sqrt(1 - ((2 * Mu * Math.Pow(10, 6)) / (0.45 * fcu * b * d * d))));
                    if (a > amax)
                    {
                        MessageBox.Show("UnSafe section against Moment .. Increase Dim.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }
                    double C = 1.25 * a;
                    double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                    if (J > 0.826)             // Ductility Condition 
                    {
                        J = 0.826;
                    }
                    double ASreq = (Mu * Math.Pow(10, 6)) / (fy * J * d);
                    //// Get ASmin 
                    double Asmin1 = 0.225 * b * d * Math.Sqrt(fcu) / fy;
                    double Asmin2 = 1.3 * ASreq;
                    double ASdesign;
                    double AShunger;
                    if (ASreq < Asmin1)
                    {
                        double Asmin = Math.Min(Asmin1, Asmin2);
                        double Assteel = 0.15 * 0.01 * b * d;      /// for hight steel
                        ASdesign = Math.Max(Assteel, Asmin);   /// لسا من غير تورشن 
                        AShunger = 0.15 * ASdesign;              ////// Top RFT                    
                    }
                    else
                    {
                        ASdesign = ASreq;
                        AShunger = 0.15 * ASdesign;
                    }


                    //// let assume Cracked Section
                    double qcumin = 0.12 * Math.Sqrt(fcu / 1.5);
                    double qcumax = 0.70 * Math.Sqrt(fcu / 1.5);
                    double qtumin = 0.06 * Math.Sqrt(fcu / 1.5);
                    double qu = (Q * 1000) / (b * d);
                    double x1 = b - 80;
                    double y1 = t - 80;
                    double A0h = x1 * y1;
                    double A0 = 0.85 * A0h;
                    double Ph = 2 * (x1 + y1);
                    double te = A0h / Ph;
                    double qtu = (Mtu * Math.Pow(10, 6)) / (2 * A0 * te);
                    double Check = Math.Sqrt((qu * qu) + (qtu * qtu));
                    if (Check > qcumax)
                    {
                        MessageBox.Show("UnSafe Section for Shear Stresses .. try to Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }
                    else
                    {
                        ////// design against shaer && torsion
                        ///// case 1 

                        if (qu <= qcumin && qtu <= qtumin)
                        {
                            txtnotorsion.Enabled = false;
                            groupBox11.Enabled = false;
                            txtfaistirrinn.Text = "--";
                            txtnumstiinn.Text = "--";
                            txtbranch.Text = "2";
                            txtfaistirrout.Text = "8";
                            txtnumstiout.Text = "5";

                            double num1 = Math.Ceiling((ASdesign / (3.1459 * 0.25 * fai1 * fai1)));
                            double num2 = Math.Ceiling((AShunger / (3.1459 * 0.25 * fai2 * fai2)));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                        }
                        ////// case 2 
                        if (qu > qcumin && qtu <= qtumin)
                        {
                            txtnotorsion.Enabled = false;
                            groupBox11.Enabled = false;
                            ///// design against shear
                            double qsu = qu - qcumin;
                            double S = 2 * 50.3 * 0.87 * fys / (qsu * b);
                            if ((S >= 100 && S <= 200) || S > 200)
                            {
                                txtfaistirrinn.Text = "--";
                                txtnumstiinn.Text = "--";
                                txtbranch.Text = "2";
                                txtfaistirrout.Text = "8";
                                double numst = Math.Ceiling(1000 / S);
                                double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                txtnumstiout.Text = numfinal.ToString();
                            }
                            else
                            {
                                S = 2 * 78.5 * 0.87 * fys / (qsu * b);
                                if (S >= 100 && S <= 200)
                                {
                                    txtfaistirrinn.Text = "--";
                                    txtnumstiinn.Text = "--";
                                    txtbranch.Text = "2";
                                    txtfaistirrout.Text = "10";
                                    double numst = Math.Ceiling(1000 / S);
                                    double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                    txtnumstiout.Text = numfinal.ToString();
                                }
                                else
                                {
                                    S = 4 * 50.3 * 0.87 * fys / (qsu * b);
                                    if (S >= 100 && S <= 200)
                                    {
                                        txtbranch.Text = "4";
                                        txtfaistirrout.Text = "8";
                                        txtfaistirrinn.Text = "8";
                                        double numst = Math.Ceiling(1000 / S);
                                        double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                        txtnumstiout.Text = numfinal.ToString();
                                        txtnumstiinn.Text = numfinal.ToString();
                                    }
                                    else
                                    {
                                        S = 4 * 78.5 * 0.87 * fys / (qsu * b);
                                        if (S >= 100 && S <= 200)
                                        {
                                            txtbranch.Text = "4";
                                            txtfaistirrout.Text = "10";
                                            txtfaistirrinn.Text = "10";
                                            double numst = Math.Ceiling(1000 / S);
                                            double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                            txtnumstiout.Text = numfinal.ToString();
                                            txtnumstiinn.Text = numfinal.ToString();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Shear Design Not Suitable for Four Cases (100 <= S <= 200 mm) .. you can Check Manual to be Persuaded ,,, try diffrent Criteria !! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            txtt.Focus();
                                            txtt.SelectAll();
                                            return;
                                        }
                                    }
                                }
                            }

                            double num1 = Math.Ceiling((ASdesign / (3.1459 * 0.25 * fai1 * fai1)));
                            double num2 = Math.Ceiling((AShunger / (3.1459 * 0.25 * fai2 * fai2)));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                        }

                        //// case 3 
                        if (qu <= qcumin && qtu > qtumin)
                        {
                            groupBox11.Enabled = true;
                            txtnotorsion.Enabled = false;
                            ///// Design against Torsion
                            ////try Astr = fai 8 ==50.3 mm2
                            double St = (1.7 * A0h * 0.87 * fys * 50.3) / (Mtu * Math.Pow(10, 6));
                            if ((St >= 100 && St <= 200) || St > 200)
                            {
                                txtbranch.Text = "2";
                                txtfaistirrout.Text = "8";
                                txtfaistirrinn.Text = "--";
                                txtnumstiinn.Text = "--";
                                double numst = Math.Ceiling(1000 / St);
                                double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                txtnumstiout.Text = numfinal.ToString();
                            }
                            else if (St < 100)
                            {
                                //// we need to increase fai
                                /// try fai = 10 mm
                                St = (1.7 * A0h * 0.87 * fys * 78.5) / (Mtu * Math.Pow(10, 6));
                                if (St >= 100 && St <= 200)
                                {
                                    txtfaistirrinn.Text = "--";
                                    txtnumstiinn.Text = "--";
                                    txtbranch.Text = "2";
                                    txtfaistirrout.Text = "10";
                                    double numst = Math.Ceiling(1000 / St);
                                    double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                    txtnumstiout.Text = numfinal.ToString();
                                }
                                else
                                {
                                    St = (1.7 * A0h * 0.87 * fys * 113) / (Mtu * Math.Pow(10, 6));
                                    if (St >= 100 && St <= 200)
                                    {
                                        txtfaistirrinn.Text = "--";
                                        txtnumstiinn.Text = "--";
                                        txtbranch.Text = "2";
                                        txtfaistirrout.Text = "12";
                                        double numst = Math.Ceiling(1000 / St);
                                        double numfinal = Math.Max(numst, 5);     //// لا يقل عن 5
                                        txtnumstiout.Text = numfinal.ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Torsion Design Not Suitable for three Cases (100 <= S <= 200 mm) .. you can Check Manual to be Persuaded ,,, try diffrent Criteria !! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        txtt.Focus();
                                        txtt.SelectAll();
                                        return;
                                    }
                                }

                            }
                            ////// for Longitudinal Bars
                            double newSt = 1000 / (double.Parse(txtnumstiout.Text));
                            double fai = (double.Parse(txtfaistirrout.Text));
                            double Astrnew = 0.25 * 3.1459 * fai * fai;
                            double ASL = Astrnew * (Ph / newSt) * (fys / fy);
                            double ASLeach = 0.25 * ASL;
                            double ASbottom = ASdesign + ASLeach;
                            double AStop = AShunger + ASLeach;
                            double num1 = Math.Ceiling(ASbottom / (0.25 * 3.1459 * fai1 * fai1));
                            double num2 = Math.Ceiling(AStop / (0.25 * 3.1459 * fai2 * fai2));
                            double num3 = Math.Ceiling(ASLeach / (0.25 * 3.1459 * fai3 * fai3));
                            ///// ألمسافة بين الاسياخ لا تزيد عن 300 مم
                            double dist = (t - cover - 50) / 300;
                            double numdist = Math.Floor(dist);
                            double numside = Math.Max(num3, numdist);
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            txtnum3.Text = numside.ToString();

                        }
                        /////case4 4 
                        if (qu > qcumin && qtu > qtumin)
                        {
                            groupBox11.Enabled = true;
                            txtnotorsion.Enabled = true;
                            //// against shear and torsion
                            /// Assume n=2
                            /// firstly recommended to be No/m' = 7.0
                            double qsu = qu - qcumin;
                            double St = 1000 / No;
                            double Ss = 1000 / No;
                            double Astr = St * Mtu * Math.Pow(10, 6) / (1.7 * A0h * 0.87 * fys);
                            double Ast = qsu * b * Ss / (2 * 0.87 * fys);
                            double Asouter = Astr + Ast;
                            if (Asouter <= 113 && Asouter > 78.5)
                            {
                                txtbranch.Text = "2";
                                txtfaistirrinn.Text = "--";
                                txtnumstiout.Text = txtnotorsion.Text;
                                txtnumstiinn.Text = "--";
                                txtfaistirrout.Text = "12";
                            }
                            else if (Asouter >= 50.3 && Asouter < 78.5)
                            {
                                txtbranch.Text = "2";
                                txtfaistirrinn.Text = "--";
                                txtnumstiout.Text = txtnotorsion.Text;
                                txtnumstiinn.Text = "--";
                                txtfaistirrout.Text = "10";

                            }
                            else if (Asouter < 50.3)
                            {
                                txtbranch.Text = "2";
                                txtfaistirrinn.Text = "--";
                                txtnumstiout.Text = txtnotorsion.Text;
                                txtnumstiinn.Text = "--";
                                txtfaistirrout.Text = "8";
                            }

                            ////// use n=4
                            else if (Asouter > 113)
                            {
                                double Astnew = Ast / 2;
                                Asouter = Astr + Astnew;  //inner
                                if (Asouter <= 113 && Asouter > 78.5)
                                {
                                   
                                    txtbranch.Text = "4";                              
                                    txtnumstiout.Text = txtnotorsion.Text;                                   
                                    txtfaistirrout.Text = "12";

                                }
                                else if (Asouter >= 50.3 && Asouter < 78.5)
                                {
                                   
                                    txtbranch.Text = "4";
                                    txtnumstiout.Text = txtnotorsion.Text;                                    
                                    txtfaistirrout.Text = "10";
                                }
                                else if (Asouter < 50.3)
                                {
                                   
                                    txtbranch.Text = "4";
                                    txtnumstiout.Text = txtnotorsion.Text;                                   
                                    txtfaistirrout.Text = "8";
                                }
                                else if (Asouter > 113)
                                {
                                    MessageBox.Show("try to Increase Dimen. or Increse No. /m' ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtnotorsion.Focus();
                                    return;
                                }

                                ///
                                
                                if (Astnew <= 113 && Astnew > 78.5)
                                {
                                    txtfaistirrinn.Text = "12";
                                    txtnumstiinn.Text = txtnotorsion.Text;

                                }
                                else if (Astnew >= 50.3 && Astnew < 78.5)
                                {
                                    txtfaistirrinn.Text = "10";
                                    txtnumstiinn.Text = txtnotorsion.Text;
                                }
                                else if (Astnew < 50.3)
                                {
                                    txtfaistirrinn.Text = "8";
                                    txtnumstiinn.Text = txtnotorsion.Text;
                                }
                                else
                                {
                                    
                                    MessageBox.Show("try to Increase Dimen. or Increse No. /m' ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtnotorsion.Focus();
                                    return;
                                }

                            }
                            else
                            {
                                MessageBox.Show("try to Increase Dimen. or Increse No. /m' ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtnotorsion.Focus();
                                return;
                            }
                            ////// for Longitudinal Bars
                            double newSt = 1000 / (double.Parse(txtnumstiout.Text));
                            double fai = (double.Parse(txtfaistirrout.Text));
                            double Astrnew = 0.25 * 3.1459 * fai * fai;
                            double ASL = Astrnew * (Ph / newSt) * (fys / fy);
                            double ASLeach = 0.25 * ASL;
                            double ASbottom = ASdesign + ASLeach;
                            double AStop = AShunger + ASLeach;
                            double num1 = Math.Ceiling(ASbottom / (0.25 * 3.1459 * fai1 * fai1));
                            double num2 = Math.Ceiling(AStop / (0.25 * 3.1459 * fai2 * fai2));
                            double num3 = Math.Ceiling(ASLeach / (0.25 * 3.1459 * fai3 * fai3));
                            ///// ألمسافة بين الاسياخ لا تزيد عن 300 مم
                            double dist = (t - cover - 50) / 300;
                            double numdist = Math.Floor(dist);
                            double numside = Math.Max(num3, numdist);
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            txtnum3.Text = numside.ToString();


                        }

                    }

                    ///////////////////////////////////////////////////
                    //Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string dd = (t - cover).ToString();
                        string MM = txtm.Text;
                        string QQ = txtQ.Text;
                        string Mtor = txtmtu.Text;
                        string ASbott = txtnum1.Text + " T " + txtfai1.Text;
                        string ASTop = txtnum2.Text + " T " + txtfai2.Text;
                        string ASside = txtnum3.Text + " T " + txtfai3.Text;
                        string stirrout = txtnumstiout.Text + " T " + txtfaistirrout.Text;
                        string stirrin = txtnumstiinn.Text + " T " + txtfaistirrinn.Text;
                        string branch = txtbranch.Text;
                        object[] data = { serial, bb, dd, MM, QQ, Mtor, ASbott, ASTop, ASside, stirrout, stirrin , branch };
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

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Stirrups (ɸ8 or ɸ10 or ɸ12) all are designded according to (Fys) .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void button3_Click(object sender, EventArgs e)
        {
            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ECP 203-2018 ")
                {
                    Isopen = true;
                    f.Focus();
                    break;

                }

            }
            if (Isopen == false)
            {
                codercsection code = new codercsection();
                
                code.Show();


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtnum1.Clear();
            txtnum2.Clear();
            txtnum3.Clear();

            txtnumstiinn.Clear();
            txtnumstiout.Clear();
            txtfaistirrinn.Clear();
            txtfaistirrout.Clear();

            txtbranch.Clear();

            txtt1.Clear();
            txtt2.Clear();
            txttrecom.Clear();
        }
    }
}

