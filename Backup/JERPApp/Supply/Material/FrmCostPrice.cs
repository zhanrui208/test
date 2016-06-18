using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Supply.Material
{
    public partial class FrmCostPrice : Form
    {
        public FrmCostPrice()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accPrds = new JERPData.Product.Product (); 
            this.SetPermit();
        }

     
        private JERPData.Product .Product  accPrds; 
        private DataTable dtblPrds ; 
        private JCommon.FrmFileBrowse frmFileBrowse;
        private JCommon.FrmImgBrowse frmImgBrowse;
        private JCommon.FrmExcelImport frmImport;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(469);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(470);
            if (this.enableBrowse)
            {
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh .Click +=new EventHandler(mItemRefresh_Click);
                this.ctrlQFind.SeachGridView = this.dgrdv;
                this.ctrlQFind.BeforeFilter += this.LoadData;               
                foreach (DataGridViewColumn col in this.dgrdv.Columns)
                {
                    col.ReadOnly = true;
                }
                this.ColumnCostPrice.ReadOnly = false;  
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
                this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
                this.LoadData();
            }
            this.btnSave.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.btnImport.Click += new EventHandler(btnImport_Click);
                this.btnSave.Click += new EventHandler(btnSave_Click); 
                this.btnExport.Click += new EventHandler(btnExport_Click); 
            }
        }

        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                grow.ErrorText = string.Empty;
                if (grow.Cells[this.ColumnCostPrice.Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "δ�赥��";
                }
            }
        }

      
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        } 
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "ԭ�ϳɱ�һ��" );
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(this.dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, false);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.Show();
            FrmMsg.Hide();
        } 
        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            object objPrdID = this.dtblPrds.DefaultView[irow]["PrdID"];
            if ((objPrdID == null) || (objPrdID == DBNull.Value)) return;
            string errormsg = string.Empty; 
            if (this.dgrdv.Columns[icol].Name == this.ColumnFileCount .Name)
            {
                if (frmFileBrowse == null)
                {
                    frmFileBrowse = new JCommon.FrmFileBrowse();
                    frmFileBrowse.ReadOnly = true;
                    new FrmStyle(frmFileBrowse).SetPopFrmStyle(this);

                }
                frmFileBrowse.Browse(JERPData.ServerParameter.ERPFileFolder + @"\Product\PrdFile\" + objPrdID.ToString());
                frmFileBrowse.ShowDialog();
           
            }
            if (this.dgrdv.Columns[icol].Name == this.ColumnImgCount .Name)
            {
                if (frmImgBrowse == null)
                {
                    frmImgBrowse = new JCommon.FrmImgBrowse();
                    frmImgBrowse.ReadOnly = true; ;
                    new FrmStyle(frmImgBrowse).SetPopFrmStyle(this);
                }
                frmImgBrowse.Browse(JERPData.ServerParameter.ERPFileFolder + @"\Product\PrdImg\" + objPrdID.ToString());
                frmImgBrowse.ShowDialog();
              
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
       

     
       
        private void LoadData()
        { 
           this.dtblPrds = this.accPrds.GetDataProductForSetCostPrice().Tables[0];            
           this.dgrdv.DataSource = this.dtblPrds;
       
        } 
        void FrmProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.affterSave != null) this.affterSave();
        }
        void btnImport_Click(object sender, EventArgs e)
        {
            if (frmImport == null)
            {
                frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                DataColumn[] columns = new DataColumn[]{    
                    new DataColumn ("���ϱ��",typeof (string)), 
                    new DataColumn ("��������",typeof (string)), 
                    new DataColumn ("���Ϲ��",typeof (string)),  
                    new DataColumn ("Ʒ��",typeof (string)),  
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
            DataRow[] drowPrds = this.dtblPrds.Select("PrdCode='" + drow["���ϱ��"].ToString()+"'"); 
           
            if (drowPrds.Length== 0)
            {
                flag = false;
                msg = "��������ϱ��";
                return;
            }
            else
            {
                drowPrds[0]["CostPrice"]=drow["�ɱ���"]; 
            }
        }
        void frmImport_AffterSave()
        {
            MessageBox.Show("������ɣ���������");
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("���ڱ���ɱ�����,��Ϊ����������ϵĳɱ�,��Ҫʱ����ܳ�");
            string errormsg=string.Empty ;
            foreach (DataRow drow in this.dtblPrds.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                this.accPrds.UpdateProductForCostPrice(ref errormsg,
                    drow["PrdID"],
                    drow["CostPrice"]);
            }
            FrmMsg.Hide();
            MessageBox.Show("�ɹ������˲�Ʒ�ĳɱ���");
        }

    }
}