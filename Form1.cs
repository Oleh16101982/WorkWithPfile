using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace WorkWithPfile
{
    using System.Drawing;
    public partial class Form1 : Form
    {

        private NewDataSet dsPfileXML = new NewDataSet();

        DECLARATION KVIDECLARATION = new DECLARATION();

        private frmV_DECLARATION_HEAD frmV_Declaration_Head =  null;
        private frmV_Struct_SUM_BANK frmV_Struct_Sum_Bank = null;
        private frmV_Struct_OWNER frmV_Struct_Owner = null;
        private frmV_Struct_MAN_BANK frmV_Struct_Man_Bank = null;
        private frmV_Struct_PERE_GOLOS frmV_Struct_Pere_Golos = null;
        private Size ScreenSize;

        private String[] base36 = new String[] { "0" , "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private SqlConnection sqlConn = new SqlConnection();
        private SqlCommand sqlCmd = new SqlCommand();
//        private String connStr = @"Server=USER\SQLEXPRESS;DataBase=P7_file;User ID=baranov;Password=Back_Office;";
//        private String connStr = @"Server=OBARA\SQLEXPRESS;DataBase=P7_file;User ID=oleg;Password=Back_Office;";
        private String connStr = @"Server=OBARA\SQLEXPRESS2014;DataBase=P7_file;User ID=oleg;Password=Back_Office;";

//        private NewDataSet dsXML = new NewDataSet();

        private DataSet dsData = new DataSet();

        private DataSet dsDataXML = new DataSet();

        private DataGridView dgvV_DECLARATION_HEAD = new DataGridView();
        private DataGridView dgvV_Struct_MAN_BANK = new DataGridView();
        private DataGridView dgvV_Struct_OWNER = new DataGridView();
        private DataGridView dgvV_Struct_PERE_GOLOS = new DataGridView();
        private DataGridView dgvV_Struct_SUM_BANK = new DataGridView();

        private BindingSource bsV_DECLARATION_HEAD = new BindingSource();
        private BindingSource bsV_Struct_MAN_BANK = new BindingSource();
        private BindingSource bsV_Struct_OWNER = new BindingSource();
        private BindingSource bsV_Struct_PERE_GOLOS = new BindingSource();
        private BindingSource bsV_Struct_SUM_BANK = new BindingSource();

        private Button btnSUM_BANK = null;
        private Button btnOWNER = null;
        private Button btnPERE_GOLOS = null;
        private Button btnMAN_BANK = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScreenSize = Screen.GetWorkingArea(this).Size;
            this.Height  = ScreenSize.Height - 20;
            this.Width  = ScreenSize.Width - 20;
            this.Left = 10;
            this.Top = 10;

//            fCreateDataGrids();
        }
// mode - 1 - enter data; 2 - create file
        private void fCreateDataGrids(Int16 mode)
        {
            dgvV_DECLARATION_HEAD.Parent = this;
            dgvV_DECLARATION_HEAD.Visible = false;
            dgvV_DECLARATION_HEAD.Left = 0;
            dgvV_DECLARATION_HEAD.Top = menuStrip1.Height;
            dgvV_DECLARATION_HEAD.Width = this.ClientSize.Width;
            dgvV_DECLARATION_HEAD.Height = (this.ClientSize.Height - menuStrip1.Height) / 3 ;
            ContextMenuStrip cmsV_DECLARATION_HEAD = new ContextMenuStrip();
            dgvV_DECLARATION_HEAD.ContextMenuStrip = cmsV_DECLARATION_HEAD;
            
            switch (mode)
            {
                case 1 :
                    cmsV_DECLARATION_HEAD.Items.Add("Додати запис");
                    cmsV_DECLARATION_HEAD.Items.Add("Редагувати запис");
                    cmsV_DECLARATION_HEAD.Items.Add("Видалити запис");
                    break;
                case 2 :
                    cmsV_DECLARATION_HEAD.Items.Add("Перегляд");
                    cmsV_DECLARATION_HEAD.Items.Add("Сформувати файл");
                    break;
            }
            
/////            cmsV_DECLARATION_HEAD.Items.Add("Додати запис");
/////            cmsV_DECLARATION_HEAD.Items.Add("Редагувати запис");
/////            cmsV_DECLARATION_HEAD.Items.Add("Видалити запис");
            cmsV_DECLARATION_HEAD.ItemClicked += new ToolStripItemClickedEventHandler(cmsV_DECLARATION_HEAD_ItemClicked);

            dgvV_Struct_SUM_BANK.Parent = this;
            dgvV_Struct_SUM_BANK.Visible = false;
            dgvV_Struct_SUM_BANK.Left = 0;
            dgvV_Struct_SUM_BANK.Top = menuStrip1.Height + dgvV_DECLARATION_HEAD.Height + 46;

            dgvV_Struct_SUM_BANK.Width = this.ClientSize.Width;
            dgvV_Struct_SUM_BANK.Height = (this.ClientSize.Height - menuStrip1.Height) * 2 / 3 - 40;
            ContextMenuStrip cmsV_Struct_SUM_BANK = new ContextMenuStrip();
            dgvV_Struct_SUM_BANK.ContextMenuStrip = cmsV_Struct_SUM_BANK;

            switch (mode)
            {
                case 1:
                    cmsV_Struct_SUM_BANK.Items.Add("Додати запис");
                    cmsV_Struct_SUM_BANK.Items.Add("Редагувати запис");
                    cmsV_Struct_SUM_BANK.Items.Add("Видалити запис");
                    break;
                case 2:
                    cmsV_Struct_SUM_BANK.Items.Add("Перегляд");
                    cmsV_Struct_SUM_BANK.Items.Add("Сформувати файл");
                    break;
            }

            /////           cmsV_Struct_SUM_BANK.Items.Add("Додати запис");
            /////            cmsV_Struct_SUM_BANK.Items.Add("Редагувати запис");
            /////            cmsV_Struct_SUM_BANK.Items.Add("Видалити запис");
            cmsV_Struct_SUM_BANK.ItemClicked += new ToolStripItemClickedEventHandler(cmsV_Struct_SUM_BANK_ItemClicked);

            dgvV_Struct_OWNER.Parent = this;
            dgvV_Struct_OWNER.Visible = false;
            dgvV_Struct_OWNER.Left = 0;
            dgvV_Struct_OWNER.Top = menuStrip1.Height + dgvV_DECLARATION_HEAD.Height + 46;

            dgvV_Struct_OWNER.Width = this.ClientSize.Width;
            dgvV_Struct_OWNER.Height = (this.ClientSize.Height - menuStrip1.Height) * 2 / 3 - 40;
            ContextMenuStrip cmsV_Struct_OWNER = new ContextMenuStrip();
            dgvV_Struct_OWNER.ContextMenuStrip = cmsV_Struct_OWNER;

            switch (mode)
            {
                case 1:
                    cmsV_Struct_OWNER.Items.Add("Додати запис");
                    cmsV_Struct_OWNER.Items.Add("Редагувати запис");
                    cmsV_Struct_OWNER.Items.Add("Видалити запис");
                    break;
                case 2:
                    cmsV_Struct_OWNER.Items.Add("Перегляд");
                    cmsV_Struct_OWNER.Items.Add("Сформувати файл");
                    break;
            }

            /////            cmsV_Struct_OWNER.Items.Add("Додати запис");
            /////            cmsV_Struct_OWNER.Items.Add("Редагувати запис");
            /////            cmsV_Struct_OWNER.Items.Add("Видалити запис");
            cmsV_Struct_OWNER.ItemClicked += new ToolStripItemClickedEventHandler(cmsV_Struct_OWNER_ItemClicked);

            dgvV_Struct_MAN_BANK.Parent = this;
            dgvV_Struct_MAN_BANK.Visible = false;
            dgvV_Struct_MAN_BANK.Left = 0;
            dgvV_Struct_MAN_BANK.Top = menuStrip1.Height + dgvV_DECLARATION_HEAD.Height + 46;

            dgvV_Struct_MAN_BANK.Width = this.ClientSize.Width;
            dgvV_Struct_MAN_BANK.Height = (this.ClientSize.Height - menuStrip1.Height) * 2 / 3 - 40;
            ContextMenuStrip cmsV_Struct_MAN_BANK = new ContextMenuStrip();
            dgvV_Struct_MAN_BANK.ContextMenuStrip = cmsV_Struct_MAN_BANK;
            switch (mode)
            {
                case 1:
                    cmsV_Struct_MAN_BANK.Items.Add("Додати запис");
                    cmsV_Struct_MAN_BANK.Items.Add("Редагувати запис");
                    cmsV_Struct_MAN_BANK.Items.Add("Видалити запис");
                    break;
                case 2:
                    cmsV_Struct_MAN_BANK.Items.Add("Перегляд");
                    cmsV_Struct_MAN_BANK.Items.Add("Сформувати файл");
                    break;
            }

/////            cmsV_Struct_MAN_BANK.Items.Add("Додати запис");
            /////            cmsV_Struct_MAN_BANK.Items.Add("Редагувати запис");
            /////            cmsV_Struct_MAN_BANK.Items.Add("Видалити запис");
            cmsV_Struct_MAN_BANK.ItemClicked += new ToolStripItemClickedEventHandler(cmsV_Struct_MAN_BANK_ItemClicked);

            dgvV_Struct_PERE_GOLOS.Parent = this;
            dgvV_Struct_PERE_GOLOS.Visible = false;
            dgvV_Struct_PERE_GOLOS.Left = 0;
            dgvV_Struct_PERE_GOLOS.Top = menuStrip1.Height + dgvV_DECLARATION_HEAD.Height + 46;

            dgvV_Struct_PERE_GOLOS.Width = this.ClientSize.Width;
            dgvV_Struct_PERE_GOLOS.Height = (this.ClientSize.Height - menuStrip1.Height) * 2 / 3 - 40;
            ContextMenuStrip cmsV_Struct_PERE_GOLOS = new ContextMenuStrip();
            dgvV_Struct_PERE_GOLOS.ContextMenuStrip = cmsV_Struct_PERE_GOLOS;

            switch (mode)
            {
                case 1:
                    cmsV_Struct_PERE_GOLOS.Items.Add("Додати запис");
                    cmsV_Struct_PERE_GOLOS.Items.Add("Редагувати запис");
                    cmsV_Struct_PERE_GOLOS.Items.Add("Видалити запис");
                    break;
                case 2:
                    cmsV_Struct_PERE_GOLOS.Items.Add("Перегляд");
                    cmsV_Struct_PERE_GOLOS.Items.Add("Сформувати файл");
                    break;
            }

/////            cmsV_Struct_PERE_GOLOS.Items.Add("Додати запис");
/////            cmsV_Struct_PERE_GOLOS.Items.Add("Редагувати запис");
/////            cmsV_Struct_PERE_GOLOS.Items.Add("Видалити запис");
            cmsV_Struct_PERE_GOLOS.ItemClicked += new ToolStripItemClickedEventHandler(cmsV_Struct_PERE_GOLOS_ItemClicked);



            btnSUM_BANK = new Button();
            btnSUM_BANK.Height = 23;
            btnSUM_BANK.Width = 120;
            btnSUM_BANK.Top = dgvV_Struct_SUM_BANK.Top - btnSUM_BANK.Height - btnSUM_BANK.Height / 2;
            btnSUM_BANK.Left = (this.Width - (this.Width - btnSUM_BANK.Width)) / 5;
            btnSUM_BANK.Name = "btnSUM_BANK";
            btnSUM_BANK.Text = "Статутний капітал SUM_BANK";
            btnSUM_BANK.Tag = 1;
            btnSUM_BANK.Click += new EventHandler(btnChoice_Click);
            this.Controls.Add(btnSUM_BANK);
            btnSUM_BANK.Visible = false;


            btnOWNER = new Button();
            btnOWNER.Height = 23;
            btnOWNER.Width = 120;
            btnOWNER.Top = btnSUM_BANK.Top;
            btnOWNER.Left = btnSUM_BANK.Left + (this.Width - (this.Width - btnSUM_BANK.Width)) / 5 + btnSUM_BANK.Width;
            btnOWNER.Name = "btnOWNER";
            btnOWNER.Text = "Власники OWNER";
            btnOWNER.Tag = 2;
            btnOWNER.Click += new EventHandler(btnChoice_Click);
            this.Controls.Add(btnOWNER);
            btnOWNER.Visible = false;

            btnPERE_GOLOS = new Button();
            btnPERE_GOLOS.Height = 23;
            btnPERE_GOLOS.Width = 120;
            btnPERE_GOLOS.Top = btnSUM_BANK.Top;
            btnPERE_GOLOS.Left = btnOWNER.Left + (this.Width - (this.Width - btnSUM_BANK.Width)) / 5 + btnOWNER.Width;
            btnPERE_GOLOS.Name = "btnPERE_GOLOS";
            btnPERE_GOLOS.Text = "Передача голосів PERE_GOLOS";
            btnPERE_GOLOS.Tag = 3;
            btnPERE_GOLOS.Click += new EventHandler(btnChoice_Click);
            this.Controls.Add(btnPERE_GOLOS);
            btnPERE_GOLOS.Enabled = true;
            btnPERE_GOLOS.Visible = false;

            btnMAN_BANK = new Button();
            btnMAN_BANK.Height = 23;
            btnMAN_BANK.Width = 120;
            btnMAN_BANK.Top = btnSUM_BANK.Top;
            btnMAN_BANK.Left = btnPERE_GOLOS.Left + (this.Width - (this.Width - btnSUM_BANK.Width)) / 5 + btnPERE_GOLOS.Width;
            btnMAN_BANK.Name = "btnMAN_BANK";
            btnMAN_BANK.Text = "Керівник/виконавець MAN_BANK";
            btnMAN_BANK.Tag = 4;
            btnMAN_BANK.Click += new EventHandler(btnChoice_Click);
            this.Controls.Add(btnMAN_BANK);
            btnMAN_BANK.Visible = false;

        }

        private void btnChoice_Click(object sender, EventArgs e)
        {
            dgvV_Struct_PERE_GOLOS.Visible = false;
            dgvV_Struct_OWNER.Visible = false;
            dgvV_Struct_MAN_BANK.Visible = false;
            dgvV_Struct_SUM_BANK.Visible = false;

            btnPERE_GOLOS.BackColor = Control.DefaultBackColor;
            btnOWNER.BackColor = Control.DefaultBackColor;
            btnMAN_BANK.BackColor = Control.DefaultBackColor;
            btnSUM_BANK.BackColor = Control.DefaultBackColor;

            switch ((sender as Button).Name)
            {
                case "btnSUM_BANK":
                        dgvV_Struct_SUM_BANK.Visible = true;
                        btnSUM_BANK.BackColor = Color.BlueViolet;
                        break;
                case "btnOWNER" :
                        dgvV_Struct_OWNER.Visible = true;
                        btnOWNER.BackColor = Color.BlueViolet;
                        break;
                case "btnPERE_GOLOS" :
                        dgvV_Struct_PERE_GOLOS.Visible = true;
                        btnPERE_GOLOS.BackColor = Color.BlueViolet;
                        break;
                case "btnMAN_BANK":
                        dgvV_Struct_MAN_BANK.Visible = true;
                        btnMAN_BANK.BackColor = Color.BlueViolet;
                        break;
            }
        }

        private void cmsV_DECLARATION_HEAD_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            if (e.ClickedItem.Text == "Додати запис")
            {
                frmV_Declaration_Head = new frmV_DECLARATION_HEAD(1);
                fFill_V_DECLARATION_HEAD_Default();
                if (frmV_Declaration_Head.ShowDialog() == DialogResult.OK)
                {
                    fSaveAddDECLARATION_HEAD();
                }
                    
            }

            if (e.ClickedItem.Text == "Редагувати запис")
            {
                frmV_Declaration_Head = new frmV_DECLARATION_HEAD(2);
                fFill_V_DECLARATION_HEAD();
                if (frmV_Declaration_Head.ShowDialog() == DialogResult.OK)
                {
                    fSaveUpdDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }
            }

            if (e.ClickedItem.Text == "Видалити запис")
            {
                frmV_Declaration_Head = new frmV_DECLARATION_HEAD(3);
                fFill_V_DECLARATION_HEAD();
                if (frmV_Declaration_Head.ShowDialog() == DialogResult.OK)
                {
                    fSaveDelDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }

            }
            if (e.ClickedItem.Text == "Перегляд")
            {
                frmV_Declaration_Head = new frmV_DECLARATION_HEAD(3);
                fFill_V_DECLARATION_HEAD();
                if (frmV_Declaration_Head.ShowDialog() == DialogResult.OK)
                {
//                    fSaveDelDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }

            }

            if (e.ClickedItem.Text == "Сформувати файл")
            {
                fCreatePfile((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            }

        }

        private void cmsV_Struct_SUM_BANK_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "Додати запис")
            {
                frmV_Struct_Sum_Bank = new frmV_Struct_SUM_BANK(1);
                fFill_V_Struct_SUM_BANK_Default();
                if (frmV_Struct_Sum_Bank.ShowDialog() == DialogResult.OK)
                {
                    fSaveAddStruct_SUM_BANK();
                }

            }

            if (e.ClickedItem.Text == "Редагувати запис")
            {
                frmV_Struct_Sum_Bank = new frmV_Struct_SUM_BANK(2);
                fFill_V_Struct_SUM_BANK();
                if (frmV_Struct_Sum_Bank.ShowDialog() == DialogResult.OK)
                {
                    fSaveUpdStruct_SUM_BANK((Int32)dgvV_Struct_SUM_BANK.Rows[dgvV_Struct_SUM_BANK.CurrentRow.Index].Cells[0].Value);
                }
            }

            if (e.ClickedItem.Text == "Видалити запис")
            {
                frmV_Struct_Sum_Bank = new frmV_Struct_SUM_BANK(3);
                fFill_V_Struct_SUM_BANK();
                if (frmV_Struct_Sum_Bank.ShowDialog() == DialogResult.OK)
                {
                    fSaveDelStruct_SUM_BANK((Int32)dgvV_Struct_SUM_BANK.Rows[dgvV_Struct_SUM_BANK.CurrentRow.Index].Cells[0].Value);
                }

            }

            if (e.ClickedItem.Text == "Перегляд")
            {
                frmV_Struct_Sum_Bank = new frmV_Struct_SUM_BANK(3);
                fFill_V_Struct_SUM_BANK();
                if (frmV_Struct_Sum_Bank.ShowDialog() == DialogResult.OK)
                {
                    //                    fSaveDelDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }

            }

            if (e.ClickedItem.Text == "Сформувати файл")
            {
                fCreatePfile((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            }

        }

        private void cmsV_Struct_OWNER_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "Додати запис")
            {
                frmV_Struct_Owner = new frmV_Struct_OWNER(1);
                fFill_V_Struct_OWNER_Default();
                if (frmV_Struct_Owner.ShowDialog() == DialogResult.OK)
                {
                    fSaveAddStruct_OWNER();
                }

            }

            if (e.ClickedItem.Text == "Редагувати запис")
            {
                frmV_Struct_Owner = new frmV_Struct_OWNER(2);
                fFill_V_Struct_OWNER();
                if (frmV_Struct_Owner.ShowDialog() == DialogResult.OK)
                {
                    fSaveUpdStruct_OWNER((Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[31].Value);
                }
            }

            if (e.ClickedItem.Text == "Видалити запис")
            {
                frmV_Struct_Owner = new frmV_Struct_OWNER(3);
                fFill_V_Struct_OWNER();
                if (frmV_Struct_Owner.ShowDialog() == DialogResult.OK)
                {
                    fSaveDelStruct_OWNER((Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[31].Value);
                }

            }

            if (e.ClickedItem.Text == "Перегляд")
            {
                frmV_Struct_Owner = new frmV_Struct_OWNER(2);
                fFill_V_Struct_OWNER();
                if (frmV_Struct_Owner.ShowDialog() == DialogResult.OK)
                {
                    //                    fSaveDelDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }

            }

            if (e.ClickedItem.Text == "Сформувати файл")
            {
                fCreatePfile((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            }

        }

        private void cmsV_Struct_MAN_BANK_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "Додати запис")
            {
                frmV_Struct_Man_Bank = new frmV_Struct_MAN_BANK(1);
                fFill_V_Struct_MAN_BANK_Default();
                if (frmV_Struct_Man_Bank.ShowDialog() == DialogResult.OK)
                {
                    fSaveAddStruct_MAN_BANK();
                }
            }

            if (e.ClickedItem.Text == "Редагувати запис")
            {
                frmV_Struct_Man_Bank = new frmV_Struct_MAN_BANK(2);
                fFill_V_Struct_MAN_BANK();
                if (frmV_Struct_Man_Bank.ShowDialog() == DialogResult.OK)
                {
                    fSaveUpdStruct_MAN_BANK((Int32)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[10].Value);
                }
            }

            if (e.ClickedItem.Text == "Видалити запис")
            {
                frmV_Struct_Man_Bank = new frmV_Struct_MAN_BANK(3);
                fFill_V_Struct_MAN_BANK();
                if (frmV_Struct_Man_Bank.ShowDialog() == DialogResult.OK)
                {
                    fSaveDelStruct_MAN_BANK((Int32)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[10].Value);
                }

            }

            if (e.ClickedItem.Text == "Перегляд")
            {
                frmV_Struct_Man_Bank = new frmV_Struct_MAN_BANK(3);
                fFill_V_Struct_MAN_BANK();
                if (frmV_Struct_Man_Bank.ShowDialog() == DialogResult.OK)
                {
                    //                    fSaveDelDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }

            }

            if (e.ClickedItem.Text == "Сформувати файл")
            {
                fCreatePfile((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            }

        }

        private void cmsV_Struct_PERE_GOLOS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "Додати запис")
            {
                frmV_Struct_Pere_Golos = new frmV_Struct_PERE_GOLOS(1);
                fFill_V_Struct_PERE_GOLOS_Default();
                if (frmV_Struct_Pere_Golos.ShowDialog() == DialogResult.OK)
                {
                    fSaveAddStruct_PERE_GOLOS();
                }
            }

            if (e.ClickedItem.Text == "Редагувати запис")
            {
                frmV_Struct_Pere_Golos = new frmV_Struct_PERE_GOLOS(2);
                fFill_V_Struct_PERE_GOLOS();
                if (frmV_Struct_Pere_Golos.ShowDialog() == DialogResult.OK)
                {
                    fSaveUpdStruct_PERE_GOLOS((Int32)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[14].Value);
                }
            }

            if (e.ClickedItem.Text == "Видалити запис")
            {
                frmV_Struct_Pere_Golos = new frmV_Struct_PERE_GOLOS(3);
                fFill_V_Struct_PERE_GOLOS();
                if (frmV_Struct_Pere_Golos.ShowDialog() == DialogResult.OK)
                {
                    fSaveDelStruct_PERE_GOLOS((Int32)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[14].Value);
                }

            }

            if (e.ClickedItem.Text == "Перегляд")
            {
                frmV_Struct_Pere_Golos = new frmV_Struct_PERE_GOLOS(3);
                fFill_V_Struct_PERE_GOLOS();
                if (frmV_Struct_Pere_Golos.ShowDialog() == DialogResult.OK)
                {
                    //                    fSaveDelDECLARATION_HEAD((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
                }

            }

            if (e.ClickedItem.Text == "Сформувати файл")
            {
                fCreatePfile((Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            }

        }


        private void fFill_V_DECLARATION_HEAD_Default()
        {
            frmV_Declaration_Head.INF_DT = DateTime.Now;
            frmV_Declaration_Head.TABLE_PIC = "";
            frmV_Declaration_Head.PICTURE = "";
            frmV_Declaration_Head.EDRPOU = fDefReferData("SectionHEAD", "EDRPOU");
            frmV_Declaration_Head.MFO = fDefReferData("SectionHEAD", "MFO");
            frmV_Declaration_Head.IDBANK = fDefReferData("SectionHEAD", "IDBANK");
            frmV_Declaration_Head.CDTASK = fDefReferData("SectionHEAD", "CDTASK");
            frmV_Declaration_Head.CDSUB = fDefReferData("SectionHEAD", "CDSUB");
            frmV_Declaration_Head.CDFORM = fDefReferData("SectionHEAD", "CDFORM");
            frmV_Declaration_Head.KU = fDefReferData("SectionHEAD", "KU");
        }

        private void fFill_V_DECLARATION_HEAD()
        {
            frmV_Declaration_Head.INF_DT = (DateTime)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["INF_DT"];
            frmV_Declaration_Head.TABLE_PIC = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["TABLE_PIC_fullfilename"].ToString();
            frmV_Declaration_Head.PICTURE = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["PICTURE_fullfilename"].ToString();
            frmV_Declaration_Head.EDRPOU = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["EDRPOU"].ToString();
            frmV_Declaration_Head.MFO = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["MFO"].ToString();
            frmV_Declaration_Head.IDBANK = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["IDBANK"].ToString();
            frmV_Declaration_Head.CDTASK = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["CDTASK"].ToString();
            frmV_Declaration_Head.CDSUB = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["CDSUB"].ToString();
            frmV_Declaration_Head.CDFORM = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["CDFORM"].ToString();
            frmV_Declaration_Head.KU = dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["KU"].ToString();
        }

        private void fFill_V_Struct_SUM_BANK_Default()
        {
// здесь надо поработать по контролю от PERE_GOLOS
            frmV_Struct_Sum_Bank.GT_GOLOS = 0;
            frmV_Struct_Sum_Bank.GT_VIDSOTOK = 0;
        }

        private void fFill_V_Struct_SUM_BANK()
        {
            frmV_Struct_Sum_Bank.GT_GOLOS =    (Int32)dgvV_Struct_SUM_BANK.Rows[dgvV_Struct_SUM_BANK.CurrentRow.Index].Cells[3].Value;
            frmV_Struct_Sum_Bank.GT_VIDSOTOK = (Decimal)dgvV_Struct_SUM_BANK.Rows[dgvV_Struct_SUM_BANK.CurrentRow.Index].Cells[2].Value;
        }

        private void fFill_V_Struct_OWNER_Default()
        {

            frmV_Struct_Owner.OWNER_TYPE            = "1";       
            frmV_Struct_Owner.OWNER_OZN             = "20";       
            frmV_Struct_Owner.OWNER_POS             = "";
            frmV_Struct_Owner.OWNER_DATE            = DateTime.Now;
            frmV_Struct_Owner.OWNER_DORG            = "";
            
            frmV_Struct_Owner.GT_VIDSOTOK_GOL_UCH   = 0;
            frmV_Struct_Owner.GT_GOLOSI_GOL_UCH     = 0;
            
            frmV_Struct_Owner.UT_VIDSOTOK_OPR_UCH   = 0;
            frmV_Struct_Owner.UT_NOMINAL_OPR_UCH	= 0;
            frmV_Struct_Owner.UT_GOLOSI_OPR_UCH	    = 0;
            
            frmV_Struct_Owner.ADR_COD_KR            = "";         
            frmV_Struct_Owner.ADR_INDEX	            = "";
            frmV_Struct_Owner.ADR_PUNKT	            = "";
            frmV_Struct_Owner.ADR_UL                = "";
            frmV_Struct_Owner.ADR_BUD	            = "";
            frmV_Struct_Owner.ADR_KORP	            = "";
            frmV_Struct_Owner.ADR_OFF               = "";
            
            frmV_Struct_Owner.NT_COD                = "";
            frmV_Struct_Owner.NT_NM1                = "";
            frmV_Struct_Owner.NT_NM2                = "";
            frmV_Struct_Owner.NT_NM3                = "";
            
            frmV_Struct_Owner.PS_SR	                = "";
            frmV_Struct_Owner.PS_NM	                = "";
            frmV_Struct_Owner.PS_DT                 = DateTime.Now;
            frmV_Struct_Owner.PS_ORG                = "";

            frmV_Struct_Owner.UT_VIDSOTOK_PR_UCH    = 0;
            frmV_Struct_Owner.UT_NOMINAL_PR_UCH     = 0;
            frmV_Struct_Owner.UT_GOLOSI_PR_UCH      = 0;

            frmV_Struct_Owner.GT_VIDSOTOK_ZAG_UCH   = 0;
            frmV_Struct_Owner.GT_GOLOSI_ZAG_UCH     = 0;


        }

        private void fFill_V_Struct_OWNER()
        {

            // idDECLARATION, OWNER_TYPE, OWNER_OZN, OWNER_POS, OWNER_DATE, OWNER_DORG, UT_VIDSOTOK_GOL_UCH, UT_GOLOSI_GOL_UCH, UT_VIDSOTOK_OPR_UCH
            //       0          1           2           3           4           5           6                   7                   8                       

            //            , UT_NOMINAL_OPR_UCH, UT_GOLOSI_OPR_UCH, ADR_COD_KR, ADR_INDEX, ADR_PUNKT, ADR_UL, ADR_BUD, ADR_KORP, ADR_OFF, 
            //                      9               10                  11          12      13          14      15      16          17
            //            NT_COD, NT_NM1, NT_NM2, NT_NM3, PS_SR, PS_NM, PS_DT, PS_ORG, UT_VIDSOTOK_PR_UCH, UT_NOMINAL_PR_UCH, UT_GOLOSI_PR_UCH, 
            //              18      19      20      21      22      23   24     25          26                  27                  28   
            //            UT_VIDSOTOK_ZAG_UCH, UT_GOLOSI_ZAG_UCH
            //                    29                  30
            frmV_Struct_Owner.OWNER_TYPE = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[1].Value;
            frmV_Struct_Owner.OWNER_OZN = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[2].Value;

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[3].Value == DBNull.Value) 
            { 
                frmV_Struct_Owner.OWNER_POS = "";
            }
            else
            {
                frmV_Struct_Owner.OWNER_POS = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[3].Value;
            }
            
            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[4].Value == DBNull.Value)
            {
                frmV_Struct_Owner.OWNER_DATE = DateTime.Now;
            }
            else
            {
                frmV_Struct_Owner.OWNER_DATE = (DateTime)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[4].Value;
            }

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[5].Value == DBNull.Value)
            {
                frmV_Struct_Owner.OWNER_DORG = "";
            }
            else
            {
                frmV_Struct_Owner.OWNER_DORG = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[5].Value;
            }
            

            frmV_Struct_Owner.GT_VIDSOTOK_GOL_UCH = (Decimal)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[6].Value;
            frmV_Struct_Owner.GT_GOLOSI_GOL_UCH = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[7].Value;

            frmV_Struct_Owner.UT_VIDSOTOK_OPR_UCH = (Decimal)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[8].Value;
            frmV_Struct_Owner.UT_NOMINAL_OPR_UCH = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[9].Value;
            frmV_Struct_Owner.UT_GOLOSI_OPR_UCH = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[10].Value;

            frmV_Struct_Owner.ADR_COD_KR = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[11].Value;
            frmV_Struct_Owner.ADR_INDEX = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[12].Value;
            frmV_Struct_Owner.ADR_PUNKT = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[13].Value;
            frmV_Struct_Owner.ADR_UL = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[14].Value;
            frmV_Struct_Owner.ADR_BUD = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[15].Value;
            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[16].Value == DBNull.Value)
            {
                frmV_Struct_Owner.ADR_KORP = "";
            }
            else
            {
                frmV_Struct_Owner.ADR_KORP = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[16].Value;
            }

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[17].Value == DBNull.Value)
            {
                frmV_Struct_Owner.ADR_OFF = "";
            }
            else
            {
                frmV_Struct_Owner.ADR_OFF = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[17].Value;
            }

            

            frmV_Struct_Owner.NT_COD = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[18].Value;
            frmV_Struct_Owner.NT_NM1 = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[19].Value;
            frmV_Struct_Owner.NT_NM2 = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[20].Value;

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[21].Value == DBNull.Value)
            {
                frmV_Struct_Owner.NT_NM3 = "";
            }
            else
            {
                frmV_Struct_Owner.NT_NM3 = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[21].Value;
            }

            

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[22].Value == DBNull.Value)
            {
                frmV_Struct_Owner.PS_SR = "";
            }
            else
            {
                frmV_Struct_Owner.PS_SR = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[22].Value;
            }

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[23].Value == DBNull.Value)
            {
//              frmV_Struct_Owner.PS_NM = "";
            }
            else
            {
                frmV_Struct_Owner.PS_NM = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[23].Value;
            }

            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[24].Value == DBNull.Value)
            {
                frmV_Struct_Owner.PS_DT = DateTime.Now;
            }
            else
            {
                frmV_Struct_Owner.PS_DT = (DateTime)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[24].Value;
            }


            if (dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[25].Value == DBNull.Value)
            {
                frmV_Struct_Owner.PS_ORG = "";
            }
            else
            {
                frmV_Struct_Owner.PS_ORG = (String)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[25].Value; ;
            }

            

            frmV_Struct_Owner.UT_VIDSOTOK_PR_UCH = (Decimal)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[26].Value;
            frmV_Struct_Owner.UT_NOMINAL_PR_UCH = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[27].Value;
            frmV_Struct_Owner.UT_GOLOSI_PR_UCH = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[28].Value;

            frmV_Struct_Owner.GT_VIDSOTOK_ZAG_UCH = (Decimal)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[29].Value;
            frmV_Struct_Owner.GT_GOLOSI_ZAG_UCH = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[30].Value;

        }

        private void fFill_V_Struct_MAN_BANK_Default()
        {
            // здесь надо поработать по контролю от PERE_GOLOS
            frmV_Struct_Man_Bank.FIO_NM1_MB_NAZVA       = "";
            frmV_Struct_Man_Bank.FIO_NM2_MB_NAZVA       = "";
            frmV_Struct_Man_Bank.FIO_NM3_MB_NAZVA       = "";
            frmV_Struct_Man_Bank.MB_POS                 = "";
            frmV_Struct_Man_Bank.MB_DT                  = DateTime.Now;
            frmV_Struct_Man_Bank.FIO_NM1_MB_ISP_NAZVA   = "";
            frmV_Struct_Man_Bank.FIO_NM2_MB_ISP_NAZVA   = "";
            frmV_Struct_Man_Bank.FIO_NM3_MB_ISP_NAZVA   = "";
            frmV_Struct_Man_Bank.MB_TLF                 = "";
        }

        private void fFill_V_Struct_MAN_BANK()
        {
          frmV_Struct_Man_Bank.MB_POS = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[1].Value;
          frmV_Struct_Man_Bank.MB_DT = (DateTime)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[2].Value;
          frmV_Struct_Man_Bank.MB_TLF = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[3].Value;
          frmV_Struct_Man_Bank.FIO_NM1_MB_NAZVA = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[4].Value;
          frmV_Struct_Man_Bank.FIO_NM2_MB_NAZVA = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[5].Value;
          frmV_Struct_Man_Bank.FIO_NM3_MB_NAZVA = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[6].Value;
          frmV_Struct_Man_Bank.FIO_NM1_MB_ISP_NAZVA = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[7].Value;
          frmV_Struct_Man_Bank.FIO_NM2_MB_ISP_NAZVA = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[8].Value;
          frmV_Struct_Man_Bank.FIO_NM3_MB_ISP_NAZVA = (String)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[9].Value;
        }

        private void fFill_V_Struct_PERE_GOLOS_Default()
        {
            frmV_Struct_Pere_Golos.GL_NOMER                 = "";
            frmV_Struct_Pere_Golos.GL_DT                    = DateTime.Now;
            frmV_Struct_Pere_Golos.GL_PRICH                 = "";
            frmV_Struct_Pere_Golos.NT_COD_FROM_GL_OSOBA     = "";
            frmV_Struct_Pere_Golos.NT_NM1_FROM_GL_OSOBA     = "";
            frmV_Struct_Pere_Golos.NT_NM2_FROM_GL_OSOBA     = "";
            frmV_Struct_Pere_Golos.NT_NM3_FROM_GL_OSOBA     = "";
            frmV_Struct_Pere_Golos.NT_COD_TO_GL_OSOBA       = "";
            frmV_Struct_Pere_Golos.NT_NM1_TO_GL_OSOBA       = "";
            frmV_Struct_Pere_Golos.NT_NM2_TO_GL_OSOBA       = "";
            frmV_Struct_Pere_Golos.NT_NM3_TO_GL_OSOBA       = "";
            frmV_Struct_Pere_Golos.GT_VIDSOTOK              = 0;
            frmV_Struct_Pere_Golos.GT_GOLOS                 = 0;
        }

        private void fFill_V_Struct_PERE_GOLOS()
        {
/*
idDECLARATION, GL_NOMER, GL_DT, GL_PRICH, NT_COD_FROM_GL_OSOBA
    0               1       2       3       4
, NT_NM1_FROM_GL_OSOBA, NT_NM2_FROM_GL_OSOBA, NT_NM3_FROM_GL_OSOBA
    5                           6                   7
, NT_COD_TO_GL_OSOBA, NT_NM1_TO_GL_OSOBA, NT_NM2_TO_GL_OSOBA, NT_NM3_TO_GL_OSOBA
    8                       9                   10              11
, GT_VIDSOTOK, GT_GOLOS,  idDeclarationPERE_GOLOS_FROM_GL_OSOBA FROM
    12             13                   14           
 */ 
            frmV_Struct_Pere_Golos.GL_NOMER                 = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[1].Value;
            frmV_Struct_Pere_Golos.GL_DT                    = (DateTime)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[2].Value;
            frmV_Struct_Pere_Golos.GL_PRICH                 = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[3].Value;
            frmV_Struct_Pere_Golos.NT_COD_FROM_GL_OSOBA     = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[4].Value;
            frmV_Struct_Pere_Golos.NT_NM1_FROM_GL_OSOBA     = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[5].Value;
            frmV_Struct_Pere_Golos.NT_NM2_FROM_GL_OSOBA     = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[6].Value;
            frmV_Struct_Pere_Golos.NT_NM3_FROM_GL_OSOBA     = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[7].Value;
            frmV_Struct_Pere_Golos.NT_COD_TO_GL_OSOBA       = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[8].Value;
            frmV_Struct_Pere_Golos.NT_NM1_TO_GL_OSOBA       = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[9].Value;
            frmV_Struct_Pere_Golos.NT_NM2_TO_GL_OSOBA       = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[10].Value;
            frmV_Struct_Pere_Golos.NT_NM3_TO_GL_OSOBA       = (String)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[11].Value;
            frmV_Struct_Pere_Golos.GT_VIDSOTOK              = (Decimal)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[12].Value;
            frmV_Struct_Pere_Golos.GT_GOLOS                 = (Int32)dgvV_Struct_PERE_GOLOS.Rows[dgvV_Struct_PERE_GOLOS.CurrentRow.Index].Cells[13].Value;
        }
       

        private void fWorkWithData(Int16 mode)
        {
            fCreateDataGrids(mode);
            fCreateDataSet();
            btnSUM_BANK.Visible = true;
            btnOWNER.Visible = true;
            btnPERE_GOLOS.Visible = true;
            btnMAN_BANK.Visible = true;
        }

        private void fClearDataSet()
        {
            
            if (dsData.Tables.Contains("V_Struct_MAN_BANK")) { dsData.Tables["V_Struct_MAN_BANK"].Clear(); }
            if (dsData.Tables.Contains("V_Struct_OWNER")) { dsData.Tables["V_Struct_OWNER"].Clear(); }
            if (dsData.Tables.Contains("V_Struct_PERE_GOLOS")) { dsData.Tables["V_Struct_PERE_GOLOS"].Clear(); }
            if (dsData.Tables.Contains("V_Struct_SUM_BANK")) { dsData.Tables["V_Struct_SUM_BANK"].Clear(); }
            if (dsData.Tables.Contains("V_DECLARATION_HEAD")) { dsData.Tables["V_DECLARATION_HEAD"].Clear(); }
        }

        private void fCreateDataSet()
        {
//            Int32 currRow_V_DECLARATION_HEAD = dgvV_DECLARATION_HEAD.CurrentRow.Index;
//            Int32 currRow_V_SUM_BANK = dgvV_Struct_SUM_BANK.CurrentRow.Index;
            
            sqlConn.ConnectionString = connStr;
            SqlCommand sqlCmd = new SqlCommand(connStr);
            sqlConn.Open();
            fClearDataSet();
            if (!dsData.Tables.Contains("V_DECLARATION_HEAD")) { dsData.Tables.Add("V_DECLARATION_HEAD"); }
            if (!dsData.Tables.Contains("V_Struct_MAN_BANK")) { dsData.Tables.Add("V_Struct_MAN_BANK"); }
            if (!dsData.Tables.Contains("V_Struct_OWNER")) { dsData.Tables.Add("V_Struct_OWNER"); }
            if (!dsData.Tables.Contains("V_Struct_PERE_GOLOS")) { dsData.Tables.Add("V_Struct_PERE_GOLOS"); }
            if (!dsData.Tables.Contains("V_Struct_SUM_BANK")) { dsData.Tables.Add("V_Struct_SUM_BANK"); }

            SqlDataAdapter daData = new SqlDataAdapter("SELECT id, INF_DT, TABLE_PIC_filename, TABLE_PIC_fullfilename, PICTURE_filename, PICTURE_fullfilename, idDECLARATION, EDRPOU, MFO, FNAME, IDBANK, CDTASK, CDSUB, CDFORM, FILL_DATE, FILL_TIME, EI, KU from V_DECLARATION_HEAD", sqlConn);
            daData.Fill(dsData.Tables["V_DECLARATION_HEAD"]);

            daData = new SqlDataAdapter("SELECT id, idDECLARATION, GT_VIDSOTOK, GT_GOLOS from V_Struct_SUM_BANK", sqlConn);
            daData.Fill(dsData.Tables["V_Struct_SUM_BANK"]);


//            daData = new SqlDataAdapter("SELECT idDECLARATION,OWNER_TYPE,OWNER_OZN,OWNER_POS,OWNER_DATE,OWNER_DORG,idDeclaration_OWNER_GOL_UCH,UT_VIDSOTOK_GOL_UCH,UT_GOLOSI_GOL_UCH,idDeclaration_OWNER_OPR_UCH,UT_VIDSOTOK_OPR_UCH,UT_NOMINAL_OPR_UCH,UT_GOLOSI_OPR_UCH,idDeclaration_OWNER_OWNER_ADR,ADR_COD_KR,ADR_INDEX,ADR_PUNKT,ADR_UL,ADR_BUD,ADR_KORP,ADR_OFF,idDeclaration_OWNER_OWNER_NAZVA,NT_COD,NT_NM1,NT_NM2,NT_NM3,idDeclaration_OWNER_OWNER_PASS,PS_SR,PS_NM,PS_DT,PS_ORG,idDeclaration_OWNER_PR_UCH,UT_VIDSOTOK_PR_UCH,UT_NOMINAL_PR_UCH,UT_GOLOSI_PR_UCH,idDeclaration_OWNER_ZAG_UCH,UT_VIDSOTOK_ZAG_UCH,UT_GOLOSI_ZAG_UCH FROM V_Struct_OWNER", sqlConn);
            daData = new SqlDataAdapter("SELECT idDECLARATION, OWNER_TYPE, OWNER_OZN, OWNER_POS, OWNER_DATE, OWNER_DORG, GT_VIDSOTOK_GOL_UCH, GT_GOLOSI_GOL_UCH, UT_VIDSOTOK_OPR_UCH, UT_NOMINAL_OPR_UCH, UT_GOLOSI_OPR_UCH, ADR_COD_KR, ADR_INDEX, ADR_PUNKT, ADR_UL, ADR_BUD, ADR_KORP, ADR_OFF, NT_COD, NT_NM1, NT_NM2, NT_NM3, PS_SR, PS_NM, PS_DT, PS_ORG, UT_VIDSOTOK_PR_UCH, UT_NOMINAL_PR_UCH, UT_GOLOSI_PR_UCH, GT_VIDSOTOK_ZAG_UCH, GT_GOLOSI_ZAG_UCH, idDeclaration_OWNER_GOL_UCH FROM V_Struct_OWNER", sqlConn);
            daData.Fill(dsData.Tables["V_Struct_OWNER"]);

            daData = new SqlDataAdapter("SELECT idDECLARATION, MB_POS, MB_DT, MB_TLF, FIO_NM1_MB_NAZVA, FIO_NM2_MB_NAZVA, FIO_NM3_MB_NAZVA, FIO_NM1_MB_ISP_NAZVA, FIO_NM2_MB_ISP_NAZVA, FIO_NM3_MB_ISP_NAZVA, idDeclarationMAN_BANK_MB_NAZVA FROM V_Struct_MAN_BANK", sqlConn);
            daData.Fill(dsData.Tables["V_Struct_MAN_BANK"]);

            daData = new SqlDataAdapter("SELECT idDECLARATION, GL_NOMER, GL_DT, GL_PRICH, NT_COD_FROM_GL_OSOBA, NT_NM1_FROM_GL_OSOBA, NT_NM2_FROM_GL_OSOBA, NT_NM3_FROM_GL_OSOBA, NT_COD_TO_GL_OSOBA, NT_NM1_TO_GL_OSOBA, NT_NM2_TO_GL_OSOBA, NT_NM3_TO_GL_OSOBA, GT_VIDSOTOK, GT_GOLOS,  idDeclarationPERE_GOLOS_FROM_GL_OSOBA FROM V_Struct_PERE_GOLOS", sqlConn);
            daData.Fill(dsData.Tables["V_Struct_PERE_GOLOS"]);

            if (!dsData.Relations.Contains("DECLARATION_HEAD_SUM_BANK"))
            {
                DataRelation relation_DECLARATION_HEAD_SUM_BANK = new DataRelation("DECLARATION_HEAD_SUM_BANK",
                    dsData.Tables["V_DECLARATION_HEAD"].Columns["id"],
                    dsData.Tables["V_Struct_SUM_BANK"].Columns["idDECLARATION"]);
                dsData.Relations.Add(relation_DECLARATION_HEAD_SUM_BANK);
            }

            if (!dsData.Relations.Contains("DECLARATION_HEAD_OWNER"))
            {
                DataRelation relation_DECLARATION_HEAD_OWNER = new DataRelation("DECLARATION_HEAD_OWNER",
                    dsData.Tables["V_DECLARATION_HEAD"].Columns["id"],
                    dsData.Tables["V_Struct_OWNER"].Columns["idDECLARATION"]);
                dsData.Relations.Add(relation_DECLARATION_HEAD_OWNER);
            }

            if (!dsData.Relations.Contains("DECLARATION_HEAD_MAN_BANK"))
            {
                DataRelation relation_DECLARATION_HEAD_MAN_BANK = new DataRelation("DECLARATION_HEAD_MAN_BANK",
                    dsData.Tables["V_DECLARATION_HEAD"].Columns["id"],
                    dsData.Tables["V_Struct_MAN_BANK"].Columns["idDECLARATION"]);
                dsData.Relations.Add(relation_DECLARATION_HEAD_MAN_BANK);
            }

            if (!dsData.Relations.Contains("DECLARATION_HEAD_PERE_GOLOS"))
            {
                DataRelation relation_DECLARATION_HEAD_PERE_GOLOS = new DataRelation("DECLARATION_HEAD_PERE_GOLOS",
                    dsData.Tables["V_DECLARATION_HEAD"].Columns["id"],
                    dsData.Tables["V_Struct_PERE_GOLOS"].Columns["idDECLARATION"]);
                dsData.Relations.Add(relation_DECLARATION_HEAD_PERE_GOLOS);
            }


            bsV_DECLARATION_HEAD.DataSource = dsData;
            bsV_DECLARATION_HEAD.DataMember = "V_DECLARATION_HEAD";

            bsV_Struct_SUM_BANK.DataSource = bsV_DECLARATION_HEAD;
            bsV_Struct_SUM_BANK.DataMember = "DECLARATION_HEAD_SUM_BANK";

            bsV_Struct_OWNER.DataSource = bsV_DECLARATION_HEAD;
            bsV_Struct_OWNER.DataMember = "DECLARATION_HEAD_OWNER";

            bsV_Struct_MAN_BANK.DataSource = bsV_DECLARATION_HEAD;
            bsV_Struct_MAN_BANK.DataMember = "DECLARATION_HEAD_MAN_BANK";

            bsV_Struct_PERE_GOLOS.DataSource = bsV_DECLARATION_HEAD;
            bsV_Struct_PERE_GOLOS.DataMember = "DECLARATION_HEAD_PERE_GOLOS";
            
            dgvV_DECLARATION_HEAD.DataSource = bsV_DECLARATION_HEAD;
            dgvV_DECLARATION_HEAD.Visible = true;

            dgvV_Struct_SUM_BANK.DataSource = bsV_Struct_SUM_BANK;
            if (btnSUM_BANK.BackColor == Color.BlueViolet) { dgvV_Struct_SUM_BANK.Visible = true; } else { dgvV_Struct_SUM_BANK.Visible = false; }

            dgvV_Struct_OWNER.DataSource = bsV_Struct_OWNER;
            if (btnOWNER.BackColor == Color.BlueViolet) { dgvV_Struct_OWNER.Visible = true; } else { dgvV_Struct_OWNER.Visible = false; }


            dgvV_Struct_MAN_BANK.DataSource = bsV_Struct_MAN_BANK;
            if (btnMAN_BANK.BackColor == Color.BlueViolet) { dgvV_Struct_MAN_BANK.Visible = true; } else { dgvV_Struct_MAN_BANK.Visible = false; }

            dgvV_Struct_PERE_GOLOS.DataSource = bsV_Struct_PERE_GOLOS;
            if (btnPERE_GOLOS.BackColor == Color.BlueViolet) { dgvV_Struct_PERE_GOLOS.Visible = true; } else { dgvV_Struct_PERE_GOLOS.Visible = false; }


            sqlConn.Close();

//            if (dgvV_DECLARATION_HEAD.RowCount > currRow_V_DECLARATION_HEAD)
//            {
//                dgvV_DECLARATION_HEAD.Rows[currRow_V_DECLARATION_HEAD].Selected = true;
//            }
        }


        private String fDefReferData(String ReferenceName, String Parameter)
        {
            String RetVal = "";
            String SelectText = "SELECT ISNULL(VALUE, '') FROM ReferenceParameter where Enabled = 1 and ReferenceName = '" + ReferenceName + "' and Parameter = '" + Parameter + "'";
            //            MessageBox.Show(SelectText);
            SqlCommand sqlCmd = new SqlCommand(SelectText, sqlConn);
            try
            {
                sqlConn.Open();
                RetVal = (String)sqlCmd.ExecuteScalar();
            }
            catch (SqlException e) { MessageBox.Show("SqlException.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("Exception.\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return RetVal;
        }

        private void fSaveAddDECLARATION_HEAD()
        {
            fInsertDeclaration();
            fCreateDataSet();
        }

        private void fSaveUpdDECLARATION_HEAD(Int32 idDeclaration)
        {
            fUpdateDeclaration(idDeclaration);
            fCreateDataSet();   
        }

        private void fSaveDelDECLARATION_HEAD(Int32 idDeclaration)
        {
            fDeleteDeclaration(idDeclaration);
            fCreateDataSet();
        }


        private Int32 fInsertDeclaration()
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            byte[] binBytesBase64 = null;
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_DECLARATION_HEAD_I";
            SQLProc.Parameters.Add("@INF_DT", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@TABLE_PIC", System.Data.SqlDbType.VarBinary, -1);
            SQLProc.Parameters.Add("@TABLE_PIC_filename", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@TABLE_PIC_fullfilename", System.Data.SqlDbType.VarChar, 500);
            SQLProc.Parameters.Add("@PICTURE", System.Data.SqlDbType.VarBinary, -1);
            SQLProc.Parameters.Add("@PICTURE_filename", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@PICTURE_fullfilename", System.Data.SqlDbType.VarChar, 500);

            SQLProc.Parameters.Add("@EDRPOU", System.Data.SqlDbType.VarChar, 12);
            SQLProc.Parameters.Add("@MFO", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@FNAME", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@IDBANK", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@CDTASK", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@CDSUB", System.Data.SqlDbType.VarChar, 5);
            SQLProc.Parameters.Add("@CDFORM", System.Data.SqlDbType.VarChar, 8);
            SQLProc.Parameters.Add("@FILL_DATE", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@FILL_TIME", System.Data.SqlDbType.VarChar, 4);
            SQLProc.Parameters.Add("@EI", System.Data.SqlDbType.VarChar, 1);
            SQLProc.Parameters.Add("@KU", System.Data.SqlDbType.VarChar, 2);

            SQLProc.Parameters["@EDRPOU"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@MFO"].Direction = System.Data.ParameterDirection.Input;            
            SQLProc.Parameters["@FNAME"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@IDBANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDTASK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDSUB"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDFORM"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FILL_DATE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FILL_TIME"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@EI"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@KU"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@INF_DT"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@TABLE_PIC"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@TABLE_PIC_filename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@TABLE_PIC_fullfilename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PICTURE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PICTURE_filename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PICTURE_fullfilename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            if (!File.Exists(frmV_Declaration_Head.TABLE_PIC))
            {
                MessageBox.Show("Отсутствует файл - " + frmV_Declaration_Head.TABLE_PIC);
            }
            else
            {
                String strPDF = Convert.ToBase64String(File.ReadAllBytes(frmV_Declaration_Head.TABLE_PIC));
                MessageBox.Show(strPDF);
                binBytesBase64 = Encoding.Default.GetBytes(strPDF);
            }

            SQLProc.Parameters["@INF_DT"].Value =  frmV_Declaration_Head.INF_DT;
            SQLProc.Parameters["@TABLE_PIC"].Value = binBytesBase64;
            SQLProc.Parameters["@TABLE_PIC_filename"].Value = Path.GetFileName(frmV_Declaration_Head.TABLE_PIC);
            SQLProc.Parameters["@TABLE_PIC_fullfilename"].Value = frmV_Declaration_Head.TABLE_PIC;
            binBytesBase64 = null;
            if (!File.Exists(frmV_Declaration_Head.PICTURE))
            {
                MessageBox.Show("Отсутствует файл - " + frmV_Declaration_Head.PICTURE);
            }
            else
            {
                String strPDF = Convert.ToBase64String(File.ReadAllBytes(frmV_Declaration_Head.PICTURE));
                binBytesBase64 = Encoding.Default.GetBytes(strPDF);
            }
            SQLProc.Parameters["@PICTURE"].Value = binBytesBase64;
            SQLProc.Parameters["@PICTURE_filename"].Value = Path.GetFileName(frmV_Declaration_Head.PICTURE);
            SQLProc.Parameters["@PICTURE_fullfilename"].Value = frmV_Declaration_Head.PICTURE;

            SQLProc.Parameters["@EDRPOU"].Value = frmV_Declaration_Head.EDRPOU;
            SQLProc.Parameters["@MFO"].Value = Convert.ToInt32(frmV_Declaration_Head.MFO);
            SQLProc.Parameters["@FNAME"].Value = "";
            SQLProc.Parameters["@IDBANK"].Value = frmV_Declaration_Head.IDBANK;
            SQLProc.Parameters["@CDTASK"].Value = frmV_Declaration_Head.CDTASK;
            SQLProc.Parameters["@CDSUB"].Value = frmV_Declaration_Head.CDSUB;
            SQLProc.Parameters["@CDFORM"].Value = frmV_Declaration_Head.CDFORM;
            SQLProc.Parameters["@FILL_DATE"].Value = DateTime.Now.ToString().Substring(0, 10);
            SQLProc.Parameters["@FILL_TIME"].Value = DateTime.Now.ToString().Substring(11, 2) + DateTime.Now.ToString().Substring(14, 2);
            SQLProc.Parameters["@EI"].Value = DBNull.Value;
            SQLProc.Parameters["@KU"].Value = frmV_Declaration_Head.KU;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);
                
            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_V_DECLARATION_HEAD_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_V_DECLARATION_HEAD_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fUpdateDeclaration(Int32 idDeclaration)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            byte[] binBytesBase64 = null;
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_DECLARATION_HEAD_U";
            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@INF_DT", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@TABLE_PIC", System.Data.SqlDbType.VarBinary, -1);
            SQLProc.Parameters.Add("@TABLE_PIC_filename", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@TABLE_PIC_fullfilename", System.Data.SqlDbType.VarChar, 500);
            SQLProc.Parameters.Add("@PICTURE", System.Data.SqlDbType.VarBinary, -1);
            SQLProc.Parameters.Add("@PICTURE_filename", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@PICTURE_fullfilename", System.Data.SqlDbType.VarChar, 500);


            SQLProc.Parameters.Add("@EDRPOU", System.Data.SqlDbType.VarChar, 12);
            SQLProc.Parameters.Add("@MFO", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@FNAME", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@IDBANK", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@CDTASK", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@CDSUB", System.Data.SqlDbType.VarChar, 5);
            SQLProc.Parameters.Add("@CDFORM", System.Data.SqlDbType.VarChar, 8);
            SQLProc.Parameters.Add("@FILL_DATE", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@FILL_TIME", System.Data.SqlDbType.VarChar, 4);
            SQLProc.Parameters.Add("@EI", System.Data.SqlDbType.VarChar, 1);
            SQLProc.Parameters.Add("@KU", System.Data.SqlDbType.VarChar, 2);

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@EDRPOU"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@MFO"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FNAME"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@IDBANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDTASK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDSUB"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDFORM"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FILL_DATE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FILL_TIME"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@EI"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@KU"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@INF_DT"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@TABLE_PIC"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@TABLE_PIC_filename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@TABLE_PIC_fullfilename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PICTURE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PICTURE_filename"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PICTURE_fullfilename"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            if (!File.Exists(frmV_Declaration_Head.TABLE_PIC))
            {
                MessageBox.Show("Отсутствует файл - " + frmV_Declaration_Head.TABLE_PIC);
            }
            else
            {
                String strPDF = Convert.ToBase64String(File.ReadAllBytes(frmV_Declaration_Head.TABLE_PIC));
                binBytesBase64 = Encoding.Default.GetBytes(strPDF);
            }

            SQLProc.Parameters["@INF_DT"].Value = frmV_Declaration_Head.INF_DT;
            SQLProc.Parameters["@TABLE_PIC"].Value = binBytesBase64;
            SQLProc.Parameters["@TABLE_PIC_filename"].Value = Path.GetFileName(frmV_Declaration_Head.TABLE_PIC);
            SQLProc.Parameters["@TABLE_PIC_fullfilename"].Value = frmV_Declaration_Head.TABLE_PIC;
            binBytesBase64 = null;
            if (!File.Exists(frmV_Declaration_Head.PICTURE))
            {
                MessageBox.Show("Отсутствует файл - " + frmV_Declaration_Head.PICTURE);
            }
            else
            {
                String strPDF = Convert.ToBase64String(File.ReadAllBytes(frmV_Declaration_Head.PICTURE));
                binBytesBase64 = Encoding.Default.GetBytes(strPDF);
            }

            SQLProc.Parameters["@idDeclaration"].Value = idDeclaration;
            SQLProc.Parameters["@PICTURE"].Value = binBytesBase64;
            SQLProc.Parameters["@PICTURE_filename"].Value = Path.GetFileName(frmV_Declaration_Head.PICTURE);
            SQLProc.Parameters["@PICTURE_fullfilename"].Value = frmV_Declaration_Head.PICTURE;

            SQLProc.Parameters["@EDRPOU"].Value = frmV_Declaration_Head.EDRPOU;
            SQLProc.Parameters["@MFO"].Value = Convert.ToInt32(frmV_Declaration_Head.MFO);
            SQLProc.Parameters["@FNAME"].Value = "";
            SQLProc.Parameters["@IDBANK"].Value = frmV_Declaration_Head.IDBANK;
            SQLProc.Parameters["@CDTASK"].Value = frmV_Declaration_Head.CDTASK;
            SQLProc.Parameters["@CDSUB"].Value = frmV_Declaration_Head.CDSUB;
            SQLProc.Parameters["@CDFORM"].Value = frmV_Declaration_Head.CDFORM;
            SQLProc.Parameters["@FILL_DATE"].Value = DateTime.Now.ToString().Substring(0, 10);
            SQLProc.Parameters["@FILL_TIME"].Value = DateTime.Now.ToString().Substring(11, 2) + DateTime.Now.ToString().Substring(14, 2);
            SQLProc.Parameters["@EI"].Value = "";
            SQLProc.Parameters["@KU"].Value = frmV_Declaration_Head.KU;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
//                retId = (Int32)SQLProc.Parameters["@id"].Value;
//                retErr = (Int32)SQLProc.Parameters["@error"].Value;
//                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
//                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in UPDATE data in P_V_DECLARATION_HEAD_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in UPDATE data in P_V_DECLARATION_HEAD_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fDeleteDeclaration(Int32 idDeclaration)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_DECLARATION_HEAD_D";
            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            SQLProc.Parameters["@idDeclaration"].Value = idDeclaration;
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                //                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                //                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in DELETE data in P_V_DECLARATION_HEAD_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in DELETE data in P_V_DECLARATION_HEAD_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }



        private void fSaveAddStruct_SUM_BANK()
        {
            fInsertSumBank();
            fCreateDataSet();
        }

        private void fSaveUpdStruct_SUM_BANK(Int32 idSumBank)
        {
            fUpdateSumBank(idSumBank);
            fCreateDataSet();
        }

        private void fSaveDelStruct_SUM_BANK(Int32 idSumBank)
        {
            fDeleteSumBank(idSumBank);
            fCreateDataSet();
        }


        private Int32 fInsertSumBank()
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_SUM_BANK_I";

            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
                                    
            SqlParameter Gt_Vidsotok = new SqlParameter("@GT_VIDSOTOK", System.Data.SqlDbType.Decimal);
            Gt_Vidsotok.Precision = 9;
            Gt_Vidsotok.Scale = 5;
            SQLProc.Parameters.Add(Gt_Vidsotok);

            SQLProc.Parameters.Add("@GT_GOLOS", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GT_VIDSOTOK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOS"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;
//            MessageBox.Show("Ins Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"];
            SQLProc.Parameters["@GT_VIDSOTOK"].Value = frmV_Struct_Sum_Bank.GT_VIDSOTOK;
            SQLProc.Parameters["@GT_GOLOS"].Value = frmV_Struct_Sum_Bank.GT_GOLOS;
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_SUM_BANK_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_SUM_BANK_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fUpdateSumBank(Int32 idSumBank)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_SUM_BANK_U";
            SQLProc.Parameters.Add("@idSUM_BANK", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
            SqlParameter Gt_Vidsotok = new SqlParameter("@GT_VIDSOTOK", System.Data.SqlDbType.Decimal); Gt_Vidsotok.Precision = 9; Gt_Vidsotok.Scale = 5;
            SQLProc.Parameters.Add(Gt_Vidsotok);
            SQLProc.Parameters.Add("@GT_GOLOS", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idSUM_BANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GT_VIDSOTOK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOS"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            SQLProc.Parameters["@idSUM_BANK"].Value = idSumBank;

//            MessageBox.Show("Upd Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);

            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dgvV_Struct_SUM_BANK.Rows[dgvV_Struct_SUM_BANK.CurrentRow.Index].Cells[1].Value;
            SQLProc.Parameters["@GT_VIDSOTOK"].Value = frmV_Struct_Sum_Bank.GT_VIDSOTOK;
            SQLProc.Parameters["@GT_GOLOS"].Value = frmV_Struct_Sum_Bank.GT_GOLOS;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
//                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
//                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in UPDATE data in P_V_SUM_BANK_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in UPDATE data in P_V_SUM_BANK_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fDeleteSumBank(Int32 idSumBank)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_SUM_BANK_D";
            SQLProc.Parameters.Add("@idSUM_BANK", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idSUM_BANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            SQLProc.Parameters["@idSUM_BANK"].Value = idSumBank;
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
//                retId = (Int32)SQLProc.Parameters["@id"].Value;
//                retErr = (Int32)SQLProc.Parameters["@error"].Value;
//                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
//                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in DELETE data in P_V_SUM_BANK_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in DELETE data in P_V_SUM_BANK_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }



        private void fSaveAddStruct_OWNER()
        {
            fInsertOwner();
            fCreateDataSet();
        }

        private void fSaveUpdStruct_OWNER(Int32 idOwner)
        {
            fUpdateOwner(idOwner);
            fCreateDataSet();
        }

        private void fSaveDelStruct_OWNER(Int32 idOwner)
        {
            fDeleteOwner(idOwner);
            fCreateDataSet();
        }


        private Int32 fInsertOwner()
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_OWNER_I";

            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@OWNER_TYPE", System.Data.SqlDbType.VarChar , 1);
            SQLProc.Parameters.Add("@OWNER_OZN" , System.Data.SqlDbType.VarChar  , 2);
            SQLProc.Parameters.Add("@OWNER_POS" , System.Data.SqlDbType.VarChar  , 254);
            SQLProc.Parameters.Add("@OWNER_DATE", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@OWNER_DORG", System.Data.SqlDbType.VarChar, 254);
            
            SqlParameter GT_VIDSOTOK_GOL_UCH = new SqlParameter("@GT_VIDSOTOK_GOL_UCH", System.Data.SqlDbType.Decimal); GT_VIDSOTOK_GOL_UCH.Precision = 9; GT_VIDSOTOK_GOL_UCH.Scale = 5; SQLProc.Parameters.Add(GT_VIDSOTOK_GOL_UCH);
            SQLProc.Parameters.Add("@GT_GOLOSI_GOL_UCH", System.Data.SqlDbType.Int);

            SqlParameter UT_VIDSOTOK_OPR_UCH = new SqlParameter("@UT_VIDSOTOK_OPR_UCH", System.Data.SqlDbType.Decimal); UT_VIDSOTOK_OPR_UCH.Precision = 9; UT_VIDSOTOK_OPR_UCH.Scale = 5; SQLProc.Parameters.Add(UT_VIDSOTOK_OPR_UCH);
            SQLProc.Parameters.Add("@UT_NOMINAL_OPR_UCH"    , System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@UT_GOLOSI_OPR_UCH"     , System.Data.SqlDbType.Int);
                
            SQLProc.Parameters.Add("@ADR_COD_KR"            , System.Data.SqlDbType.VarChar , 3);	
            SQLProc.Parameters.Add("@ADR_INDEX"             , System.Data.SqlDbType.VarChar , 6); 	
            SQLProc.Parameters.Add("@ADR_PUNKT"             , System.Data.SqlDbType.VarChar , 50	);
            SQLProc.Parameters.Add("@ADR_UL"                , System.Data.SqlDbType.VarChar , 50);		
            SQLProc.Parameters.Add("@ADR_BUD"               , System.Data.SqlDbType.VarChar , 10);		
            SQLProc.Parameters.Add("@ADR_KORP"              , System.Data.SqlDbType.VarChar , 10);
            SQLProc.Parameters.Add("@ADR_OFF"               , System.Data.SqlDbType.VarChar, 10);

            SQLProc.Parameters.Add("@NT_COD"                , System.Data.SqlDbType.VarChar , 10);
            SQLProc.Parameters.Add("@NT_NM1"                , System.Data.SqlDbType.VarChar , 100);
            SQLProc.Parameters.Add("@NT_NM2"                , System.Data.SqlDbType.VarChar , 50);
            SQLProc.Parameters.Add("@NT_NM3"                , System.Data.SqlDbType.VarChar, 50);

            SQLProc.Parameters.Add("@PS_SR"                 , System.Data.SqlDbType.VarChar, 2);
            SQLProc.Parameters.Add("@PS_NM"                 , System.Data.SqlDbType.VarChar  , 6);
            SQLProc.Parameters.Add("@PS_DT"                 , System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@PS_ORG"                , System.Data.SqlDbType.VarChar, 254);

            SqlParameter UT_VIDSOTOK_PR_UCH = new SqlParameter("@UT_VIDSOTOK_PR_UCH", System.Data.SqlDbType.Decimal); UT_VIDSOTOK_PR_UCH.Precision = 9; UT_VIDSOTOK_PR_UCH.Scale = 5; SQLProc.Parameters.Add(UT_VIDSOTOK_PR_UCH);
            SQLProc.Parameters.Add("@UT_NOMINAL_PR_UCH", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@UT_GOLOSI_PR_UCH", System.Data.SqlDbType.Int);

            SqlParameter GT_VIDSOTOK_ZAG_UCH = new SqlParameter("@GT_VIDSOTOK_ZAG_UCH", System.Data.SqlDbType.Decimal); GT_VIDSOTOK_ZAG_UCH.Precision = 9; GT_VIDSOTOK_ZAG_UCH.Scale = 5; SQLProc.Parameters.Add(GT_VIDSOTOK_ZAG_UCH);
            SQLProc.Parameters.Add("@GT_GOLOSI_ZAG_UCH", System.Data.SqlDbType.Int);


            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@OWNER_TYPE"].Direction             = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@OWNER_OZN"].Direction              = System.Data.ParameterDirection.Input; 
            SQLProc.Parameters["@OWNER_POS"].Direction              = System.Data.ParameterDirection.Input; 
            SQLProc.Parameters["@OWNER_DATE"].Direction             = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@OWNER_DORG"].Direction             = System.Data.ParameterDirection.Input;

            GT_VIDSOTOK_GOL_UCH.Direction                           = ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOSI_GOL_UCH"].Direction      = System.Data.ParameterDirection.Input;

            UT_VIDSOTOK_OPR_UCH.Direction                           = ParameterDirection.Input;
            SQLProc.Parameters["@UT_NOMINAL_OPR_UCH"].Direction     = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@UT_GOLOSI_OPR_UCH"].Direction      = System.Data.ParameterDirection.Input;
                             
            SQLProc.Parameters["@ADR_COD_KR"].Direction             = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_INDEX"].Direction              = System.Data.ParameterDirection.Input;       
            SQLProc.Parameters["@ADR_PUNKT"].Direction              = System.Data.ParameterDirection.Input;        
            SQLProc.Parameters["@ADR_UL"].Direction                 = System.Data.ParameterDirection.Input;        
            SQLProc.Parameters["@ADR_BUD"].Direction                = System.Data.ParameterDirection.Input;           
            SQLProc.Parameters["@ADR_KORP"].Direction               = System.Data.ParameterDirection.Input;          
            SQLProc.Parameters["@ADR_OFF"].Direction                = System.Data.ParameterDirection.Input;         
                              
            SQLProc.Parameters["@NT_COD"].Direction                 = System.Data.ParameterDirection.Input;            
            SQLProc.Parameters["@NT_NM1"].Direction                 = System.Data.ParameterDirection.Input;            
            SQLProc.Parameters["@NT_NM2"].Direction                 = System.Data.ParameterDirection.Input;            
            SQLProc.Parameters["@NT_NM3"].Direction                 = System.Data.ParameterDirection.Input;            
                              
            SQLProc.Parameters["@PS_SR"].Direction                  = System.Data.ParameterDirection.Input;             
            SQLProc.Parameters["@PS_NM"].Direction                  = System.Data.ParameterDirection.Input;             
            SQLProc.Parameters["@PS_DT"].Direction                  = System.Data.ParameterDirection.Input;             
            SQLProc.Parameters["@PS_ORG"].Direction                 = System.Data.ParameterDirection.Input;            

            UT_VIDSOTOK_PR_UCH.Direction                            = ParameterDirection.Input;
            SQLProc.Parameters["@UT_NOMINAL_PR_UCH"].Direction      = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@UT_GOLOSI_PR_UCH"].Direction       = System.Data.ParameterDirection.Input;

            GT_VIDSOTOK_ZAG_UCH.Direction                           = ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOSI_ZAG_UCH"].Direction      = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            //            MessageBox.Show("Ins Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"];
            
            SQLProc.Parameters["@OWNER_TYPE"].Value             = frmV_Struct_Owner.OWNER_TYPE;
            SQLProc.Parameters["@OWNER_OZN"].Value              = frmV_Struct_Owner.OWNER_OZN;


            if (frmV_Struct_Owner.OWNER_POS.Length == 0)
            {
                SQLProc.Parameters["@OWNER_POS"].Value = DBNull.Value;
            }
            else
            {
                SQLProc.Parameters["@OWNER_POS"].Value = frmV_Struct_Owner.OWNER_POS;
            }            

            if (frmV_Struct_Owner.OWNER_TYPE.Trim() == "2")
            {
                SQLProc.Parameters["@OWNER_DATE"].Value = frmV_Struct_Owner.OWNER_DATE;
                SQLProc.Parameters["@OWNER_DORG"].Value = DBNull.Value;
            }
            else
            {
                SQLProc.Parameters["@OWNER_DATE"].Value = DBNull.Value;
                if (frmV_Struct_Owner.OWNER_DORG.Length == 0)
                {
                    SQLProc.Parameters["@OWNER_DORG"].Value = DBNull.Value;
                }
                else
                {
                    SQLProc.Parameters["@OWNER_DORG"].Value = frmV_Struct_Owner.OWNER_DORG;
                }
            }

            GT_VIDSOTOK_GOL_UCH.Value                           = frmV_Struct_Owner.GT_VIDSOTOK_GOL_UCH;
            SQLProc.Parameters["@GT_GOLOSI_GOL_UCH"].Value      = frmV_Struct_Owner.GT_GOLOSI_GOL_UCH;
            
            UT_VIDSOTOK_OPR_UCH.Value                           = frmV_Struct_Owner.UT_VIDSOTOK_OPR_UCH;
            SQLProc.Parameters["@UT_NOMINAL_OPR_UCH"].Value     = frmV_Struct_Owner.UT_NOMINAL_OPR_UCH;
            SQLProc.Parameters["@UT_GOLOSI_OPR_UCH"].Value      = frmV_Struct_Owner.UT_GOLOSI_OPR_UCH;
                             
            SQLProc.Parameters["@ADR_COD_KR"].Value             = frmV_Struct_Owner.ADR_COD_KR;
            SQLProc.Parameters["@ADR_INDEX"].Value              = frmV_Struct_Owner.ADR_INDEX;
            SQLProc.Parameters["@ADR_PUNKT"].Value              = frmV_Struct_Owner.ADR_PUNKT;
            SQLProc.Parameters["@ADR_UL"].Value                 = frmV_Struct_Owner.ADR_UL;
            SQLProc.Parameters["@ADR_BUD"].Value                = frmV_Struct_Owner.ADR_BUD;

            if (frmV_Struct_Owner.ADR_KORP.Length == 0)
            {
                SQLProc.Parameters["@ADR_KORP"].Value = DBNull.Value;
            }
            else
            {
                SQLProc.Parameters["@ADR_KORP"].Value = frmV_Struct_Owner.ADR_KORP;
            }

            if (frmV_Struct_Owner.ADR_OFF.Length == 0)
            {
                SQLProc.Parameters["@ADR_OFF"].Value = DBNull.Value;
            }
            else
            {
                SQLProc.Parameters["@ADR_OFF"].Value = frmV_Struct_Owner.ADR_OFF;
            }
                              
            SQLProc.Parameters["@NT_COD"].Value                 = frmV_Struct_Owner.NT_COD;
            SQLProc.Parameters["@NT_NM1"].Value                 = frmV_Struct_Owner.NT_NM1;
            SQLProc.Parameters["@NT_NM2"].Value                 = frmV_Struct_Owner.NT_NM2;
            
            if (frmV_Struct_Owner.OWNER_TYPE.Trim() == "2")
            {
                SQLProc.Parameters["@NT_NM3"].Value = frmV_Struct_Owner.NT_NM3;
            }
            else
            {
                SQLProc.Parameters["@NT_NM3"].Value = DBNull.Value;
            }

            if (frmV_Struct_Owner.OWNER_TYPE.Trim() == "2")
            {
                SQLProc.Parameters["@PS_DT"].Value = frmV_Struct_Owner.PS_DT;
                SQLProc.Parameters["@PS_SR"].Value = frmV_Struct_Owner.PS_SR;
                SQLProc.Parameters["@PS_NM"].Value = frmV_Struct_Owner.PS_NM;
                SQLProc.Parameters["@PS_ORG"].Value = frmV_Struct_Owner.PS_ORG;
            }
            else
            {
                SQLProc.Parameters["@PS_SR"].Value = DBNull.Value;
                SQLProc.Parameters["@PS_NM"].Value = DBNull.Value;
                SQLProc.Parameters["@PS_DT"].Value = DBNull.Value;
                SQLProc.Parameters["@PS_ORG"].Value = DBNull.Value;
            }

            
            
            UT_VIDSOTOK_PR_UCH.Value                            = frmV_Struct_Owner.UT_VIDSOTOK_PR_UCH;
            SQLProc.Parameters["@UT_NOMINAL_PR_UCH"].Value      = frmV_Struct_Owner.UT_NOMINAL_PR_UCH;
            SQLProc.Parameters["@UT_GOLOSI_PR_UCH"].Value       = frmV_Struct_Owner.UT_GOLOSI_PR_UCH;
            
            GT_VIDSOTOK_ZAG_UCH.Value                           = frmV_Struct_Owner.GT_VIDSOTOK_ZAG_UCH;
            SQLProc.Parameters["@GT_GOLOSI_ZAG_UCH"].Value = frmV_Struct_Owner.GT_GOLOSI_ZAG_UCH;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_OWNER_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_OWNER_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fUpdateOwner(Int32 idOwner)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_OWNER_U";
            SQLProc.Parameters.Add("@idOWNER", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@idDeclaration_OWNER", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@OWNER_TYPE", System.Data.SqlDbType.VarChar, 1);
            SQLProc.Parameters.Add("@OWNER_OZN", System.Data.SqlDbType.VarChar, 2);
            SQLProc.Parameters.Add("@OWNER_POS", System.Data.SqlDbType.VarChar, 254);
            SQLProc.Parameters.Add("@OWNER_DATE", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@OWNER_DORG", System.Data.SqlDbType.VarChar, 254);

            SqlParameter GT_VIDSOTOK_GOL_UCH = new SqlParameter("@GT_VIDSOTOK_GOL_UCH", System.Data.SqlDbType.Decimal); GT_VIDSOTOK_GOL_UCH.Precision = 9; GT_VIDSOTOK_GOL_UCH.Scale = 5; SQLProc.Parameters.Add(GT_VIDSOTOK_GOL_UCH);
            SQLProc.Parameters.Add("@GT_GOLOSI_GOL_UCH", System.Data.SqlDbType.Int);

            SqlParameter UT_VIDSOTOK_OPR_UCH = new SqlParameter("@UT_VIDSOTOK_OPR_UCH", System.Data.SqlDbType.Decimal); UT_VIDSOTOK_OPR_UCH.Precision = 9; UT_VIDSOTOK_OPR_UCH.Scale = 5; SQLProc.Parameters.Add(UT_VIDSOTOK_OPR_UCH);
            SQLProc.Parameters.Add("@UT_NOMINAL_OPR_UCH", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@UT_GOLOSI_OPR_UCH", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@ADR_COD_KR", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@ADR_INDEX", System.Data.SqlDbType.VarChar, 6);
            SQLProc.Parameters.Add("@ADR_PUNKT", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@ADR_UL", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@ADR_BUD", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@ADR_KORP", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@ADR_OFF", System.Data.SqlDbType.VarChar, 10);

            SQLProc.Parameters.Add("@NT_COD", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@NT_NM1", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@NT_NM2", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_NM3", System.Data.SqlDbType.VarChar, 50);

            SQLProc.Parameters.Add("@PS_SR", System.Data.SqlDbType.VarChar, 2);
            SQLProc.Parameters.Add("@PS_NM", System.Data.SqlDbType.VarChar, 6);
            SQLProc.Parameters.Add("@PS_DT", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@PS_ORG", System.Data.SqlDbType.VarChar, 254);

            SqlParameter UT_VIDSOTOK_PR_UCH = new SqlParameter("@UT_VIDSOTOK_PR_UCH", System.Data.SqlDbType.Decimal); UT_VIDSOTOK_PR_UCH.Precision = 9; UT_VIDSOTOK_PR_UCH.Scale = 5; SQLProc.Parameters.Add(UT_VIDSOTOK_PR_UCH);
            SQLProc.Parameters.Add("@UT_NOMINAL_PR_UCH", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@UT_GOLOSI_PR_UCH", System.Data.SqlDbType.Int);

            SqlParameter GT_VIDSOTOK_ZAG_UCH = new SqlParameter("@GT_VIDSOTOK_ZAG_UCH", System.Data.SqlDbType.Decimal); GT_VIDSOTOK_ZAG_UCH.Precision = 9; GT_VIDSOTOK_ZAG_UCH.Scale = 5; SQLProc.Parameters.Add(GT_VIDSOTOK_ZAG_UCH);
            SQLProc.Parameters.Add("@GT_GOLOSI_ZAG_UCH", System.Data.SqlDbType.Int);


            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idOWNER"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@idDeclaration_OWNER"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@OWNER_TYPE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@OWNER_OZN"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@OWNER_POS"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@OWNER_DATE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@OWNER_DORG"].Direction = System.Data.ParameterDirection.Input;

            GT_VIDSOTOK_GOL_UCH.Direction = ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOSI_GOL_UCH"].Direction = System.Data.ParameterDirection.Input;

            UT_VIDSOTOK_OPR_UCH.Direction = ParameterDirection.Input;
            SQLProc.Parameters["@UT_NOMINAL_OPR_UCH"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@UT_GOLOSI_OPR_UCH"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@ADR_COD_KR"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_INDEX"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_PUNKT"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_UL"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_BUD"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_KORP"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@ADR_OFF"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@NT_COD"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM1"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM2"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM3"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@PS_SR"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PS_NM"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PS_DT"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@PS_ORG"].Direction = System.Data.ParameterDirection.Input;

            UT_VIDSOTOK_PR_UCH.Direction = ParameterDirection.Input;
            SQLProc.Parameters["@UT_NOMINAL_PR_UCH"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@UT_GOLOSI_PR_UCH"].Direction = System.Data.ParameterDirection.Input;

            GT_VIDSOTOK_ZAG_UCH.Direction = ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOSI_ZAG_UCH"].Direction = System.Data.ParameterDirection.Input;


            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            SQLProc.Parameters["@idOWNER"].Value = idOwner;
            SQLProc.Parameters["@idDeclaration_OWNER"].Value = (Int32)dgvV_Struct_OWNER.Rows[dgvV_Struct_OWNER.CurrentRow.Index].Cells[0].Value;

            SQLProc.Parameters["@OWNER_TYPE"].Value = frmV_Struct_Owner.OWNER_TYPE;
            SQLProc.Parameters["@OWNER_OZN"].Value = frmV_Struct_Owner.OWNER_OZN;
            SQLProc.Parameters["@OWNER_POS"].Value = frmV_Struct_Owner.OWNER_POS;
            SQLProc.Parameters["@OWNER_DATE"].Value = frmV_Struct_Owner.OWNER_DATE;
            SQLProc.Parameters["@OWNER_DORG"].Value = frmV_Struct_Owner.OWNER_DORG;

            GT_VIDSOTOK_GOL_UCH.Value = frmV_Struct_Owner.GT_VIDSOTOK_GOL_UCH;
            SQLProc.Parameters["@GT_GOLOSI_GOL_UCH"].Value = frmV_Struct_Owner.GT_GOLOSI_GOL_UCH;

            UT_VIDSOTOK_OPR_UCH.Value = frmV_Struct_Owner.UT_VIDSOTOK_OPR_UCH;
            SQLProc.Parameters["@UT_NOMINAL_OPR_UCH"].Value = frmV_Struct_Owner.UT_NOMINAL_OPR_UCH;
            SQLProc.Parameters["@UT_GOLOSI_OPR_UCH"].Value = frmV_Struct_Owner.UT_GOLOSI_OPR_UCH;

            SQLProc.Parameters["@ADR_COD_KR"].Value = frmV_Struct_Owner.ADR_COD_KR;
            SQLProc.Parameters["@ADR_INDEX"].Value = frmV_Struct_Owner.ADR_INDEX;
            SQLProc.Parameters["@ADR_PUNKT"].Value = frmV_Struct_Owner.ADR_PUNKT;
            SQLProc.Parameters["@ADR_UL"].Value = frmV_Struct_Owner.ADR_UL;
            SQLProc.Parameters["@ADR_BUD"].Value = frmV_Struct_Owner.ADR_BUD;
            SQLProc.Parameters["@ADR_KORP"].Value = frmV_Struct_Owner.ADR_KORP;
            SQLProc.Parameters["@ADR_OFF"].Value = frmV_Struct_Owner.ADR_OFF;

            SQLProc.Parameters["@NT_COD"].Value = frmV_Struct_Owner.NT_COD;
            SQLProc.Parameters["@NT_NM1"].Value = frmV_Struct_Owner.NT_NM1;
            SQLProc.Parameters["@NT_NM2"].Value = frmV_Struct_Owner.NT_NM2;
            SQLProc.Parameters["@NT_NM3"].Value = frmV_Struct_Owner.NT_NM3;

            SQLProc.Parameters["@PS_SR"].Value = frmV_Struct_Owner.PS_SR;
            SQLProc.Parameters["@PS_NM"].Value = frmV_Struct_Owner.PS_NM;
            SQLProc.Parameters["@PS_DT"].Value = frmV_Struct_Owner.PS_DT;
            SQLProc.Parameters["@PS_ORG"].Value = frmV_Struct_Owner.PS_ORG;

            UT_VIDSOTOK_PR_UCH.Value = frmV_Struct_Owner.UT_VIDSOTOK_PR_UCH;
            SQLProc.Parameters["@UT_NOMINAL_PR_UCH"].Value = frmV_Struct_Owner.UT_NOMINAL_PR_UCH;
            SQLProc.Parameters["@UT_GOLOSI_PR_UCH"].Value = frmV_Struct_Owner.UT_GOLOSI_PR_UCH;

            GT_VIDSOTOK_ZAG_UCH.Value = frmV_Struct_Owner.GT_VIDSOTOK_ZAG_UCH;
            SQLProc.Parameters["@GT_GOLOSI_ZAG_UCH"].Value = frmV_Struct_Owner.GT_GOLOSI_ZAG_UCH;


            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in UPDATE data in P_V_OWNER_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in UPDATE data in P_V_OWNER_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fDeleteOwner(Int32 idOwner)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_OWNER_D";
            SQLProc.Parameters.Add("@idOWNER", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idOWNER"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            SQLProc.Parameters["@idOWNER"].Value = idOwner;
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                //                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                //                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in DELETE data in P_V_OWNER_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in DELETE data in P_V_OWNER_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }


        private void fSaveAddStruct_MAN_BANK()
        {
            fInsertManBank();
            fCreateDataSet();
        }

        private void fSaveUpdStruct_MAN_BANK(Int32 idManBank)
        {
            fUpdateManBank(idManBank);
            fCreateDataSet();
        }

        private void fSaveDelStruct_MAN_BANK(Int32 idManBank)
        {
            fDeleteManBank(idManBank);
            fCreateDataSet();
        }


        private Int32 fInsertManBank()
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_MAN_BANK_I";

            SQLProc.Parameters.Add("@idDeclaration"         , System.Data.SqlDbType.Int);
        	SQLProc.Parameters.Add("@MB_POS"                , System.Data.SqlDbType.VarChar, 254);
	        SQLProc.Parameters.Add("@MB_DT"                 , System.Data.SqlDbType.DateTime);
	        SQLProc.Parameters.Add("@MB_TLF"                , System.Data.SqlDbType.VarChar, 50);
        	SQLProc.Parameters.Add("@FIO_NM1_MB_NAZVA"      , System.Data.SqlDbType.VarChar, 100);
	        SQLProc.Parameters.Add("@FIO_NM2_MB_NAZVA"      , System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FIO_NM3_MB_NAZVA"      , System.Data.SqlDbType.VarChar, 50);
	        SQLProc.Parameters.Add("@FIO_NM1_MB_ISP_NAZVA"  , System.Data.SqlDbType.VarChar, 50);
	        SQLProc.Parameters.Add("@FIO_NM2_MB_ISP_NAZVA"  , System.Data.SqlDbType.VarChar, 50);
	        SQLProc.Parameters.Add("@FIO_NM3_MB_ISP_NAZVA"  , System.Data.SqlDbType.VarChar, 50);

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idDeclaration"].Direction          = System.Data.ParameterDirection.Input;       
            SQLProc.Parameters["@MB_POS"].Direction                 = System.Data.ParameterDirection.Input;              
            SQLProc.Parameters["@MB_DT"].Direction                  = System.Data.ParameterDirection.Input;               
            SQLProc.Parameters["@MB_TLF"].Direction                 = System.Data.ParameterDirection.Input;              
            SQLProc.Parameters["@FIO_NM1_MB_NAZVA"].Direction       = System.Data.ParameterDirection.Input;    
            SQLProc.Parameters["@FIO_NM2_MB_NAZVA"].Direction       = System.Data.ParameterDirection.Input;    
            SQLProc.Parameters["@FIO_NM3_MB_NAZVA"].Direction       = System.Data.ParameterDirection.Input;   
            SQLProc.Parameters["@FIO_NM1_MB_ISP_NAZVA"].Direction   = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM2_MB_ISP_NAZVA"].Direction   = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM3_MB_ISP_NAZVA"].Direction   = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;
            //            MessageBox.Show("Ins Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"];
            SQLProc.Parameters["@MB_POS"].Value                 = frmV_Struct_Man_Bank.MB_POS;
            SQLProc.Parameters["@MB_DT"].Value                  = frmV_Struct_Man_Bank.MB_DT;
            SQLProc.Parameters["@MB_TLF"].Value                 = frmV_Struct_Man_Bank.MB_TLF;
            SQLProc.Parameters["@FIO_NM1_MB_NAZVA"].Value       = frmV_Struct_Man_Bank.FIO_NM1_MB_NAZVA;
            SQLProc.Parameters["@FIO_NM2_MB_NAZVA"].Value       = frmV_Struct_Man_Bank.FIO_NM2_MB_NAZVA;
            SQLProc.Parameters["@FIO_NM3_MB_NAZVA"].Value       = frmV_Struct_Man_Bank.FIO_NM3_MB_NAZVA;
            SQLProc.Parameters["@FIO_NM1_MB_ISP_NAZVA"].Value   = frmV_Struct_Man_Bank.FIO_NM1_MB_ISP_NAZVA;
            SQLProc.Parameters["@FIO_NM2_MB_ISP_NAZVA"].Value   = frmV_Struct_Man_Bank.FIO_NM2_MB_ISP_NAZVA;
            SQLProc.Parameters["@FIO_NM3_MB_ISP_NAZVA"].Value   = frmV_Struct_Man_Bank.FIO_NM3_MB_ISP_NAZVA;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_MAN_BANK_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_MAN_BANK_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fUpdateManBank(Int32 idManBank)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_MAN_BANK_U";
            SQLProc.Parameters.Add("@idMAN_BANK", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);

            SQLProc.Parameters.Add("@MB_POS", System.Data.SqlDbType.VarChar, 254);
            SQLProc.Parameters.Add("@MB_DT", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@MB_TLF", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FIO_NM1_MB_NAZVA", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@FIO_NM2_MB_NAZVA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FIO_NM3_MB_NAZVA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FIO_NM1_MB_ISP_NAZVA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FIO_NM2_MB_ISP_NAZVA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FIO_NM3_MB_ISP_NAZVA", System.Data.SqlDbType.VarChar, 50);            
            
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idMAN_BANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@MB_POS"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@MB_DT"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@MB_TLF"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM1_MB_NAZVA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM2_MB_NAZVA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM3_MB_NAZVA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM1_MB_ISP_NAZVA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM2_MB_ISP_NAZVA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FIO_NM3_MB_ISP_NAZVA"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            SQLProc.Parameters["@idMAN_BANK"].Value = idManBank;

            //            MessageBox.Show("Upd Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);

            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dgvV_Struct_MAN_BANK.Rows[dgvV_Struct_MAN_BANK.CurrentRow.Index].Cells[0].Value;
            SQLProc.Parameters["@MB_POS"].Value = frmV_Struct_Man_Bank.MB_POS;
            SQLProc.Parameters["@MB_DT"].Value = frmV_Struct_Man_Bank.MB_DT;
            SQLProc.Parameters["@MB_TLF"].Value = frmV_Struct_Man_Bank.MB_TLF;
            SQLProc.Parameters["@FIO_NM1_MB_NAZVA"].Value = frmV_Struct_Man_Bank.FIO_NM1_MB_NAZVA;
            SQLProc.Parameters["@FIO_NM2_MB_NAZVA"].Value = frmV_Struct_Man_Bank.FIO_NM2_MB_NAZVA;
            SQLProc.Parameters["@FIO_NM3_MB_NAZVA"].Value = frmV_Struct_Man_Bank.FIO_NM3_MB_NAZVA;
            SQLProc.Parameters["@FIO_NM1_MB_ISP_NAZVA"].Value = frmV_Struct_Man_Bank.FIO_NM1_MB_ISP_NAZVA;
            SQLProc.Parameters["@FIO_NM2_MB_ISP_NAZVA"].Value = frmV_Struct_Man_Bank.FIO_NM2_MB_ISP_NAZVA;
            SQLProc.Parameters["@FIO_NM3_MB_ISP_NAZVA"].Value = frmV_Struct_Man_Bank.FIO_NM3_MB_ISP_NAZVA;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in UPDATE data in P_V_MAN_BANK_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in UPDATE data in P_V_MAN_BANK_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fDeleteManBank(Int32 idManBank)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_MAN_BANK_D";
            SQLProc.Parameters.Add("@idMAN_BANK", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idMAN_BANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            SQLProc.Parameters["@idMAN_BANK"].Value = idManBank;
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                //                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                //                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in DELETE data in P_V_MAN_BANK_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in DELETE data in P_V_MAN_BANK_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private void fSaveAddStruct_PERE_GOLOS()
        {
            fInsertPereGolos();
            fCreateDataSet();
        }

        private void fSaveUpdStruct_PERE_GOLOS(Int32 idPereGolos)
        {
            fUpdatePereGolos(idPereGolos);
            fCreateDataSet();
        }

        private void fSaveDelStruct_PERE_GOLOS(Int32 idPereGolos)
        {
            fDeletePereGolos(idPereGolos);
            fCreateDataSet();
        }


        private Int32 fInsertPereGolos()
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_PERE_GOLOS_I";

            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@GL_NOMER"                  , System.Data.SqlDbType.VarChar, 20);
            SQLProc.Parameters.Add("@GL_DT"                     , System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@GL_PRICH", System.Data.SqlDbType.VarChar, 254);
            SQLProc.Parameters.Add("@NT_COD_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@NT_NM1_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@NT_NM2_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_NM3_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_COD_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@NT_NM1_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@NT_NM2_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_NM3_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SqlParameter Gt_Vidsotok = new SqlParameter("@GT_VIDSOTOK", System.Data.SqlDbType.Decimal);
            Gt_Vidsotok.Precision = 9;
            Gt_Vidsotok.Scale = 5;
            SQLProc.Parameters.Add(Gt_Vidsotok);
            SQLProc.Parameters.Add("@GT_GOLOS", System.Data.SqlDbType.Int);				

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idDeclaration"].Direction          = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GL_NOMER"].Direction               = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GL_DT"].Direction                  = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GL_PRICH"].Direction               = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_COD_FROM_GL_OSOBA"].Direction   = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM1_FROM_GL_OSOBA"].Direction   = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM2_FROM_GL_OSOBA"].Direction   = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM3_FROM_GL_OSOBA"].Direction   = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_COD_TO_GL_OSOBA"].Direction     = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM1_TO_GL_OSOBA"].Direction     = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM2_TO_GL_OSOBA"].Direction     = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM3_TO_GL_OSOBA"].Direction     = System.Data.ParameterDirection.Input;
            Gt_Vidsotok.Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOS"].Direction               = System.Data.ParameterDirection.Input;             

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;
            //            MessageBox.Show("Ins Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"];

            SQLProc.Parameters["@GL_NOMER"].Value = frmV_Struct_Pere_Golos.GL_NOMER;
            SQLProc.Parameters["@GL_DT"].Value                  = frmV_Struct_Pere_Golos.GL_DT;
            SQLProc.Parameters["@GL_PRICH"].Value               = frmV_Struct_Pere_Golos.GL_PRICH;
            SQLProc.Parameters["@NT_COD_FROM_GL_OSOBA"].Value   = frmV_Struct_Pere_Golos.NT_COD_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_NM1_FROM_GL_OSOBA"].Value   = frmV_Struct_Pere_Golos.NT_NM1_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_NM2_FROM_GL_OSOBA"].Value   = frmV_Struct_Pere_Golos.NT_NM2_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_NM3_FROM_GL_OSOBA"].Value   = frmV_Struct_Pere_Golos.NT_NM3_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_COD_TO_GL_OSOBA"].Value     = frmV_Struct_Pere_Golos.NT_COD_TO_GL_OSOBA;
            SQLProc.Parameters["@NT_NM1_TO_GL_OSOBA"].Value     = frmV_Struct_Pere_Golos.NT_NM1_TO_GL_OSOBA;
            SQLProc.Parameters["@NT_NM2_TO_GL_OSOBA"].Value     = frmV_Struct_Pere_Golos.NT_NM2_TO_GL_OSOBA;
            SQLProc.Parameters["@NT_NM3_TO_GL_OSOBA"].Value     = frmV_Struct_Pere_Golos.NT_NM3_TO_GL_OSOBA;
            Gt_Vidsotok.Value                                   = frmV_Struct_Pere_Golos.GT_VIDSOTOK;
            SQLProc.Parameters["@GT_GOLOS"].Value = frmV_Struct_Pere_Golos.GT_GOLOS;
            
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_PERE_GOLOS_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_V_Struct_PERE_GOLOS_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fUpdatePereGolos(Int32 idPereGolos)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_PERE_GOLOS_U";
            SQLProc.Parameters.Add("@idPERE_GOLOS", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
            
            SQLProc.Parameters.Add("@GL_NOMER", System.Data.SqlDbType.VarChar, 20);
            SQLProc.Parameters.Add("@GL_DT", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@GL_PRICH", System.Data.SqlDbType.VarChar, 254);
            SQLProc.Parameters.Add("@NT_COD_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@NT_NM1_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@NT_NM2_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_NM3_FROM_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_COD_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@NT_NM1_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@NT_NM2_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@NT_NM3_TO_GL_OSOBA", System.Data.SqlDbType.VarChar, 50);
            SqlParameter Gt_Vidsotok = new SqlParameter("@GT_VIDSOTOK", System.Data.SqlDbType.Decimal);
            Gt_Vidsotok.Precision = 9;
            Gt_Vidsotok.Scale = 5;
            SQLProc.Parameters.Add(Gt_Vidsotok);
            SQLProc.Parameters.Add("@GT_GOLOS", System.Data.SqlDbType.Int);				

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idPERE_GOLOS"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@idDeclaration"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GL_NOMER"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GL_DT"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GL_PRICH"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_COD_FROM_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM1_FROM_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM2_FROM_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM3_FROM_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_COD_TO_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM1_TO_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM2_TO_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@NT_NM3_TO_GL_OSOBA"].Direction = System.Data.ParameterDirection.Input;
            Gt_Vidsotok.Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@GT_GOLOS"].Direction = System.Data.ParameterDirection.Input;             

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            SQLProc.Parameters["@idPERE_GOLOS"].Value = idPereGolos;

            //            MessageBox.Show("Upd Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);
            SQLProc.Parameters["@idDeclaration"].Value = (Int32)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"];
            SQLProc.Parameters["@GL_NOMER"].Value = frmV_Struct_Pere_Golos.GL_NOMER;
            SQLProc.Parameters["@GL_DT"].Value = frmV_Struct_Pere_Golos.GL_DT;
            SQLProc.Parameters["@GL_PRICH"].Value = frmV_Struct_Pere_Golos.GL_PRICH;
            SQLProc.Parameters["@NT_COD_FROM_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_COD_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_NM1_FROM_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_NM1_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_NM2_FROM_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_NM2_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_NM3_FROM_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_NM3_FROM_GL_OSOBA;
            SQLProc.Parameters["@NT_COD_TO_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_COD_TO_GL_OSOBA;
            SQLProc.Parameters["@NT_NM1_TO_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_NM1_TO_GL_OSOBA;
            SQLProc.Parameters["@NT_NM2_TO_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_NM2_TO_GL_OSOBA;
            SQLProc.Parameters["@NT_NM3_TO_GL_OSOBA"].Value = frmV_Struct_Pere_Golos.NT_NM3_TO_GL_OSOBA;
            Gt_Vidsotok.Value = frmV_Struct_Pere_Golos.GT_VIDSOTOK;
            SQLProc.Parameters["@GT_GOLOS"].Value = frmV_Struct_Pere_Golos.GT_GOLOS;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in UPDATE data in P_V_PERE_GOLOS_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in UPDATE data in P_V_PERE_GOLOS_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }

        private Int32 fDeletePereGolos(Int32 idPereGolos)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_V_Struct_PERE_GOLOS_D";
            SQLProc.Parameters.Add("@idPERE_GOLOS", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@idPERE_GOLOS"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;


            SQLProc.Parameters["@idPERE_GOLOS"].Value = idPereGolos;
            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                //                retId = (Int32)SQLProc.Parameters["@id"].Value;
                //                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                //                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                //                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in DELETE data in P_V_PERE_GOLOS_U.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in DELETE data in P_V_PERE_GOLOS_U\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;
        }



        private void tsm_Data_Click(object sender, EventArgs e)
        {
            fWorkWithData(1);
        }

        private void tsmCreateFileToSenf_Click(object sender, EventArgs e)
        {
            fWorkWithData(2);
//            clCreateFileToSend CreateFileToSend = new clCreateFileToSend();
        }

        private void fCreatePfile(Int32 idDeclaration)
        {
//            clCreateFileToSend CreateFileToSend = new clCreateFileToSend(idDeclaration);
            String FileName = fCreateFileNameReport();
            String DirName = fDefDirName();

            fCreateDataSetXML(idDeclaration, FileName);
            try
            {
                dsPfileXML.WriteXml("d:\\dsOut.xml", XmlWriteMode.IgnoreSchema);
                fInsertPFile(idDeclaration, Convert.ToInt32(FileName.Substring(9, 2)), FileName, DirName);
            }
            catch (Exception e) { MessageBox.Show("Exception for create XML.\n\r" + e.Message); }
        }

        private String fDefDirName()
        {
            return fDefReferData("File", "PathToSave");
        }

        private String fDefNumberFile(String mask)
        {
            String RetVal = "";
            Int32 retSQL;
            String SelectText = "SELECT ISNULL(MAX(Number) + 1, 1) FROM PFiles where FileName LIKE '" + mask + "%'";
            MessageBox.Show("SELECT ISNULL(MAX(Number) + 1, 1) FROM PFiles where FileName LIKE '" + mask + "%'");
            //            MessageBox.Show(SelectText);
            SqlCommand sqlCmd = new SqlCommand(SelectText, sqlConn);
            try
            {
                sqlConn.Open();
                 
                retSQL = (Int32)sqlCmd.ExecuteScalar();
                RetVal = retSQL.ToString();
                if (RetVal.Length < 2) { RetVal = "0" + RetVal; }
            }
            catch (SqlException e) { MessageBox.Show("SqlException.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("Exception.\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }

            return RetVal;
        
        }


        private String fCreateFileNameReport()
        {
            String RetVal = "";
            String tmpFileName = fDefReferData("FileName", "TypeFile") + fDefReferData("FileName", "Report") + fDefReferData("FileName", "IdBank") + base36[DateTime.Now.Day] + base36[DateTime.Now.Month] + "." + DateTime.Now.Year.ToString().Substring(3, 1);
            RetVal = tmpFileName + fDefNumberFile(tmpFileName);
            return RetVal;
        }

        private void fCreateDataSetXML(Int32 id , String fileName)
        {
            SqlDataAdapter daDataXML    = new SqlDataAdapter("SELECT INF_DT, TABLE_PIC, PICTURE, id as DECLARATION_Id from StructDECLARATION where id = " + id.ToString(), sqlConn);
            daDataXML.Fill(dsPfileXML.DECLARATION);
        
//            daDataXML = new SqlDataAdapter("SELECT FNAME, EDRPOU, IDBANK, MFO, CDTASK, CDSUB, CDFORM, FILL_DATE, FILL_TIME, EI, KU, IdDeclaration as DECLARATION_Id from StructDeclarationHEAD where idDECLARATION = " + id.ToString(), sqlConn);
            daDataXML = new SqlDataAdapter("SELECT '" + fileName + "'" + " FNAME, EDRPOU, IDBANK, MFO, CDTASK, CDSUB, CDFORM, FILL_DATE, FILL_TIME, EI, KU, IdDeclaration as DECLARATION_Id from StructDeclarationHEAD where idDECLARATION = " + id.ToString(), sqlConn);
            daDataXML.Fill(dsPfileXML.HEAD);

            daDataXML = new SqlDataAdapter("SELECT RowNum as ROWNUM, OWNER_TYPE, OWNER_OZN, OWNER_POS, OWNER_DATE, OWNER_DORG, id as OWNER_Id, idDECLARATION as DECLARATION_Id FROM StructDeclarationOWNER where idDeclaration = " + id.ToString(), sqlConn);
            daDataXML.Fill(dsPfileXML.OWNER);

            daDataXML = new SqlDataAdapter("SELECT NT_COD, NT_NM1, NT_NM2, ISNULL(NT_NM3, '') as NT_NM3, idDeclaration_OWNER as OWNER_Id FROM StructOwnerOWNER_NAZVA where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.OWNER_NAZVA);

            daDataXML = new SqlDataAdapter("SELECT ADR_COD_KR, ADR_INDEX, ADR_PUNKT, ADR_UL, ADR_BUD, ADR_KORP, ADR_OFF, idDeclaration_OWNER as OWNER_Id FROM StructOwnerOWNER_ADR where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.OWNER_ADR);

            daDataXML = new SqlDataAdapter("SELECT PS_SR, PS_NM, PS_DT, PS_ORG, idDeclaration_OWNER as OWNER_Id FROM StructOwnerOWNER_PASS where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where OWNER_TYPE = '2' and idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.OWNER_PASS);

            daDataXML = new SqlDataAdapter("SELECT UT_VIDSOTOK, UT_NOMINAL, UT_GOLOSI, idDeclaration_OWNER as OWNER_Id FROM StructOwnerPR_UCH where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.PR_UCH);

            daDataXML = new SqlDataAdapter("SELECT UT_VIDSOTOK, UT_NOMINAL, UT_GOLOSI, idDeclaration_OWNER as OWNER_Id FROM StructOwnerOPR_UCH where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.OPR_UCH);

            daDataXML = new SqlDataAdapter("SELECT GT_VIDSOTOK, GT_GOLOSI as GT_GOLOS, idDeclaration_OWNER as OWNER_Id FROM StructOwnerGOL_UCH where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.GOL_UCH);

            daDataXML = new SqlDataAdapter("SELECT GT_VIDSOTOK, GT_GOLOSI as GT_GOLOS, idDeclaration_OWNER as OWNER_Id FROM StructOwnerZAG_UCH where idDeclaration_OWNER in (SELECT id from StructDeclarationOWNER where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.ZAG_UCH);

            daDataXML = new SqlDataAdapter("SELECT RowNum as ROWNUM, GL_NOMER, GL_DT, GL_PRICH, id as PERE_GOLOS_Id, idDeclaration as DECLARATION_Id FROM StructDeclarationPERE_GOLOS where idDeclaration = " + id.ToString(), sqlConn);
            daDataXML.Fill(dsPfileXML.PERE_GOLOS);

            daDataXML = new SqlDataAdapter("SELECT NT_COD, NT_NM1, NT_NM2, NT_NM3, idDeclarationPERE_GOLOS as PERE_GOLOS_Id FROM StructPere_golosTO_GL_OSOBA where idDeclarationPERE_GOLOS in (SELECT id from StructDeclarationPERE_GOLOS where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.TO_GL_OSOBA);

            daDataXML = new SqlDataAdapter("SELECT NT_COD, NT_NM1, NT_NM2, NT_NM3, idDeclarationPERE_GOLOS as PERE_GOLOS_Id FROM StructPere_golosFROM_GL_OSOBA where idDeclarationPERE_GOLOS in (SELECT id from StructDeclarationPERE_GOLOS where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.FROM_GL_OSOBA);

            daDataXML = new SqlDataAdapter("SELECT GT_VIDSOTOK, GT_GOLOS, idDeclarationPERE_GOLOS as PERE_GOLOS_Id FROM StructPere_golosGL_NABUT where idDeclarationPERE_GOLOS in (SELECT id from StructDeclarationPERE_GOLOS where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.GL_NABUT);

            daDataXML = new SqlDataAdapter("SELECT GT_VIDSOTOK, GT_GOLOS, idDeclaration as DECLARATION_Id FROM StructDeclarationSUM_BANK where idDeclaration = " + id.ToString(), sqlConn);
            daDataXML.Fill(dsPfileXML.SUM_BANK);

            daDataXML = new SqlDataAdapter("SELECT MB_POS, MB_DT, MB_TLF, id as MAN_BANK_Id, idDeclaration as DECLARATION_Id FROM StructDeclarationMAN_BANK where idDeclaration = " + id.ToString(), sqlConn);
            daDataXML.Fill(dsPfileXML.MAN_BANK);

            daDataXML = new SqlDataAdapter("SELECT FIO_NM1, FIO_NM2, FIO_NM3, idDeclarationMAN_BANK as MAN_BANK_Id FROM StructMan_bankMB_NAZVA where idDeclarationMAN_BANK in (SELECT id from StructDeclarationMAN_BANK where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.MB_NAZVA);

            daDataXML = new SqlDataAdapter("SELECT FIO_NM1, FIO_NM2, FIO_NM3, idDeclarationMAN_BANK as MAN_BANK_Id FROM StructMan_bankMB_ISP_NAZVA where idDeclarationMAN_BANK in (SELECT id from StructDeclarationMAN_BANK where idDeclaration = " + id.ToString() + ")", sqlConn);
            daDataXML.Fill(dsPfileXML.MB_ISP_NAZVA);

            fCreateClassXML(id, fileName);

        }

        private void fCreateClassXML(Int32 id, String fileName)
        {
            
            if (dsDataXML.Tables.Contains("V_DECLARATION_HEAD"))   { dsDataXML.Tables["V_DECLARATION_HEAD"].Clear();}
            if (dsDataXML.Tables.Contains("V_Struct_MAN_BANK"))    { dsDataXML.Tables["V_Struct_SUM_BANK"].Clear();}
            if (dsDataXML.Tables.Contains("V_Struct_OWNER"))       { dsDataXML.Tables["V_Struct_OWNER"].Clear();}
            if (dsDataXML.Tables.Contains("V_Struct_PERE_GOLOS"))  { dsDataXML.Tables["V_Struct_MAN_BANK"].Clear();}
            if (dsDataXML.Tables.Contains("V_Struct_SUM_BANK")) { dsDataXML.Tables["V_Struct_PERE_GOLOS"].Clear(); }

            if (!dsDataXML.Tables.Contains("V_DECLARATION_HEAD"))   { dsDataXML.Tables.Add("V_DECLARATION_HEAD"); }
            if (!dsDataXML.Tables.Contains("V_Struct_MAN_BANK"))    { dsDataXML.Tables.Add("V_Struct_MAN_BANK"); }
            if (!dsDataXML.Tables.Contains("V_Struct_OWNER"))       { dsDataXML.Tables.Add("V_Struct_OWNER"); }
            if (!dsDataXML.Tables.Contains("V_Struct_PERE_GOLOS"))  { dsDataXML.Tables.Add("V_Struct_PERE_GOLOS"); }
            if (!dsDataXML.Tables.Contains("V_Struct_SUM_BANK"))    { dsDataXML.Tables.Add("V_Struct_SUM_BANK"); }

            SqlDataAdapter daData = new SqlDataAdapter("SELECT id, INF_DT, TABLE_PIC, PICTURE, EDRPOU, MFO, FNAME, IDBANK, CDTASK, CDSUB, CDFORM, FILL_DATE, FILL_TIME, EI, KU from V_DECLARATION_HEAD where id = " + id.ToString(), sqlConn);
            daData.Fill(dsDataXML.Tables["V_DECLARATION_HEAD"]);

            daData = new SqlDataAdapter("SELECT id, idDECLARATION, GT_VIDSOTOK, GT_GOLOS from V_Struct_SUM_BANK where idDECLARATION = " + id.ToString(), sqlConn);
            daData.Fill(dsDataXML.Tables["V_Struct_SUM_BANK"]);

            daData = new SqlDataAdapter("SELECT idDECLARATION, OWNER_TYPE, OWNER_OZN, OWNER_POS, OWNER_DATE, OWNER_DORG, GT_VIDSOTOK_GOL_UCH, GT_GOLOSI_GOL_UCH, UT_VIDSOTOK_OPR_UCH, UT_NOMINAL_OPR_UCH, UT_GOLOSI_OPR_UCH, ADR_COD_KR, ADR_INDEX, ADR_PUNKT, ADR_UL, ADR_BUD, ADR_KORP, ADR_OFF, NT_COD, NT_NM1, NT_NM2, NT_NM3, PS_SR, PS_NM, PS_DT, PS_ORG, UT_VIDSOTOK_PR_UCH, UT_NOMINAL_PR_UCH, UT_GOLOSI_PR_UCH, GT_VIDSOTOK_ZAG_UCH, GT_GOLOSI_ZAG_UCH, idDeclaration_OWNER_GOL_UCH, ROWNUM FROM V_Struct_OWNER where idDECLARATION = " + id.ToString(), sqlConn);
            daData.Fill(dsDataXML.Tables["V_Struct_OWNER"]);

            daData = new SqlDataAdapter("SELECT idDECLARATION, MB_POS, MB_DT, MB_TLF, FIO_NM1_MB_NAZVA, FIO_NM2_MB_NAZVA, FIO_NM3_MB_NAZVA, FIO_NM1_MB_ISP_NAZVA, FIO_NM2_MB_ISP_NAZVA, FIO_NM3_MB_ISP_NAZVA, idDeclarationMAN_BANK_MB_NAZVA FROM V_Struct_MAN_BANK where idDECLARATION = " + id.ToString(), sqlConn);
            daData.Fill(dsDataXML.Tables["V_Struct_MAN_BANK"]);

            daData = new SqlDataAdapter("SELECT idDECLARATION, GL_NOMER, GL_DT, GL_PRICH, NT_COD_FROM_GL_OSOBA, NT_NM1_FROM_GL_OSOBA, NT_NM2_FROM_GL_OSOBA, NT_NM3_FROM_GL_OSOBA, NT_COD_TO_GL_OSOBA, NT_NM1_TO_GL_OSOBA, NT_NM2_TO_GL_OSOBA, NT_NM3_TO_GL_OSOBA, GT_VIDSOTOK, GT_GOLOS,  idDeclarationPERE_GOLOS_FROM_GL_OSOBA, ROWNUM FROM V_Struct_PERE_GOLOS where idDECLARATION = " + id.ToString(), sqlConn);
            daData.Fill(dsDataXML.Tables["V_Struct_PERE_GOLOS"]);

            DHEAD dHEAD = new DHEAD();
            DECLARATION_TYPE dDeclaration = new DECLARATION_TYPE();

// id, INF_DT, TABLE_PIC, PICTURE, EDRPOU, MFO, FNAME, IDBANK, CDTASK, CDSUB, CDFORM, FILL_DATE, FILL_TIME, EI, KU
//  0   1           2       3       4       5       6     7     8        9      10      11          12      13  14
            dHEAD.FNAME = fileName;
            dHEAD.CDFORM = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][10];
            dHEAD.CDSUB = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][9];
            dHEAD.CDTASK = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][8];
            dHEAD.EDRPOU = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][4];
            if (dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][13] == DBNull.Value) 
            { 
                dHEAD.EI = null; 
            } 
            else 
            {
                dHEAD.EI = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][13];
                if (dHEAD.EI.Trim().Length == 0) { dHEAD.EI = null; }
            }
            dHEAD.FILL_DATE = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][11];
            dHEAD.FILL_TIME = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][12];
            dHEAD.IDBANK = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][7];
//            dHEAD.KU = Convert.ToString((Int32)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][14]);
            dHEAD.KU = (String)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][14];
            dHEAD.MFO = Convert.ToString((Int32)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][5]);

            String dtInf = Convert.ToString((DateTime)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][1]).Substring(0, 10);
//            dDeclaration.INF_DT = Convert.ToString((DateTime)dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][1]);
            dDeclaration.INF_DT = dtInf;
            dDeclaration.TABLE_PIC = (byte[])dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][2];
            dDeclaration.PICTURE = (byte[])dsDataXML.Tables["V_DECLARATION_HEAD"].Rows[0][3];
            dDeclaration.HEAD = dHEAD;

            DECLARBODY_OWNER[] arOwner = new DECLARBODY_OWNER[dsDataXML.Tables["V_Struct_OWNER"].Rows.Count];

            for (Int32 i = 0; i < arOwner.Length; i++)
            {
                

                DECLARBODY_OWNER owner = new DECLARBODY_OWNER();


                if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][4] == DBNull.Value)
                {
                    owner.OWNER_DATE = null;
                }
                else
                {
                    String dtOwner = Convert.ToString((DateTime)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][4]).Substring(0, 10);
                    owner.OWNER_DATE = dtOwner;
                }
                if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][5] == DBNull.Value)
                {
                    owner.OWNER_DORG = null;
                }
                else
                {
                    owner.OWNER_DORG = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][5];
                }

                owner.OWNER_OZN = Convert.ToDecimal((String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][2]);
                if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][3] == DBNull.Value)
                {
                    owner.OWNER_POS = null;
                }
                else
                {
                    owner.OWNER_POS = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][3];
                }
                owner.OWNER_TYPE = Convert.ToDecimal((String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][1]);
                owner.ROWNUM = (Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][32];

                GOLOS_TYPE GOL_UCH = new GOLOS_TYPE();
                GOL_UCH.GT_VIDSOTOK = (Decimal)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][6];
                GOL_UCH.GT_GOLOS = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][7]);

                UCHAST_TYPE OPR_UCH = new UCHAST_TYPE();
                OPR_UCH.UT_VIDSOTOK = (Decimal)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][8];
                OPR_UCH.UT_GOLOSI = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][10]);
                OPR_UCH.UT_NOMINAL = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][9]);

                UCHAST_TYPE PR_UCH = new UCHAST_TYPE();
                PR_UCH.UT_VIDSOTOK = (Decimal)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][26];
                PR_UCH.UT_GOLOSI = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][28]);
                PR_UCH.UT_NOMINAL = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][27]);

                GOLOS_TYPE ZAG_UCH = new GOLOS_TYPE();
                ZAG_UCH.GT_VIDSOTOK = (Decimal)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][29];
                ZAG_UCH.GT_GOLOS = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][30]);

                ADR_TYPE OWNER_ADR = new ADR_TYPE();
                OWNER_ADR.ADR_BUD = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][15];
                OWNER_ADR.ADR_COD_KR = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][11];
                OWNER_ADR.ADR_INDEX = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][12];
                if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][16] == DBNull.Value)
                {
                    OWNER_ADR.ADR_KORP = null;
                }
                else
                {
                    OWNER_ADR.ADR_KORP = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][16];
                }
                if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][17] == DBNull.Value)
                {
                    OWNER_ADR.ADR_OFF = null;
                }
                else
                {
                    OWNER_ADR.ADR_OFF = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][17];
                }
                OWNER_ADR.ADR_PUNKT = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][13];
                OWNER_ADR.ADR_UL = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][14];
                
                NAZVA_TYPE OWNER_NAZVA = new NAZVA_TYPE();
                OWNER_NAZVA.NT_COD = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][18];
                OWNER_NAZVA.NT_NM1 = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][19];
                OWNER_NAZVA.NT_NM2 = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][20];
                if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][21] == DBNull.Value)
                {
                    OWNER_NAZVA.NT_NM3 = null;
                }
                else
                {
                    OWNER_NAZVA.NT_NM3 = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][21];
                }                 

                
                PASS_TYPE OWNER_PASS = new PASS_TYPE();
//                if (dsPfileXML.OWNER_PASS.Rows[i][dsPfileXML.OWNER_PASS.PS_DTColumn] == DBNull.Value)

// idDECLARATION, OWNER_TYPE, OWNER_OZN, OWNER_POS, OWNER_DATE, OWNER_DORG, GT_VIDSOTOK_GOL_UCH, GT_GOLOSI_GOL_UCH
//      0           1           2           3           4           5           6                   7                          
// , UT_VIDSOTOK_OPR_UCH, UT_NOMINAL_OPR_UCH, UT_GOLOSI_OPR_UCH, ADR_COD_KR, ADR_INDEX, ADR_PUNKT, ADR_UL, ADR_BUD, ADR_KORP
//          8                       9               10              11          12          13       14       15        16
// , ADR_OFF, NT_COD, NT_NM1, NT_NM2, NT_NM3, PS_SR, PS_NM, PS_DT, PS_ORG, UT_VIDSOTOK_PR_UCH, UT_NOMINAL_PR_UCH, UT_GOLOSI_PR_UCH
//      17      18      19      20      21      22      23    24    25          26                  27                  28
// , GT_VIDSOTOK_ZAG_UCH, GT_GOLOSI_ZAG_UCH, idDeclaration_OWNER_GOL_UCH, ROWNUM
//      29                  30                          31                  32

                if ((dsDataXML.Tables["V_Struct_OWNER"].Rows[i][22] == DBNull.Value) && (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][23] == DBNull.Value) && (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][24] == DBNull.Value) && (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][25] == DBNull.Value))
                {
                    OWNER_PASS = null;
                }
                else
                {
                    if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][24] == DBNull.Value)
                    {
                        OWNER_PASS.PS_DT = null;
                    }
                    else
                    {
                        String dtPs = Convert.ToString((DateTime)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][24]).Substring(0,10);
                        OWNER_PASS.PS_DT = dtPs;
                    }
                    if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][23] == DBNull.Value)
                    {
//                      OWNER_PASS.PS_NM = null;
                    }
                    else
                    {

                        OWNER_PASS.PS_NM = Convert.ToDecimal((String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][23]);
                    }
                    if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][25] == DBNull.Value)
                    {
                        OWNER_PASS.PS_ORG = null;
                    }
                    else
                    {
                        OWNER_PASS.PS_ORG = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][25];
                    }
                    if (dsDataXML.Tables["V_Struct_OWNER"].Rows[i][22] == DBNull.Value)
                    {
                        OWNER_PASS.PS_SR = null;
                    }
                    else
                    {
                        OWNER_PASS.PS_SR = (String)dsDataXML.Tables["V_Struct_OWNER"].Rows[i][22];
                    }
                }

                owner.GOL_UCH = GOL_UCH;
                owner.OPR_UCH = OPR_UCH;
                owner.OWNER_ADR = OWNER_ADR;
                owner.OWNER_NAZVA = OWNER_NAZVA;
                owner.OWNER_PASS = OWNER_PASS;
                owner.PR_UCH = PR_UCH;
                owner.ZAG_UCH = ZAG_UCH;

                arOwner[i] = owner;               

            }

            dDeclaration.OWNER = arOwner;

// idDECLARATION, MB_POS, MB_DT, MB_TLF, FIO_NM1_MB_NAZVA, FIO_NM2_MB_NAZVA, FIO_NM3_MB_NAZVA, FIO_NM1_MB_ISP_NAZVA, FIO_NM2_MB_ISP_NAZVA
//      0           1       2       3       4                   5               6                   7                       8   
// , FIO_NM3_MB_ISP_NAZVA, idDeclarationMAN_BANK_MB_NAZVA
//      9                           10              
            MAN_BANK_TYPE MAN_BANK = new MAN_BANK_TYPE();
            String dtMb = Convert.ToString((DateTime)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][2]).Substring(0, 10);
            MAN_BANK.MB_DT = dtMb;
            MAN_BANK.MB_POS = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][1];
            MAN_BANK.MB_TLF = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][3];

            FIO_TYPE MB_NAZVA = new FIO_TYPE();
            MB_NAZVA.FIO_NM1 = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][4];
            MB_NAZVA.FIO_NM2 = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][5];
            MB_NAZVA.FIO_NM3 = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][6];
            MAN_BANK.MB_NAZVA = MB_NAZVA;

            FIO_TYPE MB_ISP_NAZVA = new FIO_TYPE();
            MB_ISP_NAZVA.FIO_NM1 = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][7];
            MB_ISP_NAZVA.FIO_NM2 = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][8];
            MB_ISP_NAZVA.FIO_NM3 = (String)dsDataXML.Tables["V_Struct_MAN_BANK"].Rows[0][9];
            MAN_BANK.MB_ISP_NAZVA = MB_ISP_NAZVA;

            dDeclaration.MAN_BANK = MAN_BANK;

// idDECLARATION, GL_NOMER, GL_DT, GL_PRICH, NT_COD_FROM_GL_OSOBA, NT_NM1_FROM_GL_OSOBA, NT_NM2_FROM_GL_OSOBA, NT_NM3_FROM_GL_OSOBA
//       0          1        2          3       4                           5                   6                   7     
// NT_COD_TO_GL_OSOBA, NT_NM1_TO_GL_OSOBA, NT_NM2_TO_GL_OSOBA, NT_NM3_TO_GL_OSOBA, GT_VIDSOTOK, GT_GOLOS,  idDeclarationPERE_GOLOS_FROM_GL_OSOBA
//      8                   9                   10                      11              12          13              14
            PERE_GOLOS_TYPE[] arPERE_GOLOS = new PERE_GOLOS_TYPE[dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows.Count];
            if (dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows.Count > 0)
            {
                for (Int32 i = 0; i < arOwner.Length; i++)
                {
                    PERE_GOLOS_TYPE PERE_GOLOS = new PERE_GOLOS_TYPE();
                    String dtGl = Convert.ToString((DateTime)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][2]).Substring(0, 10);
                    PERE_GOLOS.GL_DT = dtGl;
                    PERE_GOLOS.GL_NOMER = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][1];
                    PERE_GOLOS.ROWNUM = (Int32)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][15];
                    PERE_GOLOS.GL_PRICH = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][3];

                    NAZVA_TYPE FROM_GL_OSOBA = new NAZVA_TYPE();
                    FROM_GL_OSOBA.NT_COD = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][4];
                    FROM_GL_OSOBA.NT_NM1 = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][5];
                    FROM_GL_OSOBA.NT_NM2 = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][6];
                    FROM_GL_OSOBA.NT_NM3 = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][7];
                    PERE_GOLOS.FROM_GL_OSOBA = FROM_GL_OSOBA;

                    NAZVA_TYPE TO_GL_OSOBA = new NAZVA_TYPE();
                    TO_GL_OSOBA.NT_COD = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][8];
                    TO_GL_OSOBA.NT_NM1 = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][9];
                    TO_GL_OSOBA.NT_NM2 = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][10];
                    TO_GL_OSOBA.NT_NM3 = (String)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][11];
                    PERE_GOLOS.TO_GL_OSOBA = TO_GL_OSOBA;

                    GOLOS_TYPE GL_NABUT = new GOLOS_TYPE();
                    GL_NABUT.GT_VIDSOTOK = (Decimal)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][12];
                    GL_NABUT.GT_GOLOS = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_PERE_GOLOS"].Rows[i][13]);
                    PERE_GOLOS.GL_NABUT = GL_NABUT;
                }
            }
            else
            {
                arPERE_GOLOS = null;
            }
            dDeclaration.PERE_GOLOS = arPERE_GOLOS;
// id, idDECLARATION, GT_VIDSOTOK, GT_GOLOS
            GOLOS_TYPE SUM_BANK = new GOLOS_TYPE();
            if (dsDataXML.Tables["V_Struct_SUM_BANK"].Rows.Count > 0)
            {
                SUM_BANK.GT_VIDSOTOK = (Decimal)dsDataXML.Tables["V_Struct_SUM_BANK"].Rows[0][2];
                SUM_BANK.GT_GOLOS = Convert.ToDecimal((Int32)dsDataXML.Tables["V_Struct_SUM_BANK"].Rows[0][3]);
            }
            else
            {
//                SUM_BANK.GT_VIDSOTOK = null;
//                SUM_BANK.GT_GOLOS = null;
            }
            dDeclaration.SUM_BANK = SUM_BANK;

            FileStream saveFile = new FileStream(fDefDirName() + fileName, FileMode.Create, FileAccess.Write);
            XmlSerializer xs = new XmlSerializer(dDeclaration.GetType());
            XmlTextWriter xmlTextWriter = new XmlTextWriter(saveFile, Encoding.GetEncoding("windows-1251"));
            xs.Serialize(xmlTextWriter, dDeclaration);
        }


        private Int32 fInsertPFile(Int32 idDeclaration, Int32 Number, String fileName, String dirName)
        {
            int retId = -1;
            int retErr = -1;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_PFiles_I";
           
            SQLProc.Parameters.Add("@FileTape", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@FileName", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@FullFileName", System.Data.SqlDbType.VarChar, 100);
            SQLProc.Parameters.Add("@idDeclaration", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@DateCreate", System.Data.SqlDbType.DateTime);
            SQLProc.Parameters.Add("@Number", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@Status", System.Data.SqlDbType.TinyInt);

            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters["@FileTape"].Direction           = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FileName"].Direction           = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FullFileName"].Direction       = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@idDeclaration"].Direction      = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@DateCreate"].Direction         = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@Number"].Direction             = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@Status"].Direction             = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;
            //            MessageBox.Show("Ins Sum Bank - " + (String)dsData.Tables["V_DECLARATION_HEAD"].Rows[dgvV_DECLARATION_HEAD.CurrentRow.Index]["id"]);

            SQLProc.Parameters["@FileTape"].Value = "7";
            SQLProc.Parameters["@FileName"].Value = fileName;
            SQLProc.Parameters["@FullFileName"].Value = dirName + fileName;
            SQLProc.Parameters["@idDeclaration"].Value = idDeclaration;
            SQLProc.Parameters["@DateCreate"].Value = DateTime.Now;
            SQLProc.Parameters["@Number"].Value = Number;
            SQLProc.Parameters["@Status"].Value = 1;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_PFiles_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_PFiles_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;


        }



        private void ReciveReceipt_Click(object sender, EventArgs e)
        {
            ofdChoiceReceipt.InitialDirectory = "c:\\";
            ofdChoiceReceipt.Filter = "receipt files (P8*.*)|P80*.*|All files (*.*)|*.*";
            ofdChoiceReceipt.FilterIndex = 1;
            ofdChoiceReceipt.RestoreDirectory = true;

            if (ofdChoiceReceipt.ShowDialog() == DialogResult.OK)
            {
                fDeserializeXML(ofdChoiceReceipt.FileName);
            }
        }

        private void fDeserializeXML(String FileName)
        {
            Int32 Id = 0;
            Int32 IdKVIHEAD = 0;
            Int32 IdDECLARBODY = 0;
            
            DateTime fDate;

            FileInfo fi = new FileInfo(FileName);
            fDate = fi.CreationTime;

            XmlSerializer serializer = new
            XmlSerializer(typeof(DECLARATION));

            FileStream fs = new FileStream(FileName, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            KVIDECLARATION = (DECLARATION)serializer.Deserialize(reader);
            fs.Close();

            Id = fInsertKviDeclaration(FileName, fDate);
            IdKVIHEAD = fInsertKVIDECLARATION_KVIHEAD(Id);
            fInsertKVIHEAD_DOCHEAD(IdKVIHEAD);
            IdDECLARBODY = fInsertKVIDECLARATION_DECLARBODY(Id);
            if (KVIDECLARATION.DECLARBODY.INF_ERR.Length > 0)
            {
                for (Int32 i = 0; i < KVIDECLARATION.DECLARBODY.INF_ERR.Length; i++)
                {
                    fInsertDECLARBODY_INF_ERR(IdDECLARBODY, KVIDECLARATION.DECLARBODY.INF_ERR[i]); 
                }
            }


        }
/*
	@FileName		[varchar](50) ,
	@FullFileName	[varchar],
	@FileDate		[datetime] ,
*/
        private Int32 fInsertKviDeclaration(String fNam, DateTime fDate)
        {
            int retId = -1;
            int retErr = -1;
            sqlConn.ConnectionString = connStr;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_KVIDECLARATION_I";

            SqlParameter FileName       = new SqlParameter("@FileName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0 , 0 , "FileName", DataRowVersion.Default , Path.GetFileName(fNam));
            SqlParameter FullFIleName   = new SqlParameter("@FullFIleName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FullFIleName", DataRowVersion.Default, fNam);
            SqlParameter FileDate       = new SqlParameter("@FileDate", SqlDbType.DateTime, 10, ParameterDirection.Input, false, 0, 0, "FileDate", DataRowVersion.Default, fDate);

            SQLProc.Parameters.Add(FileName);
            SQLProc.Parameters.Add(FullFIleName);
            SQLProc.Parameters.Add(FileDate);


            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_KVIDECLARATION_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_KVIDECLARATION_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;


        }

        private Int32 fInsertKVIDECLARATION_KVIHEAD(Int32 Id)
        {
            int retId = -1;
            int retErr = -1;
            sqlConn.ConnectionString = connStr;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_KVIDECLARATION_KVIHEAD_I";

/*
 @idKviDeclaration	
 @FNAME				
 @DOCFNAME			
 @RESULT				
 @KVTDATE			
 @KVTTIME			
 @KVTNUM				
 @TEXT				
  */

            SqlParameter idKviDeclaration = new SqlParameter("@idKviDeclaration", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "idKviDeclaration", DataRowVersion.Default, Id);
            SqlParameter FNAME = new SqlParameter("@FNAME", SqlDbType.VarChar, 31, ParameterDirection.Input, false, 0, 0, "FNAME", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.FNAME);
            SqlParameter DOCFNAME = new SqlParameter("@DOCFNAME", SqlDbType.VarChar, 31, ParameterDirection.Input, false, 0, 0, "DOCFNAME", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.DOCFNAME);
            SqlParameter RESULT = new SqlParameter("@RESULT", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "RESULT", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.RESULT);
            SqlParameter KVTDATE = new SqlParameter("@KVTDATE", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "KVTDATE", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.KVTDATE);
            SqlParameter KVTTIME = new SqlParameter("@KVTTIME", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "KVTTIME", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.KVTTIME);
            SqlParameter KVTNUM = new SqlParameter("@KVTNUM", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "KVTNUM", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.KVTNUM);
            SqlParameter TEXT = new SqlParameter("@TEXT", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "TEXT", DataRowVersion.Default, KVIDECLARATION.KVIHEAD.TEXT);

            SQLProc.Parameters.Add(idKviDeclaration);
            SQLProc.Parameters.Add(FNAME);
            SQLProc.Parameters.Add(DOCFNAME);
            SQLProc.Parameters.Add(RESULT);
            SQLProc.Parameters.Add(KVTDATE);
            SQLProc.Parameters.Add(KVTTIME);
            SQLProc.Parameters.Add(KVTNUM);
            SQLProc.Parameters.Add(TEXT);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_KVIDECLARATION_KVIHEAD_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_KVIDECLARATION_KVIHEAD_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;

        }

        private Int32 fInsertKVIHEAD_DOCHEAD(Int32 Id)
        {
            int retId = -1;
            int retErr = -1;
            sqlConn.ConnectionString = connStr;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_KVIHEAD_DOCHEAD_I";

            /*
	@idKVIHEAD int,
	@EDRPOU varchar (12),
	@MFO int,
    @FNAME varchar(50),
    @IDBANK char(3),
    @CDTASK char(3),
    @CDSUB char(5) ,
    @CDFORM char(8),
    @FILL_DATE varchar (10),
    @FILL_TIME varchar (4),
    @EI char(1),
    @KU char(2) ,			
              */
            SqlParameter idKVIHEAD = new SqlParameter("@idKVIHEAD", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "idKVIHEAD", DataRowVersion.Default, Id);
            SQLProc.Parameters.Add("@EDRPOU", System.Data.SqlDbType.VarChar, 12);
            SQLProc.Parameters.Add("@MFO", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@FNAME", System.Data.SqlDbType.VarChar, 50);
            SQLProc.Parameters.Add("@IDBANK", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@CDTASK", System.Data.SqlDbType.VarChar, 3);
            SQLProc.Parameters.Add("@CDSUB", System.Data.SqlDbType.VarChar, 5);
            SQLProc.Parameters.Add("@CDFORM", System.Data.SqlDbType.VarChar, 8);
            SQLProc.Parameters.Add("@FILL_DATE", System.Data.SqlDbType.VarChar, 10);
            SQLProc.Parameters.Add("@FILL_TIME", System.Data.SqlDbType.VarChar, 4);
            SQLProc.Parameters.Add("@EI", System.Data.SqlDbType.VarChar, 1);
            SQLProc.Parameters.Add("@KU", System.Data.SqlDbType.VarChar, 2);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);

            SQLProc.Parameters.Add(idKVIHEAD);

            SQLProc.Parameters["@EDRPOU"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@MFO"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FNAME"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@IDBANK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDTASK"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDSUB"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@CDFORM"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FILL_DATE"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@FILL_TIME"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@EI"].Direction = System.Data.ParameterDirection.Input;
            SQLProc.Parameters["@KU"].Direction = System.Data.ParameterDirection.Input;

            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            SQLProc.Parameters["@EDRPOU"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.EDRPOU;
            SQLProc.Parameters["@MFO"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.MFO;
            SQLProc.Parameters["@FNAME"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.FNAME;
            SQLProc.Parameters["@IDBANK"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.IDBANK;
            SQLProc.Parameters["@CDTASK"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.CDTASK;
            SQLProc.Parameters["@CDSUB"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.CDSUB;
            SQLProc.Parameters["@CDFORM"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.CDFORM;
            SQLProc.Parameters["@FILL_DATE"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.FILL_DATE;
            SQLProc.Parameters["@FILL_TIME"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.FILL_TIME;
            SQLProc.Parameters["@EI"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.EI;
            SQLProc.Parameters["@KU"].Value = KVIDECLARATION.KVIHEAD.DOCHEAD.KU;


            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_KVIHEAD_DOCHEAD_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_KVIHEAD_DOCHEAD_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;

        }

        private Int32 fInsertKVIDECLARATION_DECLARBODY(Int32 Id)
        {
            int retId = -1;
            int retErr = -1;
            sqlConn.ConnectionString = connStr;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_KVIDECLARATION_DECLARBODY_I";

            /*
                @idKviDeclaration	
	            @KVI_ERROR	
	            @REESTR_ID	
	            @KVI_COMMENT
	            @RESERVE	
              */

            SqlParameter idKviDeclaration = new SqlParameter("@idKviDeclaration", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "idKviDeclaration", DataRowVersion.Default, Id);
            SqlParameter KVI_ERROR = new SqlParameter("@KVI_ERROR", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "KVI_ERROR", DataRowVersion.Default, KVIDECLARATION.DECLARBODY.KVI_ERROR);
            SqlParameter REESTR_ID = new SqlParameter("@REESTR_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "REESTR_ID", DataRowVersion.Default, KVIDECLARATION.DECLARBODY.REESTR_ID);
            SqlParameter KVI_COMMENT = new SqlParameter("@KVI_COMMENT", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "KVI_COMMENT", DataRowVersion.Default, KVIDECLARATION.DECLARBODY.KVI_COMMENT);
//            SqlParameter RESERVE = new SqlParameter("@RESERVE", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "RESERVE", DataRowVersion.Default, KVIDECLARATION.DECLARBODY.RESERVE);

            SQLProc.Parameters.Add(idKviDeclaration);
            SQLProc.Parameters.Add(KVI_ERROR);
            SQLProc.Parameters.Add(REESTR_ID);
            SQLProc.Parameters.Add(KVI_COMMENT);
//            SQLProc.Parameters.Add(RESERVE);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_KVIDECLARATION_DECLARBODY_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_KVIDECLARATION_DECLARBODY_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;

        }

        private Int32 fInsertDECLARBODY_INF_ERR(Int32 Id, TROWS inf_err)
        {
            int retId = -1;
            int retErr = -1;
            sqlConn.ConnectionString = connStr;
            String retMessage = "";
            SqlCommand SQLProc = new SqlCommand();
            SQLProc.Connection = sqlConn;
            SQLProc.CommandType = CommandType.StoredProcedure;
            SQLProc.CommandText = "P_DECLARBODY_INF_ERR_I";

            /*
	            @idDECLARBODY	[int]		,
	            @ERR_REK		[varchar](20)		,
	            @ERR_KOD		[varchar](4)		,
	            @ROWNUM			[int]	
              */

            SqlParameter idDECLARBODY = new SqlParameter("@idDECLARBODY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "idDECLARBODY", DataRowVersion.Default, Id);
            SqlParameter ERR_REK = new SqlParameter("@ERR_REK", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "ERR_REK", DataRowVersion.Default, inf_err.ERR_REK.Value);
            SqlParameter ERR_KOD = new SqlParameter("@ERR_KOD", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "ERR_KOD", DataRowVersion.Default, inf_err.ERR_KOD.Value);
            SqlParameter ROWNUM = new SqlParameter("@ROWNUM", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ROWNUM", DataRowVersion.Default, inf_err.ROWNUM);

            SQLProc.Parameters.Add(idDECLARBODY);
            SQLProc.Parameters.Add(ERR_REK);
            SQLProc.Parameters.Add(ERR_KOD);
            SQLProc.Parameters.Add(ROWNUM);
            SQLProc.Parameters.Add("@id", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@error", System.Data.SqlDbType.Int);
            SQLProc.Parameters.Add("@ErrorText", System.Data.SqlDbType.VarChar, 200);
            SQLProc.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@error"].Direction = System.Data.ParameterDirection.Output;
            SQLProc.Parameters["@ErrorText"].Direction = System.Data.ParameterDirection.Output;

            try
            {
                sqlConn.Open();
                SQLProc.ExecuteNonQuery();
                retId = (Int32)SQLProc.Parameters["@id"].Value;
                retErr = (Int32)SQLProc.Parameters["@error"].Value;
                retMessage = (String)SQLProc.Parameters["@ErrorText"].Value;
                MessageBox.Show("id - " + retId.ToString() + "\n\rRetErr - " + retErr.ToString() + "\n\rretMessage - " + retMessage);

            }
            catch (SqlException e) { MessageBox.Show("SQLException in INSERT data in P_DECLARBODY_INF_ERR_I.\n\r" + e.Message); }
            catch (Exception e) { MessageBox.Show("SQLException in INSERT data in P_DECLARBODY_INF_ERR_I\n\r" + e.Message); }
            finally
            {
                sqlConn.Close();
            }
            return retId;


        }

        private void tsm_Quit_Click(object sender, EventArgs e)
        {


        }
    }



}


