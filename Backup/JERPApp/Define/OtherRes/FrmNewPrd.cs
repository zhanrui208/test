using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Define.OtherRes
{
    public partial class FrmPrdNew : Form
    {
        public FrmPrdNew()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accPrds = new JERPData.OtherRes .Product ();
            this.accUnits = new JERPData.General.Unit();  
            this.SetColumnSrc();  
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnImport.Click += new EventHandler(btnImport_Click); 
            this.dgrdv.RowEnter += new DataGridViewCellEventHandler(dgrdv_RowEnter);
             
        }

     
        private JERPData.OtherRes  .Product  accPrds;
        private JERPData.General.Unit accUnits;  
        private DataTable dtblProduct,dtblUnits ;
        private JCommon.FrmExcelImport frmImport; 
        void dgrdv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow <1) || (icol == -1)) return;
            object objUnitID = this.dgrdv[this.ColumnUnitID.Name, irow].Value;
            if ((objUnitID == null) || (objUnitID == DBNull.Value))
            {
                this.dgrdv[this.ColumnUnitID.Name, irow].Value = this.dgrdv[this.ColumnUnitID.Name, irow - 1].Value;
            }
           
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
        private void SetColumnSrc()
        { 
           
            this.dtblUnits = this.accUnits.GetDataUnit().Tables[0];
            this.ColumnUnitID.DataSource = this.dtblUnits;
            this.ColumnUnitID.ValueMember = "UnitID";
            this.ColumnUnitID.DisplayMember = "UnitName";

        } 
        public  void NewPrd()
        { 
            this.dtblProduct = this.accPrds.GetDataProductByPrdID (this.ctrlPrdTypeID .PrdTypeID).Tables[0];
            this.dtblProduct.Columns["UnitID"].DefaultValue = 1; 
            this.dgrdv.DataSource = this.dtblProduct; 
        }
      
      
       
        void btnImport_Click(object sender, EventArgs e)
        {
           
            if (this.frmImport == null)
            {
                this.frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                this.frmImport.AffterSave += new JCommon.FrmExcelImport.AffterSaveDelegate(frmImport_AffterSave);
                this.frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                DataColumn[] columns=new DataColumn[] {      
                
                    new DataColumn ("��Ʒ����",typeof (string)),  
                    new DataColumn ("�ͺż����",typeof (string)),         
                    new DataColumn ("��λ",typeof (string)),       
                    new DataColumn ("��Ӧ��",typeof (string)),                    
                    new DataColumn ("�ɱ���",typeof (decimal)),
                    new DataColumn ("��С��װ",typeof (decimal)) 
                };
                this.frmImport.SetImportColumn(columns, "��Ʒ���Ʋ���Ϊ�գ���λ����Ĭ��ΪPCS");
            }
            this.frmImport.ShowDialog();

        }
        bool GetBool(string BoolInfor)
        {
            return (BoolInfor == "��");
        }
        int GetUnitID(string UnitName)
        {
            int UnitID=1;
            this.accUnits.GetParmUnitUnitID(ref UnitID, UnitName);
            return UnitID;
        }      
        
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = "�ɹ�!";
            object objPrdID=DBNull .Value ;
            string UnitName="PCS";
            if (drow["��λ"]!=DBNull .Value )
            {
                UnitName = drow["��λ"].ToString ();
            }
              
            ; 
            DataRow drowNew = this.dtblProduct.NewRow();

            drowNew["PrdName"] = drow["��Ʒ����"]; 
            drowNew["PrdSpec"] = drow["�ͺż����"]; 
            drowNew["MinPackingQty"] = drow["��С��װ"];
            drowNew["Supplier"] = drow["��Ӧ��"];
            drowNew["CostPrice"] = drow["�ɱ���"]; 
            drowNew["UnitID"] = this.GetUnitID(UnitName); 
            this.dtblProduct.Rows.Add(drowNew);
        }

        void frmImport_AffterSave()
        {
            this.SetColumnSrc();
        }
        private bool ValidateData()
        { 
            DataRow[] drows = this.dtblProduct.Select("PrdName is null", "");
            if (drows.Length > 0)
            {
                MessageBox.Show("��Ʒ���Ʋ���Ϊ��");
                return false;
            }
            return true;
        }
    
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("����������ǰ��Ʒ,�����ڲ�Ʒ����򹤳���������б����", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            string errormsg = string.Empty;
            bool flag = false;
            foreach (DataRow drow in this.dtblProduct.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                
                object objPrdID = DBNull.Value;
                flag = this.accPrds.InsertProduct (
                        ref errormsg,
                        ref objPrdID,
                        this.ctrlPrdTypeID .PrdTypeID, 
                        drow["PrdName"],
                        drow["PrdSpec"],
                        drow["AssistantCode"],
                        drow["UnitID"],
                        drow["CostPrice"],
                        drow["MinPackingQty"],
                        drow["Supplier"]);
                if (!flag)
                {
                    drow.RowError = errormsg;
                }
            }
            this.dgrdv.Refresh();
            MessageBox.Show("�ɹ������˵�ǰ���ĺĲ�");
            if (this.affterSave != null) this.affterSave();
           
        }

    }
}