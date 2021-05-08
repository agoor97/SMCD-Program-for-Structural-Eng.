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
    public partial class alfab : Form
    {
        PMM PMM;
        public alfab(PMM PMM1)
        {
            InitializeComponent();
            PMM = PMM1;
        }

        private void alfab_Load(object sender, EventArgs e)
        {
            try
            {
                txtalfasend.Focus();


                if (PMM.radioButton2.Checked == true)
                {

                    double t = double.Parse(PMM.txtt.Text);
                    double b = double.Parse(PMM.txtb.Text);
                    double fcu = double.Parse(PMM.txtfcu.Text);
                    double Mx = double.Parse(PMM.txtmx.Text);
                    double My = double.Parse(PMM.txtmy.Text);
                    double aa = t - 50;
                    double bb = b - 50;
                    double P = double.Parse(PMM.txtp.Text);
                    double Rb = (P * 1000) / (fcu * b * t);
                    double smaller1 = (Mx / aa) / (My / bb);
                    double smaller2 = (My / bb) / (Mx / aa);
                    double smaller = Math.Min(smaller1, smaller2);
                    txtalfa1.Text = Math.Round(smaller, 2).ToString();
                    txtalfa2.Text = Math.Round(Rb, 2).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
