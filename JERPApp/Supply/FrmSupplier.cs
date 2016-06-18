using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Supply
{
    public partial class FrmSupplier : Form
    {
        public FrmSupplier()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accSuppliers = new JERPData.General.Supplier ();
            this.SetPermit();
        }
        private JERPData.General .Supplier accSuppliers;
        private DataTable dtblSupplier;
        private JCommon.FrmExcelImport frmImport;
        private FrmSupplierOper frmOper;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(13);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(14);
            if (this.enableBrowse)
            {
                this.ctrlQFind.SeachGridView = this.dgrdv;
                this.btnExport .Click +=new EventHandler(btnExport_Click);
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                //��������
                LoadData();
            }          
            this.btnImport.Enabled = this.enableSave;
            this.ColumnbtnEdit.Visible = this.enableSave;
            this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
            if (this.enableSave)
            {
                
                this.btnImport .Click +=new EventHandler(btnImport_Click);
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
 
            }

        
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnbtnEdit.Name)
            {
                int CompanyID = (int)this.dtblSupplier.DefaultView[irow]["CompanyID"];
                if (frmOper == null)
                {
                    frmOper = new FrmSupplierOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += this.LoadData;
                }
                frmOper.Edit(CompanyID);
                frmOper.ShowDialog();
            }
            if (this.dgrdv.Columns[icol].Name == this.ColumnURL .Name)
            {
                System.Diagnostics.Process.Start(this.dgrdv[icol,irow].Value .ToString ());
            }
        }

        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmOper == null)
            {
                frmOper = new FrmSupplierOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += this.LoadData;
            }
            frmOper.New();
            frmOper.ShowDialog();
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
            this.dtblSupplier = this.accSuppliers.GetDataSupplier().Tables[0];
            this.dgrdv.DataSource = this.dtblSupplier;
        }
        bool GetBool(string BoolInfor)
        {
            return (BoolInfor == "��");
        }
        void btnImport_Click(object sender, EventArgs e)
        {
            if (this.frmImport == null)
            {
                this.frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                this.frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                this.frmImport.AffterSave += new JCommon.FrmExcelImport.AffterSaveDelegate(frmImport_AffterSave);
                DataColumn[] columns = new DataColumn[] {
                    new DataColumn ("���",typeof (string)),
                    new DataColumn ("���",typeof (string)),  
                    new DataColumn ("ȫ��",typeof (string)),   
                    new DataColumn ("����",typeof (string)),       
                    new DataColumn ("��ע",typeof (string)),      
                    new DataColumn ("��ϵ��",typeof (string)),  
                    new DataColumn ("�绰",typeof (string)),    
                    new DataColumn ("�ֻ�",typeof (string)),    
                    new DataColumn ("����",typeof (string)),    
                    new DataColumn ("QQ",typeof (string)),
                    new DataColumn ("΢��",typeof (string)),  
                    new DataColumn ("������Ϣ",typeof (string)),  
                    new DataColumn ("Email",typeof(string)),     
                    new DataColumn ("��ַ",typeof(string)),     
                    new DataColumn ("��ַ",typeof (string)),  
                    new DataColumn ("ԭ��",typeof (string)), 
                    new DataColumn ("��Ʒ",typeof (string)),                   
                    new DataColumn ("ί��",typeof (string)),
                    new DataColumn ("�Ĳ�",typeof (string)), 
                    new DataColumn ("����",typeof (string)),
                    new DataColumn ("�ξ�",typeof (string))
                };
                this.frmImport.SetImportColumn(columns, "��Ӧ�̼�Ʋ����ظ�,�ұ���;\n�ɹ���ί��,�ǹ�Ӧ�����ͣ���'��',�����ĵ����Ƿ�");
            }
            this.frmImport.ShowDialog();

        }

        void frmImport_AffterSave()
        {
            frmImport.Close();
            this.LoadData();
        }
        private int GetCompanyID(string CompanyCode)
        {
            int rut = -1;
            this.accSuppliers.GetParmSupplierCompanyID(CompanyCode, ref rut);
            return rut;
        }
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = "�ɹ�!";
            //���һ����û��������
            int CompanyID = this.GetCompanyID(drow["���"].ToString());
            if (CompanyID > -1)
            {
                msg = "�Ѵ��ڴ˱��";
                flag = false;
                return;

            }
            string errormsg=string.Empty ;
            object objCompanyID = DBNull.Value;
            this.accSuppliers .InsertSupplier (
                ref errormsg ,
                ref objCompanyID ,
                drow["���"],
                drow["���"],
                drow["ȫ��"], 
                drow["����"],
                this.GetBool(drow["ԭ��"].ToString()),
                this.GetBool(drow["��Ʒ"].ToString()), 
                this.GetBool(drow["�Ĳ�"].ToString()),
                this.GetBool(drow["�ξ�"].ToString()),
                this.GetBool(drow["ί��"].ToString()),
                this.GetBool(drow["����"].ToString()),
                drow["��ϵ��"] ,
                drow["�绰"] ,
                drow["�ֻ�"],
                drow["����"],
                drow["QQ"],
                drow["΢��"],
                drow["Email"],
                drow["��ַ"],
                drow["��ַ"],
                drow["������Ϣ"],
                drow["��ע"]
                );
         
        }

    
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("��������Excel�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��Ӧ��");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, true);
            FrmMsg.Hide();
            excel.Show();
        }
    }
}