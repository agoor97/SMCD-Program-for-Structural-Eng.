namespace Design_Concrete
{
    partial class K
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labeltype = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtk = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radiocase2U = new System.Windows.Forms.RadioButton();
            this.radiocase4U = new System.Windows.Forms.RadioButton();
            this.radiocase3U = new System.Windows.Forms.RadioButton();
            this.radiocase1U = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radiocase2L = new System.Windows.Forms.RadioButton();
            this.radiocase3L = new System.Windows.Forms.RadioButton();
            this.radiocase1L = new System.Windows.Forms.RadioButton();
            this.radiounbraced = new System.Windows.Forms.RadioButton();
            this.radiobraced = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 448);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labeltype);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtk);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 427);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose ";
            // 
            // labeltype
            // 
            this.labeltype.AutoSize = true;
            this.labeltype.ForeColor = System.Drawing.Color.Blue;
            this.labeltype.Location = new System.Drawing.Point(264, 384);
            this.labeltype.Name = "labeltype";
            this.labeltype.Size = new System.Drawing.Size(35, 29);
            this.labeltype.TabIndex = 8;
            this.labeltype.Text = "**";
            this.labeltype.Click += new System.EventHandler(this.labeltype_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Aqua;
            this.button2.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(372, 373);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 40);
            this.button2.TabIndex = 13;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtk
            // 
            this.txtk.BackColor = System.Drawing.SystemColors.Window;
            this.txtk.ForeColor = System.Drawing.Color.Blue;
            this.txtk.Location = new System.Drawing.Point(112, 378);
            this.txtk.Name = "txtk";
            this.txtk.ReadOnly = true;
            this.txtk.Size = new System.Drawing.Size(100, 35);
            this.txtk.TabIndex = 4;
            this.txtk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "K = ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.radiounbraced);
            this.groupBox2.Controls.Add(this.radiobraced);
            this.groupBox2.Location = new System.Drawing.Point(15, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 332);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Aqua;
            this.button1.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(165, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 40);
            this.button1.TabIndex = 11;
            this.button1.Text = "Calculate ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radiocase2U);
            this.groupBox4.Controls.Add(this.radiocase4U);
            this.groupBox4.Controls.Add(this.radiocase3U);
            this.groupBox4.Controls.Add(this.radiocase1U);
            this.groupBox4.Location = new System.Drawing.Point(271, 73);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(154, 207);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Upper End ";
            // 
            // radiocase2U
            // 
            this.radiocase2U.AutoSize = true;
            this.radiocase2U.Location = new System.Drawing.Point(23, 78);
            this.radiocase2U.Name = "radiocase2U";
            this.radiocase2U.Size = new System.Drawing.Size(99, 33);
            this.radiocase2U.TabIndex = 7;
            this.radiocase2U.TabStop = true;
            this.radiocase2U.Text = "Case 2";
            this.radiocase2U.UseVisualStyleBackColor = true;
            // 
            // radiocase4U
            // 
            this.radiocase4U.AutoSize = true;
            this.radiocase4U.Location = new System.Drawing.Point(23, 156);
            this.radiocase4U.Name = "radiocase4U";
            this.radiocase4U.Size = new System.Drawing.Size(99, 33);
            this.radiocase4U.TabIndex = 6;
            this.radiocase4U.TabStop = true;
            this.radiocase4U.Text = "Case 4";
            this.radiocase4U.UseVisualStyleBackColor = true;
            // 
            // radiocase3U
            // 
            this.radiocase3U.AutoSize = true;
            this.radiocase3U.Location = new System.Drawing.Point(23, 117);
            this.radiocase3U.Name = "radiocase3U";
            this.radiocase3U.Size = new System.Drawing.Size(99, 33);
            this.radiocase3U.TabIndex = 5;
            this.radiocase3U.TabStop = true;
            this.radiocase3U.Text = "Case 3";
            this.radiocase3U.UseVisualStyleBackColor = true;
            // 
            // radiocase1U
            // 
            this.radiocase1U.AutoSize = true;
            this.radiocase1U.Checked = true;
            this.radiocase1U.Location = new System.Drawing.Point(23, 34);
            this.radiocase1U.Name = "radiocase1U";
            this.radiocase1U.Size = new System.Drawing.Size(108, 33);
            this.radiocase1U.TabIndex = 4;
            this.radiocase1U.TabStop = true;
            this.radiocase1U.Text = "Case 1 ";
            this.radiocase1U.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radiocase2L);
            this.groupBox3.Controls.Add(this.radiocase3L);
            this.groupBox3.Controls.Add(this.radiocase1L);
            this.groupBox3.Location = new System.Drawing.Point(43, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 207);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lower End ";
            // 
            // radiocase2L
            // 
            this.radiocase2L.AutoSize = true;
            this.radiocase2L.Location = new System.Drawing.Point(23, 78);
            this.radiocase2L.Name = "radiocase2L";
            this.radiocase2L.Size = new System.Drawing.Size(99, 33);
            this.radiocase2L.TabIndex = 7;
            this.radiocase2L.TabStop = true;
            this.radiocase2L.Text = "Case 2";
            this.radiocase2L.UseVisualStyleBackColor = true;
            // 
            // radiocase3L
            // 
            this.radiocase3L.AutoSize = true;
            this.radiocase3L.Location = new System.Drawing.Point(23, 117);
            this.radiocase3L.Name = "radiocase3L";
            this.radiocase3L.Size = new System.Drawing.Size(99, 33);
            this.radiocase3L.TabIndex = 5;
            this.radiocase3L.TabStop = true;
            this.radiocase3L.Text = "Case 3";
            this.radiocase3L.UseVisualStyleBackColor = true;
            // 
            // radiocase1L
            // 
            this.radiocase1L.AutoSize = true;
            this.radiocase1L.Checked = true;
            this.radiocase1L.Location = new System.Drawing.Point(23, 34);
            this.radiocase1L.Name = "radiocase1L";
            this.radiocase1L.Size = new System.Drawing.Size(108, 33);
            this.radiocase1L.TabIndex = 4;
            this.radiocase1L.TabStop = true;
            this.radiocase1L.Text = "Case 1 ";
            this.radiocase1L.UseVisualStyleBackColor = true;
            // 
            // radiounbraced
            // 
            this.radiounbraced.AutoSize = true;
            this.radiounbraced.Checked = true;
            this.radiounbraced.Location = new System.Drawing.Point(57, 34);
            this.radiounbraced.Name = "radiounbraced";
            this.radiounbraced.Size = new System.Drawing.Size(126, 33);
            this.radiounbraced.TabIndex = 3;
            this.radiounbraced.TabStop = true;
            this.radiounbraced.Text = "UnBraced";
            this.radiounbraced.UseVisualStyleBackColor = true;
            // 
            // radiobraced
            // 
            this.radiobraced.AutoSize = true;
            this.radiobraced.Location = new System.Drawing.Point(262, 34);
            this.radiobraced.Name = "radiobraced";
            this.radiobraced.Size = new System.Drawing.Size(110, 33);
            this.radiobraced.TabIndex = 2;
            this.radiobraced.Text = "Braced ";
            this.radiobraced.UseVisualStyleBackColor = true;
            // 
            // K
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(524, 448);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "K";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "End Condition";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radiounbraced;
        private System.Windows.Forms.RadioButton radiobraced;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radiocase2U;
        private System.Windows.Forms.RadioButton radiocase3U;
        private System.Windows.Forms.RadioButton radiocase1U;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radiocase2L;
        private System.Windows.Forms.RadioButton radiocase4U;
        private System.Windows.Forms.RadioButton radiocase3L;
        private System.Windows.Forms.RadioButton radiocase1L;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtk;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Label labeltype;
    }
}