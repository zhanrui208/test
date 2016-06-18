using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Receivable.Templet
{
    public partial class FrmRepairInvoiceFormatSetting : Form
    {
        public FrmRepairInvoiceFormatSetting()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;           
            this.accFormat = new JERPData.Product.RepairInvoiceFormat();
            this.accXCustomer = new JERPData.Product.RepairInvoiceFormatXCustomer();
            this.SetPermit();
        }
        private JERPData.Product.RepairInvoiceFormat accFormat;
        private JERPData.Product.RepairInvoiceFormatXCustomer accXCustomer;
        private FrmRepairInvoiceFormat frmFormat;
        private DataTable dtblFormat, dtblXCustomer;
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����          
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(548);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(549);
            if (this.enableBrowse)
            {
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.LoadData();

            }
            this.btnSave.Enabled = this.enableSave;
            this.lnkFormat.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.btnSave.Click += new EventHandler(btnSave_Click);
                this.lnkFormat.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkFormat_LinkClicked);
            }
        }
        private void LoadData()
        {
            this.dtblFormat = this.accFormat.GetDataRepairInvoiceFormat().Tables[0];
            while (this.dgrdv.ColumnCount > 2)
            {
                this.dgrdv.Columns.RemoveAt(2);
            }
            DataGridViewCheckBoxColumn ckbcol;
            foreach (DataRow drow in dtblFormat.Rows)
            {
                ckbcol = new DataGridViewCheckBoxColumn();
                ckbcol.Width = 80;
                ckbcol.HeaderText = drow["TmpSheetName"].ToString();            
                ckbcol.DataPropertyName = drow["FormatID"].ToString();
                this.dgrdv.Columns.Add(ckbcol);
            }
            this.dtblXCustomer = this.accXCustomer.GetDataRepairInvoiceFormatXCustomer().Tables[0];
            this.dgrdv.DataSource = this.dtblXCustomer;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.ctrlCheckedAll.SeachGridView = this.dgrdv;
        }
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

     
        void lnkFormat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmFormat == null)
            {
                frmFormat = new FrmRepairInvoiceFormat();
                new FrmStyle(frmFormat).SetPopFrmStyle(this);
                frmFormat.AffterSave += this.LoadData;
            }
            frmFormat.ShowDialog();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string errormsg=string.Empty ;
            bool flag = false;
            FrmMsg.Show("���ڱ������ã����Ժ�....");
            foreach (DataRow drow in this.dtblXCustomer.Rows)
            {
                if (drow.RowState == DataRowState.Unchanged) continue;
                int CompanyID=(int)drow["CompanyID"];
                foreach (DataRow drowColumn in this.dtblFormat.Rows)
                {
                    int FormatID=(int)drowColumn["FormatID"];
                    if (drow[FormatID.ToString()] != DBNull.Value)
                    {                   
                        if ((bool)drow[FormatID.ToString()])
                        {
                            flag=this.accXCustomer.SaveRepairInvoiceFormatXCustomer(ref errormsg, FormatID, CompanyID);
                        }
                        else
                        {
                            flag=this.accXCustomer.DeleteRepairInvoiceFormatXCustomer(ref errormsg, FormatID, CompanyID);
                        }
                        if (!flag)
                        {
                            MessageBox.Show(errormsg);
                        }
                    }
                }
            }
            FrmMsg.Hide();
            MessageBox.Show("�ɹ����浱ǰ���֮����");
        }
       
     
    }
}