using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture.SemiPrd
{
    public partial class FrmIntoStoreNoteOper : Form
    {
        public FrmIntoStoreNoteOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accItems = new JERPData.SemiPrd .IntoStoreItems();
            this.accNotes = new JERPData.SemiPrd.IntoStoreNotes();
            this.accStore = new JERPData.SemiPrd.Store();
            this.accManufProcess = new JERPData.Manufacture.ManufProcess();
            this.accPrds = new JERPData.Product.Product();
            this.accWorkingSheets = new JERPData.Manufacture.WorkingSheets();
            this.SetColumnSrc();
            this.dgrdv.CellValueChanged += new DataGridViewCellEventHandler(dgrdv_CellValueChanged);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.dgrdv .CellContextMenuStripNeeded +=new DataGridViewCellContextMenuStripNeededEventHandler(dgrdv_CellContextMenuStripNeeded);
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

        private JERPData.SemiPrd.IntoStoreNotes accNotes;
        private JERPData.SemiPrd.IntoStoreItems accItems;
        private JERPData.SemiPrd.Store accStore;
        private JERPData.Product.Product accPrds;
        private JERPApp.Define.Product.FrmManufPrd  frmPrd;
        private JERPData.Manufacture.WorkingSheets accWorkingSheets;
        private JERPData.Manufacture.ManufProcess  accManufProcess;
        private DataTable dtblItems, dtblPrds, dtblWorkingSheets;
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

        private void SetColumnSrc()
        {

            this.dtblWorkingSheets = this.accWorkingSheets.GetDataWorkingSheetsLastRecord(500).Tables[0];
           
            if (this.dtblWorkingSheets.Rows.Count > 0)
            { 
                DataRow drowNew = this.dtblWorkingSheets.NewRow();
                drowNew["WorkingSheetID"] = DBNull.Value;
                drowNew["WorkingSheetCode"] = " ";
                this.dtblWorkingSheets.Rows.InsertAt(drowNew, 0);
            }
          
            this.ColumnWorkingSheetCode.DataSource = this.dtblWorkingSheets;
            this.ColumnWorkingSheetCode.ValueMember = "WorkingSheetID";
            this.ColumnWorkingSheetCode.DisplayMember = "WorkingSheetCode";

            this.dtblPrds = this.accPrds.GetDataProductForManufPrd ().Tables[0];
            this.ColumnPrdCode.DataSource = this.dtblPrds;
            this.ColumnPrdCode.ValueMember = "PrdID";
            this.ColumnPrdCode.DisplayMember = "PrdCode";

            this.ColumnPrdName.DataSource = this.dtblPrds;
            this.ColumnPrdName.ValueMember = "PrdID";
            this.ColumnPrdName.DisplayMember = "PrdName";

            this.ColumnPrdSpec.DataSource = this.dtblPrds;
            this.ColumnPrdSpec.ValueMember = "PrdID";
            this.ColumnPrdSpec.DisplayMember = "PrdSpec";

            this.ColumnModel.DataSource = this.dtblPrds;
            this.ColumnModel.ValueMember = "PrdID";
            this.ColumnModel.DisplayMember = "Model";

            this.ColumnUnitName.DataSource = this.dtblPrds;
            this.ColumnUnitName.ValueMember = "PrdID";
            this.ColumnUnitName.DisplayMember = "UnitName";

            this.ColumnManufProcessID.ValueMember = "ManufProcessID";
            this.ColumnManufProcessID.DisplayMember = "PrdStatus";

        }
     
   
       
        public void NewNote()
        {
          
            this.dtpDateNote.Value = DateTime.Now.Date;  
            this.rchMemo.Text = string.Empty;
            this.dtblItems = this.accItems.GetDataIntoStoreItemsByNoteID(-1).Tables[0];
            this.dgrdv.DataSource = this.dtblItems;
          
        }
        void dgrdv_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].DataPropertyName == "PrdID")
            {
                if (this.dgrdv[icol, irow].ReadOnly == true) return;
                this.dgrdv.CurrentCell = this.dgrdv[icol, irow];
                if (frmPrd == null)
                {
                    this.frmPrd = new JERPApp.Define.Product.FrmManufPrd();
                    new FrmStyle(this.frmPrd).SetPopFrmStyle(this);
                    this.frmPrd.AffterSelected += frmPrd_AffterSelected;
                }
                this.frmPrd.ShowDialog();
            }
        }
        void frmPrd_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            this.dgrdv.CurrentCell.Value = PrdID;
            this.frmPrd.Close();
        }


        void dgrdv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int icol = e.ColumnIndex; ;
            int irow = e.RowIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].DataPropertyName == "WorkingSheetID")
            {
                object objWorkingSheetID = this.dgrdv[icol, irow].Value;
                if (objWorkingSheetID != DBNull.Value)
                {
                    DataRow[] drows = this.dtblWorkingSheets.Select("WorkingSheetID=" + objWorkingSheetID.ToString());
                    if (drows.Length == 0) return;
                    foreach (DataGridViewColumn col in this.dgrdv.Columns)
                    {
                        if (col.DataPropertyName == "PrdID")
                        {
                            this.dgrdv[col.Name, irow].Value = drows[0]["PrdID"]; 
                        }
                    }

                }
            }
            if (this.dgrdv.Columns[icol].DataPropertyName == "PrdID")
            {
                object objPrdID = this.dgrdv[icol, irow].Value;
                if ((objPrdID != null) && (objPrdID != DBNull.Value))
                {
                    DataTable dtblProcess = this.accManufProcess.GetDataManufProcessByPrdID((int)objPrdID).Tables[0];
                    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)this.dgrdv[this.ColumnManufProcessID.Name, irow];
                    cmb.Value = DBNull.Value;
                    cmb.DataSource = dtblProcess;
                    if (dtblProcess.Rows.Count > 0)
                    {
                        cmb.Value = dtblProcess.Rows[0]["ManufProcessID"];
                    }
                }
            }


        }
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                bool flag = (grow.Cells[this.ColumnWorkingSheetCode.Name].Value != DBNull.Value);
                foreach (DataGridViewColumn col in this.dgrdv.Columns)
                {
                    if (col.DataPropertyName == "PrdID")
                    {
                        grow.Cells[col.Name].ReadOnly = flag;
                    }
                }
                grow.ErrorText = string.Empty; 
                if (grow.Cells[this.ColumnManufProcessID.Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "������Ϊ��";
                    continue;
                }  
                if (grow.Cells[this.ColumnQuantity.Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "��������Ϊ��";
                    continue;
                }
            }
        }


        bool ValidateData()
        {
            bool flag = true;
            DataRow[] drows = this.dtblItems.Select("", "", DataViewRowState.CurrentRows);
            if (drows.Length == 0)
            {
                MessageBox.Show("δ�����κ���ϸ��");
                return false;
            }
            drows = this.dtblItems.Select("ManufProcessID is null or Quantity is null", "", DataViewRowState.CurrentRows);
            if (drows.Length > 0)
            {
                MessageBox.Show("���ڹ������������!");
                return false;
            }
            return flag;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
             DialogResult rul = MessageBox.Show("�����б�������һ�����˲��ܱ������Ҫ������", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            string errormsg = string.Empty;
            bool flag = false;
            long NoteID = -1;
            string NoteCode = string.Empty;
            object objNoteID = DBNull.Value; 
            flag = this.accNotes.InsertIntoStoreNotes(ref errormsg, ref objNoteID,
                this.dtpDateNote.Value.Date,
                JERPBiz.Frame.UserBiz.PsnID,
                this.rchMemo.Text);
            if (!flag)
            {
                MessageBox.Show(errormsg);
                return;
            }           
            NoteID = (long)objNoteID; 
            foreach (DataRow drow in this.dtblItems.Rows )
            {
                if (drow.RowState == DataRowState.Deleted) continue; 
                this.accItems.InsertIntoStoreItems(ref errormsg,
                   NoteID,
                   drow["WorkingSheetID"],
                   drow["ManufProcessID"],
                   drow["Quantity"],
                   drow["Memo"]);   
                //����
                this.accStore.SaveStoreForIntoStore(ref errormsg,
                   drow["ManufProcessID"],
                   drow["Quantity"]);
               
            }
           
            MessageBox.Show("�ɹ����沢���˵�ǰ����");
            if (this.affterSave != null) this.affterSave();
            this.NewNote();              
         
        }

 

    }
}