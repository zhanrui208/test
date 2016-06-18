using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Define
{
    public partial class FrmManufProcessPrice : Form
    {
        public FrmManufProcessPrice()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accManufProcess = new JERPData.Manufacture.ManufProcess();
            this.SetPermit();
        }
        private JERPData.Manufacture.ManufProcess accManufProcess;
        private DataTable dtblManufProcess;
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(63);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(64);
            if (this.enableBrowse)
            {
                //��������
                LoadData();
                this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            }       
            this.dgrdv.ReadOnly = !enableSave;
            if (this.enableSave)
            {
                this.btnSave.Click += new EventHandler(btnSave_Click);
            }


        }

     

        private void LoadData()
        {
            this.dtblManufProcess = this.accManufProcess.GetDataManufProcess().Tables[0];
            this.dgrdv.DataSource = this.dtblManufProcess;
        }

        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                grow.ErrorText = (grow.Cells[this.ColumnProcessCostPrice.Name].Value == DBNull.Value) ? "δ��" : string.Empty;
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("���ڱ���䶯�ļ�¼����Ϊ�漰����Ʒ�ɱ����㣬��Ҫһ��ʱ��...");
            string errormsg=string.Empty ;
            foreach (DataRow drow in this.dtblManufProcess.Rows)
            {
                if (drow.RowState == DataRowState.Unchanged) continue;
                this.accManufProcess.UpdateManufProcessForProcessCostPrice(ref errormsg,
                    drow["ManufProcessID"],
                    drow["ProcessCostPrice"]);
                
            }
            this.dtblManufProcess.AcceptChanges();
            FrmMsg.Hide();
            MessageBox.Show("�ɹ�����������");
        }
    }
}