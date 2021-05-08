using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Concrete
{
    public partial class columnpunch : Form
    {
        public columnpunch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfcu.Text.Trim() == "" || txtC1.Text.Trim() == "" || txtC2.Text.Trim() == "" || txtcover.Text.Trim() == ""
                || txtR.Text.Trim() == "" || txtt.Text.Trim() == "" || txtposition.Text.Trim() == "" || txtshape.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ... ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                double fcu = double.Parse(txtfcu.Text);
                double c1 = double.Parse(txtC1.Text);
                double c2 = double.Parse(txtC2.Text);
                double R = double.Parse(txtR.Text);
                double ts = double.Parse(txtt.Text);
                double cover = double.Parse(txtcover.Text);

                double d = ts - cover;


                //////////////////get allowable punching

                double qall1 = 1.70;
                double qall2 = 0.316 * Math.Sqrt(fcu / 1.5);
              
                double qpu = Math.Min(qall1, qall2);

                qpu = Math.Round(qpu, 2);

                txtqall.Text = qpu.ToString();




                /////////////get actual
                if (txtshape.Text == "Rectangle")
                {
                    if (txtposition.Text == "Interior")
                    {

                        double A0 = 2 * d * (c1 + c2 + 2 * d);
                        double qact = Math.Round((R * 1000 * 1.15 / A0), 2);

                        double ratio = qact / qpu;

                        txtqact.Text = qact.ToString();
                        txtrario.Text = Math.Round(ratio, 3).ToString();

                        if (ratio <= 1.0)
                        {
                            lblnote.Text = "Safe";
                        }
                        else
                        {
                            lblnote.Text = "UnSafe";
                        }
                        ////
                    }



                    //////////////////
                    if (txtposition.Text == "Edge")
                    {

                        double A0 = d * (2 * (c1 + (d / 2)) + (c2 + d));
                        double qact = Math.Round((R * 1000 * 1.30 / A0), 2);

                        double ratio = qact / qpu;

                        txtqact.Text = qact.ToString();
                        txtrario.Text = Math.Round(ratio, 3).ToString();

                        if (ratio <= 1.0)
                        {
                            lblnote.Text = "Safe";
                        }
                        else
                        {
                            lblnote.Text = "UnSafe";
                        }
                        ////
                    }

                    ///////////////////////

                    if (txtposition.Text == "Corner")
                    {

                        double A0 = d * ((c1 + (d / 2)) + (c2 + (d / 2)));
                        double qact = Math.Round((R * 1000 * 1.50 / A0), 2);

                        double ratio = qact / qpu;

                        txtqact.Text = qact.ToString();
                        txtrario.Text = Math.Round(ratio, 3).ToString();

                        if (ratio <= 1.0)
                        {
                            lblnote.Text = "Safe";
                        }
                        else
                        {
                            lblnote.Text = "UnSafe";
                        }
                        ////
                    }


                }

                ////////////////////////// finish rectangle column
                if (txtshape.Text == "Circular")
                {
                    if (txtposition.Text == "Interior")
                    {
                        MessageBox.Show(c1.ToString());

                        double A0 = 3.1459 * (c1 + d) * d;
                        double qact = Math.Round((R * 1000 * 1.15 / A0), 2);

                        double ratio = qact / qpu;

                        txtqact.Text = qact.ToString();
                        txtrario.Text = Math.Round(ratio, 3).ToString();

                        if (ratio <= 1.0)
                        {
                            lblnote.Text = "Safe";
                        }
                        else
                        {
                            lblnote.Text = "UnSafe";
                        }
                        ////
                    }



                    ////

                    //////////////////
                    if (txtposition.Text == "Edge")
                    {
                        MessageBox.Show("Edge Circular Column .. this Option is Not avilable to be Checked", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    ///////////////////////

                    if (txtposition.Text == "Corner")
                    {
                        MessageBox.Show("Corner Circular Column .. this Option is Not avilable to be Checked", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void txtposition_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {
                if (txtposition.Text == "Interior" && txtshape.Text == "Rectangle")
                {
                    pictureBox3.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox4.Visible = false;

                    lblc1.Text = "C1 :";
                    txtC2.Enabled = true;
                    lblc2.Enabled = true;
                }

                if (txtposition.Text == "Edge" && txtshape.Text == "Rectangle")
                {
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox4.Visible = false;

                    lblc1.Text = "C1 :";
                    txtC2.Enabled = true;
                    lblc2.Enabled = true;
                }

                if (txtposition.Text == "Corner" && txtshape.Text == "Rectangle")
                {
                    pictureBox1.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox4.Visible = false;


                    lblc1.Text = "C1 :";
                    txtC2.Enabled = true;
                    lblc2.Enabled = true;
                }



                /////////////////////////////////////////////


                if (txtposition.Text == "Interior" && txtshape.Text == "Circular")
                {
                    pictureBox4.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox3.Visible = false;

                    lblc1.Text = "D :";
                    txtC2.Enabled = false;
                    lblc2.Enabled = false;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        private void txtshape_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtshape.Text == "Rectangle")
            {
                if (txtposition.Text == "Interior")
                {
                    pictureBox3.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox4.Visible = false;

                    lblc1.Text = "C1 :";
                    txtC2.Enabled = true;
                    lblc2.Enabled = true;
                }

                if (txtposition.Text == "Edge")
                {
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox4.Visible = false;

                    lblc1.Text = "C1 :";
                    txtC2.Enabled = true;
                    lblc2.Enabled = true;
                }

                if (txtposition.Text == "Corner")
                {
                    pictureBox1.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox4.Visible = false;

                    lblc1.Text = "C1 :";
                    txtC2.Enabled = true;
                    lblc2.Enabled = true;
                }

            }
            //////////////////////////////////////////////
            if (txtshape.Text == "Circular")
            {
                if (txtposition.Text == "Interior")
                {
                    pictureBox4.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox3.Visible = false;

                    lblc1.Text = "D :";
                    txtC2.Enabled = false;
                    lblc2.Enabled = false;
                }

              

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtqact.Clear();
            txtqall.Clear();
            txtrario.Clear();
            lblnote.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Check Column Punching (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void columnpunch_Load(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            pictureBox4.Visible = false;


            lblc1.Text = "C1 :";
            txtC2.Enabled = true;
            lblc2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Only Circular Columns for Interior Positions", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            codecolumnpunch code = new codecolumnpunch();
            code.ShowDialog();
        }
    }
}
