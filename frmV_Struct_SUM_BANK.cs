using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkWithPfile
{
    public partial class frmV_Struct_SUM_BANK : Form
    {
        public Int32 mode;

        public Decimal GT_VIDSOTOK { get { return Convert.ToDecimal(mtbGT_VIDSOTOK.Text); } set { mtbGT_VIDSOTOK.Text = value.ToString(); } }
        public Int32 GT_GOLOS { get { return Convert.ToInt32(mtbGT_GOLOS.Text); } set { mtbGT_GOLOS.Text = value.ToString(); } }
       
        public frmV_Struct_SUM_BANK(Int32 Mode)
        {
            mode = Mode;
            InitializeComponent();

            switch (mode)
            {
                case 1:
                    this.Text = "Введення даних (SUM_BANK)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 2:
                    this.Text = "Редагування даних (SUM_BANK)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 3:
                    this.Text = "Видалення даних (SUM_BANK)";
                    this.btnSave.Text = "Видалити";
                    mtbGT_GOLOS.Enabled = false;
                    mtbGT_VIDSOTOK.Enabled = false;
                    break;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GT_GOLOS = Convert.ToInt32(mtbGT_GOLOS.Text.Trim());
            GT_VIDSOTOK = Convert.ToDecimal(mtbGT_VIDSOTOK.Text.Trim());
        }
    }
}
