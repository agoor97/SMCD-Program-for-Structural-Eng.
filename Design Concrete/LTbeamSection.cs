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
    public partial class LTbeamSection : Form
    {
        public LTbeamSection()
        {
            InitializeComponent();
        }

        private void LTbeamSection_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtfys.Text.Trim() == "" || txtb.Text.Trim() == ""
                || txtflange.Text.Trim() == "" || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "" || txtt.Text.Trim() == ""
                || txtts.Text.Trim() == "" || txtm.Text.Trim() == "" || txtQ.Text.Trim() == "" || txtc.Text.Trim() == "")
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
                    double M = double.Parse(txtm.Text);
                    double Q = double.Parse(txtQ.Text);
                    double t = double.Parse(txtt.Text);
                    double c = double.Parse(txtc.Text);
                    double ts = double.Parse(txtts.Text);
                    double B = double.Parse(txtflange.Text);
                    double b = double.Parse(txtb.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);

                    double d = t - c;          // mm 
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double amin = 0.1 * d;
                    double qcumin = 0.12 * Math.Sqrt(fcu / 1.5);
                    double qcumax = 0.70 * Math.Sqrt(fcu / 1.5);
                    double qu = (Q * 1000) / (b * d);
                    double qsu = qu - qcumin;
                    double ASmin1 = 0.225 * Math.Sqrt(fcu) * b * d / fy;
                    double Asteel = 0.15 * 0.01 * b * d;
                    double AS1design;
                    double AS2design;
                    ///// firstly assume R section (Comp. in the Flange Onl)
                    double a = d * (1 - Math.Sqrt(1 - ((2 * M * Math.Pow(10, 6)) / (0.45 * fcu * B * d * d))));
                    if (a > amax)
                    {
                        MessageBox.Show("UnSafe Section against Moment .. Increase Dimen.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt.Focus();
                        txtt.SelectAll();
                        return;
                    }
                    if (a <= ts)
                    {
                        //Right Assumption ..... ok R section
                        double C = 1.25 * a;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        double AS1req = (M * Math.Pow(10, 6)) / (fy * J * d);

                        if (AS1req<ASmin1)
                        {
                           double ASmin22 = 1.3 * AS1req;
                            ASmin1 = Math.Min(ASmin1, ASmin22);
                             AS1design = Math.Max(ASmin1, Asteel);
                             AS2design = 0.15 * AS1design;
                        }
                        else
                        {
                            AS1design = AS1req;
                            AS2design = 0.15 * AS1design;
                        }
                    

                        double num1 = Math.Ceiling((AS1design/ (3.1459 * 0.25 * fai1 * fai1)));
                        double num2 = Math.Ceiling((AS2design / (3.1459 * 0.25 * fai2 * fai2)));

                        //against Shear 
                        if (qu <= qcumin)
                        {
                            /////safe min 
                            txtnumst.Text = "5";
                            txtbranch.Text = "2";
                            txtfaist.Text = "8";
                        }
                        else if (qu > qcumax)
                        {
                            MessageBox.Show("UnSafe Section against Shear ... try to Increse Dimen.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        else
                        {
                            double S = (2 * 0.87 * fys * 50.3) / (qsu * b);
                            if ((S >= 100 && S <= 200) || S > 200)
                            {
                                txtbranch.Text = "2";
                                txtfaist.Text = "8";
                                double num = Math.Ceiling(1000 / S);
                                double Final = Math.Max(num, 5);
                                txtnumst.Text = Final.ToString();
                            }
                            else
                            {
                                S = 2 * 78.5 * 0.87 * fys / (qsu * b);
                                if (S >= 100 && S <= 200)
                                {
                                    txtbranch.Text = "2";
                                    txtfaist.Text = "10";
                                    double num = Math.Ceiling(1000 / S);
                                    double Final = Math.Max(num, 5);
                                    txtnumst.Text = Final.ToString();

                                }
                                else
                                {
                                    S = 4 * 50.3 * 0.87 * fys / (qsu * b);
                                    if (S >= 100 & S <= 200)
                                    {
                                        txtbranch.Text = "4";
                                        txtfaist.Text = "8";
                                        double num = Math.Ceiling(1000 / S);
                                        double Final = Math.Max(num, 5);
                                        txtnumst.Text = Final.ToString();

                                    }
                                    else
                                    {
                                        S = 4 * 78.5 * 0.87 * fys / (qsu * b);
                                        if (S >= 100 && S <= 200)
                                        {
                                            txtbranch.Text = "4";
                                            txtfaist.Text = "10";
                                            double num = Math.Ceiling(1000 / S);
                                            double Final = Math.Max(num, 5);
                                            txtnumst.Text = Final.ToString();

                                        }
                                        else
                                        {
                                            MessageBox.Show("UnSafe Shear for Four Cases (100 <= S <= 200 ) .. Check Manual to be Persuaded .. try Diffrent Criteria ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            txtt.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();
                      
                        /////// table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string bb = txtb.Text;
                            string dd = d.ToString();
                            string BB = txtflange.Text;
                            string tss = txtts.Text;
                            string MM = txtm.Text;
                            string QQ = txtQ.Text;
                            string ASmain = txtnum1.Text + " T " + txtfai1.Text;
                            string ASsec = txtnum2.Text + " T " + txtfai2.Text;
                            string stirr = txtnumst.Text + " T " + txtfaist.Text;
                            string branch = txtbranch.Text;

                            object[] data = { serial, bb, dd, BB, tss, MM, QQ, ASmain, ASsec, stirr, branch };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    ///////////////////////////////////////////////////////
                    if (a > ts)
                    {
                        double No1 = (M * Math.Pow(10, 6) - 0.45 * fcu * B * ts * (d - (ts / 2))) / (0.45 * fcu * b);
                        double NO2 = No1 + ts * d - (0.5 * ts * ts);
                        /////// a2 -2d *a + 2*No2 =0.0   2nd eqn
                        double root = Math.Sqrt(4 * d * d - 4 * 2 * NO2);
                        double a1 = 0.5 * (2 * d + root);
                        double a2 = 0.5 * (2 * d - root);
                        double anew = Math.Min(a1, a2);

                        if (anew > amax)
                        {
                            MessageBox.Show("UnSafe Section against Moment .. Increase Dimen.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        double Cnew = 1.25 * anew;
                        double Jnew = (1 / 1.15) * (1 - 0.4 * (Cnew / d));
                        if(Jnew > 0.826)
                        {
                            Jnew = 0.826;              ///Ductility Condition
                        }
                        ///Check min
                        

                        double AS1new = (M * Math.Pow(10, 6)) / (fy * Jnew * d);

                        
                        if(AS1new <ASmin1)
                        {
                            double ASmin2 = 1.3 * AS1new;
                           double ASmin = Math.Min(ASmin1, ASmin2);
                             AS1design = Math.Max(ASmin, Asteel);
                             AS2design = 0.15 * AS1new;
                        }
                        else
                        {
                            AS1design = AS1new;
                            AS2design = 0.15 * AS1new;
                        }
                        double num1new = Math.Ceiling(AS1design / (0.25 * 3.1459 * fai1 * fai1));
                        double num2new = Math.Ceiling(AS2design / (0.25 * 3.1459 * fai2 * fai2));



                        //// against shear //// no diffrence 

                        //against Shear 
                        if (qu <= qcumin)
                        {
                            /////safe min 
                            txtnumst.Text = "5";
                            txtbranch.Text = "2";
                            txtfaist.Text = "8";
                        }
                        else if (qu > qcumax)
                        {
                            MessageBox.Show("UnSafe Section against Shear ... try to Increse Dimen.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }
                        else
                        {
                            double S = (2 * 0.87 * fys * 50.3) / (qsu * b);
                            if ((S >= 100 && S <= 200) || S > 200)
                            {
                                txtbranch.Text = "2";
                                txtfaist.Text = "8";
                                double num = Math.Ceiling(1000 / S);
                                double Final = Math.Max(num, 5);
                                txtnumst.Text = Final.ToString();
                            }
                            else
                            {
                                S = 2 * 78.5 * 0.87 * fys / (qsu * b);
                                if (S >= 100 && S <= 200)
                                {
                                    txtbranch.Text = "2";
                                    txtfaist.Text = "10";
                                    double num = Math.Ceiling(1000 / S);
                                    double Final = Math.Max(num, 5);
                                    txtnumst.Text = Final.ToString();

                                }
                                else
                                {
                                    S = 4 * 50.3 * 0.87 * fys / (qsu * b);
                                    if (S >= 100 & S <= 200)
                                    {
                                        txtbranch.Text = "4";
                                        txtfaist.Text = "8";
                                        double num = Math.Ceiling(1000 / S);
                                        double Final = Math.Max(num, 5);
                                        txtnumst.Text = Final.ToString();

                                    }
                                    else
                                    {
                                        S = 4 * 78.5 * 0.87 * fys / (qsu * b);
                                        if (S >= 100 && S <= 200)
                                        {
                                            txtbranch.Text = "4";
                                            txtfaist.Text = "10";
                                            double num = Math.Ceiling(1000 / S);
                                            double Final = Math.Max(num, 5);
                                            txtnumst.Text = Final.ToString();

                                        }
                                        else
                                        {
                                            MessageBox.Show("UnSafe Shear for Four Cases (100 <= S <= 200 ) .. Check Manual to be Persuaded .. try Diffrent Criteria ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            txtt.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                      
                        txtnum1.Text = num1new.ToString();
                        txtnum2.Text = num2new.ToString();

                        /////// table 
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string bb = txtb.Text;
                            string dd = d.ToString();
                            string BB = txtflange.Text;
                            string tss = txtts.Text;
                            string MM = txtm.Text;
                            string QQ = txtQ.Text;
                            string ASmain = txtnum1.Text + " T " + txtfai1.Text;
                            string ASsec = txtnum2.Text + " T " + txtfai2.Text;
                            string stirr = txtnumst.Text + " T " + txtfaist.Text;
                            string branch = txtbranch.Text;

                            object[] data = { serial, bb, dd, BB, tss, MM, QQ, ASmain, ASsec, stirr, branch };
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
            }
            if(radioButton2.Checked==true)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
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

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ECP 203-2018")
                {
                    Isopen = true;
                    f.Focus();
                    break;

                }

            }
            if (Isopen == false)
            {
                coseTLsection code = new coseTLsection();
                code.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtnum1.Clear();
            txtnum2.Clear();
            txtnumst.Clear();

            txtfaist.Clear();
            txtbranch.Clear();
        }
    }
}

