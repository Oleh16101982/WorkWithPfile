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
    public partial class frmV_Struct_OWNER : Form
    {
        public Int32 mode = 0;
        private Boolean isCanClose = true;

        
//	  @idDeclaration_OWNER				int,
public String   OWNER_TYPE    { get { return tbOWNER_TYPE.Text;}  set {tbOWNER_TYPE.Text = value;}}
public String OWNER_OZN { get { return tbOWNER_OZN.Text; } set { tbOWNER_OZN.Text = value; } }
public String   OWNER_POS	    { get { return tbOWNER_POS.Text;} set {tbOWNER_POS.Text = value;}}
public DateTime OWNER_DATE	{ get { return dtpOWNER_DATE.Value;} set {dtpOWNER_DATE.Value = value;}}
public String   OWNER_DORG    { get { return tbOWNER_DORG.Text;} set {tbOWNER_DORG.Text = value;}}	
//      @idDeclaration_OWNER_GOL_UCH		int,
public Decimal  GT_VIDSOTOK_GOL_UCH	{ get { return Convert.ToDecimal(tbGT_VIDSOTOK_GOL_UCH.Text.Trim()); } set { tbGT_VIDSOTOK_GOL_UCH.Text = value.ToString(); } }
public Int32 GT_GOLOSI_GOL_UCH { get { return Convert.ToInt32(tbGT_GOLOSI_GOL_UCH.Text.Trim()); } set { tbGT_GOLOSI_GOL_UCH.Text = value.ToString(); } }			
//      @idDeclaration_OWNER_OPR_UCH		int,
public Decimal  UT_VIDSOTOK_OPR_UCH	{ get { return Convert.ToDecimal(tbUT_VIDSOTOK_OPR_UCH.Text); } set { tbUT_VIDSOTOK_OPR_UCH.Text = value.ToString(); } }		
public Int32    UT_NOMINAL_OPR_UCH	{ get { return Convert.ToInt32(tbUT_NOMINAL_OPR_UCH.Text); } set { tbUT_NOMINAL_OPR_UCH.Text = value.ToString(); } }			
public Int32    UT_GOLOSI_OPR_UCH	{ get { return Convert.ToInt32(tbUT_GOLOSI_OPR_UCH.Text); } set { tbUT_GOLOSI_OPR_UCH.Text = value.ToString(); } }					
//      @idDeclaration_OWNER_OWNER_ADR	int,
public String ADR_COD_KR { get { return tbADR_COD_KR.Text;} set {tbADR_COD_KR.Text = value;}}	
public String ADR_INDEX	{ get { return tbADR_INDEX.Text;} set {tbADR_INDEX.Text = value;}}	
public String ADR_PUNKT	{ get { return tbADR_PUNKT.Text;} set {tbADR_PUNKT.Text = value;}}	
public String ADR_UL    { get { return tbADR_UL.Text;} set {tbADR_UL.Text = value;}}	
public String ADR_BUD	{ get { return tbADR_BUD.Text;} set {tbADR_BUD.Text = value;}}	
public String ADR_KORP	{ get { return tbADR_KORP.Text;} set {tbADR_KORP.Text = value;}}	
public String ADR_OFF	{ get { return tbADR_OFF.Text;} set {tbADR_OFF.Text = value;}}	
//      @idDeclaration_OWNER_OWNER_NAZVA	int,
public String NT_COD	{ get { return tbNT_COD.Text;} set {tbNT_COD.Text = value;}}	
public String NT_NM1	{ get { return tbNT_NM1.Text;} set {tbNT_NM1.Text = value;}}	
public String NT_NM2	{ get { return tbNT_NM2.Text;} set {tbNT_NM2.Text = value;}}	
public String NT_NM3	{ get { return tbNT_NM3.Text;} set {tbNT_NM3.Text = value;}}	
//      @idDeclaration_OWNER_OWNER_PASS	int,
public String   PS_SR		{ get { return tbPS_SR.Text;} set {tbPS_SR.Text = value;}}	
public String   PS_NM		{ get { return tbPS_NM.Text;} set {tbPS_NM.Text = value;}}	
public DateTime PS_DT	{get { return dtpPS_DT.Value;} set {dtpPS_DT.Value = value;}}	
public String   PS_ORG	{ get { return tbPS_ORG.Text;} set {tbPS_ORG.Text = value;}}				
//      @idDeclaration_OWNER_PR_UCH		int,
public Decimal  UT_VIDSOTOK_PR_UCH { get { return Convert.ToDecimal(tbUT_VIDSOTOK_PR_UCH.Text); } set { tbUT_VIDSOTOK_PR_UCH.Text = value.ToString(); } }				
public Int32    UT_NOMINAL_PR_UCH	{ get { return Convert.ToInt32(tbUT_NOMINAL_PR_UCH.Text); } set { tbUT_NOMINAL_PR_UCH.Text = value.ToString(); } }					
public Int32    UT_GOLOSI_PR_UCH { get { return Convert.ToInt32(tbUT_GOLOSI_PR_UCH.Text); } set { tbUT_GOLOSI_PR_UCH.Text = value.ToString(); } }					
//      @idDeclaration_OWNER_ZAG_UCH		int,
public Decimal  GT_VIDSOTOK_ZAG_UCH  { get { return Convert.ToDecimal(tbGT_VIDSOTOK_ZAG_UCH.Text); } set { tbGT_VIDSOTOK_ZAG_UCH.Text = value.ToString(); } }
public Int32    GT_GOLOSI_ZAG_UCH { get { return Convert.ToInt32(tbGT_GOLOSI_ZAG_UCH.Text); } set { tbGT_GOLOSI_ZAG_UCH.Text = value.ToString(); } }				


        public frmV_Struct_OWNER(Int32 Mode)
        {
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
                            
                            if (currControl.Name.Substring(0,3) == "dtp") { currControl.Enabled = false; }
                            if (currControl.Name.Substring(0, 2) == "cb") { currControl.Enabled = false; }
                            if (currControl.Name.Substring(0, 2) == "tb") { currControl.Enabled = false; }
                        }
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Decimal vids = 0;
            Int32 gol = 0;

            OWNER_TYPE           = tbOWNER_TYPE.Text.Trim();
            OWNER_OZN            = tbOWNER_OZN.Text.Trim();
            OWNER_POS            = tbOWNER_POS.Text;
            OWNER_DATE           = dtpOWNER_DATE.Value;
            OWNER_DORG           = tbOWNER_DORG.Text;



            if (!Decimal.TryParse(tbGT_VIDSOTOK_GOL_UCH.Text.Trim(), out vids)) { GT_VIDSOTOK_GOL_UCH = vids; MessageBox.Show("Невірне значення відсотків НАБУТЕ ПРАВО ГОЛОСУ (GOL_UCH)"); isCanClose = false; }
            if (!Int32.TryParse(tbGT_GOLOSI_GOL_UCH.Text.Trim(), out gol)) { GT_GOLOSI_GOL_UCH = gol; MessageBox.Show("Невірне значення кількості НАБУТЕ ПРАВО ГОЛОСУ (GOL_UCH)"); isCanClose = false; }


            if (!Decimal.TryParse(tbUT_VIDSOTOK_OPR_UCH.Text.Trim(), out vids)) { UT_VIDSOTOK_OPR_UCH = vids; MessageBox.Show("Невірне значення відсотків ОПОСЕРЕДКОВАНА УЧАСТЬ (OPR_UCH)"); isCanClose = false; }
            if (!Int32.TryParse(tbUT_NOMINAL_OPR_UCH.Text.Trim(), out gol)) { UT_NOMINAL_OPR_UCH = gol; MessageBox.Show("Невірне значення номіналу ОПОСЕРЕДКОВАНА УЧАСТЬ (OPR_UCH)"); isCanClose = false; }
            if (!Int32.TryParse(tbUT_GOLOSI_OPR_UCH.Text.Trim(), out gol)) { UT_GOLOSI_OPR_UCH = gol; MessageBox.Show("Невірне значення кількості ОПОСЕРЕДКОВАНА УЧАСТЬ (OPR_UCH)"); isCanClose = false; }
            
            ADR_COD_KR           = tbADR_COD_KR.Text;           
            ADR_INDEX	         = tbADR_INDEX.Text;	         
            ADR_PUNKT	         = tbADR_PUNKT.Text;	         
            ADR_UL               = tbADR_UL.Text;               
            ADR_BUD	             = tbADR_BUD.Text;	             
            ADR_KORP	         = tbADR_KORP.Text;	         
            ADR_OFF              = tbADR_OFF.Text;              
	
            NT_COD               = tbNT_COD.Text;               
            NT_NM1               = tbNT_NM1.Text;               
            NT_NM2               = tbNT_NM2.Text;               
            NT_NM3               = tbNT_NM3.Text;               

            PS_SR	             = tbPS_SR.Text;	             
            PS_NM	             = tbPS_NM.Text;	             
            PS_DT	             = dtpPS_DT.Value;	             
            PS_ORG	             = tbPS_ORG.Text;	             

            if (!Decimal.TryParse(tbUT_VIDSOTOK_PR_UCH.Text.Trim(), out vids)) { UT_VIDSOTOK_PR_UCH = vids; MessageBox.Show("Невірне значення відсотків ПРЯМА УЧАСТЬ (PR_UCH)"); isCanClose = false; }
            if (!Int32.TryParse(tbUT_NOMINAL_PR_UCH.Text.Trim(), out gol)) { UT_NOMINAL_PR_UCH = gol; MessageBox.Show("Невірне значення номіналу ПРЯМА УЧАСТЬ (PR_UCH)"); isCanClose = false; }
            if (!Int32.TryParse(tbUT_GOLOSI_PR_UCH.Text.Trim(), out gol)) { UT_GOLOSI_PR_UCH = gol; MessageBox.Show("Невірне значення кількості ПРЯМА УЧАСТЬ (PR_UCH)"); isCanClose = false; }

            if (!Decimal.TryParse(tbGT_VIDSOTOK_ZAG_UCH.Text.Trim(), out vids)) { GT_VIDSOTOK_ZAG_UCH = vids; MessageBox.Show("Невірне значення відсотків ЗАГАЛЬНА УЧАСТЬ (ZAG_UCH)"); isCanClose = false; }
            if (!Int32.TryParse(tbGT_GOLOSI_ZAG_UCH.Text.Trim(), out gol)) { GT_GOLOSI_ZAG_UCH = gol; MessageBox.Show("Невірне значення кількості ЗАГАЛЬНА УЧАСТЬ (ZAG_UCH)"); isCanClose = false; }
           
        }

        private void frmV_Struct_OWNER_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isCanClose) { e.Cancel = true; isCanClose = true; }
        }

        private void tbOWNER_TYPE_TextChanged(object sender, EventArgs e)
        {
            if (tbOWNER_TYPE.Text.Trim() == "2")
            {
                panel1.Visible = true;
                tbPS_SR.Text = "";
                tbPS_NM.Text = "";
                tbPS_ORG.Text = "";
            }
            else
            {
                panel1.Visible = false;
                tbPS_SR.Text = "";
                tbPS_NM.Text = "";
                tbPS_ORG.Text = "";
                dtpPS_DT.Value = dtpPS_DT.MinDate;
                dtpOWNER_DATE.Value = dtpOWNER_DATE.MinDate;
            }
        }


    }
}
