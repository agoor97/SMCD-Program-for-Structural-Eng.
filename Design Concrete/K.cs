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
    public partial class K : Form
    {
        public K()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radiounbraced.Checked==true)
            {
                if(radiocase1L.Checked ==true && radiocase1U.Checked==true)
                {
                    txtk.Text = "1.2";
                }
                if (radiocase1L.Checked == true && radiocase2U.Checked == true)
                {
                    txtk.Text = "1.3";
                }
                if (radiocase1L.Checked == true && radiocase3U.Checked == true)
                {
                    txtk.Text = "1.6";
                }
                if (radiocase1L.Checked == true && radiocase4U.Checked == true)
                {
                    txtk.Text = "2.2";
                }
                if (radiocase2L.Checked == true && radiocase1U.Checked == true)
                {
                    txtk.Text = "1.3";
                }
                if (radiocase2L.Checked == true && radiocase2U.Checked == true)
                {
                    txtk.Text = "1.5";
                }
                if (radiocase2L.Checked == true && radiocase3U.Checked == true)
                {
                    txtk.Text = "1.8";
                }
                if (radiocase2L.Checked == true && radiocase4U.Checked == true)
                {
                    txtk.Text = "--";
                }
                if (radiocase3L.Checked == true && radiocase1U.Checked == true)
                {
                    txtk.Text = "1.6";
                }
                if (radiocase3L.Checked == true && radiocase2U.Checked == true)
                {
                    txtk.Text = "1.8";
                }
                if (radiocase3L.Checked == true && radiocase3U.Checked == true)
                {
                    txtk.Text = "--";
                }
                if (radiocase3L.Checked == true && radiocase4U.Checked == true)
                {
                    txtk.Text = "--";
                }
            }

            /////////////////////////////////
            if (radiobraced.Checked == true)
            {
                if (radiocase1L.Checked == true && radiocase1U.Checked == true)
                {
                    txtk.Text = "0.75";
                }
                if (radiocase1L.Checked == true && radiocase2U.Checked == true)
                {
                    txtk.Text = "0.8";
                }
                if (radiocase1L.Checked == true && radiocase3U.Checked == true)
                {
                    txtk.Text = "0.9";
                }
                if (radiocase1L.Checked == true && radiocase4U.Checked == true)
                {
                    txtk.Text = "--";
                }
                if (radiocase2L.Checked == true && radiocase1U.Checked == true)
                {
                    txtk.Text = "0.8";
                }
                if (radiocase2L.Checked == true && radiocase2U.Checked == true)
                {
                    txtk.Text = "0.85";
                }
                if (radiocase2L.Checked == true && radiocase3U.Checked == true)
                {
                    txtk.Text = "0.95";
                }
                if (radiocase2L.Checked == true && radiocase4U.Checked == true)
                {
                    txtk.Text = "--";
                }
                if (radiocase3L.Checked == true && radiocase1U.Checked == true)
                {
                    txtk.Text = "0.9";
                }
                if (radiocase3L.Checked == true && radiocase2U.Checked == true)
                {
                    txtk.Text = "0.95";
                }
                if (radiocase3L.Checked == true && radiocase3U.Checked == true)
                {
                    txtk.Text = "1.0";
                }
                if (radiocase3L.Checked == true && radiocase4U.Checked == true)
                {
                    txtk.Text = "--";
                }
            }
            ////
            if (radiobraced.Checked == true)
            {
                labeltype.Text = "Braced";
            }
            if (radiounbraced.Checked == true)
            {
                labeltype.Text = "UnBraced";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void labeltype_Click(object sender, EventArgs e)
        {
            
        }
    }
}
