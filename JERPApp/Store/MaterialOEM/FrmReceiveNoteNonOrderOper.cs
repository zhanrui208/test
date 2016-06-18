using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.MaterialOEM
{
    public partial class FrmReceiveNoteNonOrderOper : Form
    {
        public FrmReceiveNoteNonOrderOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accItems = new JERPData.Material.OEMReceiveItems();
            this.accNotes = new JERPData.Material.OEMReceiveNotes();
            this.accPrds = new JERPData.Product .Product();
            this.accStore = new JERPData.Material.OEMStore();
            this.dtblPrds = this.accPrds.GetDataProductForMaterial().Tables[0];           
            this.btnNew.Click += new EventHandler(btnNew_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.dgrdv .CellContextMenuStripNeeded +=new DataGridViewCellContextMenuStripNeededEventHandler(dgrdv_CellContextMenuStripNeeded);
            this.dgrdv.CellQuery += new JCommon.MyDataGridView.CellQueryDelegate(dgrdv_CellQuery);
            this.btnAddPrd.Click += new EventHandler(btnAddPrd_Click);
        }


        private JERPData.Material.OEMReceiveNotes accNotes;
        private JERPData.Material.OEMReceiveItems accItems;
        private JERPData.Product.Product accPrds;
        private JERPData.Material.OEMStore accStore;
        private JCommon.FrmGridFind frmGridPrd;
        private JERPApp.Define.Material.FrmProduct frmPrd;
        private JERPApp.Define.Material.FrmProduct frmAddPrd;
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
            this.rchMemo.Text = string.Empty;
            this.dtblItems = this.accItems.GetDataOEMReceiveItemsByNoteID (-1).Tables[0];        
            this.dgrdv.DataSource = this.dtblItems;
        }
      

        void btnNew_Click(object sender, EventArgs e)
        {
            this.NewNote();
        }
        private bool ValidateData()
        {
            if (this.ctrlCompanyID.CompanyID == -1)
            {
                MessageBox.Show("�ͻ�����Ϊ��");
                return false;
            }
            if (this.dtblItems.Select("", "").Length == 0)
            {
                MessageBox.Show("û���κ���ϸ��¼");
                return false;
            }
            if (this.dtblItems.Select("PrdID is null or Quantity is null","",
                DataViewRowState .CurrentRows ).Length > 0)
            {
                MessageBox.Show("��¼δ��ȫ,���ܱ���");
                return false;
            }
            return true;
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
                    this.frmPrd = new JERPApp.Define.Material.FrmProduct();
                    new FrmStyle(this.frmPrd).SetPopFrmStyle(this);
                    this.frmPrd.AffterSelected += frmPrd_AffterSelected;
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
            this.dtblItems.Rows.Add(drowNew);

        }
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                if ((grow.Cells[this.ColumnPrdCode.Name].Value == DBNull.Value)
                    || (grow.Cells[this.ColumnQuantity.Name].Value == DBNull.Value))
                {
                    grow.ErrorText = "��¼δ��ȫ";
                }
                else
                {
                    grow.ErrorText = string.Empty;
                }

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("�����б�������һ��ȷ�ϲ��ܱ����", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            bool flag = false;
            string errormsg = string.Empty;
            long NoteID = -1;
            object objNoteID = DBNull.Value;          
            flag = this.accNotes.InsertOEMReceiveNotes(
               ref errormsg,
               ref objNoteID,
               this.txtNoteCode.Text,
               this.dtpDateNote.Value.Date,
               DBNull .Value ,
               this.ctrlCompanyID.CompanyID,
               this.ctrlQCPsnID .PsnID ,
               JERPBiz.Frame.UserBiz.PsnID,
               this.rchMemo.Text);
            if (!flag)
            {
                MessageBox.Show(errormsg, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NoteID = (long)objNoteID;         
            //��ϸ          
            foreach (DataRow drow in this.dtblItems.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                this.accItems.InsertOEMReceiveItemsNonOrder(ref errormsg, 
                    NoteID,
                    drow["PrdID"],
                    drow["Quantity"]);
                //�͹����
                this.accStore.InsertOEMStoreForIntoStore(ref errormsg,
                    Report.Bill.FrmBizBill.ReceiveNoteID,
                    Report.Bill.FrmBizBill.ReceiveNoteNoteName,
                    NoteID,
                    this.txtNoteCode.Text,
                    drow["PrdID"],
                    this.ctrlCompanyID.CompanyID,
                    drow["Quantity"]);
            }
            MessageBox.Show("�ɹ����沢���˵�ǰ�ջ���");
            if (this.affterSave != null) this.affterSave();
            this.NewNote();

        }

   
 

     

    }
}