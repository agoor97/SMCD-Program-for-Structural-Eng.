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
    public partial class PMCOL : Form
    {
        public PMCOL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            try
            {

                if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtb.Text.Trim() == ""
                    || txtt.Text.Trim() == "" || txtc.Text.Trim() == "" || txtm.Text.Trim() == ""
                    || txtp.Text.Trim() == "" || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtfcu.Focus();
                    return;
                }
                else
                {
                    double fcu = double.Parse(txtfcu.Text);
                    double fy = double.Parse(txtfy.Text);
                    double t = double.Parse(txtt.Text);
                    double b = double.Parse(txtb.Text);
                    double cover = double.Parse(txtc.Text);
                    double M = double.Parse(txtm.Text);
                    double P = double.Parse(txtp.Text);
                    int fai1 = int.Parse(txtfai1.Text);
                    int fai2 = int.Parse(txtfai2.Text);

                    //// ideal thickness 
                    double d1 = 3.5 * Math.Sqrt(M * Math.Pow(10, 6) / (fcu * b));
                    double t1 = Math.Round((d1 + 50), 2);
                    double t2 = (P * 1000) / (b * (0.35 * 0.99 * fcu + 0.67 * 0.01 * fy));
                    t2 = Math.Round(t2, 2);
                    double tmax = Math.Max(t1, t2);
                    double trecomm = Math.Round((1.2 * tmax), 2);
                    txtt1.Text = t1.ToString();
                    txtt2.Text = t2.ToString();
                    txttrecomend.Text = trecomm.ToString();
                    //////////////////

                    double d = t - cover;      ///mm
                    double amax = (320 * d) / (600 + 0.87 * fy);
                    double ecc = M / P;         // m
                    double ratio = ecc / (0.001 * t);

                    if (ratio >= 0.5)
                    {
                        //// big eccentricity 
                        //no need for I.D
                        ///// Use es 
                        double es = ecc + (0.5 * 0.001 * t) - (0.001 * cover);
                        double Ms = P * es;
                        double a = d * (1 - Math.Sqrt((1 - (2 * Ms * Math.Pow(10, 6)) / (0.45 * fcu * b * d * d))));
                        if (amax < a)
                        {
                            MessageBox.Show("UnSafe Section .. try to Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtt.Focus();
                            txtt.SelectAll();
                            return;
                        }

                        double C = 1.25 * a;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)
                        {
                            J = 0.826;
                        }
                        double As1 = ((Ms * Math.Pow(10, 6)) / (fy * J * d)) - ((P * 1000) / (0.87 * fy));
                        double Asmin1 = 0.225 * Math.Sqrt(fcu) * b * d / fy;
                        double Asmin2 = 1.3 * As1;
                        double Asteel = 0.15 * 0.01 * b * d;
                        double As1design;
                        double As2design;
                        if (As1 < Asmin1)
                        {
                            double Asmin = Math.Min(Asmin1, Asmin2);
                            As1design = Math.Max(Asmin, Asteel);
                            As2design = 0.4 * As1design;
                        }
                        else
                        {
                            As1design = As1;
                            As2design = 0.4 * As1design;
                        }
                        double num1 = Math.Ceiling(As1design / (3.1459 * 0.25 * fai1 * fai1));
                        double num2 = Math.Ceiling(As2design / (3.1459 * 0.25 * fai2 * fai2));
                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();
                        lblecc.Text = " Big ecc. ";

                        //// table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string bb = txtb.Text;
                            string tt = txtt.Text;
                            string PP = txtp.Text;
                            string MM = txtm.Text;
                            string ASmain = txtnum1.Text + " T " + txtfai1.Text;
                            string ASsec = txtnum2.Text + " T " + txtfai2.Text;
                            string type = lblecc.Text;
                            object[] data = { serial, bb, tt, PP, MM, ASmain, ASsec, type };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }



                        ///////////////////// Ideal thickness

                    }

                    /////////////////////////////////////////// finish of big ecc

                    if (ratio < 0.5)
                    {
                        if (txtro1.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Value of ρ ?", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtro1.Focus();
                            return;
                        }
                        else
                        {
                            double Roo = double.Parse(txtro1.Text);
                            double Alfa = double.Parse(txtalfa.Text);
                            double AS1 = Roo * fcu * Math.Pow(10, -4) * b * t;
                            double AS2 = Alfa * AS1;

                            /// check MIninmum 
                            double Astotal = AS1 + AS2;
                            double Asmin = 0.8 * 0.01 * b * t;
                            double As1design;
                            double As2design;
                            if (Asmin > Astotal)
                            {
                                As1design = 0.5 * Asmin;
                                As2design = 0.5 * Asmin;
                            }
                            else
                            {
                                As1design = AS1;
                                As2design = AS2;
                            }
                            double num1 = Math.Ceiling(As1design / (3.1459 * 0.25 * fai1 * fai1));
                            double num2 = Math.Ceiling(As2design / (3.1459 * 0.25 * fai2 * fai2));
                            txtnum1.Text = num1.ToString();
                            txtnum2.Text = num2.ToString();
                            lblecc.Text = "Small ecc.";



                            //// table
                            DialogResult dr;
                            dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.OK)
                            {
                                string serial = "";
                                string bb = txtb.Text;
                                string tt = txtt.Text;
                                string PP = txtp.Text;
                                string MM = txtm.Text;
                                string ASmain = txtnum1.Text + " T " + txtfai1.Text;
                                string ASsec = txtnum2.Text + " T " + txtfai2.Text;
                                string type = lblecc.Text;
                                object[] data = { serial, bb, tt, PP, MM, ASmain, ASsec, type };
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
            sf.Title = " Column (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save( path );
            }
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
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


        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Get ρ from Interaction Diagram ")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                if (txtfy.Text.Trim() == "" || txtfcu.Text.Trim() == "" || txtt.Text.Trim() == ""
              || txtc.Text.Trim() == "" || txtb.Text.Trim() == "" || txtm.Text.Trim() == ""
              || txtp.Text.Trim() == "" || txtalfa.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                double M = double.Parse(txtm.Text);
                double P = double.Parse(txtp.Text);
                double t = double.Parse(txtt.Text);

                double ecc = M / P;         // m
                double ratio = ecc / (0.001 * t);

                if (ratio > 0.5)
                {
                    MessageBox.Show("𝑒⁄𝑡 > 0.5 ... No need For I.D >> Click Design directly.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    Interaction ID = new Interaction(this);
                    ID.ShowDialog();
                    txtro1.Text = ID.txtro.Text;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblecc.Text = "";
            txtnum1.Clear();
            txtnum2.Clear();
            txtro1.Clear();
            txtt1.Clear();
            txtt2.Clear();
            txttrecomend.Clear();
        }
    }
}
