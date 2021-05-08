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
    public partial class solid : Form
    {
        public solid()
        {
            InitializeComponent();
        }

        private void solid_Load(object sender, EventArgs e)
        {
            txtmy.Enabled = false;
            txtLy.Enabled = false;
            txtnum2.Enabled = false;
            txtfai2.Enabled = false;

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {

                ///////////// هنا يتم التصميم 

                if (comboBox1.Text == "One_Way ")
                {

                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtc.Text.Trim() == ""
                     || txtts.Text.Trim() == "" || txtmx.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfcu.Focus();
                        return;
                    }
                    else
                    {

                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double ts = double.Parse(txtts.Text);
                        double cov = double.Parse(txtc.Text);
                        // one way solid slab 
                        txtmy.Enabled = false;
                        txtLy.Enabled = false;
                        txtnum2.Enabled = false;
                        txtfai2.Enabled = false;
                        double mx = double.Parse(txtmx.Text);
                        double Lx = double.Parse(txtLx.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        double d = ts - cov;
                        double comp = d * (1 - Math.Sqrt(1 - ((2 * mx * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * d * d))));
                        double C = 1.25 * comp;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)
                        {
                            J = 0.826;
                        }
                        double Asx = (mx * Math.Pow(10, 6)) / (fy * J * d);
                        double num1 = Math.Ceiling((Asx / (3.1459 * 0.25 * fai1 * fai1)));
                        if (num1 < 5)
                        {
                            num1 = 5;
                        }
                        ////   

                        txtnum1.Text = num1.ToString();


                        ///////////////////////////////
                        ////table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if(dr == DialogResult.OK)
                        {

                            string serial = "";
                            string depth = d.ToString();
                            string Lxx = txtLx.Text;
                            string Lyy = "----";
                            string Mux = txtmx.Text;
                            string Muy = "----";
                            string ASX = num1.ToString() + " T " + txtfai1.Text;
                            string ASY = "----";
                            string type = "One Way";

                            object[] data = { serial, depth, Lxx, Lyy, Mux, Muy, ASX, ASY, type };
                            DataGridView1.Rows.Add(data);
                            return;

                        }
                        else
                        {
                            return;
                        }

                        

                    }

                }



                //////// Two way 



                if (comboBox1.Text == "Two_Way ")
                {
                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtmx.Text.Trim() == ""
                        || txtts.Text.Trim() == "" || txtc.Text.Trim() == "" || txtmyy.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ... ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfcu.Focus();
                        return;
                    }
                    else
                    {

                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double ts = double.Parse(txtts.Text);
                        double cov = double.Parse(txtc.Text);
                        double mx = double.Parse(txtmx.Text);
                        double my = double.Parse(txtmyy.Text);
                        double Lx = double.Parse(txtLx.Text);
                        double Ly = double.Parse(txtLy.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        int fai2 = int.Parse(txtfai2.Text);
                        
                        double d = ts - cov;
                        double comp1 = d * (1 - Math.Sqrt(1 - ((2 * mx * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * d * d))));
                        double comp2 = d * (1 - Math.Sqrt(1 - ((2 * my * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * d * d))));
                        double C1 = 1.25 * comp1;
                        double C2 = 1.25 * comp2;
                        double J1 = (1 / 1.15) * (1 - 0.4 * (C1 / d));
                        double J2 = (1 / 1.15) * (1 - 0.4 * (C2 / d));
                        if (J1 > 0.826)
                        {
                            J1 = 0.826;
                        }
                        if (J2 > 0.826)
                        {
                            J2 = 0.826;
                        }
                        double Asx = (mx * Math.Pow(10, 6)) / (fy * J1 * d);
                        double Asy = (my * Math.Pow(10, 6)) / (fy * J2 * d);
                        double num1 = Math.Ceiling((Asx / (3.1459 * 0.25 * fai1 * fai1)));
                        double num2 = Math.Ceiling((Asy / (3.1459 * 0.25 * fai2 * fai2)));
                        if (num1 < 5)
                        {
                            num1 = 5;
                        }
                        if (num2 < 5)
                        {
                            num2 = 5;
                        }
                        ////   

                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();




                        /////////////// 
                        //// Table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if(dr == DialogResult.OK)
                        {
                            string serial = "";
                            string depth = d.ToString();
                            string Lxx = txtLx.Text;
                            string Lyy = txtLy.Text;
                            string Mux = txtmx.Text;
                            string Muy = txtmyy.Text;
                            string ASX = num1.ToString() + " T " + txtfai1.Text;
                            string ASY = num2.ToString() + " T " + txtfai2.Text;
                            string type = "Two Way";

                            object[] data = { serial, depth, Lxx, Lyy, Mux, Muy, ASX, ASY, type };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }

                    }


                }



                ////// Cantiliver Slab 

                if (comboBox1.Text == "Canti. ")
                {

                    if (txtfcu.Text.Trim() == "" || txtfy.Text.Trim() == "" || txtc.Text.Trim() == ""
                     || txtts.Text.Trim() == "" || txtmx.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtfcu.Focus();
                        return;
                    }
                    else
                    {

                        double fcu = double.Parse(txtfcu.Text);
                        double fy = double.Parse(txtfy.Text);
                        double ts = double.Parse(txtts.Text);
                        double cov = double.Parse(txtc.Text);
                        // Cantiliver 

                        txtmy.Enabled = false;
                        txtLy.Enabled = false;
                        txtnum2.Enabled = false;
                        txtfai2.Enabled = false;

                        double mx = double.Parse(txtmx.Text);
                        double Lx = double.Parse(txtLx.Text);
                        int fai1 = int.Parse(txtfai1.Text);
                        double d = ts - cov;
                        double comp = d * (1 - Math.Sqrt(1 - ((2 * mx * Math.Pow(10, 6)) / (0.45 * fcu * 1000 * d * d))));
                        double C = 1.25 * comp;
                        double J = (1 / 1.15) * (1 - 0.4 * (C / d));
                        if (J > 0.826)
                        {
                            J = 0.826;
                        }
                        double Asx = (mx * Math.Pow(10, 6)) / (fy * J * d);
                        double num1 = Math.Ceiling((Asx / (3.1459 * 0.25 * fai1 * fai1)));
                        if (num1 < 5)
                        {
                            num1 = 5;
                        }
                        ////   

                        txtnum1.Text = num1.ToString();

                        //// get minimum thickness هحسبها هنا مباشرة ةهقفل اللي تحت 

                        txtLeff.Text = txtLx.Text;
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 10), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();

                        ////////////////////
                        ////table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if(dr==DialogResult.OK)
                        {
                            string serial = "";
                            string depth = d.ToString();
                            string Lxx = txtLx.Text;
                            string Lyy = "----";
                            string Mux = txtmx.Text;
                            string Muy = "----";
                            string ASX = num1.ToString() + " T " + txtfai1.Text;
                            string ASY = "----";
                            string type = "Cant.";

                            object[] data = { serial, depth, Lxx, Lyy, Mux, Muy, ASX, ASY, type };
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

        private void btn2_Click(object sender, EventArgs e)
        {       
        }

        private void solid_Resize(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ///////////////////////// هنا يتم التحكم عند اختيار نوع البلاطة من غلق وفتح للعناصر 


                if (comboBox1.Text == "One_Way ")
                {
                    pictureBox3.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    txtmy.Enabled = false;
                    txtLy.Enabled = false;
                    txtnum2.Enabled = false;
                    txtfai2.Enabled = false;
                    lblx.Text = "Lx :";
                    txtend.Enabled = true;
                }

                if (comboBox1.Text == "Canti. ")
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    txtmy.Enabled = false;
                    txtLy.Enabled = false;
                    txtnum2.Enabled = false;
                    txtfai2.Enabled = false;
                    lblx.Text = "Lc :";
                    txtend.Enabled = false;

                }


                if (comboBox1.Text == "Two_Way ")
                {
                    pictureBox2.Visible = true;
                    pictureBox1.Visible = false;
                    pictureBox3.Visible = false;
                    txtmy.Enabled = true;
                    txtLy.Enabled = true;
                    txtnum2.Enabled = true;
                    txtfai2.Enabled = true;
                    lblx.Text = "Lx :";
                    txtend.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtend_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                /////////////////////////// هنا يتم حساب ts min  للبلاطات 
                /////////////////////// تم حسابها للبلاطة المستمرة من ناحين ومن ناحيتين 
                /////////////////////// البلاطة الكابولية تم حسابها مباشرة عند الضغط على زر التصميم 
                /////////////////////// لحساب هنا البلاطات المستمرة من ناحين ومن ناحيتين يجب تحديد النهايات 
                /////////////////////// اختيار النهاية يتم على اساس الطول الاصغر ف البلاطة 


                if (comboBox1.Text == "One_Way ")
                {
                    if (txtend.Text == "Simple ")
                    {
                        txtLeff.Text = txtLx.Text;
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 30), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();
                    }

                    if (txtend.Text == "Cont. (1)")
                    {
                        double Lx = double.Parse(txtLx.Text);
                        txtLeff.Text = (0.87 * Lx).ToString();
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 35), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();
                    }

                    if (txtend.Text == "Cont. (2)")
                    {
                        double Lx = double.Parse(txtLx.Text);
                        txtLeff.Text = (0.76 * Lx).ToString();
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 40), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();
                    }

                }


                ////// for two way 

                if (comboBox1.Text == "Two_Way ")
                {
                    if (txtend.Text == "Simple ")
                    {
                        double Lx = double.Parse(txtLx.Text);
                        double Ly = double.Parse(txtLy.Text);
                        double Lmin = Math.Min(Lx, Ly);
                        txtLeff.Text = Lmin.ToString();
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 35), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();
                    }

                    if (txtend.Text == "Cont. (1)")
                    {
                        double Lx = double.Parse(txtLx.Text);
                        double Ly = double.Parse(txtLy.Text);
                        double Lmin = Math.Min(Lx, Ly);
                        txtLeff.Text = (0.87 * Lmin).ToString();
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 40), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();
                    }

                    if (txtend.Text == "Cont. (2)")
                    {
                        double Lx = double.Parse(txtLx.Text);
                        double Ly = double.Parse(txtLy.Text);
                        double Lmin = Math.Min(Lx, Ly);
                        txtLeff.Text = (0.76 * Lmin).ToString();
                        double L = double.Parse(txtLeff.Text);
                        double tsmin = Math.Round(((L * 1000) / 45), 2);
                        if (tsmin < 80)
                        {
                            tsmin = 80;
                        }
                        txttsmin.Text = tsmin.ToString();
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            


        }

        private void btn4_Click(object sender, EventArgs e)
        {
            bool ISOPEN = false;
            foreach(Form f in Application.OpenForms)
            {
                if(f.Text == "ECP 203 -2018")
                {
                    ISOPEN = true;
                    f.Focus();
                    break;
                }
            }
            if(ISOPEN == false)
            {
                codesolid code = new codesolid();
                
                code.Show();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose End Condition for Short Length of Slab to get the minimum ts to be safe Deflection .. , Where (Leff) represents the distance between Overturning Points ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ToExcel(DataGridView dgv, string filename )
        {
            string stoutput = "";
            string sheader = "";
            for (int j = 0; j < dgv.Columns.Count; j++)
                sheader = sheader.ToString() + Convert.ToString(dgv.Columns[j].HeaderText)+ "\t" ;
                 stoutput += sheader+ "\r\n" ;

            for(int i = 0 ; i <dgv.RowCount-1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dgv.Rows[i].Cells[j].Value)+ "\t" ;
                stoutput += stLine+ "\r\n" ;
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stoutput);
            FileStream fs = new FileStream(filename,FileMode.Create);
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
                ToExcel(DataGridView1 , path);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtnum1.Clear();
            txtnum2.Clear();
            txtLeff.Clear();
            txttsmin.Clear();
        }
    }
}
