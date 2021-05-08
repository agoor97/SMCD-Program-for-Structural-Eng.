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
    public partial class Seismic : Form
    {
        public Seismic()
        {
            InitializeComponent();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }


        double S;
        double TB;
        double TC;
        double TD;
        double ag;
        double Ct;
        double lmda;
        double Sd;
    
        private void button7_Click(object sender, EventArgs e)
        {
          
            int Nofloors = int.Parse(txtnfloors.Text);
            double Wfloor = double.Parse(txtwfloor.Text);
            double Wtotal = Wfloor * Nofloors;
            double Hground = double.Parse(txtHground.Text);
            double Htypical = double.Parse(txtHtypical.Text);

            double Htotal = Hground + (Nofloors - 1) * Htypical;

            double R = double.Parse(txtresponse.Text);
            double Gama = double.Parse(txtimport.Text);
            double Damp = double.Parse(txtdamp.Text);

         
            if(rdiostaticalframe.Checked == true)
            {
                Ct = 0.075;
            }
            if(rdiostaticalother.Checked == true) 
            {
                Ct = 0.05;
            }
            
            
            double T1 = Ct * Math.Pow(Htotal, 0.75);

            

            if(rdiocurve1.Checked == true)
            {
                if(txtsoiltype.Text  == "Soil A")
                {
                    S = 1.0;
                    TB = 0.05;
                    TC = 0.25;
                    TD = 1.20;
                }
                else if(txtsoiltype.Text == "Soil B")
                {
                    S = 1.35;
                    TB = 0.05;
                    TC = 0.25;
                    TD = 1.20;
                }
                else if(txtsoiltype.Text == "Soil C")
                {
                    S = 1.50;
                    TB = 0.10;
                    TC = 0.25;
                    TD = 1.20;
                }
                else if(txtsoiltype.Text == "Soil D")
                {
                    S = 1.80;
                    TB = 0.10;
                    TC = 0.30;
                    TD = 1.20;
                }
               else if(txtsoiltype.Text == "Soil E")
                {
                    S = 1.60;
                    TB = 0.05;
                    TC = 0.25;
                    TD = 1.20;
                }
            }
            //.
            if (rdiocurve2.Checked == true)
            {
                if (txtsoiltype.Text == "Soil A")
                {
                    S = 1.0;
                    TB = 0.15;
                    TC = 0.40;
                    TD = 2.0;
                }
                else if (txtsoiltype.Text == "Soil B")
                {
                    S = 1.20;
                    TB = 0.15;
                    TC = 0.50;
                    TD = 2.0;
                }
               else if (txtsoiltype.Text == "Soil C")
                {
                    S = 1.15;
                    TB = 0.20;
                    TC = 0.60;
                    TD = 2.0;
                }
                else if (txtsoiltype.Text == "Soil D")
                {
                    S = 1.35;
                    TB = 0.20;
                    TC = 0.80;
                    TD = 2.0;
                    
                }
                else if (txtsoiltype.Text == "Soil E")
                {
                    S = 1.40;
                    TB = 0.15;
                    TC = 0.50;
                    TD = 2.0;
                }
            }


            //
           // Get ag 
           if(txtlocation.Text == "Zone 1")
            {
                ag = 0.1 * 9.81;
            }
           if(txtlocation.Text == "Zone 2")
            {
                ag = 0.125 * 9.81;
            }
           if(txtlocation.Text == "Zone 3")
            {
                ag = 0.15 * 9.81;
            }
           if(txtlocation.Text == "Zone 4")
            {
                ag = 0.20 * 9.81;
            }
           if(txtlocation.Text == "Zone 5A")
            {
                ag = 0.25 * 9.81;
            }
           if(txtlocation.Text == "Zone 5B")
            {
                ag = 0.3 * 9.81;
            }


           ///////// Get acceleration Sd(T1)

            if(T1>=0 && T1<=TB)
            {
                Sd = ag * Gama * S * ((2 / 3) + (T1 / TB) * ((2.5 * Damp / R) - (2 / 3)));
            }
            else if(T1>=TB && T1<=TC)
            {
                Sd = ag * Gama * S * (2.5 * Damp / R);
            }
            else if(T1>=TC && T1<=TD)
            {
                double Sd1 = ag * Gama * S * (2.5 * Damp / R) * (TC / T1);
                double Sd2 = 0.2 * ag * Gama;
                Sd = Math.Max(Sd1, Sd2);
            }
            else if (T1 >=TD && T1<=4)
            {
                double Sd11 = ag * Gama * S * (2.5 * Damp / R) * (TC * TD / (T1 * T1));
                double Sd22 = 0.2 * ag * Gama;
                Sd = Math.Max(Sd11, Sd22);
            }
            else
            {
                MessageBox.Show("T1 > 4 sec .. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

       

            //
            // get Lmda
            if(T1 <= (2*TC))
            {
                lmda = 0.85;
            }
            else if (T1 > (2*TC))
            {
                lmda = 1.0;
            }

            // get Fb 
            double Fb = Sd * lmda * Wtotal / 9.81;                    //kN


            
            //// Get Hfinal
            double Htotalfinal;
            double hLoopp = 0;
            int NO = 0;

            double Hfinal = 0;
           
            for (int i =1 ; i <= Nofloors ; i++)
            {
                Htotalfinal = Hground + hLoopp;
                NO = NO + 1;
                hLoopp = NO * Htypical;

                Hfinal = Hfinal + Htotalfinal;
            }

          
            ///// طباعة من اخر دول اللى الدور قبل الاخير 
            double Noloop = Nofloors - 1;
            double Hloop = Htotal;        
            double Qloop = 0.0;
            double Mloop = 0.0;
            double M2 = 0;
            double Q2 = 0;

            for (int j = 1 ; j <= Noloop ; j++)
            {
                string Numb = " Story " + Nofloors.ToString();
                string Wi = Wfloor.ToString();
                string Hi = Hloop.ToString();
                string wihi = (Wfloor * Hloop).ToString();
                string Fi = Math.Round((Fb * (Hloop / Hfinal)), 2).ToString();

                ////////
                // help Calculations
                double QII = (Fb * (Hloop / Hfinal)) + Qloop;
                double HII = Htypical;
                ////////

                string Qi = Math.Round(QII, 2).ToString();
                string Mi = Math.Round(((QII * HII) + Mloop), 2).ToString();

                object[] data = { Numb, Wi, Hi, wihi, Fi, Qi, Mi };
                DataGridView1.Rows.Add(data);

                Hloop = Hloop - Htypical;

                Qloop = double.Parse(Qi);
                Mloop = double.Parse(Mi);
                Nofloors = Nofloors - 1;


                /// هستخدمهم علشان اطبع الدور الاول واكمل من عليهم يعني 
                M2 = double.Parse(Mi);
                Q2 = double.Parse(Qi);

            }
           

            //// Ground Floor 
            string serial = " Story 1 ";
            string w11 = Wfloor.ToString();
            string h11 = Hground.ToString();
            string wh = (Wfloor * Hground).ToString();
            string f1 = Math.Round((Fb * (Hground / Hfinal)), 2).ToString();

            /////////  
            double Q1 = (Fb * (Hground / Hfinal));           
            double H1 = Hground;
            ////////////

            string Q11 = Math.Round((Q1 + Q2), 2).ToString();

            ////for Help
            double Q111 = double.Parse(Q11);
            //////

            string M11 = Math.Round(((Q111 * H1) + M2), 2).ToString();
        
            object[] data1 = {serial,w11,h11,wh,f1,Q11,M11 };
            DataGridView1.Rows.Add(data1);

            //


         

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

        private void button9_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DataGridView1.Rows.Clear();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(DataGridView1 !=null)
            {
                DataGridView1.Rows.Clear();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zone zone = new zone();
            zone.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Damp damp = new Damp();
            damp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            soiltype soil = new soiltype();
            soil.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            response R = new response();
            R.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            import gama = new import();
            gama.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            alfa load = new alfa();
            load.ShowDialog();
        }
    }
}
