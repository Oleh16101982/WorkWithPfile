namespace WorkWithPfile
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsm_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Files = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCreateFileToSenf = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Receipt = new System.Windows.Forms.ToolStripMenuItem();
            this.ReciveReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрКвитанцийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdChoiceReceipt = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Data,
            this.tsm_Files,
            this.tsm_Receipt,
            this.tsm_Quit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsm_Data
            // 
            this.tsm_Data.Name = "tsm_Data";
            this.tsm_Data.Size = new System.Drawing.Size(59, 20);
            this.tsm_Data.Text = "Данные";
            this.tsm_Data.Click += new System.EventHandler(this.tsm_Data_Click);
            // 
            // tsm_Files
            // 
            this.tsm_Files.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCreateFileToSenf});
            this.tsm_Files.Name = "tsm_Files";
            this.tsm_Files.Size = new System.Drawing.Size(53, 20);
            this.tsm_Files.Text = "Файлы";
            // 
            // tsmCreateFileToSenf
            // 
            this.tsmCreateFileToSenf.Name = "tsmCreateFileToSenf";
            this.tsmCreateFileToSenf.Size = new System.Drawing.Size(260, 22);
            this.tsmCreateFileToSenf.Text = "Формирование файла на отправку";
            this.tsmCreateFileToSenf.Click += new System.EventHandler(this.tsmCreateFileToSenf_Click);
            // 
            // tsm_Receipt
            // 
            this.tsm_Receipt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReciveReceipt,
            this.просмотрКвитанцийToolStripMenuItem});
            this.tsm_Receipt.Name = "tsm_Receipt";
            this.tsm_Receipt.Size = new System.Drawing.Size(74, 20);
            this.tsm_Receipt.Text = "Квитанции";
            // 
            // ReciveReceipt
            // 
            this.ReciveReceipt.Name = "ReciveReceipt";
            this.ReciveReceipt.Size = new System.Drawing.Size(190, 22);
            this.ReciveReceipt.Text = "Прием квитанций";
            this.ReciveReceipt.Click += new System.EventHandler(this.ReciveReceipt_Click);
            // 
            // просмотрКвитанцийToolStripMenuItem
            // 
            this.просмотрКвитанцийToolStripMenuItem.Name = "просмотрКвитанцийToolStripMenuItem";
            this.просмотрКвитанцийToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.просмотрКвитанцийToolStripMenuItem.Text = "Просмотр квитанций";
            // 
            // tsm_Quit
            // 
            this.tsm_Quit.Name = "tsm_Quit";
            this.tsm_Quit.Size = new System.Drawing.Size(52, 20);
            this.tsm_Quit.Text = "Выход";
            this.tsm_Quit.Click += new System.EventHandler(this.tsm_Quit_Click);
            // 
            // ofdChoiceReceipt
            // 
            this.ofdChoiceReceipt.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 545);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsm_Data;
        private System.Windows.Forms.ToolStripMenuItem tsm_Files;
        private System.Windows.Forms.ToolStripMenuItem tsm_Receipt;
        private System.Windows.Forms.ToolStripMenuItem tsm_Quit;
        private System.Windows.Forms.ToolStripMenuItem tsmCreateFileToSenf;
        private System.Windows.Forms.ToolStripMenuItem ReciveReceipt;
        private System.Windows.Forms.ToolStripMenuItem просмотрКвитанцийToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdChoiceReceipt;


    }
}

