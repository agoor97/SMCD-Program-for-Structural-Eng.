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
    public partial class columnsp : Form
    {
        
        public columnsp()
        {
            InitializeComponent();
            
        }

        private void txtpmax_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttype.Text == "Rectangle")
                {
                    label14.Text = "t";
                    label18.Enabled = true;
                    txtb1.Enabled = true;
                    label15.Enabled = true;
                    if (txtt1.Text.Trim() == "" || txtb1.Text.Trim() == "" || txtnum1.Text.Trim() == ""
                        || txtfai1.Text.Trim() == "" || txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt1.Focus();
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double t1 = double.Parse(txtt1.Text);
                        double b1 = double.Parse(txtb1.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int num1 = int.Parse(txtnum1.Text);
                        double As1 = num1 * 3.1459 * 0.25 * fai1 * fai1;
                        double Ac1 = b1 * t1 * 1000 * 1000;
                        double pamax = 0.35 * fcu * (Ac1 - As1) + 0.67 * fy * As1;     ///N
                        double P1 = 0.001 * pamax;
                        txtpmax.Text = Math.Round(P1, 2).ToString();
                    }
                }

                if (txttype.Text == "Circular")
                {

                    label14.Text = "D";
                    label18.Enabled = false;
                    txtb1.Enabled = false;
                    label15.Enabled = false;

                    if (txtt1.Text.Trim() == "" || txtnum1.Text.Trim() == ""
                        || txtfai1.Text.Trim() == "" || txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtt1.Focus();
                        return;
                    }
                    else
                    {
                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double D1 = double.Parse(txtt1.Text);

                        int fai1 = int.Parse(txtfai1.Text);
                        int num1 = int.Parse(txtnum1.Text);
                        double As1 = num1 * 3.1459 * 0.25 * fai1 * fai1;
                        double Ac1 = 0.25 * 3.1459 * D1 * D1 * 1000 * 1000;
                        double pamax = 0.35 * fcu * (Ac1 - As1) + 0.67 * fy * As1;     ///N
                        double P1 = 0.001 * pamax;
                        txtpmax.Text = Math.Round(P1, 2).ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void txttype_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {
                if (txttype.Text == "Circular")
                {
                    if (checkBox1.Checked == false)
                    {
                        pictureBox1.Visible = false;
                        pictureBox2.Visible = true;
                        label14.Text = "D :";
                        label8.Text = "D :";
                        txtb1.Enabled = false;
                        txtb.Enabled = false;
                        txtt.Enabled = true;
                    }
                    if (checkBox1.Checked == true)
                    {
                        pictureBox1.Visible = false;
                        pictureBox2.Visible = true;
                        label14.Text = "D :";
                        label8.Text = "D :";
                        txtb1.Enabled = false;
                        txtb.Enabled = false;
                        txtt.Enabled = false;
                    }

                }

                if (txttype.Text == "Rectangle")
                {
                    if (checkBox1.Checked == false)
                    {
                        pictureBox2.Visible = false;
                        pictureBox1.Visible = true;
                        label14.Text = "t :";
                        label8.Text = "t :";
                        txtb1.Enabled = true;
                        txtb.Enabled = true;
                        txtt.Enabled = true;
                    }
                    if (checkBox1.Checked == true)
                    {
                        pictureBox2.Visible = false;
                        pictureBox1.Visible = true;
                        label14.Text = "t :";
                        label8.Text = "t :";
                        txtb1.Enabled = true;
                        txtb.Enabled = false;
                        txtt.Enabled = true;
                    }




                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "End Condition")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                K k1 = new K();
               
                k1.ShowDialog();
                txtk1.Text = k1.txtk.Text;
                labeltype1.Text = k1.labeltype.Text;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttype.Text == "Rectangle")
                {
                    if (checkBox1.Checked == false)
                    {
                        if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txthcol.Text.Trim() == ""
                        || txtp.Text.Trim() == "" || txtt.Text.Trim() == "" || txtb.Text.Trim() == ""
                        || txtfai.Text.Trim() == "" || txtk1.Text.Trim() == "")
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
                            double P = double.Parse(txtp.Text);
                            int fai = int.Parse(txtfai.Text);

                            ////////////////// check Type of Col.
                            string k = txtk1.Text;
                            if (k == "--")
                            {
                                MessageBox.Show("Unallowable End Conditions of Columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtk1.Focus();
                                txtk1.SelectAll();
                                return;
                            }
                            else
                            {
                                double kk = double.Parse(txtk1.Text);
                                double H = double.Parse(txthcol.Text);
                                double mindim = Math.Min(b, t);
                                double Lamda = Math.Round((kk * H / mindim),2);
                                lamda.Text = Lamda.ToString();
                                if (labeltype1.Text == "Braced")
                                {
                                    if (Lamda > 15)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }
                                /////
                                if (labeltype1.Text == "UnBraced")
                                {
                                    if (Lamda > 10)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }

                            }



                            double Ac = b * t * 1000 * 1000;
                            double Asdedign;
                            ///// here Ac is Known
                            double As = (P * 1000 - 0.35 * fcu * Ac) / (0.67 * fy - 0.35 * fcu);
                            double Asmin = 0.8 * 0.01 * b * t * 1000 * 1000;
                            if (Asmin > As)
                            {
                                Asdedign = Asmin;
                            }
                            else
                            {
                                Asdedign = As;
                            }
                            double num1 = Math.Ceiling((Asdedign / (3.1459 * 0.25 * fai * fai)));

                            double Asused = num1 * 0.25 * 3.1459 * fai * fai;
                            double mio = Math.Round((Asused / Ac), 3);
                            txtmiofinal.Text = mio.ToString();
                            txtnum.Text = num1.ToString();


                            //////
                            ///table
                            DialogResult dr;
                            dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.OK)
                            {
                                string serial = "";
                                string bb = txtb.Text;
                                string tt = txtt.Text;
                                string DD = "---";
                                string H = txthcol.Text;
                                string PP = txtp.Text;
                                string Ratio = txtmiofinal.Text;
                                string RFT = txtnum.Text + " T " + txtfai.Text;
                                string Shape = "Rectangle";
                                object[] data = { serial, bb, tt, DD, H, PP, Ratio, RFT, Shape };
                                DataGridView1.Rows.Add(data);
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }



                    }

                    ////////////////////////with known steel ratio
                    if (checkBox1.Checked == true)
                    {

                        if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txthcol.Text.Trim() == ""
                        || txtp.Text.Trim() == "" || txtt.Text.Trim() == "" || txtmio.Text.Trim() == ""
                        || txtfai.Text.Trim() == "" || txtk1.Text.Trim() == "")
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
                            double mio = double.Parse(txtmio.Text);
                            double P = double.Parse(txtp.Text);
                            int fai = int.Parse(txtfai.Text);
                            double b;

                            double Ac = (P * 1000) / (0.35 * fcu * (1 - mio) + 0.67 * fy * mio);
                            b = Ac / (t * 1000);          ///mm جاهزة

                            if (b < 200)
                            {

                                b = 200;
                            }
                            b = Math.Ceiling(b);

                            ////////////////// check Type of Col.
                            string k = txtk1.Text;
                            if (k == "--")
                            {
                                MessageBox.Show("Unallowable End Conditions of Columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtk1.Focus();
                                txtk1.SelectAll();
                                return;
                            }
                            else
                            {
                                double kk = double.Parse(txtk1.Text);
                                double H = double.Parse(txthcol.Text);
                                double mindim = Math.Min(b, t);
                                double Lamda =Math.Round((kk * H / mindim),2);
                                lamda.Text = Lamda.ToString();
                                if (labeltype1.Text == "Braced")
                                {
                                    if (Lamda > 15)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }
                                /////
                                if (labeltype1.Text == "UnBraced")
                                {
                                    if (Lamda > 10)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }

                            }

                            double Acnew = b * t * 1000;
                            double As = (P * 1000 - 0.35 * fcu * Acnew) / (0.67 * fy - 0.35 * fcu);
                            double Asdedign;

                            double Asmin = 0.8 * 0.01 * b * t * 1000;
                            if (Asmin > As)
                            {
                                Asdedign = Asmin;
                            }
                            else
                            {
                                Asdedign = As;
                            }
                            double num1 = Math.Ceiling((Asdedign / (3.1459 * 0.25 * fai * fai)));

                            double Asused = num1 * 0.25 * 3.1459 * fai * fai;
                            double mio2 = Math.Round((Asused / Acnew), 3);
                            txtmiofinal.Text = mio2.ToString();
                            txtnum.Text = num1.ToString();



                            //////
                            ///table
                            DialogResult dr;
                            dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.OK)
                            {

                                string serial = "";
                                string bb = (0.001 * b).ToString();
                                string tt = txtt.Text;
                                string DD = "---";
                                string H = txthcol.Text;
                                string PP = txtp.Text;
                                string Ratio = txtmiofinal.Text;
                                string RFT = txtnum.Text + " T " + txtfai.Text;
                                string Shape = "Rectangle";
                                object[] data = { serial, bb, tt, DD, H, PP, Ratio, RFT, Shape };
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

                /////////////////////////////////////////////////////////////////
                // for Circular Columns 
                if (txttype.Text == "Circular")
                {
                    if (checkBox1.Checked == false)
                    {
                        if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txthcol.Text.Trim() == ""
                        || txtp.Text.Trim() == "" || txtt.Text.Trim() == "" || txtfai.Text.Trim() == ""
                        || txtk1.Text.Trim() == "")
                        {
                            MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtfcu.Focus();
                            return;
                        }
                        else
                        {
                            double fcu = double.Parse(txtfcu.Text);
                            double fy = double.Parse(txtfy.Text);
                            double D = double.Parse(txtt.Text);

                            double P = double.Parse(txtp.Text);
                            int fai = int.Parse(txtfai.Text);

                            ////////////////// check Type of Col.
                            string k = txtk1.Text;
                            if (k == "--")
                            {
                                MessageBox.Show("Unallowable End Conditions of Columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtk1.Focus();
                                txtk1.SelectAll();
                                return;
                            }
                            else
                            {
                                double kk = double.Parse(txtk1.Text);
                                double H = double.Parse(txthcol.Text);
                                double mindim = D;
                                double Lamda =Math.Round((kk * H / mindim),2);
                                lamda.Text = Lamda.ToString();
                                if (labeltype1.Text == "Braced")
                                {
                                    if (Lamda > 12)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }
                                /////
                                if (labeltype1.Text == "UnBraced")
                                {
                                    if (Lamda > 8)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }

                            }



                            double Ac = 3.1459 * D * D * 0.25 * 1000 * 1000;  ///mm
                            double Asdedign;
                            ///// here Ac is Known
                            double As = (P * 1000 - 0.35 * fcu * Ac) / (0.67 * fy - 0.35 * fcu);
                            double Asmin = 0.8 * 0.01 * Ac;
                            if (Asmin > As)
                            {
                                Asdedign = Asmin;
                            }
                            else
                            {
                                Asdedign = As;
                            }
                            double num1 = Math.Ceiling((Asdedign / (3.1459 * 0.25 * fai * fai)));

                            double Asused = num1 * 0.25 * 3.1459 * fai * fai;
                            double mio = Math.Round((Asused / Ac), 3);
                            txtmiofinal.Text = mio.ToString();
                            txtnum.Text = num1.ToString();


                            //////
                            ///table
                            DialogResult dr;
                            dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.OK)
                            {
                                string serial = "";
                                string bb = "---";
                                string tt = "---";
                                string DD = txtt.Text;
                                string H = txthcol.Text;
                                string PP = txtp.Text;
                                string Ratio = txtmiofinal.Text;
                                string RFT = txtnum.Text + " T " + txtfai.Text;
                                string Shape = "Circular";
                                object[] data = { serial, bb, tt, DD, H, PP, Ratio, RFT, Shape };
                                DataGridView1.Rows.Add(data);
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }



                    }

                    ////////////////////////with known steel ratio
                    if (checkBox1.Checked == true)
                    {

                        if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txthcol.Text.Trim() == ""
                        || txtp.Text.Trim() == "" || txtmio.Text.Trim() == ""
                        || txtfai.Text.Trim() == "" || txtk1.Text.Trim() == "")
                        {
                            MessageBox.Show("Missing Data ..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtfcu.Focus();
                            return;
                        }
                        else
                        {
                            double fcu = double.Parse(txtfcu.Text);
                            double fy = double.Parse(txtfy.Text);

                            double mio = double.Parse(txtmio.Text);
                            double P = double.Parse(txtp.Text);
                            int fai = int.Parse(txtfai.Text);


                            double Ac = (P * 1000) / (0.35 * fcu * (1 - mio) + 0.67 * fy * mio);
                            double D = Math.Sqrt(4 * Ac / 3.1459);
                            D = Math.Round(D, 2);  ////D mm
                            if (D < 300)
                            {
                                D = 300;
                            }

                            D = Math.Ceiling(D);

                            ////////////////// check Type of Col.
                            string k = txtk1.Text;
                            if (k == "--")
                            {
                                MessageBox.Show("Unallowable End Conditions of Columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtk1.Focus();
                                txtk1.SelectAll();
                                return;
                            }
                            else
                            {
                                double kk = double.Parse(txtk1.Text);
                                double H = double.Parse(txthcol.Text);
                                double mindim = D;        //Look D is (mm) & H (m)
                                double Lamda = Math.Round((kk * H /(0.001*mindim)),2);
                                lamda.Text = Lamda.ToString();
                                if (labeltype1.Text == "Braced")
                                {
                                    if (Lamda > 12)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }
                                /////
                                if (labeltype1.Text == "UnBraced")
                                {
                                    if (Lamda > 8)
                                    {
                                        MessageBox.Show("Can not be Designed as Short Column .." + "Lamda = " + Lamda, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtk1.Focus();
                                        txtk1.SelectAll();
                                        return;
                                    }
                                }

                            }

                            double Acnew = 0.25 * D * D * 3.1459;        ///mm2
                            double As = (P * 1000 - 0.35 * fcu * Acnew) / (0.67 * fy - 0.35 * fcu);
                            double Asdedign;

                            double Asmin = 0.8 * 0.01 * Acnew;
                            if (Asmin > As)
                            {
                                Asdedign = Asmin;
                            }
                            else
                            {
                                Asdedign = As;
                            }
                            double num1 = Math.Ceiling((Asdedign / (3.1459 * 0.25 * fai * fai)));

                            double Asused = num1 * 0.25 * 3.1459 * fai * fai;
                            double mio2 = Math.Round((Asused / Acnew), 3);
                            txtmiofinal.Text = mio2.ToString();
                            txtnum.Text = num1.ToString();



                            //////
                            ///table
                            DialogResult dr;
                            dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (dr == DialogResult.OK)
                            {

                                string serial = "";
                                string bb = "---";
                                string tt = "---";
                                string DD = (0.001 * D).ToString();
                                string H = txthcol.Text;
                                string PP = txtp.Text;
                                string Ratio = txtmiofinal.Text;
                                string RFT = txtnum.Text + " T " + txtfai.Text;
                                string Shape = "Circular";
                                object[] data = { serial, bb, tt, DD, H, PP, Ratio, RFT, Shape };
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

        private void columnsp_Load(object sender, EventArgs e)
        {
            txtmio.Enabled = false;
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == false)
                {
                    if (txttype.Text == "Rectangle")
                    {
                        txtmio.Enabled = false;
                        txtb.Enabled = true;
                    }
                    if (txttype.Text == "Circular")
                    {
                        txtmio.Enabled = false;
                        txtb.Enabled = false;
                        txtt.Enabled = true;
                    }


                }
                if (checkBox1.Checked == true)
                {
                    if (txttype.Text == "Rectangle")
                    {
                        txtmio.Enabled = true;
                        txtb.Enabled = false;
                    }
                    if (txttype.Text == "Circular")
                    {
                        txtmio.Enabled = true;
                        txtb.Enabled = false;
                        txtt.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("an Option in the main Program if you Want to get Pmax .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }


        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
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

        private void button6_Click_1(object sender, EventArgs e)
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

        private void button7_Click_1(object sender, EventArgs e)
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

        private void button8_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
                codecolumn code = new codecolumn();
               
               code.Show();


            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtnum.Clear();
            txtmiofinal.Clear();
            lamda.Text = "";
        }
    }
}
