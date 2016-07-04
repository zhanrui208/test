using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Test
{
    public partial class FrmOtherInStoreOper : Form
    {
        private JERPData.Product.StoreNotesOther accNotes;
        private JERPData.Product.StoreItemsOther accItems;

        private JERPData.Material.Store accMtrStore;
        private JERPData.Material.BranchStore accBranchStore;
        private JERPBiz.OtherRes.BuyReturnNotePrintHelper PrintHelper;

        private JERPApp.Define.Product.FrmProduct frmAddPrd;

        private AffterSaveDelegate affterSave;
        public  delegate void AffterSaveDelegate();

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

         private DataTable dtblItems,dtblBranchStore;

        public FrmOtherInStoreOper()
        {
            InitializeComponent();

            this.accNotes = new JERPData.Product.StoreNotesOther();
            this.accItems = new JERPData.Product.StoreItemsOther();
            this.mdgrdv.AutoGenerateColumns = false;
            this.accMtrStore = new JERPData.Material.Store();
            this.PrintHelper = new JERPBiz.OtherRes.BuyReturnNotePrintHelper();
            this.accBranchStore = new JERPData.Material.BranchStore();
            this.SetColumnSrc();
            //this.dgrdv.CellValueChanged += new DataGridViewCellEventHandler(dgrdv_CellValueChanged);
            //this.dgrdv.CellContextMenuStripNeeded += new DataGridViewCellContextMenuStripNeededEventHandler(dgrdv_CellContextMenuStripNeeded);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnNew.Click += new EventHandler(btnNew_Click);
            this.btnPrd.Click += new EventHandler(btnPrd_Click);


        }

        private void SetColumnSrc()
        {
            this.dtblBranchStore  = this.accBranchStore .GetDataBranchStore().Tables[0];
            this.ColumnBranchStoreID.DataSource = this.dtblBranchStore;
            this.ColumnBranchStoreID.ValueMember = "BranchStoreID";
            this.ColumnBranchStoreID.DisplayMember = "BranchStoreName";
        }

        private void btnPrd_Click(object sender, EventArgs e)
        {

            //this.ctrlUserPsn1.Enabled = false;
            //this.ctrlSupplier2.Enabled = false;
            if (frmAddPrd == null)
            {
                this.frmAddPrd = new JERPApp.Define.Product.FrmProduct();
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
            drowNew["UnitName"] = drow["UnitName"];
            drowNew["IntoQty"] = 0;
            this.dtblItems.Rows.Add(drowNew);

            //int PrdID = (int)drow["PrdID"];
            //DataGridViewRow grow = this.dgrdv.CurrentRow;
            //grow.Cells[this.ColumnPrdID.Name].Value = PrdID;
            //grow.Cells[this.ColumnPrdCode.Name].Value = drow["PrdCode"];
            //grow.Cells[this.ColumnPrdName.Name].Value = drow["PrdName"];
            //grow.Cells[this.ColumnPrdSpec.Name].Value = drow["PrdSpec"];
            //grow.Cells[this.ColumnModel.Name].Value = drow["Model"];
            //grow.Cells[this.ColumnManufacturer.Name].Value = drow["Manufacturer"];
            //grow.Cells[this.ColumnUnitName.Name].Value = drow["UnitName"];

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("即将进行入库, 一经确认不能变更！", "保存确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                bool flag = false;
                string errormsg = string.Empty;
                object objNoteID = DBNull.Value;
                object objNoteCode = DBNull.Value;
                object objTotalAMT = DBNull.Value;
             
                flag = this.accNotes.InsertIntoStoreNotesOther(ref errormsg, ref objNoteID,
                    ref objNoteCode,
                    dtpDateNote.Value.Date,
                    objNoteCode,
                    JERPBiz.Frame.UserBiz.PsnID,
                    this.ctrlUserPsn1.PsnID,
                    this.rchMemo.Text);
                if (!flag)
                {
                    MessageBox.Show(errormsg);
                    return;
                }
                long NoteID = (long)objNoteID;
                String NoteCode = objNoteCode.ToString();
                this.txtNoteCode.Text = NoteCode;
                foreach (DataRow drow in this.dtblItems.Rows)
                {
                    if (drow.RowState == DataRowState.Deleted) continue;
                    object objItemID = DBNull.Value;
                    byte ValidFlag = 0;
                    int PrdID = (int)drow["PrdID"];
                    int BranchStoreID = (int)drow["BranchStoreID"];
                    decimal Quantity = (decimal)drow["Quantity"];
                    decimal CostPrice = this.GetCostPrice(PrdID,BranchStoreID);
                    decimal InventoryQty = Quantity;
                    decimal InventoryPrice = CostPrice;
                    decimal InventoryAMT = InventoryQty * InventoryPrice;
                    String memo = (String)drow["ColMemo"];
                    flag = this.accItems.InsertStoreItemsOther(ref errormsg, NoteID,BranchStoreID,
                        ValidFlag,
                        PrdID ,
                        Quantity,
                        CostPrice,
                        InventoryQty,
                        InventoryPrice,
                        InventoryAMT,
                        memo);

                    if (flag)
                    {

                        //入库
                        this.accMtrStore.InsertStoreForIntoStore(
                            ref errormsg,
                            JERPBiz.Finance.NoteNameParm.MtrOutStoreNoteNameID,
                            NoteID,
                            NoteCode,
                            drow["PrdID"],
                            BranchStoreID,
                            drow["Quantity"],
                            CostPrice);
                    }
                }
                //打印相关
                //rul = MessageBox.Show("成功生成退货单, 需要立即将打印吗?", "打印确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (rul == DialogResult.Yes)
                //{
                //    this.PrintHelper.ExportToExcel(NoteID);
                //    this.accNotes.UpdateBuyReturnNotesForPrint(ref errormsg, NoteID, JERPBiz.Frame.UserBiz.PsnID);
                //} 
                if (this.affterSave != null) this.affterSave();
                this.NewNote();
            }

        }       

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewNote();
        }

        private bool ValidateData()
        {

            DataRow[] drows = this.dtblItems.Select("", "", DataViewRowState.CurrentRows);
            if (drows.Length == 0)
            {
                MessageBox.Show("没有任何记录,不能保存");
                return false;
            }
            //r入库不需要验证库存
            //drows = this.dtblItems.Select("Quantity>InventoryQty", "", DataViewRowState.CurrentRows);
            //if (drows.Length > 0)
            //{
            //    MessageBox.Show("对不起,存在库存不足记录,不能出货");
            //    return false;
            //}
            return true;
        }

        private decimal GetCostPrice(int PrdID, int BranchStoreID)
        {
            decimal rut = 0;
            //this.accMtrStore.GetParmStoreInventoryPrice(PrdID, this.ctrlMtrBranchStoreID.BranchStoreID, ref rut);
            this.accMtrStore.GetParmStoreInventoryPrice(PrdID, BranchStoreID, ref rut);//此处无用errD
            return rut;
        }
        public void NewNote()
        {
            this.ctrlUserPsn1.PsnID = -1;
            this.ctrlUserPsn1.Enabled = true;
            this.ctrlSupplier2.Enabled = true;
            this.dtpDateNote.Value = DateTime.Now.Date;
            this.txtNoteCode.Text = string.Empty;
            this.rchMemo.Text = string.Empty;
            //this.mdgrdv.Rows.Clear();

            this.dtblItems = this.accItems.GetDataStoreItemsOtherByNoteID(-1).Tables[0];
            this.mdgrdv.DataSource = this.dtblItems;
            this.dtblItems.Columns["PrdID"].AllowDBNull = false;
            //this.dtblItems.Columns["BranchStoreID"].AllowDBNull = false; //这里如果设为不能为BULL之的话，则 this.dtblItems.Rows.Add(drowNew);必须要加改字段的值
            //this.dtblItems.Columns.Add("InventoryQty", typeof(decimal));

        }
    }
}
