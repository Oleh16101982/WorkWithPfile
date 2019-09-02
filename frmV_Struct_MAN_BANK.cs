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
    public partial class frmV_Struct_MAN_BANK : Form
    {
        public Int32 mode;

        public String   MB_POS                  { get { return tbMB_POS.Text;} set {tbMB_POS.Text = value;}}
        public DateTime MB_DT                   { get { return dtpMB_DT.Value; } set { dtpMB_DT.Value = value; } }
        public String   MB_TLF                  { get { return tbMB_TLF.Text; } set { tbMB_TLF.Text = value; } }
        public String   FIO_NM1_MB_NAZVA        { get { return tbFIO_NM1_MB_NAZVA.Text; } set { tbFIO_NM1_MB_NAZVA.Text = value; } }
        public String   FIO_NM2_MB_NAZVA        { get { return tbFIO_NM2_MB_NAZVA.Text; } set { tbFIO_NM2_MB_NAZVA.Text = value; } }
        public String   FIO_NM3_MB_NAZVA        { get { return tbFIO_NM3_MB_NAZVA.Text; } set { tbFIO_NM3_MB_NAZVA.Text = value; } }
        public String   FIO_NM1_MB_ISP_NAZVA    { get { return tbFIO_NM1_MB_ISP_NAZVA.Text; } set { tbFIO_NM1_MB_ISP_NAZVA.Text = value; } }
        public String FIO_NM2_MB_ISP_NAZVA { get { return tbFIO_NM2_MB_ISP_NAZVA.Text; } set { tbFIO_NM2_MB_ISP_NAZVA.Text = value; } }
        public String   FIO_NM3_MB_ISP_NAZVA    { get { return tbFIO_NM3_MB_ISP_NAZVA.Text; } set { tbFIO_NM3_MB_ISP_NAZVA.Text = value; } }

        public frmV_Struct_MAN_BANK(Int32 Mode)
        {
            mode = Mode;
            InitializeComponent();

            switch (mode)
            {
                case 1:
                    this.Text = "Введення даних керівника та виконавця (MAN_BANK)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 2:
                    this.Text = "Редагування даних керівника та виконавця (MAN_BANK)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 3:
                    this.Text = "Видалення даних керівника та виконавця (MAN_BANK)";
                    this.btnSave.Text = "Видалити";

                    break;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MB_POS = tbMB_POS.Text;
            MB_DT  = dtpMB_DT.Value; 
            MB_TLF = tbMB_TLF.Text;
            FIO_NM1_MB_NAZVA = tbFIO_NM1_MB_NAZVA.Text;
            FIO_NM2_MB_NAZVA = tbFIO_NM2_MB_NAZVA.Text;  
            FIO_NM3_MB_NAZVA = tbFIO_NM3_MB_NAZVA.Text;
            FIO_NM1_MB_ISP_NAZVA = tbFIO_NM1_MB_ISP_NAZVA.Text;
            FIO_NM2_MB_ISP_NAZVA = tbFIO_NM2_MB_ISP_NAZVA.Text; 
            FIO_NM3_MB_ISP_NAZVA = tbFIO_NM3_MB_ISP_NAZVA.Text;

        }
    }
}
