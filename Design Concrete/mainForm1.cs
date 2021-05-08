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
    public partial class mainForm1 : Form
    {
        public mainForm1()
        {
            InitializeComponent();
        }

        private void capacityMORToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void capacityMORToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            bool Isopen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Capacity of Slab Sections")
                {
                    Isopen = true;
                    f.Focus();
                    break;

                }

            }
            if (Isopen == false)
            {
                slabMOR slabb = new slabMOR();
                slabb.MdiParent = this;
                slabb.Show();


            }

        }

        private void solidSlabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool open = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Solid Slabs")
                {
                    open = true;
                    f.BringToFront();
                    break;
                }
            }

            if (open == false)
            {
                solid slab = new solid();
                slab.MdiParent = this;
                slab.Show();
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool ISOpen = false;
            foreach(Form f in Application.OpenForms)
            {
                if(f.Text== "Coefficient for Slabs ")
                {
                    ISOpen = true;
                    f.Focus();
                    break;
                }
                
            }

            if (ISOpen == false)
            {
                alfabeta alfabeta = new alfabeta();
                alfabeta.MdiParent = this;
                alfabeta.Show();

            }
        }

        private void loadsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Two Way H.B.")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                TwoHB HB2 = new TwoHB( );
                HB2.MdiParent = this;
                HB2.Show();

            }
        }
        private void oneWayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Loads Of H.B Slabs")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                Loads load = new Loads();
                load.MdiParent = this;
                load.Show();

            }

        }

        private void twoWayToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "One Way H.B.")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                onewayHb onehb = new onewayHb();
                onehb.MdiParent = this;
                onehb.Show();

            }
        }

        private void capacityMORToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Capacity of Beam Sections")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                BeamMOR beammor = new BeamMOR();
                beammor.MdiParent = this;
                beammor.Show();

            }

        }

        private void rCSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "R.C Beam Section ")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                RCsection RCbeam = new RCsection();
                RCbeam.MdiParent = this;
                RCbeam.Show();

            }

        }

        private void lOrTSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "T & L Beam Section")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                LTbeamSection LT = new LTbeamSection();
                LT.MdiParent = this;
                LT.Show();

            }
        }

        private void designOnPOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Import from Excel -- Design On (P) Only")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                columnImport colimport = new columnImport();
                colimport.MdiParent = this;
                colimport.Show();

            }
        }

        private void designOnPMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "M & P Columns ")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                PMCOL PM = new PMCOL();
                PM.MdiParent = this;
                PM.Show();

            }
        }

        private void interactionDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check on (P & M) Column by I.D")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                check check = new check();
                check.MdiParent = this;
                check.Show();

            }

        }

        private void designOnPMxMyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "(P & Mx & My) Column")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                PMM PMM = new PMM();
                PMM.MdiParent = this;
                PMM.Show();

            }


        }

        private void shortCantiliverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Short Cantilevers (Brackets)")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                shortcant cant = new shortcant();
                cant.MdiParent = this;
                cant.Show();

            }

        }

        private void longColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Long Column")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                longcolumn Long = new longcolumn();
                Long.MdiParent = this;
                Long.Show();

            }
        }

        private void isolatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Interior Footing (N) Only")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                footN  footN = new footN();
                footN.MdiParent = this;
                footN.Show();

            }
        }

        private void isolatedNMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "(N & M) Footing with Uniform Stress ")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               NMfoot footNM = new NMfoot();
                footNM.MdiParent = this;
                footNM.Show();

            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "(N & M) with Non-Uniform Stress")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               NMfoot2 footNM2 = new NMfoot2();
                footNM2.MdiParent = this;
                footNM2.Show();

            }
        }

        private void stripFootingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Strip Footing")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               Strip strip = new Strip();
                strip.MdiParent = this;
                strip.Show();

            }
        }

        private void combinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Combined Footing")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                combined comb = new combined();
                comb.MdiParent = this;
                comb.Show();

            }

        }

        private void stapBeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Strap Beam")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                strap strap = new strap();
                strap.MdiParent = this;
                strap.Show();

            }
        }

        private void windLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Seismic Loads (Simplified Response Spectrum)")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               Seismic EQ = new Seismic();
                EQ.MdiParent = this;
                EQ.Show();

            }
        }

        private void seismicLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Wind Load")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               wind W = new wind();
                W.MdiParent = this;
                W.Show();

            }
        }

        private void crackControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Crack Width")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                cracks crack = new cracks();
                crack.MdiParent = this;
                crack.Show();

            }
        }

        private void flatSlabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Front")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                front front = new front();
                front.MdiParent = this;
                front.Show();

            }
        }

        private void unCrackedSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "UnCracked Sections")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                tanks tank = new tanks();
                tank.MdiParent = this;
                tank.Show();

            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Help")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                Help help = new Help();
                help.MdiParent = this;
                help .Show();

            }
        }

      

        private void oneFlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "InDoor Two Flight Stairs")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                stairTwoFlight StairTwo = new stairTwoFlight();
                StairTwo.MdiParent = this;
                StairTwo.Show();

            }
        }

        private void twoFlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "InDoor Three Flight Stairs")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                stairThreeFlight StairThree = new stairThreeFlight();
                StairThree.MdiParent = this;
                StairThree.Show();

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Center of Mass and Center of Rigidity")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                CMandCR CMCR = new CMandCR();
                CMCR.MdiParent = this;
                CMCR.Show();

            }
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Soft Story")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               softstory Soft = new softstory();
                Soft.MdiParent = this;
                Soft.Show();

            }
        }

        private void storyDriftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Heavy Story")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                HeavyStory heavy = new HeavyStory();
                heavy.MdiParent = this;
                heavy.Show();

            }
        }

        private void torsionalAmplToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check P-δ effect")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               Pdelta Pdelta = new Pdelta();
                Pdelta.MdiParent = this;
                Pdelta.Show();

            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Story Drift")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                storydrift drift = new storydrift();
                drift.MdiParent = this;
                drift.Show();

            }
        }

        private void torsionalAmplToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Torsion Irregularity")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                TorsionIrregularity Tors = new TorsionIrregularity();
                Tors.MdiParent = this;
                Tors.Show();

            }
        }

        private void Pic_Click(object sender, EventArgs e)
        {
           
        }

        private void deepBeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Deep Beams (Code Empirical Method)")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                deepbeam deep = new deepbeam();
                deep.MdiParent = this;
                deep.Show();

            }
        }

        private void couplingBeamSpandrelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Coupling Beam (Spandrel)")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                couplingbeam beam = new couplingbeam();
                beam.MdiParent = this;
                beam.Show();

            }
        }

        private void pilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deflectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Deflection")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                deflection def = new deflection();
                def.MdiParent = this;
                def.Show();


            }
        }

        private void punchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Check Punching (Ordinary)")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
               columnpunch Punch = new columnpunch();
                Punch.MdiParent = this;
                Punch.Show();

            }
        }

        private void byStirrupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Punching Using Stirrups")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                Punch Punching = new Punch();
                Punching.MdiParent = this;
                Punching.Show();

            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Pure Compression Columns")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }

            }

            if (IsOpen == false)
            {
                columnsp P = new columnsp();
                P.MdiParent = this;
                P.Show();

            }

        }
    }
}
