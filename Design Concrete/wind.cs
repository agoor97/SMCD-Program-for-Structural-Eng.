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
    public partial class wind : Form
    {
        public wind()
        {
            InitializeComponent();
        }


     
       
       
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtL1.Text.Trim()==""||txtL2.Text.Trim()==""||txtv.Text.Trim()==""||txtHground.Text.Trim()==""
                    ||txtHtypical.Text.Trim()==""||txtnfloors.Text.Trim()==""||txtHbase.Text.Trim()=="")
                {
                    MessageBox.Show("Missing Data ... ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                double L1 = double.Parse(txtL1.Text);
                double L2 = double.Parse(txtL2.Text);

                double HGround = double.Parse(txtHground.Text);
                double Hbase = double.Parse(txtHbase.Text);
                double Htypical = double.Parse(txtHtypical.Text);
                double V = double.Parse(txtv.Text);
                double NoFloors = double.Parse(txtnfloors.Text);

                double habove =  HGround + (NoFloors - 1) * Htypical;
                double Htotal = Hbase + habove;

                double Ce = 1.3;

           
              

                ////// for typical
                double k;
                double yloop = habove;
                double Number = NoFloors;

                double FtotalX = 0;
                double FtotalY = 0;
                double MtotalX = 0;
                double MtotalY = 0;


                for ( int J = 1 ; J < NoFloors ; J++)
                {
                    ////

                    string serial = " Story " +  Number.ToString();
                    string H2 = yloop.ToString();


                    //
                    if (yloop >= 0  && yloop <= 10)
                    {
                        k = 1.0;
                    }
                    else if ( yloop  >= 10 && yloop <= 20)
                    {
                        k = 1.15;
                    }
                    else if ( yloop >= 20 && yloop <= 30)
                    {
                        k = 1.40;
                    }
                    else if ( yloop >= 30 && yloop <= 50 )
                    {
                        k = 1.60;
                    }
                    else if ( yloop >= 50 && yloop <= 80)
                    {
                        k = 1.85;
                    }
                    else if ( yloop >= 80 && yloop <= 120)
                    {
                        k = 2.10;
                    }
                    else if ( yloop >= 120 && yloop <= 160)
                    {
                        k = 2.30;
                    }
                    else
                    {
                        k = 2.5;
                    }


                    /////
                    double Px = Ce * k * 6.25 * Math.Pow(10, -4) * V * V;
                    double Py = Ce * k * 6.25 * Math.Pow(10, -4) * V * V;
                    double Fxx = Px * Htypical * L2;
                    double Fyy = Px * Htypical * L1;


                    string Wx2 =Math.Round((Fxx),2).ToString();
                    string Wy2 = Math.Round((Fyy), 2).ToString();

                    object[] data2 = { serial, H2, k, Wx2, Wy2 };
                    DataGridView1.Rows.Add(data2);

                    //// get  total force 
                    FtotalX = FtotalX + Fxx;
                    FtotalY = FtotalY + Fyy;

                    /// get total Moment 
                    MtotalX = MtotalX + Fxx * Htotal;
                    MtotalY = MtotalY + Fyy * Htotal;

                    /////// complete the Loop
                    Number = Number - 1;
                    yloop = yloop - Htypical;
                    Htotal = Htotal - Htypical;

                    

                }

                ////// for ground 
                double Pe = Ce * 1.0 * 6.25 * Math.Pow(10, -4) * V * V;
                double Fxground = Pe * HGround * L2;
                double Fyground = Pe * HGround * L1;

                string sreial1 = " Story 1";
                string H1 = HGround.ToString();
                string K1 = " 1 ";
                string Wx1 =Math.Round(Fxground,2).ToString();
                string Wy1 = Math.Round(Fyground,2).ToString();

                object[] data1 = { sreial1, H1, K1, Wx1, Wy1 };
                DataGridView1.Rows.Add(data1);



                ////force and Moment for Ground
                FtotalX = FtotalX + Fxground;
                FtotalY = FtotalY + Fyground;

                MtotalX = MtotalX + Fxground * (HGround + Hbase);
                MtotalY = MtotalY + Fyground * (HGround + Hbase);
               



                /////// get the force and the moment 

                txttotalFx.Text = Math.Round(FtotalX, 2).ToString();
                txttotalFy.Text = Math.Round(FtotalY, 2).ToString();

                txttotalMx.Text = Math.Round(MtotalX, 2).ToString();
                txttotalMy.Text = Math.Round(MtotalY, 2).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Velocity V = new Velocity();
            V.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Wind Load (Screen)";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
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
                        txttotalFx.Clear();
                        txttotalMx.Clear();
                        txttotalFy.Clear();
                        txttotalMy.Clear();

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



        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }
    }
}
