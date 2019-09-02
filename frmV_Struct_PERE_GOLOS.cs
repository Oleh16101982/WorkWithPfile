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
    public partial class frmV_Struct_PERE_GOLOS : Form
    {
        public Int32 mode;

        public String   GL_NOMER			    {get {return tbGL_NOMER.Text;} set {tbGL_NOMER.Text = value;}}
        public DateTime GL_DT				    {get {return dtpGL_DT.Value;} set {dtpGL_DT.Value = value;}}
        public String   GL_PRICH				{get {return tbGL_PRICH.Text;} set {tbGL_PRICH.Text = value;}}
        public String   NT_COD_FROM_GL_OSOBA	{get {return tbNT_COD_FROM_GL_OSOBA.Text;} set {tbNT_COD_FROM_GL_OSOBA.Text = value;}}
        public String NT_NM1_FROM_GL_OSOBA { get { return tbNT_NM1_FROM_GL_OSOBA.Text; } set { tbNT_NM1_FROM_GL_OSOBA.Text = value; } }
        public String   NT_NM2_FROM_GL_OSOBA	{get {return tbNT_NM2_FROM_GL_OSOBA.Text;} set {tbNT_NM2_FROM_GL_OSOBA.Text = value;}}
        public String   NT_NM3_FROM_GL_OSOBA	{get {return tbNT_NM3_FROM_GL_OSOBA.Text;} set {tbNT_NM3_FROM_GL_OSOBA.Text = value;}}
        public String   NT_COD_TO_GL_OSOBA	    {get {return tbNT_COD_TO_GL_OSOBA.Text;} set {tbNT_COD_TO_GL_OSOBA.Text = value;}}
        public String   NT_NM1_TO_GL_OSOBA	    {get {return tbNT_NM1_TO_GL_OSOBA.Text;} set {tbNT_NM1_TO_GL_OSOBA.Text = value;}}
        public String   NT_NM2_TO_GL_OSOBA	    {get {return tbNT_NM2_TO_GL_OSOBA.Text;} set {tbNT_NM2_TO_GL_OSOBA.Text = value;}}
        public String   NT_NM3_TO_GL_OSOBA	    {get {return tbNT_NM3_TO_GL_OSOBA.Text;} set {tbNT_NM3_TO_GL_OSOBA.Text = value;}}
        public Decimal  GT_VIDSOTOK			    { get { return Convert.ToDecimal(tbGT_VIDSOTOK.Text); } set { tbGT_VIDSOTOK.Text = value.ToString(); } }
        public Int32    GT_GOLOS                { get { return Convert.ToInt32(tbGT_GOLOS.Text); } set { tbGT_GOLOS.Text = value.ToString(); } }


        public frmV_Struct_PERE_GOLOS(Int32 Mode)
        {
            mode = Mode;
            InitializeComponent();

            switch (mode)
            {
                case 1:
                    this.Text = "Введення даних по власнику (OWNER)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 2:
                    this.Text = "Редагування даних  по власнику (OWNER)";
                    this.btnSave.Text = "Зберегти";
                    break;
                case 3:
                    this.Text = "Видалення даних  по власнику (OWNER)";
                    this.btnSave.Text = "Видалити";
                    foreach (Control currControl in this.Controls)
                    {
                        if (currControl.Name.Substring(0, 3) == "dtp") { currControl.Enabled = false; }
                        if (currControl.Name.Substring(0, 2) == "cb") { currControl.Enabled = false; }
                        if (currControl.Name.Substring(0, 2) == "tb") { currControl.Enabled = false; }
                    }
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GL_NOMER			 = tbGL_NOMER.Text;
            GL_DT				 = dtpGL_DT.Value;
            GL_PRICH			 = tbGL_PRICH.Text;
            NT_COD_FROM_GL_OSOBA = tbNT_COD_FROM_GL_OSOBA.Text;
            NT_NM2_FROM_GL_OSOBA = tbNT_NM2_FROM_GL_OSOBA.Text;
            NT_NM3_FROM_GL_OSOBA = tbNT_NM3_FROM_GL_OSOBA.Text;
            NT_COD_TO_GL_OSOBA	 = tbNT_COD_TO_GL_OSOBA.Text;
            NT_NM1_TO_GL_OSOBA	 = tbNT_NM1_TO_GL_OSOBA.Text;
            NT_NM2_TO_GL_OSOBA	 = tbNT_NM2_TO_GL_OSOBA.Text;
            NT_NM3_TO_GL_OSOBA	 = tbNT_NM3_TO_GL_OSOBA.Text;
            GT_VIDSOTOK			 = Convert.ToDecimal(tbGT_VIDSOTOK.Text);
            GT_GOLOS = Convert.ToInt32(tbGT_GOLOS.Text);
        }
    }
}
