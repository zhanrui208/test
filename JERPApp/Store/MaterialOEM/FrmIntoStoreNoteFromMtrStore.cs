using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.MaterialOEM
{
    public partial class FrmIntoStoreNoteFromMtrStore : Form
    {
        public FrmIntoStoreNoteFromMtrStore()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accItems = new JERPData.Material.OutStoreItems();
            this.accNotes = new JERPData.Material.OutStoreNotes();
            this.accPrds = new JERPData.Product.Product ();
            this.accStore = new JERPData.Material.Store();
            this.accStoreReserve = new JERPData.Material.StoreReserve();
            this.accWorkingSheetReserve = new JERPData.Material.StoreWorkingSheetReserve();
            this.accExpenseAccount = new JERPData.Finance.ExpenseAccount();

            this.accOEMItems = new JERPData.Material.OEMIntoStoreItems();
            this.accOEMNotes = new JERPData.Material.OEMIntoStoreNotes();
            this.accOEMStore = new JERPData.Material.OEMStore();
             
            this.dgrdv.CellValueChanged += new DataGridViewCellEventHandler(dgrdv_CellValueChanged);
            this.dgrdv.CellQuery += new JCommon.MyDataGridView.CellQueryDelegate(dgrdv_CellQuery);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.ctrlMtrBranchStoreID.AffterSelected += new JERPApp.Define.Material.CtrlBranchStore.AffterSelectedDelegate(ctrlMtrBranchStoreID_AffterSelected);
            this.dgrdv .CellContextMenuStripNeeded +=new DataGridViewCellContextMenuStripNeededEventHandler(dgrdv_CellContextMenuStripNeeded);
            this.dtblPrds = this.accPrds.GetDataProductForMaterial().Tables[0];
            this.btnSave.Click += new EventHandler(btnSave_Click); 
            this.btnAddPrd.Click += new EventHandler(btnAddPrd_Click);
        }

        private JERPData.Material.Store accStore;     
        private JERPData.Material.OutStoreNotes  accNotes;
        private JERPData.Material.OutStoreItems  accItems;
        private JERPData.Product.Product accPrds;
        private JERPData.Material.StoreReserve accStoreReserve;
        private JERPData.Material.StoreWorkingSheetReserve accWorkingSheetReserve;

        private JERPData.Material.OEMStore accOEMStore;
        private JERPData.Material.OEMIntoStoreItems accOEMItems;
        private JERPData.Material.OEMIntoStoreNotes accOEMNotes;
        private JERPData.Finance.ExpenseAccount accExpenseAccount;
        private JERPApp.Define.Material.FrmProduct frmPrd;
        private JERPApp.Define.Material.FrmProduct frmAddPrd;
        private JCommon.FrmGridFind frmGridPrd;
        private DataTable dtblItems,dtblPrds;
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
       
    
        public void NewNote()
        {
            this.dtpDateNote.Value = DateTime.Now.Date;
            this.txtNoteCode.Text = string.Empty; 
            this.dtblItems = this.accOEMItems.GetDataOEMIntoStoreItemsByNoteID  (-1).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
            this.dtblItems.Columns["PrdID"].AllowDBNull = false;
            this.dtblItems.Columns["PrdID"].Unique = true;
            this.dtblItems.Columns["Quantity"].AllowDBNull = false;
            this.dtblItems.Columns.Add("InventoryQty", typeof(decimal));
        }
     

        void dgrdv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].DataPropertyName == "PrdID")
            {
                object objPrdID = this.dgrdv[icol, irow].Value;
                if (objPrdID == DBNull.Value) return;
                this.dgrdv[this.ColumnInventoryQty.Name, irow].Value = this.GetInventoryQty((int)objPrdID);
            }
        }


        bool ValidateData()
        {
            if (this.ctrlMtrBranchStoreID.BranchStoreID ==-1)
            {
                MessageBox.Show("�Բ���,ԭ�Ͽⲻ��Ϊ��!");
                return false;
            }
            if (this.ctrlCompanyID .CompanyID == -1)
            {
                MessageBox.Show("�Բ���,�͹��ⲻ��Ϊ��!");
                return false;
            }
            DataRow[] drows = this.dtblItems.Select("Quantity>InventoryQty", "", DataViewRowState.CurrentRows);
            if (drows.Length > 0)
            {
                MessageBox.Show("�Բ���,��ǰ�����ڲ���,���ܳ���");
                return false;
            }
            return true;
        }
        private decimal GetInventoryQty(int PrdID)
        {
            decimal rut = 0;
            this.accStore.GetParmStoreInventoryQty(PrdID,this.ctrlMtrBranchStoreID .BranchStoreID , ref rut);
            return rut;
        }
        private decimal GetCostPrice(int PrdID)
        {
            decimal rut = 0;
            this.accStore.GetParmStoreInventoryPrice(PrdID, this.ctrlMtrBranchStoreID.BranchStoreID, ref rut);
            return rut;
        }

        void ctrlMtrBranchStoreID_AffterSelected()
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                object objPrdID = grow.Cells[this.ColumnPrdCode.Name].Value;
                if (objPrdID == DBNull.Value) continue;
                grow.Cells[this.ColumnInventoryQty.Name].Value = this.GetInventoryQty ((int)objPrdID);

            }
        }

       
        void dgrdv_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if ((this.dgrdv.Columns[icol].DataPropertyName == "PrdCode")
                || (this.dgrdv.Columns[icol].DataPropertyName == "PrdName")
                || (this.dgrdv.Columns[icol].DataPropertyName == "PrdSpec")
                || (this.dgrdv.Columns[icol].DataPropertyName == "Model")
                || (this.dgrdv.Columns[icol].DataPropertyName == "Manufacturer"))
            {

                this.dgrdv.CurrentCell = this.dgrdv[icol, irow];
                if (frmPrd == null)
                {
                    this.frmPrd = new JERPApp.Define.Material .FrmProduct();
                    new FrmStyle(this.frmPrd).SetPopFrmStyle(this);
                    this.frmPrd.AffterSelected += new JERPApp.Define.Material.FrmProduct.AffterSelectedDelegate(frmPrd_AffterSelected);
                }
                this.frmPrd.ShowDialog();
            }
        }

        void frmPrd_AffterSelected(DataRow drow)
        {
            DataGridViewRow grow = this.dgrdv.CurrentRow;
            int PrdID = (int)drow["PrdID"];
            grow.Cells[this.ColumnPrdID.Name].Value = PrdID;
            grow.Cells[this.ColumnPrdCode.Name].Value = drow["PrdCode"];
            grow.Cells[this.ColumnPrdName.Name].Value = drow["PrdName"];
            grow.Cells[this.ColumnPrdSpec.Name].Value = drow["PrdSpec"];
            grow.Cells[this.ColumnModel.Name].Value = drow["Model"];
            grow.Cells[this.ColumnManufacturer.Name].Value = drow["Manufacturer"];
            grow.Cells[this.ColumnUnitName.Name].Value = drow["UnitName"]; 
            grow.Cells[this.ColumnInventoryQty.Name].Value = this.GetInventoryQty(PrdID);
            frmPrd.Close();

        }
        void btnAddPrd_Click(object sender, EventArgs e)
        {
            if (frmAddPrd == null)
            {
                this.frmAddPrd = new JERPApp.Define.Material.FrmProduct();
                new FrmStyle(this.frmAddPrd).SetPopFrmStyle(this);
                this.frmAddPrd.AffterSelected += frmAddPrd_AffterSelected;
            }
            this.frmAddPrd.ShowDialog();
        }
        void frmAddPrd_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            if (this.dtblItems.Select("PrdID=" + PrdID.ToString(), "", DataViewRowState.CurrentRows).Length > 0)
            {
                return;
            }
            DataRow drowNew = this.dtblItems.NewRow();
            drowNew["PrdID"] = PrdID;
            drowNew["PrdCode"] = drow["PrdCode"];
            drowNew["PrdName"] = drow["PrdName"];
            drowNew["PrdSpec"] = drow["PrdSpec"];
            drowNew["Model"] = drow["Model"];
            drowNew["Manufacturer"] = drow["Manufacturer"];
            drowNew["UnitName"] = drow["UnitName"]; 
            drowNew["Quantity"] = 0;
            drowNew["InventoryQty"] = this.GetInventoryQty (PrdID);           
            this.dtblItems.Rows.Add(drowNew);

        }
        void dgrdv_CellQuery(DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnAssistantCode.Name)
            {
              
                if (this.frmGridPrd == null)
                {
                    this.frmGridPrd = new JCommon.FrmGridFind();
                    this.frmGridPrd.AddGridColumn("PrdCode", "���ϱ��", 100);
                    this.frmGridPrd.AddGridColumn("PrdName", "��������", 100);
                    this.frmGridPrd.AddGridColumn("PrdSpec", "���Ϲ��", 100);
                    this.frmGridPrd.AddGridColumn("Model", "����", 66);
                    this.frmGridPrd.AddGridColumn("Manufacturer", "Ʒ��", 66);
                    this.frmGridPrd.AddGridColumn("AssistantCode", "������", 66);
                    this.frmGridPrd.AffterSelected += frmGridPrd_AffterSelected;

                }
                this.frmGridPrd.Query(this.dtblPrds, "AssistantCode", this.dgrdv.CurrentCell);
              
            }
        }
        void frmGridPrd_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            DataGridViewRow grow = this.dgrdv.CurrentRow;
            grow.Cells[this.ColumnPrdID.Name].Value = PrdID;
            grow.Cells[this.ColumnPrdCode.Name].Value = drow["PrdCode"];
            grow.Cells[this.ColumnPrdName.Name].Value = drow["PrdName"];
            grow.Cells[this.ColumnPrdSpec.Name].Value = drow["PrdSpec"];
            grow.Cells[this.ColumnModel.Name].Value = drow["Model"];
            grow.Cells[this.ColumnManufacturer.Name].Value = drow["Manufacturer"];
            grow.Cells[this.ColumnUnitName.Name].Value = drow["UnitName"];

        }
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                object objQuantity = grow.Cells[this.ColumnQuantity.Name].Value;
                object objInventory = grow.Cells[this.ColumnInventoryQty.Name].Value;
                if ((objQuantity != DBNull.Value) && (objInventory != DBNull.Value))
                {
                    grow.ErrorText = ((decimal)objQuantity > (decimal)objInventory) ? "��治��" : string.Empty;
                }
            }
        }
        void SaveOEMNote()
        {
            string errormsg = string.Empty;
            bool flag = false;
            long NoteID = -1;
            string NoteCode = string.Empty;
            object objNoteID = DBNull.Value;
            object objNoteCode = DBNull.Value;
            flag = this.accOEMNotes.InsertOEMIntoStoreNotes (ref errormsg,
                ref objNoteID,
                ref objNoteCode,
                this.dtpDateNote.Value.Date,
                JERPBiz.Frame.UserBiz.PsnID,
                "ԭ�Ͽ����");
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            } 
            NoteID = (long)objNoteID;
            NoteCode = objNoteCode.ToString(); 
            foreach (DataRow drow in this.dtblItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                int PrdID = (int)drow["PrdID"];
                this.accOEMItems .InsertOEMIntoStoreItems(ref errormsg,
                   NoteID,
                   PrdID,
                   drow["Quantity"],
                   this.ctrlCompanyID .CompanyID ,
                   DBNull .Value ); 
                //���
                this.accOEMStore.InsertOEMStoreForIntoStore(ref errormsg,
                          JERPApp.Store .MaterialOEM .Report .Bill .FrmBizBill.IntoStoreNoteID , 
                         JERPApp.Store .MaterialOEM .Report .Bill .FrmBizBill.IntoStoreNoteID  ,
                          NoteID,
                          NoteCode,
                          PrdID,
                          this.ctrlCompanyID .CompanyID ,
                          drow["Quantity"]);
              
            
        }
        }
        void SaveNote()
        {
            bool flag = false;
            string errormsg = string.Empty;
            long NoteID = -1;
            string NoteCode = string.Empty;
            object objNoteID = DBNull.Value;
            object objNoteCode = DBNull.Value;
            flag = this.accNotes.InsertOutStoreNotes (ref errormsg,
                ref objNoteID,
                ref objNoteCode,
                this.dtpDateNote.Value.Date,
                JERPBiz.Frame.UserBiz.PsnID,
               "�������͹���");
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            NoteID = (long)objNoteID;
            NoteCode = objNoteCode.ToString();
            this.txtNoteCode.Text = NoteCode;
            decimal Quantity,CostPrice, TotalAMT = 0;
            int PrdID, BranchStoreID = this.ctrlMtrBranchStoreID.BranchStoreID;
            foreach (DataRow drow in this.dtblItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                PrdID = (int)drow["PrdID"];
                CostPrice = this.GetCostPrice(PrdID);
                Quantity = (decimal)drow["Quantity"];
                TotalAMT +=CostPrice*Quantity;
                this.accItems.InsertOutStoreItems(ref errormsg,
                     NoteID, 
                     PrdID ,
                     Quantity,
                     BranchStoreID,
                    CostPrice );
                //����
                this.accStore .InsertStoreForOutStore(
                    ref errormsg,
                    JERPBiz.Finance.NoteNameParm.MtrOutStoreNoteNameID,
                    NoteID,
                    NoteCode,
                    PrdID ,
                    BranchStoreID,
                    Quantity ,
                    CostPrice);
                this.accStoreReserve.SaveStoreReserveForSubReserve(ref errormsg,
                  PrdID,
                  Quantity);
                this.accWorkingSheetReserve.SaveStoreWorkingSheetReserveForSubReserve(ref errormsg,
                    PrdID,
                    Quantity);

            }
            if (TotalAMT > 0)
            {
                //�������ϵ�
                this.accExpenseAccount.InsertExpenseAccountForDebit(
                    ref errormsg,
                    "ԭ�����ϵ�[" + NoteCode + "]",
                    JERPBiz.Finance.ExpenseTypeParm.MtrExpense,
                    TotalAMT,
                    JERPBiz.Frame.UserBiz.PsnID);
            }    
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("�����б�������һ�����˲��ܱ����", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            this.SaveNote();
            this.SaveOEMNote();
            MessageBox.Show("�ɹ����沢���˵�ǰ����");
            if (this.affterSave != null) this.affterSave();
            this.Close();
        }

    
    }
}