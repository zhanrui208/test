using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Define.Product
{
    public partial class FrmPrdNew : Form
    {
        public FrmPrdNew()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accPrds = new JERPData.Product.Product ();
            this.accUnits = new JERPData.General.Unit();  
            this.SetColumnSrc();  
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnImport.Click += new EventHandler(btnImport_Click); 
            this.dgrdv.RowEnter += new DataGridViewCellEventHandler(dgrdv_RowEnter);
             
        }

     
        private JERPData.Product .Product  accPrds;
        private JERPData.General.Unit accUnits;  
        private DataTable dtblProduct,dtblUnits;
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
            this.ctrlPrdTypeID.PrdTypeID = -1;
            this.dtblProduct = this.accPrds.GetDataProductByPrdID (-1).Tables[0];
            this.dtblProduct.Columns["UnitID"].DefaultValue = 1;  
            this.dtblProduct.Columns["SaleFlag"].DefaultValue = false;
            this.dtblProduct.Columns["StopFlag"].DefaultValue = false ;
            this.dgrdv.DataSource = this.dtblProduct; 
        }
      
        private int GetPrdID(string PrdCode)
        {
            int rut = -1;
            this.accPrds.GetParmProductPrdID(PrdCode, ref rut);
            return rut; 
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
                    new DataColumn ("��Ʒ���",typeof (string)),
                    new DataColumn ("��Ʒ����",typeof (string)),             
                    new DataColumn ("��Ʒ���",typeof (string)),                 
                    new DataColumn ("�ͺ�",typeof (string)),                
                    new DataColumn ("��װ/���洦��",typeof (string)),        
                    new DataColumn ("Ʒ��",typeof (string)),    
                    new DataColumn ("��λ",typeof (string)),      
                    new DataColumn ("����",typeof (string)),                              
                    new DataColumn ("ͼ��",typeof (string)),                    
                    new DataColumn ("������",typeof (string)),   
                    new DataColumn ("��Ӧ��",typeof (string)),                    
                    new DataColumn ("�ɱ���",typeof (decimal)),
                    new DataColumn ("��С��װ",typeof (decimal)),
                    new DataColumn ("��ע",typeof (string)),
                    new DataColumn ("ͣ��",typeof (string)),                                
                    new DataColumn ("��˰��",typeof (string)),                                      
                    new DataColumn ("Rohs�ϸ�",typeof (string)),                                      
                    new DataColumn ("RohsҪ��",typeof (string)),   
                    new DataColumn ("����",typeof (decimal))      
                };
                this.frmImport.SetImportColumn(columns, "��Ʒ��Ų���Ϊ�գ���λ����Ĭ��ΪPCS,��˰��,Rohs���ǻ���");
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
            int PrdID = this.GetPrdID(drow["��Ʒ���"].ToString());
            if (PrdID > -1)
            {
                flag = false;
                msg = "�Ѵ��ڴ˱��";
                return;
            } 
            bool SaleFlag = this.GetBool(drow["����"].ToString());
            bool TaxfreeFlag = this.GetBool(drow["��˰��"].ToString());
            bool RohsFlag = this.GetBool(drow["Rohs�ϸ�"].ToString());
            bool RohsRequireFlag = this.GetBool(drow["RohsҪ��"].ToString());
            bool StopFlag = this.GetBool(drow["ͣ��"].ToString());  
            DataRow drowNew = this.dtblProduct.NewRow();            
            drowNew["PrdCode"] = drow["��Ʒ���"];
            drowNew["PrdName"] = drow["��Ʒ����"];
            drowNew["PrdSpec"] = drow["��Ʒ���"];
            drowNew["Model"] = drow["�ͺ�"];
            drowNew["Surface"] = drow["��װ/���洦��"]; 
            drowNew["Manufacturer"] = drow["Ʒ��"];
            drowNew["DWGNo"] = drow["ͼ��"];
            drowNew["AssistantCode"] = drow["������"];
            drowNew["MinPackingQty"] = drow["��С��װ"];
            drowNew["Supplier"] = drow["��Ӧ��"];
            drowNew["CostPrice"] = drow["�ɱ���"];
            drowNew["PrdWeight"] = drow["����"]; 
            drowNew["SaleFlag"] = SaleFlag;
            drowNew["TaxfreeFlag"] = TaxfreeFlag;
            drowNew["RohsFlag"] = RohsFlag;
            drowNew["RohsRequireFlag"] = RohsRequireFlag;
            drowNew["UnitID"] = this.GetUnitID(UnitName);
            drowNew["Memo"] = drow["��ע"];
            drowNew["StopFlag"] = StopFlag;
            this.dtblProduct.Rows.Add(drowNew);
        }

        void frmImport_AffterSave()
        {
            this.SetColumnSrc();
        }
        private bool ValidateData()
        {
            if (this.ctrlPrdTypeID.PrdTypeID == -1)
            {
                MessageBox.Show("��Ʒ���Ͳ���Ϊ��");
                return false;
            }
            DataRow[] drows = this.dtblProduct.Select("PrdCode is null", "");
            if (drows.Length > 0)
            {
                MessageBox.Show("��Ʒ��Ų���Ϊ��");
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
                int oldprdid = this.GetPrdID(drow["PrdCode"].ToString());
                if (oldprdid > -1)
                {
                    drow.RowError = "�Բ��𣬴˱���Ѵ���";
                    return;
                }
                object objPrdID = DBNull.Value;
                flag = this.accPrds.InsertProductForImport(
                        ref errormsg,
                        ref objPrdID,
                        this.ctrlPrdTypeID .PrdTypeID  ,
                        drow["PrdCode"],
                        drow["PrdName"],
                        drow["PrdSpec"],
                        drow["Model"], 
                        drow["Surface"], 
                        drow["Manufacturer"],
                        drow["AssistantCode"],
                        drow["DWGNo"],
                        drow["TaxfreeFlag"],
                        drow["RohsFlag"],
                        drow["RohsRequireFlag"],
                        drow["PrdWeight"],
                        drow["SaleFlag"],
                        drow["UnitID"], 
                        drow["MinPackingQty"], 
                        drow["URL"],
                        drow["Memo"],
                        drow["StopFlag"]);
                if (!flag)
                {
                    drow.RowError = errormsg;
                }
            }
            this.dgrdv.Refresh();
            MessageBox.Show("�ɹ������˵�ǰ���Ĳ�Ʒ");
            if (this.affterSave != null) this.affterSave();
           
        }

    }
}