using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer
{
    public partial class FrmPrdDelete : Form
    {
        public FrmPrdDelete()
        {
            InitializeComponent();
            this.accPrds = new JERPData.Product.Product();
            this.accPackingType = new JERPData.Product.PackingType();
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.dgrdvSet.AutoGenerateColumns = false;
            this.dgrdvManuf.AutoGenerateColumns = false;
            this.dgrdvPackage.AutoGenerateColumns = false;
            this.dgrdvPackingType.AutoGenerateColumns = false;
            this.dgrdvManuf.ContextMenuStrip = this.cMenu;
            this.dgrdvPackage.ContextMenuStrip = this.cMenu;
            this.dgrdvPackingType.ContextMenuStrip = this.cMenu;
            this.dgrdvSet.ContextMenuStrip = this.cMenu;
            this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
            this.dgrdvManuf.CellContentClick += new DataGridViewCellEventHandler(dgrdvManuf_CellContentClick);
            this.dgrdvPackage.CellContentClick += new DataGridViewCellEventHandler(dgrdvPackage_CellContentClick);
            this.dgrdvSet .CellContentClick +=new DataGridViewCellEventHandler(dgrdvSet_CellContentClick);
            this.dgrdvPackingType.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvPackingType_UserDeletingRow);
        }

        private JERPData.Product.Product accPrds;
        private JERPData.Product.PackingType accPackingType;
        private DataTable dtblManuf, dtblPackage,dtblPrdSet,dtblPackingType;
        private FrmProductOper frmOper;
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
        int PrdID = -1;
        public bool DeleteFlag = false;
        public void PrdDelete(int PrdID)
        {
            this.PrdID = PrdID;
            this.DeleteFlag = false;
            this.LoadData();
         
        }
        private void LoadData()
        {
            this.dtblManuf = this.accPrds.GetDataProductReferedFromManuf(this.PrdID).Tables[0];
            this.dgrdvManuf.DataSource = this.dtblManuf;
            this.pageManuf.Text = "ԭ������[" + this.dtblManuf.Rows.Count.ToString() + "]";

            this.dtblPackage = this.accPrds.GetDataProductReferedFromPacking(this.PrdID).Tables[0];
            this.dgrdvPackage.DataSource = this.dtblPackage;
            this.pagePackage.Text = "��������[" + this.dtblPackage.Rows.Count.ToString() + "]";

            this.dtblPackingType = this.accPackingType.GetDataPackingTypeByPrdID(this.PrdID).Tables[0];
            this.dgrdvPackingType.DataSource = this.dtblPackingType;
            this.pagePackingType.Text = "��װ����[" + this.dtblPackingType.Rows.Count.ToString() + "]";

            this.dtblPrdSet = this.accPrds.GetDataProductReferedFromPrdSet(this.PrdID).Tables[0];
            this.dgrdvSet.DataSource = this.dtblPrdSet;
            this.pagePrdSet.Text = "��������[" + this.dtblPrdSet.Rows.Count.ToString() + "]";
        }
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }      
        private void ShowFrmOper(int PrdID)
        {
            if (frmOper == null)
            {
                frmOper = new FrmProductOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave +=this.LoadData ;
            }
            frmOper.Edit(PrdID);
            frmOper.ShowDialog();
        }

       
        void dgrdvPackage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if(this.dgrdvPackage .Columns[icol].GetType ().Equals (typeof(DataGridViewLinkColumn )))
            {
                int PrdID=(int)this.dtblPackage .DefaultView[irow]["PrdID"];
                this.ShowFrmOper(PrdID);
            }
        }

        void dgrdvManuf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdvManuf.Columns[icol].GetType().Equals(typeof(DataGridViewLinkColumn)))
            {
                int PrdID = (int)this.dtblManuf .DefaultView[irow]["PrdID"];
                this.ShowFrmOper(PrdID);
            }
        }
        void dgrdvSet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdvSet.Columns[icol].GetType().Equals(typeof(DataGridViewLinkColumn)))
            {
                int PrdID = (int)this.dtblPrdSet .DefaultView[irow]["PrdID"];
                this.ShowFrmOper(PrdID);
            }
        }

        void dgrdvPackingType_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult rlt = MessageBox.Show("���棡��Ҫɾ����ǰ����װ���������ɾ�������ָܻ���" +
              "\n�ǣ�ɾ������ȡ��!", "����ȷ��",
  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.No) return;
            string errormsg=string.Empty ;
            bool flag=this.accPackingType.DeletePackingType(ref errormsg,
                this.dtblPackingType.DefaultView[e.Row.Index]["PackingTypeID"]);
            if (flag)
            {
                MessageBox.Show("�ɹ�ɾ����ǰ��װ����");
            }
            else
            {
                MessageBox.Show(errormsg);
                e.Cancel = true;
            }
            this.pagePackingType.Text = "��װ����[" + this.dgrdvPackingType.Rows.Count.ToString() + "]";
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult rlt = MessageBox.Show("���棡��Ҫɾ����ǰ��ǰ��¼�����ɾ�������ָܻ���"  +
               "\n�ǣ�ɾ������ȡ��!", "����ȷ��",
   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.No) return; 
            bool flag = false;
            string errormsg = string.Empty;
            flag = this.accPrds.DeleteProduct(
                ref errormsg,
                this.PrdID);
            if (flag)
            {
                MessageBox.Show("�ɹ�ɾ���˵�ǰ��Ʒ");
                this.DeleteFlag = true;
                if (this.affterSave != null) this.affterSave();
                this.Close();
            }
            else
            {
                this.DeleteFlag = false;
                MessageBox.Show(errormsg);
            } 
        }
    }
}