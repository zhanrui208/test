using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Supply.Product
{
    public partial class FrmCostPrice: Form
    {
        public FrmCostPrice()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accPrds = new JERPData.Product.Product();
            this.SetPermit();
        }
        private JERPData.Product.Product  accPrds;
        private DataTable dtblPrds;
        private JCommon.FrmExcelImport frmImport;
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(389);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(390);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
                this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            }       
            this.dgrdv.ReadOnly = !enableSave;
            if (this.enableSave)
            {
                this.btnImport.Click += new EventHandler(btnImport_Click);
                this.btnSave.Click += new EventHandler(btnSave_Click);
            }


        }

     

        private void LoadData()
        {
            this.dtblPrds = this.accPrds.GetDataProductForFinishedPrd().Tables[0];
            this.dgrdv.DataSource = this.dtblPrds;
        }

        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                grow.ErrorText = (grow.Cells[this.ColumnCostPrice.Name].Value == DBNull.Value) ? "δ��" : string.Empty;
            }
        }
        void btnImport_Click(object sender, EventArgs e)
        {
            if (frmImport == null)
            {
                frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                DataColumn[] columns = new DataColumn[]{    
                    new DataColumn ("��Ʒ���",typeof (string)), 
                    new DataColumn ("��Ʒ����",typeof (string)), 
                    new DataColumn ("��Ʒ���",typeof (string)),  
                    new DataColumn ("����",typeof (string)),  
                    new DataColumn ("�ɱ���",typeof (decimal)) 
                 };
                frmImport.SetImportColumn(columns, "��Ʒ��Ų���Ϊ��,�µ���ĳɱ��۽��Ḳ��֮ǰ�ĵ���");
                frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                frmImport.AffterSave += new JCommon.FrmExcelImport.AffterSaveDelegate(frmImport_AffterSave);
            }
            frmImport.ShowDialog();
        }
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = "�ɹ�";
            DataRow[] drowPrds = this.dtblPrds .Select("PrdCode='" + drow["��Ʒ���"].ToString() + "'");         
            if (drowPrds.Length == 0)
            {
                flag = false;
                msg = "����˲�Ʒ���";
                return;
            }
            else
            {
                drowPrds[0]["CostPrice"] = drow["�ɱ���"];
            }
        }
        void frmImport_AffterSave()
        {
            MessageBox.Show("������ɣ���������");
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("���ڱ���䶯�ļ�¼����Ϊ�漰����Ʒ�ɱ����㣬��Ҫһ��ʱ��...");
            string errormsg=string.Empty ;
            foreach (DataRow drow in this.dtblPrds.Rows)
            {
                if (drow.RowState == DataRowState.Unchanged) continue;
                this.accPrds.UpdateProductForCostPrice (ref errormsg,
                    drow["PrdID"],
                    drow["CostPrice"]);
                
            }
            this.dtblPrds.AcceptChanges();
            FrmMsg.Hide();
        }
    }
}