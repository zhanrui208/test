using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Define
{
    public partial class FrmInvoiceType : Form
    {
        private DataTable dtblInvoiceType;
        private JERPData.Finance .InvoiceType accInvoiceType;
        public FrmInvoiceType()
        {
            InitializeComponent();
            this.accInvoiceType = new JERPData.Finance .InvoiceType();
            this.dgrdv.AutoGenerateColumns = false;
            this.SetPermit();

        }
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(725);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(726);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
            }
            this.dgrdv.AllowUserToAddRows = enableSave;
            this.dgrdv.AllowUserToDeleteRows = enableSave;
            this.dgrdv.ReadOnly = !enableSave;
            if (this.enableSave)
            {
                this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);

            }


        }
       
        private void LoadData()
        {
            this.dtblInvoiceType = this.accInvoiceType.GetDataInvoiceType().Tables[0];           
            this.dtblInvoiceType.Columns["InvoiceTypeCode"].AllowDBNull = false;
            this.dtblInvoiceType.Columns["InvoiceTypeCode"].Unique  = true;
            this.dtblInvoiceType.Columns["InvoiceTypeName"].AllowDBNull = false;
            this.dtblInvoiceType.Columns["InvoiceTypeName"].Unique = true;
            this.dgrdv.DataSource = this.dtblInvoiceType;
        }

 

        void dgrdv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
          
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            bool flag = false;
            DataRow drow = null;
            try
            {
                drow = this.dtblInvoiceType.DefaultView[irow].Row;
            }
            catch 
            {
                return;
            }
            if (drow == null) return;
            if (drow.RowState == DataRowState.Unchanged) return;
            string errormsg = string.Empty;
            if (drow["InvoiceTypeID"] == DBNull.Value)
            {
                object objInvoiceTypeID =DBNull .Value ;
                flag = this.accInvoiceType.InsertInvoiceType(ref errormsg, ref objInvoiceTypeID,
                        drow["InvoiceTypeCode"],
                        drow["InvoiceTypeName"]);
                if (flag)
                {
                    drow["InvoiceTypeID"] = objInvoiceTypeID;
                    if(this.affterSave !=null) this.affterSave ();
                }
                else
                {
                    MessageBox.Show(errormsg);
                    this.LoadData();
                }
            }
            else
            {
                flag=this.accInvoiceType.UpdateInvoiceType(ref errormsg,
                        drow["InvoiceTypeID"],
                        drow["InvoiceTypeCode"],
                        drow["InvoiceTypeName"]);
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

        private void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblInvoiceType  .DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["InvoiceTypeID"] == DBNull.Value)
            {
                e.Cancel = true;
                return;
            }

            int  InvoiceTypeID = (int)drow["InvoiceTypeID"];
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accInvoiceType.DeleteInvoiceType(ref ErrorMsg,
                    InvoiceTypeID);
                if (flag)
                {
                    if (this.affterSave != null)
                    {
                        this.affterSave();
                    }
                }
                else
                {
                   MessageBox .Show ("�˼�¼�ѱ�����ҵ�����ã����ܴ����ݿ���ɾ���˵�λ");
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
    }
}