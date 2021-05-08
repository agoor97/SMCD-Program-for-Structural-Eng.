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
    public partial class footN : Form
    {
        public footN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttype.Text == "Rectangle")
                {
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txta.Text.Trim() == "" || txtb.Text.Trim() == ""
                   || txtp.Text.Trim() == "" || txtqall.Text.Trim() == "" || txttpc.Text.Trim() == "" || txttrc.Text.Trim() == ""
                    || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double qall = double.Parse(txtqall.Text);
                        double a = double.Parse(txta.Text);
                        double b = double.Parse(txtb.Text);
                        double Pw = double.Parse(txtp.Text);
                        double tpc = double.Parse(txttpc.Text);
                        double trc = double.Parse(txttrc.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int fai2 = int.Parse(txtfai2.Text);
                        double Lpc;
                        double Bpc;
                        double Lrc;
                        double Brc;
                        double d = trc - 0.07;           // m   >> cover = 70mm

                        // P.c 
                        if (tpc >= 0.2)
                        {
                            // P.C resist Loads
                            double Apc = Pw / qall;
                            double root = Math.Sqrt((b - a) * (b - a) + 4 * Apc);
                            double Bpc1 = (-1 * (b - a) + root) / 2;
                            double Bpc2 = (-1 * (b - a) - root) / 2;
                            Bpc = Math.Max(Bpc1, Bpc2);
                            Lpc = Apc / Bpc;
                            Brc = Bpc - 2 * tpc;
                            Lrc = Lpc - 2 * tpc;

                        }
                        else
                        {
                            // P.c Can not Resist Loads
                            double Arc = Pw / qall;
                            double root = Math.Sqrt((b - a) * (b - a) + 4 * Arc);
                            double Brc1 = (-1 * (b - a) + root) / 2;
                            double Brc2 = (-1 * (b - a) - root) / 2;
                            Brc = Math.Max(Brc1, Brc2);
                            Lrc = Arc / Brc;
                            Bpc = Brc + 2 * tpc;
                            Lpc = Lrc + 2 * tpc;
                        }

                        //// Ultimate Stage 
                        double Pu = Pw * 1.5;
                        double fact = Pu / (Lrc * Brc);

                        // Long Direction 
                        double Z1 = (Lrc - b) / 2;
                        double MLong = fact * Z1 * Z1 * 0.5;       // kN.m/m'

                        // Short Direction
                        double Z2 = (Brc - a) / 2;
                        double Mshort = fact * Z2 * Z2 * 0.5;

                        /// Check Shear 
                        /// 
                        double Z = Math.Max(Z1, Z2);
                        double L = Z - (d / 2);
                        double Qu = fact * L;
                        double qu = Qu / (d * 1.0 * 1000);          // >> N/mm2                       
                        double qsh = 0.16 * Math.Sqrt(fcu / 1.5);
                        if (qu > qsh)
                        {
                            MessageBox.Show("UnSafe against Shear .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            lblshear.Text = "UnSafe";
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        else
                        {
                            lblshear.Text = "Safe";
                        }

                        // Check Punching 
                        double Qp = Pu - fact * (a + d) * (b + d);         // kN
                        double Ap = 2 * d * (a + b + 2 * d);                 // m2
                        double qpu = Qp / (Ap * 1000);             // N/mm2
                        double qpun1 = 0.316 * Math.Sqrt(fcu / 1.5);
                        double qpun2 = 1.6;
                        double qpun3 = 0.316 * (0.5 + (a / b)) * Math.Sqrt(fcu / 1.5);
                        double qpunmin = Math.Min(qpun1, qpun2);
                        double qpun = Math.Min(qpunmin, qpun3);

                        if (qpu > qpun)
                        {
                            MessageBox.Show("UnSafe against Punching .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            lblpunch.Text = "UnSafe";
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        else
                        {
                            lblpunch.Text = "Safe";
                        }

                        ///// Get RFT
                        double Asmin1 = 1.5 * d * 1000;            // mm2 
                        double Asmin2 = 565;
                        double Asmin = Math.Max(Asmin1, Asmin2);
                        double amax = (320 * d * 1000) / (600 + 0.87 * fy);
                        double a1 = d * 1000 * (1 - Math.Sqrt((1 - (2 * MLong * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                        if (a1 > amax)
                        {
                            MessageBox.Show("UnSafe against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        double C1 = 1.25 * a1;
                        double J1 = (1 / 1.15) * (1 - 0.4 * (C1 / (d * 1000)));
                        if (J1 > 0.826)
                        {
                            J1 = 0.826;              // Ductility Condition

                        }

                        double Aslong = (MLong * 1000 * 1000) / (fy * J1 * d * 1000);
                        if (Aslong < Asmin)
                        {
                            Aslong = Asmin;
                        }


                        //
                        double a2 = d * 1000 * (1 - Math.Sqrt((1 - (2 * Mshort * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                        if (a2 > amax)
                        {
                            MessageBox.Show("UnSafe against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        double C2 = 1.25 * a2;
                        double J2 = (1 / 1.15) * (1 - 0.4 * (C2 / (d * 1000)));
                        if (J2 > 0.826)
                        {
                            J2 = 0.826;              // Ductility Condition

                        }

                        double Asshort = (Mshort * 1000 * 1000) / (fy * J2 * d * 1000);
                        if (Asshort < Asmin)
                        {
                            Asshort = Asmin;
                        }


                        double num1 = Math.Ceiling(Aslong / (3.1459 * 0.25 * fai1 * fai1));
                        double num2 = Math.Ceiling(Asshort / (3.1459 * 0.25 * fai2 * fai2));
                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();


                        // pc
                        txtLpc.Text = (0.05 * Math.Ceiling(Lpc/0.05)).ToString();
                        txtBpc.Text = (0.05*Math.Ceiling(Bpc/0.05)).ToString();

                        txttpcfinal.Text = txttpc.Text;

                        // Rc
                        txtLrc.Text = (0.05 * Math.Ceiling(Lrc/0.05)).ToString();
                        txtBrc.Text = (0.05 * Math.Ceiling(Brc/0.05)).ToString();

                        txttrcfinal.Text = txttrc.Text;


                        // table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string col = txtb.Text + " * " + txta.Text;
                            string PP = txtp.Text;
                            string PC = txtLpc.Text + " * " + txtBpc.Text + " * " + txttpcfinal.Text;
                            string RC = txtLrc.Text + " * " + txtBrc.Text + " * " + txttrcfinal.Text;
                            string shear = lblshear.Text;
                            string punch = lblpunch.Text;
                            string LongAs = txtnum1.Text + " T " + txtfai1.Text;
                            string shortAs = txtnum2.Text + " T " + txtfai2.Text;
                            object[] data = { serial, col, PP, PC, RC, shear, punch, LongAs, shortAs };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }




                    }
                }


                /////////////////////////////////////////////////////////////////////////////////////////


                if (txttype.Text == "Square")
                {
                  
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txta.Text.Trim() == "" || txtb.Text.Trim() == ""
                     || txtp.Text.Trim() == "" || txtqall.Text.Trim() == "" || txttpc.Text.Trim() == "" || txttrc.Text.Trim() == ""
                      || txtfai1.Text.Trim() == "" || txtfai2.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double qall = double.Parse(txtqall.Text);
                        double a = double.Parse(txta.Text);
                        double b = double.Parse(txtb.Text);
                        double Pw = double.Parse(txtp.Text);
                        double tpc = double.Parse(txttpc.Text);
                        double trc = double.Parse(txttrc.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int fai2 = int.Parse(txtfai2.Text);
                        double Lpc;
                        double Bpc;
                        double Lrc;
                        double Brc;
                        double d = trc - 0.07;           // m   >> cover = 70mm

                        // P.c 
                        if (tpc >= 0.2)
                        {
                            // P.C resist Loads
                            Bpc = Math.Sqrt(Pw / qall);
                            Lpc = Bpc;
                            Brc = Bpc - 2 * tpc;
                            Lrc = Brc;

                        }
                        else
                        {
                            // P.c Can not Resist Loads
                            Brc = Math.Sqrt(Pw / qall);
                            Lrc = Brc;
                            Bpc = Brc + 2 * tpc;
                            Lpc = Bpc;
                        }

                        //// Ultimate Stage 
                        double Pu = Pw * 1.5;
                        double fact = Pu / (Lrc * Brc);

                        // Long Direction 
                        double Z1 = (Brc - b) / 2;
                        double M1 = fact * Z1 * Z1 * 0.5;       // kN.m/m'

                        // Short Direction
                        double Z2 = (Brc - a) / 2;
                        double M2 = fact * Z2 * Z2 * 0.5;
                        double M = Math.Max(M1, M2);

                        /// Check Shear 
                        /// 
                        double Z = Math.Max(Z1, Z2);
                        double L = Z - (d / 2);
                        double Qu = fact * L;
                        double qu = Qu / (d * 1.0 * 1000);          // >> N/mm2
                       

                        double qsh = 0.16 * Math.Sqrt(fcu / 1.5);
                        if (qu > qsh)
                        {
                            MessageBox.Show("UnSafe against Shear .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            lblshear.Text = "UnSafe";
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        else
                        {
                            lblshear.Text = "Safe";
                        }

                        // Check Punching 
                        double Qp = Pu - fact * (a + d) * (b + d);         // kN
                        double Ap = 2 * d * (a + b + 2 * d);                 // m2
                        double qpu = Qp / (Ap * 1000);             // N/mm2
                        double qpun1 = 0.316 * Math.Sqrt(fcu / 1.5);
                        double qpun2 = 1.6;
                        double qpun3 = 0.316 * (0.5 + (a / b)) * Math.Sqrt(fcu / 1.5);
                        double qpunmin = Math.Min(qpun1, qpun2);
                        double qpun = Math.Min(qpunmin, qpun3);

                        if (qpu > qpun)
                        {
                            MessageBox.Show("UnSafe against Punching .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            lblpunch.Text = "UnSafe";
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        else
                        {
                            lblpunch.Text = "Safe";
                        }

                        ///// Get RFT
                        double Asmin1 = 1.5 * d * 1000;            // mm2 
                        double Asmin2 = 565;
                        double Asmin = Math.Max(Asmin1, Asmin2);
                        double amax = (320 * d * 1000) / (600 + 0.87 * fy);
                        double a1 = d * 1000 * (1 - Math.Sqrt((1 - (2 * M * 1000 * 1000) / (0.45 * fcu * 1000 * d * d * 1000 * 1000))));
                        if (a1 > amax)
                        {
                            MessageBox.Show("UnSafe against Moment .. Increase Dimens.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txttrc.Focus();
                            txttrc.SelectAll();
                            return;
                        }
                        double C1 = 1.25 * a1;
                        double J1 = (1 / 1.15) * (1 - 0.4 * (C1 / (d * 1000)));
                        if (J1 > 0.826)
                        {
                            J1 = 0.826;              // Ductility Condition

                        }

                        double As = (M * 1000 * 1000) / (fy * J1 * d * 1000);
                        if (As < Asmin)
                        {
                            As = Asmin;
                        }



                        double num1 = Math.Ceiling(As / (3.1459 * 0.25 * fai1 * fai1));
                        double num2 = Math.Ceiling(As / (3.1459 * 0.25 * fai2 * fai2));
                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();


                        // pc
                        txtLpc.Text = (0.05 * Math.Ceiling(Lpc/0.05)).ToString();
                        txtBpc.Text = (0.05 * Math.Ceiling(Bpc/0.05)).ToString();

                        txttpcfinal.Text = txttpc.Text;

                        // Rc
                        txtLrc.Text = (0.05 * Math.Ceiling(Lrc/0.05)).ToString();
                        txtBrc.Text = (0.05 * Math.Ceiling(Brc/0.05)).ToString();

                        txttrcfinal.Text = txttrc.Text;


                        // table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string col = txtb.Text + " * " + txta.Text;
                            string PP = txtp.Text;
                            string PC = txtLpc.Text + " * " + txtBpc.Text + " * " + txttpcfinal.Text;
                            string RC = txtLrc.Text + " * " + txtBrc.Text + " * " + txttrcfinal.Text;
                            string shear = lblshear.Text;
                            string punch = lblpunch.Text;
                            string LongAs = txtnum1.Text + " T " + txtfai1.Text;
                            string shortAs = txtnum2.Text + " T " + txtfai2.Text;
                            object[] data = { serial, col, PP, PC, RC, shear, punch, LongAs, shortAs };
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Footing (Normal) (Screen)";
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

        private void footN_Load(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }

        private void txttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txttype.Text == "Rectangle")
            {
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
            }
            if(txttype.Text == "Square")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtLpc.Clear();
            txtBpc.Clear();
            txttpcfinal.Clear();

            txtLrc.Clear();
            txtBrc.Clear();
            txttrcfinal.Clear();

            lblpunch.Text = "";
            lblshear.Text = "";

            txtnum1.Clear();
            txtnum2.Clear();
        }
    }
}
