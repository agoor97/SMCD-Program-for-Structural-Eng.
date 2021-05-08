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
    public partial class getKlong : Form
    {
        public getKlong()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(txtkin1.Text == "" || txtkout1.Text == "")
            {
                Close();
            }
            else
            {
                if (txtkin1.Text == "--" || txtkout1.Text == "--")
                {
                    MessageBox.Show("Not Suitable End Conditions .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(rdiobraced.Checked == true)
            {
                if(rdio1inL.Checked == true && rdio1inU.Checked == true)
                {
                    txtkin1.Text = "0.75";
                }
                if (rdio1inL.Checked == true && rdio2inU.Checked == true)
                {
                    txtkin1.Text = "0.8";
                }
                if (rdio1inL.Checked == true && rdio3inU.Checked == true)
                {
                    txtkin1.Text = "0.9";
                }
                if (rdio1inL.Checked == true && rdio4inU.Checked == true)
                {
                    txtkin1.Text = "--";
                }
                if (rdio2inL.Checked == true && rdio1inU.Checked == true)
                {
                    txtkin1.Text = "0.8";
                }
                if (rdio2inL.Checked == true && rdio2inU.Checked == true)
                {
                    txtkin1.Text = "0.85";
                }
                if (rdio2inL.Checked == true && rdio3inU.Checked == true)
                {
                    txtkin1.Text = "0.95";
                }
                if (rdio2inL.Checked == true && rdio4inU.Checked == true)
                {
                    txtkin1.Text = "--";
                }
                if (rdio3inL.Checked == true && rdio1inU.Checked == true)
                {
                    txtkin1.Text = "0.9";
                }
                if (rdio3inL.Checked == true && rdio2inU.Checked == true)
                {
                    txtkin1.Text = "0.95";
                }
                if (rdio3inL.Checked == true && rdio3inU.Checked == true)
                {
                    txtkin1.Text = "1.0";
                }
                if (rdio3inL.Checked == true && rdio4inU.Checked == true)
                {
                    txtkin1.Text = "--";
                }
            }

            if(rdiounbraced.Checked == true)
            {
                if (rdio1inL.Checked == true && rdio1inU.Checked == true)
                {
                    txtkin1.Text = "1.2";
                }
                if (rdio1inL.Checked == true && rdio2inU.Checked == true)
                {
                    txtkin1.Text = "1.3";
                }
                if (rdio1inL.Checked == true && rdio3inU.Checked == true)
                {
                    txtkin1.Text = "1.6";
                }
                if (rdio1inL.Checked == true && rdio4inU.Checked == true)
                {
                    txtkin1.Text = "2.2";
                }
                if (rdio2inL.Checked == true && rdio1inU.Checked == true)
                {
                    txtkin1.Text = "1.3";
                }
                if (rdio2inL.Checked == true && rdio2inU.Checked == true)
                {
                    txtkin1.Text = "1.5";
                }
                if (rdio2inL.Checked == true && rdio3inU.Checked == true)
                {
                    txtkin1.Text = "1.8";
                }
                if (rdio2inL.Checked == true && rdio4inU.Checked == true)
                {
                    txtkin1.Text = "--";
                }
                if (rdio3inL.Checked == true && rdio1inU.Checked == true)
                {
                    txtkin1.Text = "1.6";
                }
                if (rdio3inL.Checked == true && rdio2inU.Checked == true)
                {
                    txtkin1.Text = "1.8";
                }
                if (rdio3inL.Checked == true && rdio3inU.Checked == true)
                {
                    txtkin1.Text = "--";
                }
                if (rdio3inL.Checked == true && rdio4inU.Checked == true)
                {
                    txtkin1.Text = "--";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(rdiobraced.Checked == true)
            {
                if(rdio1Lout.Checked == true && rdio1Uout.Checked == true)
                {
                    txtkout1.Text = "0.75";
                }
                if (rdio1Lout.Checked == true && rdio2Uout.Checked == true)
                {
                    txtkout1.Text = "0.8";
                }
                if (rdio1Lout.Checked == true && rdio3Uout.Checked == true)
                {
                    txtkout1.Text = "0.9";
                }
                if (rdio1Lout.Checked == true && rdio4Uout.Checked == true)
                {
                    txtkout1.Text = "--";
                }
                if (rdio2Lout.Checked == true && rdio1Uout.Checked == true)
                {
                    txtkout1.Text = "0.8";
                }
                if (rdio2Lout.Checked == true && rdio2Uout.Checked == true)
                {
                    txtkout1.Text = "0.85";
                }
                if (rdio2Lout.Checked == true && rdio3Uout.Checked == true)
                {
                    txtkout1.Text = "0.95";
                }
                if (rdio2Lout.Checked == true && rdio4Uout.Checked == true)
                {
                    txtkout1.Text = "--";
                }
                if (rdio3Lout.Checked == true && rdio1Uout.Checked == true)
                {
                    txtkout1.Text = "0.9";
                }
                if (rdio3Lout.Checked == true && rdio2Uout.Checked == true)
                {
                    txtkout1.Text = "0.95";
                }
                if (rdio3Lout.Checked == true && rdio3Uout.Checked == true)
                {
                    txtkout1.Text = "1.0";
                }
                if (rdio3Lout.Checked == true && rdio4Uout.Checked == true)
                {
                    txtkout1.Text = "--";
                }
            }

            ////
            if (rdiounbraced.Checked == true)
            {
                if (rdio1Lout.Checked == true && rdio1Uout.Checked == true)
                {
                    txtkout1.Text = "1.2";
                }
                if (rdio1Lout.Checked == true && rdio2Uout.Checked == true)
                {
                    txtkout1.Text = "1.3";
                }
                if (rdio1Lout.Checked == true && rdio3Uout.Checked == true)
                {
                    txtkout1.Text = "1.6";
                }
                if (rdio1Lout.Checked == true && rdio4Uout.Checked == true)
                {
                    txtkout1.Text = "2.2";
                }
                if (rdio2Lout.Checked == true && rdio1Uout.Checked == true)
                {
                    txtkout1.Text = "1.3";
                }
                if (rdio2Lout.Checked == true && rdio2Uout.Checked == true)
                {
                    txtkout1.Text = "1.5";
                }
                if (rdio2Lout.Checked == true && rdio3Uout.Checked == true)
                {
                    txtkout1.Text = "1.8";
                }
                if (rdio2Lout.Checked == true && rdio4Uout.Checked == true)
                {
                    txtkout1.Text = "--";
                }
                if (rdio3Lout.Checked == true && rdio1Uout.Checked == true)
                {
                    txtkout1.Text = "1.6";
                }
                if (rdio3Lout.Checked == true && rdio2Uout.Checked == true)
                {
                    txtkout1.Text = "1.8";
                }
                if (rdio3Lout.Checked == true && rdio3Uout.Checked == true)
                {
                    txtkout1.Text = "--";
                }
                if (rdio3Lout.Checked == true && rdio4Uout.Checked == true)
                {
                    txtkout1.Text = "--";
                }
            }


        }
    }
}
