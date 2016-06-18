using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer
{
    public partial class FrmPrdSetOper : Form
    {
        public FrmPrdSetOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accBOM = new JERPData.Product.PrdSet ();
            this.prdEntity = new JERPBiz.Product.ProductEntity();
            this.accPrds = new JERPData.Product.Product();
            foreach (DataGridViewColumn col in this.dgrdv.Columns)
            {
                col.ReadOnly = true;
            }
            this.ColumnAssemblyQty.ReadOnly = false;
             this.dgrdv.CellContextMenuStripNeeded +=dgrdv_CellContextMenuStripNeeded;
            this.btnImport.Click += new EventHandler(btnImport_Click);
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

      
        private JERPBiz.Product.ProductEntity prdEntity;
        private JERPData.Product.Product accPrds;
        private JERPData.Product.PrdSet  accBOM; 
        private JCommon.FrmExcelImport frmImport;
        private JERPApp.Define.Product.FrmProduct frmPrd;
        private JERPApp.Define.Product.FrmProduct frmPrdAlter;     
        private DataTable dtblBom;
        public delegate void AffterSaveDelegate(int SetPrdCount);
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
        private int prdid = -1;
        private   int PrdID
        {
            get
            {
                return this.prdid;
            }
            set
            {
                this.prdid = value;
                this.btnImport.Enabled = (value > -1);
                this.btnAdd.Enabled = (value > -1);   

            }
        }
      
        private void LoadBom()
        {
          
            this.dtblBom = this.accBOM.GetDataPrdSetBySetPrdID (this.PrdID).Tables[0];
            this.dtblBom.Columns["AssemblyQty"].DefaultValue = 1; 
            this.dgrdv.DataSource = this.dtblBom;
        }    
        public void PrdSetBomOper(int PrdID)
        {           
            this.PrdID = PrdID;
            this.LoadBom();
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadBom();
        }
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {               
                grow.ErrorText = string.Empty;
                if (grow.Cells[this.ColumnPrdID.Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "���ϲ���Ϊ��";
                    continue;
                }
                int PrdID = (int)grow.Cells[this.ColumnPrdID.Name].Value;
                if (PrdID ==-1)
                {
                    grow.ErrorText = "�����ڴ�����";
                    continue;
                }    
                if (grow.Cells[this.ColumnAssemblyQty .Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "��������Ϊ��";
                    continue;
                }
                
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.frmPrd  == null)
            {
                this.frmPrd = new JERPApp.Define.Product.FrmProduct();
                this.frmPrd.AllowAppendFlag = true;
                new FrmStyle(this.frmPrd).SetPopFrmStyle(this);
                this.frmPrd .AffterSelected+=new JERPApp.Define.Product.FrmProduct.AffterSelectedDelegate(frmPrd_AffterSelected);
            }
            this.frmPrd.ShowDialog(); 
        }
        void frmPrd_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            if (this.dtblBom.Select("PrdID=" + PrdID.ToString(), "", DataViewRowState.CurrentRows).Length > 0)
            {
                return;
            }
            DataRow drowNew = this.dtblBom.NewRow(); 
            drowNew["PrdID"] = drow["PrdID"];
            drowNew["PrdCode"] = drow["PrdCode"];
            drowNew["PrdName"] = drow["PrdName"];
            drowNew["PrdSpec"] = drow["PrdSpec"];
            drowNew["Model"] = drow["Model"];
            drowNew["Manufacturer"] = drow["Manufacturer"];
            drowNew["UnitName"] = drow["UnitName"];
            drowNew["AssemblyQty"] = 1; 
            this.dtblBom.Rows.Add(drowNew); 
        }
     
     
         void dgrdv_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return; 
            if ((this.dgrdv.Columns[icol].DataPropertyName == "PrdCode")
                ||(this.dgrdv.Columns[icol].DataPropertyName == "PrdName")
                 ||(this.dgrdv.Columns[icol].DataPropertyName == "PrdSpec")
                 ||(this.dgrdv.Columns[icol].DataPropertyName == "Model")
                 ||(this.dgrdv.Columns[icol].DataPropertyName == "Manufacturer")
                )
            {
                this.dgrdv.CurrentCell = this.dgrdv[icol, irow];
                if (this.frmPrdAlter  == null)
                {
                    this.frmPrdAlter = new JERPApp.Define.Product.FrmProduct();
                    this.frmPrdAlter.AllowAppendFlag = true;
                    new FrmStyle(this.frmPrdAlter).SetPopFrmStyle(this);
                    this.frmPrdAlter.AffterSelected += new JERPApp.Define.Product.FrmProduct.AffterSelectedDelegate(frmPrdAlter_AffterSelected);
                }
                this.frmPrdAlter .ShowDialog();
            }
        }
    
        void frmPrdAlter_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            if (this.dtblBom.Select("PrdID=" + PrdID.ToString(),"",DataViewRowState.CurrentRows ).Length>0)
            {
                return;
            }
           DataGridViewRow grow = this.dgrdv.CurrentRow;
           grow.Cells [this.ColumnPrdID .Name ].Value  =PrdID;
           grow.Cells[this.ColumnPrdCode.Name].Value = drow["PrdCode"];
           grow.Cells[this.ColumnPrdName.Name].Value = drow["PrdName"];
           grow.Cells[this.ColumnPrdSpec.Name].Value = drow["PrdSpec"];
           grow.Cells[this.ColumnModel.Name].Value = drow["Model"];
           grow.Cells[this.ColumnManufacturer.Name].Value = drow["Manufacturer"];
           grow.Cells[this.ColumnUnitName .Name].Value = drow["UnitName"]; 
           this.frmPrdAlter.Close();
        }
      
        void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblBom.DefaultView[irow].Row;
            if (drow["ID"] == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accBOM.DeletePrdSet(ref errormsg, drow["ID"]);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private int GetPrdID(string PrdCode,string PrdName,string PrdSpec)
        {
            int PrdID = -1;
            this.accPrds.GetParmProductPrdID(PrdCode, ref PrdID);
            if (PrdID > -1) return PrdID;
            this.accPrds.GetParmProductPrdIDFromOtherInfor(PrdName, PrdSpec, ref PrdID);
            return PrdID;
        }
       
       
        void btnImport_Click(object sender, EventArgs e)
        {            
            string errormsg = string.Empty;        
            if (this.frmImport == null)
            {
                frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this.ParentForm);
                DataColumn[] columns = new DataColumn[]{     
                    new DataColumn ("���ϱ��",typeof (string)), 
                    new DataColumn ("��������",typeof (string)), 
                    new DataColumn ("���Ϲ��",typeof (string)), 
                    new DataColumn ("����",typeof (decimal)),       
                    new DataColumn ("��λ",typeof (string))           
                };

                frmImport.SetImportColumn(columns, "[���ϱ��]�����Ƽ������Ϊ�� \n[����]�ǿ�����;�����%" +
                    "\n��Դ��ʽ:����;�ɹ�;�͹�;��Ӧ���Ա�;�̶�:�ǻ��,�մ�����");
                frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                frmImport.AffterSave += new JCommon.FrmExcelImport.AffterSaveDelegate(frmImport_AffterSave);
            }
            frmImport.ShowDialog();
        }
        void frmImport_AffterSave()
        {
            MessageBox.Show("������ɣ�����������û�г�����У��ٵ㱣��!");
            this.frmImport.Close();
        }
     
  
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = string.Empty;
            string errormsg = string.Empty; 
            int PrdID = this.GetPrdID(drow["���ϱ��"].ToString(),drow["��������"].ToString (),drow["���Ϲ��"].ToString ()) ;            
            decimal AssemblyQty = (decimal)drow["����"];
            object objID = DBNull.Value;
            if (PrdID > -1)
            {
                if (this.dtblBom.Select("PrdID=" + PrdID.ToString()).Length > 0)
                {
                    errormsg = "�����ظ�";
                    flag = false;
                }
            }
            DataRow drowNew = this.dtblBom.NewRow(); 
            drowNew["PrdID"] = PrdID; 
            drowNew["PrdCode"] = drow["���ϱ��"];
            drowNew["PrdName"] = drow["��������"];
            drowNew["PrdSpec"] = drow["���Ϲ��"];
            this.prdEntity.LoadData(PrdID);
            drowNew["Model"] = this.prdEntity.Model ;
            drowNew["Manufacturer"] = this.prdEntity.Manufacturer;
            drowNew["UnitName"] = this.prdEntity.UnitName ;
            drowNew["AssemblyQty"] = AssemblyQty;  
            this.dtblBom.Rows.Add(drowNew);
          
        }
        private bool ValidateData()
        {

            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                if (grow.ErrorText != string.Empty)
                {
                    MessageBox.Show("�����嵥��ϸ����,���ܱ����嵥");
                    return false;

                }
            }
            return true;
        }
       
        void btnSave_Click(object sender, EventArgs e)
        {
                      
            if (this.PrdID == -1) return;
            if (this.ValidateData() == false) return;
            string errormsg = string.Empty;
            bool flag = false;
            foreach (DataRow drow in this.dtblBom.Rows)            
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                if (drow["PrdID"] == DBNull.Value) continue;
                if (drow["ID"] == DBNull.Value)
                {
                    object objID = DBNull.Value;
                    flag = this.accBOM.InsertPrdSet(ref errormsg,
                        ref objID,
                        this.PrdID, 
                        drow["PrdID"],
                        drow["AssemblyQty"]);
                    if (flag)
                    {
                        drow["ID"] = objID;                       
                    }
                }
                else
                {
                    
                    flag = this.accBOM.UpdatePrdSet(
                        ref errormsg,
                        drow["ID"], 
                        drow["PrdID"],
                        drow["AssemblyQty"]); 
                }
                drow.AcceptChanges();
             
            }
            this.accPrds.UpdateProductForPrdSetCount(ref errormsg, this.PrdID);
            if (this.affterSave != null) this.affterSave(this.dtblBom.Rows.Count);
            MessageBox.Show("�ɹ����浱ǰ��������");
        } 
    }
}