namespace WorkWithPfile
{
    partial class frmV_Struct_SUM_BANK
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mtbGT_VIDSOTOK = new System.Windows.Forms.MaskedTextBox();
            this.mtbGT_GOLOS = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Відсотки статутного капіталу бфнку (GT_VIDSOTOK)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Кількість голосів (GT_GOLOS)";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(49, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(241, 103);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Відмовитись";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // mtbGT_VIDSOTOK
            // 
            this.mtbGT_VIDSOTOK.Location = new System.Drawing.Point(291, 20);
            this.mtbGT_VIDSOTOK.Mask = "000.00";
            this.mtbGT_VIDSOTOK.Name = "mtbGT_VIDSOTOK";
            this.mtbGT_VIDSOTOK.PromptChar = ' ';
            this.mtbGT_VIDSOTOK.Size = new System.Drawing.Size(100, 20);
            this.mtbGT_VIDSOTOK.TabIndex = 6;
            // 
            // mtbGT_GOLOS
            // 
            this.mtbGT_GOLOS.Location = new System.Drawing.Point(291, 57);
            this.mtbGT_GOLOS.Mask = "0000000000";
            this.mtbGT_GOLOS.Name = "mtbGT_GOLOS";
            this.mtbGT_GOLOS.PromptChar = ' ';
            this.mtbGT_GOLOS.Size = new System.Drawing.Size(100, 20);
            this.mtbGT_GOLOS.TabIndex = 7;
            // 
            // frmV_Struct_SUM_BANK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSave;
            this.ClientSize = new System.Drawing.Size(422, 148);
            this.Controls.Add(this.mtbGT_GOLOS);
            this.Controls.Add(this.mtbGT_VIDSOTOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmV_Struct_SUM_BANK";
            this.Text = "frmV_SUM_BANK";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.MaskedTextBox mtbGT_VIDSOTOK;
        private System.Windows.Forms.MaskedTextBox mtbGT_GOLOS;
    }
}