﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3649
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace WorkWithPfile
{
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("DECLARATION", Namespace="", IsNullable=false)]
    public partial class DECLARATION_TYPE {
        
        private DHEAD hEADField;
        
        private string iNF_DTField;
        
        private byte[] tABLE_PICField;
        
        private byte[] pICTUREField;
        
        private DECLARBODY_OWNER[] oWNERField;
        
        private PERE_GOLOS_TYPE[] pERE_GOLOSField;
        
        private GOLOS_TYPE sUM_BANKField;
        
        private MAN_BANK_TYPE mAN_BANKField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DHEAD HEAD {
            get {
                return this.hEADField;
            }
            set {
                this.hEADField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string INF_DT {
            get {
                return this.iNF_DTField;
            }
            set {
                this.iNF_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] TABLE_PIC {
            get {
                return this.tABLE_PICField;
            }
            set {
                this.tABLE_PICField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] PICTURE {
            get {
                return this.pICTUREField;
            }
            set {
                this.pICTUREField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OWNER", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DECLARBODY_OWNER[] OWNER {
            get {
                return this.oWNERField;
            }
            set {
                this.oWNERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PERE_GOLOS", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PERE_GOLOS_TYPE[] PERE_GOLOS {
            get {
                return this.pERE_GOLOSField;
            }
            set {
                this.pERE_GOLOSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GOLOS_TYPE SUM_BANK {
            get {
                return this.sUM_BANKField;
            }
            set {
                this.sUM_BANKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public MAN_BANK_TYPE MAN_BANK {
            get {
                return this.mAN_BANKField;
            }
            set {
                this.mAN_BANKField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DHEAD {
        
        private string fNAMEField;
        
        private string eDRPOUField;
        
        private string iDBANKField;
        
        private string mFOField;
        
        private string cDTASKField;
        
        private string cDSUBField;
        
        private string cDFORMField;
        
        private string fILL_DATEField;
        
        private string fILL_TIMEField;
        
        private string eiField;
        
        private string kuField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FNAME {
            get {
                return this.fNAMEField;
            }
            set {
                this.fNAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EDRPOU {
            get {
                return this.eDRPOUField;
            }
            set {
                this.eDRPOUField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string IDBANK {
            get {
                return this.iDBANKField;
            }
            set {
                this.iDBANKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer", IsNullable=true)]
        public string MFO {
            get {
                return this.mFOField;
            }
            set {
                this.mFOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CDTASK {
            get {
                return this.cDTASKField;
            }
            set {
                this.cDTASKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string CDSUB {
            get {
                return this.cDSUBField;
            }
            set {
                this.cDSUBField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CDFORM {
            get {
                return this.cDFORMField;
            }
            set {
                this.cDFORMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FILL_DATE {
            get {
                return this.fILL_DATEField;
            }
            set {
                this.fILL_DATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FILL_TIME {
            get {
                return this.fILL_TIMEField;
            }
            set {
                this.fILL_TIMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string EI {
            get {
                return this.eiField;
            }
            set {
                this.eiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer", IsNullable=true)]
        public string KU {
            get {
                return this.kuField;
            }
            set {
                this.kuField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FIO_TYPE {
        
        private string fIO_NM1Field;
        
        private string fIO_NM2Field;
        
        private string fIO_NM3Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FIO_NM1 {
            get {
                return this.fIO_NM1Field;
            }
            set {
                this.fIO_NM1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FIO_NM2 {
            get {
                return this.fIO_NM2Field;
            }
            set {
                this.fIO_NM2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string FIO_NM3 {
            get {
                return this.fIO_NM3Field;
            }
            set {
                this.fIO_NM3Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MAN_BANK_TYPE {
        
        private FIO_TYPE mB_NAZVAField;
        
        private string mB_POSField;
        
        private string mB_DTField;
        
        private FIO_TYPE mB_ISP_NAZVAField;
        
        private string mB_TLFField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FIO_TYPE MB_NAZVA {
            get {
                return this.mB_NAZVAField;
            }
            set {
                this.mB_NAZVAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string MB_POS {
            get {
                return this.mB_POSField;
            }
            set {
                this.mB_POSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MB_DT {
            get {
                return this.mB_DTField;
            }
            set {
                this.mB_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public FIO_TYPE MB_ISP_NAZVA {
            get {
                return this.mB_ISP_NAZVAField;
            }
            set {
                this.mB_ISP_NAZVAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MB_TLF {
            get {
                return this.mB_TLFField;
            }
            set {
                this.mB_TLFField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PERE_GOLOS_TYPE {
        
        private NAZVA_TYPE tO_GL_OSOBAField;
        
        private NAZVA_TYPE fROM_GL_OSOBAField;
        
        private GOLOS_TYPE gL_NABUTField;
        
        private string gL_NOMERField;
        
        private string gL_DTField;
        
        private string gL_PRICHField;
        
        private int rOWNUMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NAZVA_TYPE TO_GL_OSOBA {
            get {
                return this.tO_GL_OSOBAField;
            }
            set {
                this.tO_GL_OSOBAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NAZVA_TYPE FROM_GL_OSOBA {
            get {
                return this.fROM_GL_OSOBAField;
            }
            set {
                this.fROM_GL_OSOBAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GOLOS_TYPE GL_NABUT {
            get {
                return this.gL_NABUTField;
            }
            set {
                this.gL_NABUTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GL_NOMER {
            get {
                return this.gL_NOMERField;
            }
            set {
                this.gL_NOMERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GL_DT {
            get {
                return this.gL_DTField;
            }
            set {
                this.gL_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GL_PRICH {
            get {
                return this.gL_PRICHField;
            }
            set {
                this.gL_PRICHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ROWNUM {
            get {
                return this.rOWNUMField;
            }
            set {
                this.rOWNUMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NAZVA_TYPE {
        
        private string nT_CODField;
        
        private string nT_NM1Field;
        
        private string nT_NM2Field;
        
        private string nT_NM3Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NT_COD {
            get {
                return this.nT_CODField;
            }
            set {
                this.nT_CODField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NT_NM1 {
            get {
                return this.nT_NM1Field;
            }
            set {
                this.nT_NM1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NT_NM2 {
            get {
                return this.nT_NM2Field;
            }
            set {
                this.nT_NM2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string NT_NM3 {
            get {
                return this.nT_NM3Field;
            }
            set {
                this.nT_NM3Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GOLOS_TYPE {
        
        private decimal gT_VIDSOTOKField;
        
        private decimal gT_GOLOSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GT_VIDSOTOK {
            get {
                return this.gT_VIDSOTOKField;
            }
            set {
                this.gT_VIDSOTOKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GT_GOLOS {
            get {
                return this.gT_GOLOSField;
            }
            set {
                this.gT_GOLOSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UCHAST_TYPE {
        
        private decimal uT_VIDSOTOKField;
        
        private decimal uT_NOMINALField;
        
        private decimal uT_GOLOSIField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal UT_VIDSOTOK {
            get {
                return this.uT_VIDSOTOKField;
            }
            set {
                this.uT_VIDSOTOKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal UT_NOMINAL {
            get {
                return this.uT_NOMINALField;
            }
            set {
                this.uT_NOMINALField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal UT_GOLOSI {
            get {
                return this.uT_GOLOSIField;
            }
            set {
                this.uT_GOLOSIField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PASS_TYPE {
        
        private string pS_SRField;
        
        private decimal pS_NMField;
        
        private string pS_DTField;
        
        private string pS_ORGField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PS_SR {
            get {
                return this.pS_SRField;
            }
            set {
                this.pS_SRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal PS_NM {
            get {
                return this.pS_NMField;
            }
            set {
                this.pS_NMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PS_DT {
            get {
                return this.pS_DTField;
            }
            set {
                this.pS_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PS_ORG {
            get {
                return this.pS_ORGField;
            }
            set {
                this.pS_ORGField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ADR_TYPE {
        
        private string aDR_COD_KRField;
        
        private string aDR_INDEXField;
        
        private string aDR_PUNKTField;
        
        private string aDR_ULField;
        
        private string aDR_BUDField;
        
        private string aDR_KORPField;
        
        private string aDR_OFFField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ADR_COD_KR {
            get {
                return this.aDR_COD_KRField;
            }
            set {
                this.aDR_COD_KRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer")]
        public string ADR_INDEX {
            get {
                return this.aDR_INDEXField;
            }
            set {
                this.aDR_INDEXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ADR_PUNKT {
            get {
                return this.aDR_PUNKTField;
            }
            set {
                this.aDR_PUNKTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ADR_UL {
            get {
                return this.aDR_ULField;
            }
            set {
                this.aDR_ULField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ADR_BUD {
            get {
                return this.aDR_BUDField;
            }
            set {
                this.aDR_BUDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string ADR_KORP {
            get {
                return this.aDR_KORPField;
            }
            set {
                this.aDR_KORPField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string ADR_OFF {
            get {
                return this.aDR_OFFField;
            }
            set {
                this.aDR_OFFField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DECLARBODY_OWNER {
        
        private decimal oWNER_TYPEField;
        
        private NAZVA_TYPE oWNER_NAZVAField;
        
        private decimal oWNER_OZNField;
        
        private string oWNER_POSField;
        
        private ADR_TYPE oWNER_ADRField;
        
        private PASS_TYPE oWNER_PASSField;
        
        private string oWNER_DATEField;
        
        private string oWNER_DORGField;
        
        private UCHAST_TYPE pR_UCHField;
        
        private UCHAST_TYPE oPR_UCHField;
        
        private GOLOS_TYPE gOL_UCHField;
        
        private GOLOS_TYPE zAG_UCHField;
        
        private int rOWNUMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal OWNER_TYPE {
            get {
                return this.oWNER_TYPEField;
            }
            set {
                this.oWNER_TYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NAZVA_TYPE OWNER_NAZVA {
            get {
                return this.oWNER_NAZVAField;
            }
            set {
                this.oWNER_NAZVAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal OWNER_OZN {
            get {
                return this.oWNER_OZNField;
            }
            set {
                this.oWNER_OZNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string OWNER_POS {
            get {
                return this.oWNER_POSField;
            }
            set {
                this.oWNER_POSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ADR_TYPE OWNER_ADR {
            get {
                return this.oWNER_ADRField;
            }
            set {
                this.oWNER_ADRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public PASS_TYPE OWNER_PASS {
            get {
                return this.oWNER_PASSField;
            }
            set {
                this.oWNER_PASSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string OWNER_DATE {
            get {
                return this.oWNER_DATEField;
            }
            set {
                this.oWNER_DATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string OWNER_DORG {
            get {
                return this.oWNER_DORGField;
            }
            set {
                this.oWNER_DORGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public UCHAST_TYPE PR_UCH {
            get {
                return this.pR_UCHField;
            }
            set {
                this.pR_UCHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public UCHAST_TYPE OPR_UCH {
            get {
                return this.oPR_UCHField;
            }
            set {
                this.oPR_UCHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public GOLOS_TYPE GOL_UCH {
            get {
                return this.gOL_UCHField;
            }
            set {
                this.gOL_UCHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GOLOS_TYPE ZAG_UCH {
            get {
                return this.zAG_UCHField;
            }
            set {
                this.zAG_UCHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ROWNUM {
            get {
                return this.rOWNUMField;
            }
            set {
                this.rOWNUMField = value;
            }
        }
    }
}
