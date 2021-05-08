using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace Design_Concrete
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

 
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/civilianoagoor?ref=bookmarks");
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.youtube.com/Mohammed%20Agoor?fbclid=IwAR0acitA76g9hUnXnNbd1EJgCwf_3OWFZ7FpMZC1BPGMjYtMrRkagpo1hKk");
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/civilianoagoor/?fbclid=IwAR0zRyznQDAByaOJPgGvYHCjufrfwS0EZCs6ViOUtcAlZ-vkRLykfhAlklg");
        }
    }
}
