namespace DNSSniffer
{
    partial class Form1
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
            this.SelectInterface = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.InterfaceListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SniffButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ReportListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clearbtn = new System.Windows.Forms.Button();
            this.InterfaceStatus = new System.Windows.Forms.Label();
            this.SniffingStatus = new System.Windows.Forms.Label();
            this.settingsbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectInterface
            // 
            this.SelectInterface.Location = new System.Drawing.Point(267, 50);
            this.SelectInterface.Name = "SelectInterface";
            this.SelectInterface.Size = new System.Drawing.Size(113, 42);
            this.SelectInterface.TabIndex = 0;
            this.SelectInterface.Text = "Select Network Interface";
            this.SelectInterface.UseVisualStyleBackColor = true;
            this.SelectInterface.Click += new System.EventHandler(this.SelectInterface_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Step one: Select desired network interface";
            // 
            // InterfaceListBox
            // 
            this.InterfaceListBox.FormattingEnabled = true;
            this.InterfaceListBox.Location = new System.Drawing.Point(399, 50);
            this.InterfaceListBox.Name = "InterfaceListBox";
            this.InterfaceListBox.Size = new System.Drawing.Size(234, 82);
            this.InterfaceListBox.TabIndex = 2;
            this.InterfaceListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Step two: Start sniffing";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SniffButton
            // 
            this.SniffButton.Location = new System.Drawing.Point(267, 189);
            this.SniffButton.Name = "SniffButton";
            this.SniffButton.Size = new System.Drawing.Size(113, 39);
            this.SniffButton.TabIndex = 4;
            this.SniffButton.Text = "Sniff";
            this.SniffButton.UseVisualStyleBackColor = true;
            this.SniffButton.Click += new System.EventHandler(this.SniffButton_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // ReportListView
            // 
            this.ReportListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader5,
            this.columnHeader6});
            this.ReportListView.Location = new System.Drawing.Point(32, 246);
            this.ReportListView.Name = "ReportListView";
            this.ReportListView.Size = new System.Drawing.Size(577, 226);
            this.ReportListView.TabIndex = 5;
            this.ReportListView.UseCompatibleStateImageBehavior = false;
            this.ReportListView.View = System.Windows.Forms.View.Details;
            this.ReportListView.Visible = false;
            this.ReportListView.SelectedIndexChanged += new System.EventHandler(this.ReportListView_SelectedIndexChanged_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time:";
            this.columnHeader1.Width = 129;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "IP:";
            this.columnHeader5.Width = 148;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Report:";
            this.columnHeader6.Width = 231;
            // 
            // clearbtn
            // 
            this.clearbtn.Location = new System.Drawing.Point(534, 205);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(75, 23);
            this.clearbtn.TabIndex = 6;
            this.clearbtn.Text = "Clear";
            this.clearbtn.UseVisualStyleBackColor = true;
            this.clearbtn.Click += new System.EventHandler(this.clearbtn_Click);
            // 
            // InterfaceStatus
            // 
            this.InterfaceStatus.AutoSize = true;
            this.InterfaceStatus.Location = new System.Drawing.Point(57, 98);
            this.InterfaceStatus.Name = "InterfaceStatus";
            this.InterfaceStatus.Size = new System.Drawing.Size(105, 13);
            this.InterfaceStatus.TabIndex = 7;
            this.InterfaceStatus.Text = "Status: Not Selected";
            // 
            // SniffingStatus
            // 
            this.SniffingStatus.AutoSize = true;
            this.SniffingStatus.Location = new System.Drawing.Point(131, 202);
            this.SniffingStatus.Name = "SniffingStatus";
            this.SniffingStatus.Size = new System.Drawing.Size(98, 13);
            this.SniffingStatus.TabIndex = 8;
            this.SniffingStatus.Text = "Status: Not Sniffing";
            // 
            // settingsbtn
            // 
            this.settingsbtn.Location = new System.Drawing.Point(12, 12);
            this.settingsbtn.Name = "settingsbtn";
            this.settingsbtn.Size = new System.Drawing.Size(56, 22);
            this.settingsbtn.TabIndex = 9;
            this.settingsbtn.Text = "Settings";
            this.settingsbtn.UseVisualStyleBackColor = true;
            this.settingsbtn.Click += new System.EventHandler(this.settingsbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 501);
            this.Controls.Add(this.settingsbtn);
            this.Controls.Add(this.SniffingStatus);
            this.Controls.Add(this.InterfaceStatus);
            this.Controls.Add(this.clearbtn);
            this.Controls.Add(this.ReportListView);
            this.Controls.Add(this.SniffButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InterfaceListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectInterface);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectInterface;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox InterfaceListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SniffButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView ReportListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button clearbtn;
        private System.Windows.Forms.Label InterfaceStatus;
        private System.Windows.Forms.Label SniffingStatus;
        private System.Windows.Forms.Button settingsbtn;
    }
}

