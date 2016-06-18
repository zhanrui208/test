using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable.Templet
{
    public partial class FrmSaleReconciliationField : Form
    {
        private DataTable dtblFields;
        private JERPData.Product.SaleReconciliationField accField;
        public FrmSaleReconciliationField()
        {
            InitializeComponent();
            this.accField = new JERPData.Product.SaleReconciliationField();
            this.dgrdv.AutoGenerateColumns = false;
            this.SetPermit();
        }
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����          

        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(260);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(261);
            if (this.enableBrowse)
            {
                this.LoadData();
            }
            this.dgrdv.ReadOnly = !this.enableSave;
            this.dgrdv.AllowUserToAddRows = this.enableSave;
            this.dgrdv.AllowUserToDeleteRows = this.enableSave;
            if (this.enableSave)
            {
                this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            }
        }

        
        private void LoadData()
        {           
            this.dtblFields = this.accField .GetDataSaleReconciliationField().Tables[0];
            this.dtblFields.Columns["FieldName"].AllowDBNull = false;
            this.dtblFields.Columns["FieldName"].Unique  = true;
            this.dtblFields.Columns["FieldCaption"].AllowDBNull = false;
            this.dtblFields.Columns["FieldCaption"].Unique = true;          
            this.dgrdv.DataSource = this.dtblFields;
        }  
        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblFields  .DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["FieldID"] == DBNull.Value)
            {
             
                return;
            }
            int FieldID = (int)drow["FieldID"];
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accField.DeleteSaleReconciliationField   (ref ErrorMsg,
                    FieldID);
                if (flag)
                {
                    if (this.affterSave != null)
                    {
                        this.affterSave();
                    }
                }
                else
                {
                    MessageBox.Show(ErrorMsg);
                }
            }
            else
            {
                e.Cancel = true;
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
        void dgrdv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            DataRow drow = null;
            try
            {
                drow = this.dtblFields.DefaultView[irow].Row;
            }
            catch
            {
                return;
            }
            bool flag = false;
            string errormsg = string.Empty;
            if (drow["FieldID"] == DBNull.Value)
            {
                object objFieldID = DBNull.Value;
                flag = this.accField.InsertSaleReconciliationField (ref errormsg, ref objFieldID,
                        drow["FieldName"],
                        drow["FieldCaption"]);
                if (flag)
                {
                    drow["FieldID"] = objFieldID;
                    if (this.affterSave != null) this.affterSave();
                }
                else
                {
                    MessageBox.Show(errormsg);
                    this.LoadData();
                }
            }
            else
            {
                flag = this.accField.UpdateSaleReconciliationField(
                    ref errormsg,
                    drow["FieldID"],
                    drow["FieldName"],
                    drow["FieldCaption"]);
                if (flag)
                {
                    if (this.affterSave != null) this.affterSave();
                }
                else
                {
                    MessageBox.Show(errormsg);
                    this.LoadData();
                }
            }
            
        }

           
    }
}