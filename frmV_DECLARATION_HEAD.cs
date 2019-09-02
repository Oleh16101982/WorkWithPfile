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
    public partial class frmV_DECLARATION_HEAD : Form
    {
        public Int32 mode = 0;

        public DateTime INF_DT{ get {return dtpINF_DT.Value; } set { dtpINF_DT.Value = value; }}
        public String TABLE_PIC { get { return tbTABLE_PIC.Text; } set { tbTABLE_PIC.Text = value; } }
        public String PICTURE { get { return tbPICTURE.Text; } set { tbPICTURE.Text = value; } }
        public String EDRPOU { get { return tbEDRPOU.Text; } set { tbEDRPOU.Text = value; } }
        public String MFO { get { return tbMFO.Text; } set { tbMFO.Text = value; } }
        public String IDBANK { get { return tbIDBANK.Text; } set { tbIDBANK.Text = value; } }
        public String CDTASK { get { return tbCDTASK.Text; } set { tbCDTASK.Text = value; } }
        public String CDSUB { get { return tbCDSUB.Text; } set { tbCDSUB.Text = value; } }
        public String CDFORM { get { return tbCDFORM.Text; } set { tbCDFORM.Text = value; } }
        public String KU { get { return tbKU.Text; } set { tbKU.Text = value; } }


        public frmV_DECLARATION_HEAD(Int32 Mode)
        {
            mode = Mode;
            InitializeComponent();
            switch (mode)
            {
                case 1 : 
                    this.Text = "Введення загальних даних (DECLARATION, HEAD)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 2 :
                    this.Text = "Редагування загальних даних (DECLARATION, HEAD)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 3 :
                    this.Text = "Видалення загальних даних (DECLARATION, HEAD)";
                    this.btnSave.Text = "Видалити";
                    dtpINF_DT.Enabled = false;
                    tbTABLE_PIC.Enabled = false;
                    tbPICTURE.Enabled = false;
                    tbEDRPOU.Enabled = false;
                    tbMFO.Enabled = false;
                    tbIDBANK.Enabled = false;
                    tbCDTASK.Enabled = false;
                    tbCDSUB.Enabled = false;
                    tbCDFORM.Enabled = false;
                    tbKU.Enabled = false;
                    break;
            }
        }

        private void btnTABLE_PIC_Click(object sender, EventArgs e)
        {
            ofdPDF_PICTURE.InitialDirectory = "c:\\";
            ofdPDF_PICTURE.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            ofdPDF_PICTURE.FilterIndex = 1;
            ofdPDF_PICTURE.RestoreDirectory = true;

            if (ofdPDF_PICTURE.ShowDialog() == DialogResult.OK)
            {
                tbTABLE_PIC.Text = ofdPDF_PICTURE.FileName;
            }

        }

        private void btnPICTURE_Click(object sender, EventArgs e)
        {
            ofdPDF_PICTURE.InitialDirectory = "c:\\";
            ofdPDF_PICTURE.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            ofdPDF_PICTURE.FilterIndex = 1;
            ofdPDF_PICTURE.RestoreDirectory = true;

            if (ofdPDF_PICTURE.ShowDialog() == DialogResult.OK)
            {
                tbPICTURE.Text = ofdPDF_PICTURE.FileName;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            INF_DT = dtpINF_DT.Value;
            TABLE_PIC = tbTABLE_PIC.Text;
            PICTURE = tbPICTURE.Text;
            EDRPOU = tbEDRPOU.Text;
            MFO = tbMFO.Text;
            IDBANK = tbIDBANK.Text;
            CDTASK = tbCDTASK.Text;
            CDSUB = tbCDSUB.Text;
            CDFORM = tbCDFORM.Text;
            KU = tbKU.Text;
        }
    }
}

