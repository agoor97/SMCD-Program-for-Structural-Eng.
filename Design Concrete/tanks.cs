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
    public partial class tanks : Form
    {
        public tanks()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txttype.Text == "Pure Tension")
            {
                txtMu.Enabled = false;
                txtPu.Enabled = false;
                txtTu.Enabled = true;
                groubRoo.Enabled = false;
            }
            if (txttype.Text == "Pure Moment")
            {
                txtMu.Enabled = true;
                txtPu.Enabled = false;
                txtTu.Enabled = false;
                groubRoo.Enabled = false;
            }
            if (txttype.Text == "Comp. + Moment")
            {
                txtMu.Enabled = true;
                txtPu.Enabled = true;
                txtTu.Enabled = false;
                groubRoo.Enabled = false;
            }
            if (txttype.Text == "Tens. + Moment")
            {
                txtMu.Enabled = true;
                txtTu.Enabled = true;
                txtPu.Enabled = false;
                groubRoo.Enabled = false;
            }
        }

        private void tanks_Load(object sender, EventArgs e)
        {
            txtMu.Enabled = true;
            txtTu.Enabled = true;
            txtPu.Enabled = false;
            groubRoo.Enabled = false;
            lblAs2.Visible = false;
            lblfai.Visible = false;
            txtfai2.Visible = false;
            txtnum2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtb.Text.Trim() == "" || txtt.Text.Trim() == ""
                    || txtfai.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double fcu = double.Parse(txtfcu.Text);
                double b = double.Parse(txtb.Text);
                double t = double.Parse(txtt.Text);
                double cover = double.Parse(txtc.Text);
                int fai = int.Parse(txtfai.Text);
                double fy = double.Parse(txtfy.Text);

                /// Get Bcr
                double Bcr;
                if (fy == 350)
                {
                    if (fai == 8)
                    {
                        Bcr = 1.0;
                    }
                    else if (fai == 10)
                    {
                        Bcr = 0.93;
                    }
                    else if (fai == 12)
                    {
                        Bcr = 0.85;
                    }
                    else if (fai == 18 || fai == 16)
                    {
                        Bcr = 0.75;
                    }
                    else if (fai == 22 || fai == 20)
                    {
                        Bcr = 0.65;
                    }
                    else if (fai == 25)
                    {
                        Bcr = 0.6;
                    }
                    else if (fai == 28)
                    {
                        Bcr = 0.56;
                    }
                    else
                    {
                        Bcr = 0.8;
                    }
                }
                else
                {
                    if (fai == 8)
                    {
                        Bcr = 0.92;
                    }
                    else if (fai == 10)
                    {
                        Bcr = 0.83;
                    }
                    else if (fai == 12)
                    {
                        Bcr = 0.75;
                    }
                    else if (fai == 18 || fai == 16)
                    {
                        Bcr = 0.67;
                    }
                    else if (fai == 22 || fai == 20)
                    {
                        Bcr = 0.58;
                    }
                    else if (fai == 25)
                    {
                        Bcr = 0.6;
                    }
                    else if (fai == 28)
                    {
                        Bcr = 0.50;
                    }
                    else
                    {
                        Bcr = 0.65;
                    }
                }

                ///Get factor
                double factor;
                if (t >= 600)
                {
                    if (fcu == 17.5)
                    {
                        factor = 0.23;
                    }
                    else if (fcu == 20)
                    {
                        factor = 0.25;
                    }
                    else if (fcu == 22.5)
                    {
                        factor = 0.27;
                    }
                    else if (fcu == 25)
                    {
                        factor = 0.30;
                    }
                    else if (fcu == 27.5)
                    {
                        factor = 0.32;
                    }
                    else if (fcu == 30.0)
                    {
                        factor = 0.33;
                    }
                    else
                    {
                        factor = 0.30;
                    }
                }
                //
                else if (t >= 400 && t < 600)
                {
                    if (fcu == 17.5)
                    {
                        factor = 0.25;
                    }
                    else if (fcu == 20)
                    {
                        factor = 0.27;
                    }
                    else if (fcu == 22.5)
                    {
                        factor = 0.28;
                    }
                    else if (fcu == 25)
                    {
                        factor = 0.32;
                    }
                    else if (fcu == 27.5)
                    {
                        factor = 0.33;
                    }
                    else if (fcu == 30.0)
                    {
                        factor = 0.35;
                    }
                    else
                    {
                        factor = 0.28;
                    }
                }

                //
                else if (t >= 200 && t < 400)
                {
                    if (fcu == 17.5)
                    {
                        factor = 0.30;
                    }
                    else if (fcu == 20)
                    {
                        factor = 0.33;
                    }
                    else if (fcu == 22.5)
                    {
                        factor = 0.35;
                    }
                    else if (fcu == 25)
                    {
                        factor = 0.38;
                    }
                    else if (fcu == 27.5)
                    {
                        factor = 0.42;
                    }
                    else if (fcu == 30.0)
                    {
                        factor = 0.43;
                    }
                    else
                    {
                        factor = 0.38;
                    }
                }
                //
                else
                {
                    if (fcu == 17.5)
                    {
                        factor = 0.38;
                    }
                    else if (fcu == 20)
                    {
                        factor = 0.43;
                    }
                    else if (fcu == 22.5)
                    {
                        factor = 0.47;
                    }
                    else if (fcu == 25)
                    {
                        factor = 0.50;
                    }
                    else if (fcu == 27.5)
                    {
                        factor = 0.53;
                    }
                    else if (fcu == 30.0)
                    {
                        factor = 0.57;
                    }
                    else
                    {
                        factor = 0.50;
                    }
                }
                if (txttype.Text == "Pure Tension")
                {
                    groubRoo.Enabled = false;

                    lblAs2.Visible = false;
                    lblfai.Visible = false;
                    txtfai2.Visible = false;
                    txtnum2.Visible = false;
                    //// Design on Axial tension 
                    if (txtTu.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter  Tu ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTu.Focus();
                        txtTu.SelectAll();
                        return;
                    }
                    double Tu = double.Parse(txtTu.Text);
                    //// get K
                    double k;
                    if (fcu == 17.5)
                    {
                        k = 0.71;
                    }
                    else if (fcu == 20.0)
                    {
                        k = 0.67;
                    }
                    else if (fcu == 22.5)
                    {
                        k = 0.63;
                    }
                    else if (fcu == 25.0)
                    {
                        k = 0.59;
                    }
                    else if (fcu == 27.5)
                    {
                        k = 0.56;
                    }
                    else if (fcu == 30.0)
                    {
                        k = 0.53;
                    }
                    else
                    {
                        k = 0.59;
                    }

                    double tideal = (1000 * k * (Tu / 1.5)) / b;
                    tideal = Math.Ceiling(tideal);

                    double As = (1 / Bcr) * (Tu * 1000 / (0.87 * fy));
                    double num = Math.Ceiling(As / (3.1459 * 0.25 * fai * fai));

                    txttideal.Text = tideal.ToString();
                    txtnum.Text = num.ToString();


                    string Check;
                    if (t >= tideal)
                    {
                        Check = "Safe";
                    }
                    else
                    {
                        Check = "UnSafe";
                    }
                    //// Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string tt = txtt.Text;
                        string MM = "---";
                        string TT = txtTu.Text;
                        string PP = "---";
                        string Satage = Check;
                        string RFT = txtnum.Text + " T " + txtfai.Text;
                        object[] data = { serial, bb, tt, MM, TT, PP, Satage, RFT };
                        DataGridView1.Rows.Add(data);

                        return;
                    }
                    else
                    {
                        return;
                    }

                }

                ////////////////////////// Finish of Pure Tension 

                if (txttype.Text == "Pure Moment")
                {
                    groubRoo.Enabled = false;
                    if (txtMu.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter  Mu . ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMu.Focus();
                        txtMu.SelectAll();
                        return;
                    }

                    double Mu = double.Parse(txtMu.Text);



                    ////
                    double tideal = Math.Sqrt(((Mu / 1.5) * Math.Pow(10, 6)) / (b * factor));
                    tideal = Math.Ceiling(tideal);

                    double d = t - cover;
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double aact = d * (1 - Math.Sqrt(1 - ((2 * Mu * 1000 * 1000) / (0.45 * fcu * b * d * d))));
                    if (amax < aact)
                    {
                        MessageBox.Show("UnSafe Section against Moment .. Increse Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }
                    double C = 1.25 * aact;
                    double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                    if (J > 0.826)
                    {
                        J = 0.826;
                    }
                    double Asreq = (1 / Bcr) * (Mu * 1000 * 1000) / (fy * J * d);

                    // Check Asminimum
                    double Asmin1 = (1.1 / fy) * b * d;
                    double Asmin2 = 1.3 * Asreq;
                    double Asteel = 0.15 * 0.01 * b * d;
                    double Asdesign1;
                    double Asfinal;
                    if (Asreq < Asmin1)
                    {
                        Asdesign1 = Math.Min(Asmin1, Asmin2);
                        Asfinal = Math.Max(Asdesign1, Asteel);
                    }
                    else
                    {
                        Asfinal = Asreq;
                    }

                    double num = Math.Ceiling(Asfinal / (3.1459 * 0.25 * fai * fai));

                    txttideal.Text = tideal.ToString();
                    txtnum.Text = num.ToString();




                    string Check;
                    if (t >= tideal)
                    {
                        Check = "Safe";
                    }
                    else
                    {
                        Check = "UnSafe";
                    }
                    //// Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string tt = txtt.Text;
                        string MM = txtMu.Text;
                        string TT = "---";
                        string PP = "---";
                        string Satage = Check;
                        string RFT = txtnum.Text + " T " + txtfai.Text;
                        object[] data = { serial, bb, tt, MM, TT, PP, Satage, RFT };
                        DataGridView1.Rows.Add(data);

                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                ////////////////////////////////////// finish of Pure Moment
                if (txttype.Text == "Comp. + Moment")
                {
                    groubRoo.Enabled = false;

                    lblAs2.Visible = false;
                    lblfai.Visible = false;
                    txtfai2.Visible = false;
                    txtnum2.Visible = false;

                    if (txtMu.Text.Trim() == "" || txtPu.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMu.Focus();
                        txtMu.SelectAll();
                        return;
                    }

                    double Mu = double.Parse(txtMu.Text);
                    double Pu = double.Parse(txtPu.Text);
                    double tideal = Math.Sqrt((((Mu / 1.5) * Math.Pow(10, 6)) / (b * factor)) - 20);
                    tideal = Math.Ceiling(tideal);

                    double ft = (-1 * 1000 * Pu / (1.5 * b * t)) + (6 * Mu * 1000 * 1000) / (1.5 * b * t * t);
                    double fctr = 0.6 * Math.Sqrt(fcu);
                    double fctN = (Pu * 1000) / (b * t * 1.5);
                    double fctM = (6 * Mu * 1000 * 1000) / (b * t * t * 1.5);
                    double tv = t * (1 - (fctN / fctM));
                    double eta;
                    if (tv <= 100)
                    {
                        eta = 1.0;
                    }
                    else if (tv > 100 && tv <= 200)
                    {
                        eta = 1.30;
                    }
                    else if (tv > 200 && tv <= 400)
                    {
                        eta = 1.60;
                    }
                    else
                    {
                        eta = 1.70;
                    }
                    double Fct = fctr / eta;

                    if (Fct < ft)
                    {
                        MessageBox.Show("UnSafe Section for Stage I .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double ecc = Mu / Pu;
                    double ratio = ecc / (0.001 * t);

                    if (ratio > 0.5)
                    {
                        groubRoo.Enabled = false;

                        // big eccent.
                        double es = ecc + (t / 2000) - (0.001 * cover);
                        double Mus = Pu * es;
                        double d = t - cover;
                        double amax = (320 * d) / (600 + 0.87 * fy);
                        double aact = d * (1 - Math.Sqrt(1 - ((2 * Mus * 1000 * 1000) / (0.45 * fcu * b * d * d))));

                        if (amax < aact)
                        {
                            MessageBox.Show("UnSafe Section against Moment .. Increse Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        double C = 1.25 * aact;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)
                        {
                            J = 0.826;
                        }
                        double Asreq = (1 / Bcr) * ((Mus * 1000 * 1000 / (fy * J * d)) - (Pu * 1000 / (0.87 * fy)));


                        // Check Asminimum
                        double Asmin1 = (1.1 / fy) * b * d;
                        double Asmin2 = 1.3 * Asreq;
                        double Asteel = 0.15 * 0.01 * b * d;
                        double Asdesign1;
                        double Asfinal;
                        if (Asreq < Asmin1)
                        {
                            Asdesign1 = Math.Min(Asmin1, Asmin2);
                            Asfinal = Math.Max(Asdesign1, Asteel);
                        }
                        else
                        {
                            Asfinal = Asreq;
                        }

                        double num = Math.Ceiling(Asfinal / (3.1459 * 0.25 * fai * fai));

                        txttideal.Text = tideal.ToString();
                        txtnum.Text = num.ToString();

                    }

                    ////
                    if (ratio <= 0.5)
                    {
                        groubRoo.Enabled = true;
                        ////// enter I.D

                        if (txtro.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Value of  ρ ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtro.Focus();
                            txtro.SelectAll();
                            return;
                        }

                        double Roo = double.Parse(txtro.Text);
                        double As = Roo * fcu * Math.Pow(10, -4) * b * t;
                        double Astotal = 2 * As;
                        double Asmin = 0.8 * 0.01 * b * t;
                        if (Asmin > As)
                        {
                            As = 0.5 * Asmin;
                        }
                        double num = Math.Ceiling(As / (3.1459 * 0.25 * fai * fai));

                        txttideal.Text = tideal.ToString();
                        txtnum.Text = num.ToString();

                    }

                    //// Table
                    string Check;
                    if (t >= tideal)
                    {
                        Check = "Safe";
                    }
                    else
                    {
                        Check = "UnSafe";
                    }
                    //// Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string tt = txtt.Text;
                        string MM = txtMu.Text;
                        string TT = "---";
                        string PP = txtPu.Text;
                        string Satage = Check;
                        string RFT = txtnum.Text + " T " + txtfai.Text;
                        object[] data = { serial, bb, tt, MM, TT, PP, Satage, RFT };
                        DataGridView1.Rows.Add(data);

                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                /////////////////////////////////////////// finish Comp + Moment
                if (txttype.Text == "Tens. + Moment")
                {
                    groubRoo.Enabled = false;

                    lblAs2.Visible = false;
                    lblfai.Visible = false;
                    txtfai2.Visible = false;
                    txtnum2.Visible = false;

                    if (txtMu.Text.Trim() == "" || txtTu.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMu.Focus();
                        txtMu.SelectAll();
                        return;
                    }

                    double Mu = double.Parse(txtMu.Text);
                    double Tu = double.Parse(txtTu.Text);
                    double tideal = Math.Sqrt((((Mu / 1.5) * Math.Pow(10, 6)) / (b * factor)) + 40);
                    tideal = Math.Ceiling(tideal);

                    double ft = (1000 * Tu / (1.5 * b * t)) + (6 * Mu * 1000 * 1000) / (1.5 * b * t * t);
                    double fctr = 0.6 * Math.Sqrt(fcu);
                    double fctN = (Tu * 1000) / (b * t * 1.5);
                    double fctM = (6 * Mu * 1000 * 1000) / (b * t * t * 1.5);
                    double tv = t * (1 + (fctN / fctM));
                    double eta;
                    if (tv <= 100)
                    {
                        eta = 1.0;
                    }
                    else if (tv > 100 && tv <= 200)
                    {
                        eta = 1.30;
                    }
                    else if (tv > 200 && tv <= 400)
                    {
                        eta = 1.60;
                    }
                    else
                    {
                        eta = 1.70;
                    }
                    double Fct = fctr / eta;

                    if (Fct < ft)
                    {
                        MessageBox.Show("UnSafe Section for Stage I .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }

                    double ecc = Mu / Tu;
                    double ratio = ecc / (0.001 * t);

                    if (ratio > 0.5)
                    {
                        groubRoo.Enabled = false;

                        // big eccent.
                        double es = ecc - (t / 2000) + (0.001 * cover);
                        double Mus = Tu * es;
                        double d = t - cover;
                        double amax = (320 * d) / (600 + 0.87 * fy);
                        double aact = d * (1 - Math.Sqrt(1 - ((2 * Mus * 1000 * 1000) / (0.45 * fcu * b * d * d))));

                        if (amax < aact)
                        {
                            MessageBox.Show("UnSafe Section against Moment .. Increse Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        double C = 1.25 * aact;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)
                        {
                            J = 0.826;
                        }
                        double Asreq = (1 / Bcr) * ((Mus * 1000 * 1000 / (fy * J * d)) + (Tu * 1000 / (0.87 * fy)));

                        // Check Asminimum
                        double Asmin1 = (1.1 / fy) * b * d;
                        double Asmin2 = 1.3 * Asreq;
                        double Asteel = 0.15 * 0.01 * b * d;
                        double Asdesign1;
                        double Asfinal;
                        if (Asreq < Asmin1)
                        {
                            Asdesign1 = Math.Min(Asmin1, Asmin2);
                            Asfinal = Math.Max(Asdesign1, Asteel);
                        }
                        else
                        {
                            Asfinal = Asreq;
                        }

                        double num = Math.Ceiling(Asfinal / (3.1459 * 0.25 * fai * fai));

                        txttideal.Text = tideal.ToString();
                        txtnum.Text = num.ToString();

                    }

                    ////
                    if (ratio <= 0.5)
                    {
                        groubRoo.Enabled = false;


                        double aa = (t / 2000) - (0.001 * cover) - ecc;
                        double bb = (t / 2000) + ecc - (0.001 * cover);
                        double T1 = (Tu * bb) / (aa + bb);       //kN
                        double T2 = (Tu * aa) / (aa + bb);
                        double As1 = (1 / Bcr) * (T1 * 1000 / (0.87 * fy));
                        double As2 = (1 / Bcr) * (T2 * 1000 / (0.87 * fy));
                        lblAs2.Visible = true;
                        lblfai.Visible = true;
                        txtfai2.Visible = true;
                        txtnum2.Visible = true;
                        int fai2 = int.Parse(txtfai2.Text);
                        double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai * fai));
                        double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));

                        txttideal.Text = tideal.ToString();
                        txtnum.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();
                    }

                    //// Table
                    string Check;
                    if (t >= tideal)
                    {
                        Check = "Safe";
                    }
                    else
                    {
                        Check = "UnSafe";
                    }
                    //// Table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string bb = txtb.Text;
                        string tt = txtt.Text;
                        string MM = txtMu.Text;
                        string TT = txtTu.Text;
                        string PP = "---";
                        string Satage = Check;
                        string RFT = txtnum.Text + " T " + txtfai.Text;
                        object[] data = { serial, bb, tt, MM, TT, PP, Satage, RFT };
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

        private void button3_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Get ρ from Interaction Diagram")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               getRootanks ROOO = new getRootanks(this);
                ROOO.ShowDialog();

                txtro.Text = ROOO.txtro1.Text;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " UnCracked Section (Screen)";
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

        private void button7_Click(object sender, EventArgs e)
        {
            txttideal.Clear();
            txtnum.Clear();
            txtnum2.Clear();
            txtro.Clear();
        }
    }
}
