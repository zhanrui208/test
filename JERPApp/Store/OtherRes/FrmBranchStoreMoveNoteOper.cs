using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.OtherRes
{
    public partial class FrmBranchStoreMoveNoteOper : Form
    {
        public FrmBranchStoreMoveNoteOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accItems = new JERPData.OtherRes.BranchStoreMoveItems();
            this.accNotes = new JERPData.OtherRes.BranchStoreMoveNotes();
            this.accPrds = new JERPData.OtherRes.Product ();
            this.accStore = new JERPData.OtherRes.Store();
            this.dgrdv.CellValueChanged += new DataGridViewCellEventHandler(dgrdv_CellValueChanged);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.ctrlOutBranchStoreID.AffterSelected += ctrlOutBranchStoreID_AffterSelected;
            this.dgrdv .CellContextMenuStripNeeded +=new DataGridViewCellContextMenuStripNeededEventHandler(dgrdv_CellContextMenuStripNeeded);
            this.dtblPrds = this.accPrds.GetDataProduct().Tables[0];
            this.dgrdv.CellQuery += new JCommon.MyDataGridView.CellQueryDelegate(dgrdv_CellQuery);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnAddPrd .Click +=new EventHandler(btnAddPrd_Click);
        }

        private JERPData.OtherRes.Store accStore;     
        private JERPData.OtherRes.BranchStoreMoveNotes accNotes;
        private JERPData.OtherRes.BranchStoreMoveItems accItems;
        private JERPData.OtherRes.Product accPrds;
        private JERPApp.Define.OtherRes.FrmProduct frmPrd;
        private JERPApp.Define.OtherRes.FrmProduct frmAddPrd;
        private DataTable dtblItems,dtblPrds;
        private JCommon.FrmGridFind frmGridPrd;
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
            this.rchMemo.Text = string.Empty;
            this.dtblItems = this.accItems.GetDataBranchStoreMoveItemsByNoteID(-1).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
            this.dtblItems.Columns["PrdID"].AllowDBNull = false;
            this.dtblItems.Columns["PrdID"].Unique = true;
            this.dtblItems.Columns["Quantity"].AllowDBNull = false;
            this.dtblItems.Columns.Add("InventoryQty", typeof(decimal));
        }
        private decimal GetInventoryQty(int PrdID)
        {
            decimal inventoryQty = 0;
            this.accStore.GetParmStoreInventoryQty(PrdID, this.ctrlOutBranchStoreID.BranchStoreID,
                ref inventoryQty);
            return inventoryQty;
        }
        void ctrlOutBranchStoreID_AffterSelected()
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                object objPrdID = grow.Cells[this.ColumnPrdName.Name].Value;
                if (objPrdID == DBNull.Value) continue;
                grow.Cells[this.ColumnInventoryQty.Name].Value = this.GetInventoryQty((int)objPrdID);

            }
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
        void dgrdv_CellQuery(DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnAssistantCode.Name)
            {
                string AssistantCode = this.dgrdv[icol, irow].FormattedValue.ToString();
                if (AssistantCode == string.Empty) return;
                if (this.frmGridPrd == null)
                {
                    this.frmGridPrd = new JCommon.FrmGridFind();
                    this.frmGridPrd.AddGridColumn("PrdName", "��Ʒ����", 100);
                    this.frmGridPrd.AddGridColumn("PrdSpec", "�ͺż����", 120);   
                    this.frmGridPrd.AddGridColumn("AssistantCode", "������", 66);
                    this.frmGridPrd.AffterSelected += frmGridPrd_AffterSelected;

                }
                this.frmGridPrd.Query(this.dtblPrds, "AssistantCode", this.dgrdv.CurrentCell);
              

            }
        }
        void frmGridPrd_AffterSelected(DataRow drow)
        {
            DataGridViewRow grow = this.dgrdv.CurrentRow;
            int PrdID = (int)drow["PrdID"];
            grow.Cells[this.ColumnPrdID.Name].Value = drow["PrdID"];
            grow.Cells[this.ColumnPrdName.Name].Value = drow["PrdName"];
            grow.Cells[this.ColumnPrdSpec.Name].Value = drow["PrdSpec"];
            grow.Cells[this.ColumnUnitName.Name].Value = drow["UnitName"];
            grow.Cells[this.ColumnInventoryQty .Name].Value =this.GetInventoryQty (PrdID ); 
          
        }
        void btnAddPrd_Click(object sender, EventArgs e)
        {
            if (frmAddPrd == null)
            {
                this.frmAddPrd = new JERPApp.Define.OtherRes.FrmProduct();
                new FrmStyle(this.frmAddPrd).SetPopFrmStyle(this);
                this.frmAddPrd.AffterSelected += frmAddPrd_AffterSelected;
            }
            this.frmAddPrd.ShowDialog();
        }
        void frmAddPrd_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            if (this.dtblItems.Select("PrdID=" + PrdID.ToString(), "", DataViewRowState.CurrentRows).Length > 0) return;
            DataRow drowNew = this.dtblItems.NewRow();
            drowNew["PrdID"] = PrdID;
            drowNew["PrdName"] = drow["PrdName"];
            drowNew["PrdSpec"] = drow["PrdSpec"];
            drowNew["UnitName"] = drow["UnitName"]; 
            drowNew["Quantity"] = 0;
            drowNew["InventoryQty"] = this.GetInventoryQty(PrdID);
            this.dtblItems.Rows.Add(drowNew);

        }
        bool ValidateData()
        {
            if (this.ctrlIntoBranchStoreID.BranchStoreID == this.ctrlOutBranchStoreID.BranchStoreID)
            {
                MessageBox.Show("�Բ���,����֮��Ŀ�λ������ͬ!");
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
        private decimal GetCostPrice(int PrdID)
        {
            decimal rut = 0;
            this.accStore.GetParmStoreInventoryPrice(PrdID, this.ctrlOutBranchStoreID.BranchStoreID, ref rut);
            return rut;
        }

        void dgrdv_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
             if ((this.dgrdv.Columns[icol].DataPropertyName == "PrdName")
                ||(this.dgrdv.Columns[icol].DataPropertyName == "PrdSpec")
		)
            { 
                this.dgrdv.CurrentCell = this.dgrdv[icol, irow];
                if (frmPrd == null)
                {
                    this.frmPrd = new JERPApp.Define.OtherRes .FrmProduct();
                    new FrmStyle(this.frmPrd).SetPopFrmStyle(this);
                    this.frmPrd.AffterSelected += new JERPApp.Define.OtherRes.FrmProduct.AffterSelectedDelegate(frmPrd_AffterSelected);
                }
                this.frmPrd.ShowDialog();
            }
        }

        void frmPrd_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            this.dgrdv.CurrentCell.Value = PrdID;
            this.dgrdv.CurrentRow.Cells[this.ColumnInventoryQty.Name].Value = this.GetInventoryQty(PrdID);
            frmPrd.Close();

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
        void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult rul = MessageBox.Show("�����б�������һ�����˲��ܱ����", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            if (ValidateData() == false) return;
            bool flag = false;
            string errormsg = string.Empty;
            long NoteID=-1;
            string NoteCode=string.Empty ;
            object objNoteID = DBNull.Value;
            object objNoteCode = DBNull.Value;
            flag = this.accNotes.InsertBranchStoreMoveNotes(ref errormsg,
                ref objNoteID,
                ref objNoteCode,
                this.dtpDateNote.Value.Date,
                this.ctrlIntoBranchStoreID.BranchStoreID,
                this.ctrlOutBranchStoreID.BranchStoreID,
                JERPBiz.Frame.UserBiz.PsnID,
                this.rchMemo.Text);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }
            NoteID =(long)objNoteID ;
            NoteCode =objNoteCode .ToString ();
            this.txtNoteCode.Text = NoteCode;
            foreach (DataRow drow in this.dtblItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;  
                string ErrorMsg = string.Empty;
                object objItemID = DBNull.Value;
                decimal CostPrice = this.GetCostPrice((int)drow["PrdID"]); 
                flag = this.accItems.InsertBranchStoreMoveItems(ref ErrorMsg,
                     ref objItemID,
                     NoteID,
                     drow["PrdID"],
                     drow["Quantity"],
                     CostPrice);
                if (flag)
                {
                    drow["ItemID"] = objItemID;
                    //ת��
                    this.accStore.InsertStoreForOutStore(ref errormsg,
                         JERPBiz.Finance.NoteNameParm.OtherResBranchStoreMoveNoteNameID,
                         NoteID ,
                         NoteCode ,
                        drow["PrdID"],
                        this.ctrlOutBranchStoreID.BranchStoreID,
                        drow["Quantity"],
                        CostPrice );
                    //ת��
                    this.accStore.InsertStoreForIntoStore(ref errormsg,
                         JERPBiz.Finance.NoteNameParm.OtherResBranchStoreMoveNoteNameID,
                       NoteID ,
                       NoteCode ,
                       drow["PrdID"],
                       this.ctrlIntoBranchStoreID .BranchStoreID,
                       drow["Quantity"],
                       CostPrice);
                }

            }         
           
            MessageBox.Show("�ɹ����沢���˵�ǰ����");
            if (this.affterSave != null) this.affterSave();
            this.NewNote();
        }

    
    }
}