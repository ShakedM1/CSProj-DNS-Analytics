namespace DNSSniffer
{
    partial class Form2
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
            this.logpathbtn = new System.Windows.Forms.Button();
            this.logfolderdialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.applybtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pathtext = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // logpathbtn
            // 
            this.logpathbtn.Location = new System.Drawing.Point(22, 118);
            this.logpathbtn.Name = "logpathbtn";
            this.logpathbtn.Size = new System.Drawing.Size(75, 23);
            this.logpathbtn.TabIndex = 0;
            this.logpathbtn.Text = "Browse";
            this.logpathbtn.UseVisualStyleBackColor = true;
            this.logpathbtn.Click += new System.EventHandler(this.logpathbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Save Log File:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Enabled",
            "Disabled"});
            this.comboBox1.Location = new System.Drawing.Point(124, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(113, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // applybtn
            // 
            this.applybtn.Location = new System.Drawing.Point(106, 208);
            this.applybtn.Name = "applybtn";
            this.applybtn.Size = new System.Drawing.Size(83, 30);
            this.applybtn.TabIndex = 3;
            this.applybtn.Text = "Apply";
            this.applybtn.UseVisualStyleBackColor = true;
            this.applybtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Choose Log Path:";
            // 
            // pathtext
            // 
            this.pathtext.Location = new System.Drawing.Point(106, 118);
            this.pathtext.Name = "pathtext";
            this.pathtext.Size = new System.Drawing.Size(166, 20);
            this.pathtext.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.pathtext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.applybtn);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logpathbtn);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button logpathbtn;
        private System.Windows.Forms.FolderBrowserDialog logfolderdialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button applybtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pathtext;
    }
}