using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture.SemiPrd
{
    public partial class FrmStoreCheckNoteOper : Form
    {
        public FrmStoreCheckNoteOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accNotes = new JERPData.SemiPrd .StoreCheckNotes();
            this.accItems = new JERPData.SemiPrd.StoreCheckItems(); 
            this.accStore = new JERPData.SemiPrd .Store();
            this.accManufProcess = new JERPData.Manufacture.ManufProcess();
            this.ctrlPrdID.AffterSelected += new JERPApp.Define.Product.CtrlManufPrd.AffterSelectedDelegate(ctrlPrdID_AffterSelected);
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnImport.Click += new EventHandler(btnImport_Click); 
            this.btnExport .Click +=new EventHandler(btnExport_Click);
           
        }

    
        private JERPData.SemiPrd.StoreCheckNotes accNotes;
        private JERPData.SemiPrd.StoreCheckItems accItems; 
        private JERPData.SemiPrd .Store accStore;
        private JERPData.Manufacture.ManufProcess accManufProcess;
        private JCommon.FrmExcelImport frmImport;
        private DataTable dtblItems,dtblManufProcess;
     
        void LoadData()
        {
            this.dtblManufProcess = this.accManufProcess.GetDataManufProcess().Tables[0];
            this.dtblItems =this.accStore.GetDataStore().Tables[0];
            this.dtblItems.Columns.Add("CheckQty", typeof(decimal));
            this.dgrdv.DataSource = this.dtblItems;
        }
  
        public delegate void AffterSaveDelegate();
        private AffterSaveDelegate affterSave;
        public event AffterSaveDelegate AffterSave
        {
            add
            {
                affterSave += value;
            }
            remove
            {
                affterSave -= value;
            }
        }
        void ctrlPrdID_AffterSelected()
        {
            this.ctrlManufProcessID.LoadData(this.ctrlPrdID.PrdID);
        }    
        private long GetManufProcessID(string PrdCode, string PrdStatus)
        {
            DataRow[] drows = this.dtblManufProcess.Select("PrdCode='" + PrdCode + "' and PrdStatus='" + PrdStatus + "'");
            if (drows.Length > 0)
            {
                return (long)drows[0]["ManufProcessID"];
            }
            return -1;
        }
        void btnImport_Click(object sender, EventArgs e)
        {
            if (this.frmImport == null)
            {
                frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                DataColumn[] columns = new DataColumn[]{                  
                    new DataColumn ("��Ʒ���",typeof (string )), 
                    new DataColumn ("����",typeof (string )),
                    new DataColumn ("����",typeof (decimal))
                };
                frmImport.SetImportColumn(columns, "����:������Ϊ��ֵ;");
                frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);

            }
            frmImport.ShowDialog();
        }
      
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
           
            msg = string.Empty;
            flag = true;
            long ManufProcessID = this.GetManufProcessID(drow["��Ʒ���"].ToString(), drow["����"].ToString());
            if (ManufProcessID == -1)
            {
                flag = false;
                msg = "δ���ڴ˰��Ʒ";
                return;
            }
            DataRow[] rows = this.dtblItems.Select("ManufProcessID="+ManufProcessID .ToString ());
            if (rows.Length == 0)
            {
                DataRow[] drowProcess = this.dtblManufProcess.Select("ManufProcessID=" + ManufProcessID.ToString());
                DataRow drowNew = this.dtblItems.NewRow();

                drowNew["ManufProcessID"] = ManufProcessID;
                drowNew["PrdCode"] = drow["��Ʒ���"];
                drowNew["PrdName"] = drowProcess[0]["PrdName"];
                drowNew["PrdSpec"] = drowProcess[0]["PrdSpec"];
                drowNew["Model"] = drowProcess[0]["Model"];
                drowNew["PrdStatus"] = drow["����"];
                drowNew["UnitName"] = drowProcess[0]["UnitName"];
                drowNew["CheckQty"] = drow["����"];
                this.dtblItems.Rows.Add(drowNew);
              
            }
            else
            {
                rows[0]["CheckQty"] = drow["����"];                
            }
            

        }
   
        public  void NewNote()
        {
           
            this.txtCheckPersons.Text = string.Empty;
            this.rchMemo.Text = string.Empty;
            this.dtpDateNote .Value = DateTime.Now.Date;
            this.LoadData();
        }
        private bool ValidateData()
        { 
            if (this.dtblItems.Select("CheckQty is not null", "", DataViewRowState.CurrentRows).Length == 0)
            {
                MessageBox.Show("û���κμ�¼!");
                return false;
            }
            return true;
        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            long ManufProcessID = this.ctrlManufProcessID.ManufProcessID;
            DataRow[] rows = this.dtblItems.Select("ManufProcessID=" + ManufProcessID .ToString());
            if (rows.Length> 0)
            {
                MessageBox.Show("�Ѵ��ڴ˰��Ʒ");
                return;
            }
            DataRow drowNew = this.dtblItems.NewRow();
            drowNew["ManufProcessID"] = ManufProcessID;
            drowNew["PrdCode"] = this.ctrlPrdID.PrdEntity.PrdCode;
            drowNew["PrdName"] = this.ctrlPrdID.PrdEntity.PrdName ;
            drowNew["PrdSpec"] = this.ctrlPrdID.PrdEntity.PrdSpec ;
            drowNew["Model"] = this.ctrlPrdID.PrdEntity.Model ;
            drowNew["PrdStatus"] = this.ctrlManufProcessID.PrdStatus;
            drowNew["UnitName"] = this.ctrlPrdID.PrdEntity.UnitName;
            this.dtblItems.Rows.Add(drowNew);

           
          
        }


        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "���߰��Ʒ�̵�");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            FrmMsg.Hide();
            excel.Show();
        }
     
        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
            DialogResult rsl = MessageBox.Show("�����б��沢���ˣ�\nһ�����潫���ܱ����ȷ�Ϸ�?",
               "ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rsl == DialogResult.No) return;
            string errormsg=string .Empty ;
            bool flag = false;           
            object objNoteID = DBNull.Value;
            object objNoteCode = DBNull.Value;
            long NoteID = -1;
            string NoteCode = string.Empty; 
            flag = this.accNotes.InsertStoreCheckNotes(ref errormsg, ref objNoteID,   
                this.dtpDateNote.Value.Date,
                JERPBiz.Frame.UserBiz.PsnID,
                this.txtCheckPersons.Text, 
                this.rchMemo.Text);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
          
            NoteID = (long)objNoteID; 
            object  objInitQty, objSurplusQty, objLostQty;
            bool ExistFlag;
            long ManufProcessID = -1;
            decimal CheckQty, InventoryQty, SurplusQty ;
            
            foreach (DataRow drow in this.dtblItems.Rows )
            {
                
                if (drow["CheckQty"] == DBNull.Value) continue;
                ExistFlag = false;
                InventoryQty = 0;
                if (drow["InventoryQty"] != DBNull.Value)
                {
                    ExistFlag = true;
                    InventoryQty = (decimal)drow["InventoryQty"];
                }

                ManufProcessID = (long)drow["ManufProcessID"];
                CheckQty = (decimal)drow["CheckQty"];              
                SurplusQty = CheckQty - InventoryQty; 
                objInitQty = DBNull.Value;
                objSurplusQty = DBNull.Value;
                objLostQty = DBNull.Value;
                if (ExistFlag == false)
                {
                    objInitQty = SurplusQty;
                }
                else
                {
                    if (SurplusQty > 0)
                    {
                        objSurplusQty = SurplusQty;
                    }
                    if (SurplusQty < 0)
                    {
                        objLostQty = Math.Abs(SurplusQty);
                    }
                }
                
                flag = this.accItems.InsertStoreCheckItems(ref errormsg,                    
                    NoteID,
                    ManufProcessID ,
                    CheckQty,
                    InventoryQty, 
                    objInitQty,
                    objSurplusQty,
                    objLostQty);                
                if (flag)
                {
                 
                    if (ExistFlag == false)
                    {
                        this.accStore.SaveStoreForIntoStore (
                            ref errormsg ,
                            ManufProcessID ,
                            objInitQty );
                    }
                    else
                    {

                        //���                  
                        if (SurplusQty > 0)
                        {
                            this.accStore.SaveStoreForIntoStore (ref errormsg,
                                ManufProcessID,
                                SurplusQty );
                        }
                        if (SurplusQty < 0)
                        {
                            this.accStore.SaveStoreForOutStore (ref errormsg,
                                 ManufProcessID,
                                -SurplusQty);
                           
                        }
                    
                    }
                }
            }
           
            MessageBox.Show("�ɹ����沢���˵�ǰ�̵㵥");
            if (this.affterSave != null) this.affterSave();
            this.NewNote();
        }
    

    }
}