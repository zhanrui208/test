using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmAssistantCode : Form
    {
        public FrmAssistantCode()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accPrds = new JERPData.Product.Product();
            this.SetPermit();
        }
        private JERPData.Product.Product accPrds;
        private DataTable dtblPrds;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(467);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(468);
            if (this.enableBrowse)
            {
             
                this.LoadData();
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.ctrlQFind.BeforeFilter += this.LoadData;
               
            }
            this.btnSave.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.btnSave .Click +=new EventHandler(btnSave_Click);
            }
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        private void LoadData()
        {
            this.dtblPrds = this.accPrds.GetDataProductForFinishedPrd().Tables[0];
            this.dgrdv.DataSource = this.dtblPrds;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            string errormsg=string.Empty ;
            foreach (DataRow drow in this.dtblPrds.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                this.accPrds.UpdateProductForAssistantCode(ref errormsg,
                    drow["PrdID"],
                    drow["AssistantCode"]);
            }
            MessageBox.Show("�ɹ����ò�Ʒ������");
        }
    }
}