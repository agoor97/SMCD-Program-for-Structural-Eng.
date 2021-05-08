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
    public partial class deflection : Form
    {
        public deflection()
        {
            InitializeComponent();
        }

        private void txttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txttype.Text == "Flat Slab")
            {
                groubRFT.Enabled = true;
                lblspan.Text = " L (Long) :";
            }

            if (txttype.Text == "Solid Slab")
            {
                groubRFT.Enabled = false;
                lblspan.Text = " L (Short) :";
            }

            if (txttype.Text == "Cantiliver Slab")
            {
                groubRFT.Enabled = true;
                lblspan.Text = " L (Cant.) :";
            }
        }

        private void deflection_Load(object sender, EventArgs e)
        {
            groubRFT.Enabled = true;
            lblspan.Text = " L (Long) :";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txttype.Text.Trim() == "" || txtt.Text.Trim() == "" || txtc.Text.Trim() == "" || txtL.Text.Trim() == ""
                || txtdeltalive.Text.Trim() == "" || txtdeltatotal.Text.Trim() == "" || txtfinish.Text.Trim() == "")
                {
                    MessageBox.Show("Missing Data ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double t = double.Parse(txtt.Text);
                double C = double.Parse(txtc.Text);
                double L = double.Parse(txtL.Text);

                double deltatotal = double.Parse(txtdeltatotal.Text);
                double deltaLive = double.Parse(txtdeltalive.Text);



                double d = t - C;
                /////



                if (txttype.Text == "Flat Slab")
                {
                    if (txtnum.Text.Trim() == "" || txtfai.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtnum.Focus();
                        txtnum.SelectAll();
                        return;
                    }

                    double num = double.Parse(txtnum.Text);
                    double fai = double.Parse(txtfai.Text);

                    double Ascomp = num * (3.1459 * 0.25 * fai * fai);
                    double mio = Ascomp / (1000 * d);

                    double deltaDead = deltatotal - deltaLive;

                    double LTDall = Math.Round((1000 * L / 250), 2);
                    double Liveall = Math.Round((1000 * L / 360), 2);
                    double Partall = Math.Round((1000 * L / 480), 2);



                    ////take epsilon =2;
                    double alfa = 2 / (1 + 50 * mio);
                    double actTotal = Math.Round((alfa * deltaDead + deltatotal), 2);

                    txtactLTD.Text = actTotal.ToString();
                    txtaLLLTD.Text = LTDall.ToString();

                    if (actTotal <= LTDall)
                    {
                        lblLTD.Text = "Safe";
                    }
                    else
                    {
                        lblLTD.Text = "UnSafe";
                    }
                    //////////////

                    txtaLLL.Text = Math.Round(Liveall, 2).ToString();
                    txtactL.Text = txtdeltalive.Text;


                    if (deltaLive <= Liveall)
                    {
                        lblL.Text = "Safe";
                    }
                    else
                    {
                        lblL.Text = "UnSafe";
                    }
                    ///////////////////

                    ////// for partitions

                    double finish = double.Parse(txtfinish.Text);
                    double sus = deltaDead + finish * deltaLive;
                    double deltaPart = Math.Round((deltaLive + alfa * sus), 2);

                    txtactP.Text = deltaPart.ToString();
                    txtaLLP.Text = Partall.ToString();

                    if (deltaPart <= Partall)
                    {
                        lblP.Text = "Safe";
                    }
                    else
                    {
                        lblP.Text = "UnSafe";
                    }

                }

                ///////////////////////////////////////////////

                if (txttype.Text == "Solid Slab")
                {

                    double mio = 0.0;

                    double deltaDead = deltatotal - deltaLive;


                    double LTDall = Math.Round((1000 * L / 250), 2);
                    double Liveall = Math.Round((1000 * L / 360), 2);
                    double Partall = Math.Round((1000 * L / 480), 2);


                    ////take epsilon =2;
                    double alfa = 2 / (1 + 50 * mio);
                    double actTotal = Math.Round((alfa * deltaDead + deltatotal), 2);

                    txtactLTD.Text = actTotal.ToString();
                    txtaLLLTD.Text = LTDall.ToString();

                    if (actTotal <= LTDall)
                    {
                        lblLTD.Text = "Safe";
                    }
                    else
                    {
                        lblLTD.Text = "UnSafe";
                    }
                    //////////////

                    txtaLLL.Text = Math.Round(Liveall, 2).ToString();
                    txtactL.Text = txtdeltalive.Text;


                    if (deltaLive <= Liveall)
                    {
                        lblL.Text = "Safe";
                    }
                    else
                    {
                        lblL.Text = "UnSafe";
                    }
                    ///////////////////

                    ////// for partitions

                    double finish = double.Parse(txtfinish.Text);
                    double sus = deltaDead + finish * deltaLive;
                    double deltaPart = Math.Round((deltaLive + alfa * sus), 2);

                    txtactP.Text = deltaPart.ToString();
                    txtaLLP.Text = Partall.ToString();

                    if (deltaPart <= Partall)
                    {
                        lblP.Text = "Safe";
                    }
                    else
                    {
                        lblP.Text = "UnSafe";
                    }

                }
                //////////////////////////////////////////////////


                if (txttype.Text == "Cantiliver Slab")
                {
                    if (txtnum.Text.Trim() == "" || txtfai.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtnum.Focus();
                        txtnum.SelectAll();
                        return;
                    }

                    double num = double.Parse(txtnum.Text);
                    double fai = double.Parse(txtfai.Text);

                    double Ascomp = num * (3.1459 * 0.25 * fai * fai);
                    double mio = Ascomp / (1000 * d);
                  

                    double deltaDead = deltatotal - deltaLive;


                    double LTDall = Math.Round((1000 * L / 450), 2);
                    double Liveall = Math.Round((1000 * L / 360), 2);
                    double Partall = Math.Round((1000 * L / 480), 2);



                    ////take epsilon =2;
                    double alfa = 2 / (1 + 50 * mio);
                    double actTotal = Math.Round((alfa * deltaDead + deltatotal), 2);

                    txtactLTD.Text = actTotal.ToString();
                    txtaLLLTD.Text = LTDall.ToString();

                    if (actTotal <= LTDall)
                    {
                        lblLTD.Text = "Safe";
                    }
                    else
                    {
                        lblLTD.Text = "UnSafe";
                    }
                    //////////////

                    txtaLLL.Text = Math.Round(Liveall, 2).ToString();
                    txtactL.Text = txtdeltalive.Text;


                    if (deltaLive <= Liveall)
                    {
                        lblL.Text = "Safe";
                    }
                    else
                    {
                        lblL.Text = "UnSafe";
                    }
                    ///////////////////

                    ////// for partitions

                    double finish = double.Parse(txtfinish.Text);
                    double sus = deltaDead + finish * deltaLive;
                    double deltaPart = Math.Round((deltaLive + alfa * sus), 2);

                    txtactP.Text = deltaPart.ToString();
                    txtaLLP.Text = Partall.ToString();

                    if (deltaPart <= Partall)
                    {
                        lblP.Text = "Safe";
                    }
                    else
                    {
                        lblP.Text = "UnSafe";
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtactL.Clear();
            txtactLTD.Clear();
            txtactP.Clear();
            txtaLLL.Clear();
            txtaLLLTD.Clear();
            txtaLLP.Clear();
            lblL.Text = "";
            lblLTD.Text = "";
            lblP.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Check Deflection (Screen)";
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
            codedeflection code = new codedeflection();
            code.ShowDialog();
        }
    }
}
