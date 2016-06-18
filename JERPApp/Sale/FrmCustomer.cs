using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accCustomers = new JERPData.General.Customer();
            this.accArea = new JERPData.General.Area();
            this.accPsns = new JERPData.Hr.Personnel();
            this.accCustomerType = new JERPData.General.CustomerType();
            this.accDeliverAddress = new JERPData.General.DeliverAddress();
            this.accFinanceAddress = new JERPData.General.FinanceAddress();
            this.dtblPsns = this.accPsns.GetDataPersonnel().Tables[0];
            this.SetPermit();
        }
        private JERPData.General .Customer accCustomers;
        private FrmCustomerOper frmOper;
        private DataTable dtblCustomer;
        private JCommon.FrmExcelImport frmImport;
        private JERPData.General.Area accArea;
        private JERPData.Hr.Personnel accPsns;
        private JERPData.General.CustomerType accCustomerType;
        private JERPData.General.DeliverAddress accDeliverAddress;
        private JERPData.General.FinanceAddress accFinanceAddress;
        private FrmHandlePsnAlter frmHandlerAlter;
        private FrmSalePsnAlter frmSalePsnAlter;
        private DataTable dtblPsns;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(11);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(12);
            if (this.enableBrowse)
            {
                this.ctrlQFind.SeachGridView = this.dgrdv;
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.ctrlQFind.BeforeFilter += this.LoadData;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                //��������
                LoadData();
                this.btnExport .Click +=new EventHandler(btnExport_Click);
            }
            this.lnkNew.Enabled = this.enableSave;
            this.ColumnBtnEdit.Visible = this.enableSave;
            this.btnImport.Enabled = this.enableSave;
            this.lnkAlterHandler.Enabled = this.enableSave;
            this.lnkAlterSaler.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.lnkAlterSaler.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkAlterSaler_LinkClicked);
                this.lnkAlterHandler.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkAlterHandler_LinkClicked);
                this.FormClosed += new FormClosedEventHandler(FrmCustomer_FormClosed);
                this.btnImport .Click +=new EventHandler(btnImport_Click);
                this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            }
        }

        void lnkAlterHandler_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmHandlerAlter == null)
            {
                frmHandlerAlter = new FrmHandlePsnAlter();
                new FrmStyle(frmHandlerAlter).SetPopFrmStyle(this);
                frmHandlerAlter.AffterSave += this.LoadData;
            }
            frmHandlerAlter.HandlerPsnAlter();
            frmHandlerAlter.ShowDialog();
        }

        void lnkAlterSaler_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmSalePsnAlter == null)
            {
                frmSalePsnAlter = new FrmSalePsnAlter();
                new FrmStyle(frmSalePsnAlter).SetPopFrmStyle(this);
                frmSalePsnAlter.AffterSave += this.LoadData;
            }
            frmSalePsnAlter.SalePsnAlter();
            frmSalePsnAlter.ShowDialog();
        }

        void FrmCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.affterSave != null) this.affterSave();
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
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        private void LoadData()
        { 
            this.dtblCustomer = this.accCustomers.GetDataCustomer().Tables[0];         
            this.dgrdv.DataSource = this.dtblCustomer;
        }
        private int GetAreaID(string AreaName)
        {
            int AreaID = -1;
            this.accArea.GetParmAreaAreaID(AreaName, ref AreaID);
            return AreaID;
        }
        private int GetCustomerTypeID(string CutomerTypeName)
        {
            int rut = -1;
            this.accCustomerType.GetParmCustomerTypeID(CutomerTypeName, ref rut);
            return rut;
        }
        private bool GetBool(string BoolInfor)
        {

            return (BoolInfor == "��");
        }
        private int GetPsnID(string PsnName)
        {
             
            DataRow[] drows = this.dtblPsns.Select("PsnName='" + PsnName + "'");
            if (drows.Length > 0)
            {
                return (int)drows[0]["PsnID"];
            }
            return -1;
        }

        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmOper == null)
            {
                frmOper = new FrmCustomerOper();
                new FrmStyle(frmOper).SetPopFrmStyle(this);
                frmOper.AffterSave += this.LoadData;
            }
            frmOper.New();
            frmOper.ShowDialog();
        }

        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnBtnEdit.Name)
            {
                int CompanyID=(int)this.dtblCustomer.DefaultView [irow].Row["CompanyID"];
                if (frmOper == null)
                {
                    frmOper = new FrmCustomerOper();
                    new FrmStyle(frmOper).SetPopFrmStyle(this);
                    frmOper.AffterSave += this.LoadData;
                }
                frmOper.Edit(CompanyID);
                frmOper.ShowDialog();
            } 
            if (this.dgrdv.Columns[icol].Name == this.ColumnURL.Name)
            {
                System.Diagnostics.Process.Start(this.dgrdv[icol, irow].Value.ToString());
            }
        }
        void btnImport_Click(object sender, EventArgs e)
        {
            if (this.frmImport == null)
            {
                this.frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                this.frmImport.AffterSave += this.LoadData;
                this.frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                DataColumn[] columns = new DataColumn[] {
                    new DataColumn ("����",typeof (string)),
                    new DataColumn ("���",typeof (string)),
                    new DataColumn ("���",typeof (string)),   
                    new DataColumn ("ȫ��",typeof (string)), 
                    new DataColumn ("������",typeof (string)), 
                    new DataColumn ("�ͻ�����",typeof (string)), 
                    new DataColumn ("����",typeof (string)), 
                    new DataColumn ("ע��ʱ��",typeof (DateTime)), 
                    new DataColumn ("ҵ��Ա",typeof (string)), 
                    new DataColumn ("����Ա",typeof (string)),  
                    new DataColumn ("��ϵ��",typeof (string)), 
                    new DataColumn ("�绰",typeof (string)),  
                    new DataColumn ("�ֻ�",typeof (string)),  
                    new DataColumn ("����",typeof (string)),
                    new DataColumn ("QQ",typeof (string)),   
                    new DataColumn ("΢��",typeof (string)),   
                    new DataColumn ("Email",typeof (string)),   
                    new DataColumn ("��ַ",typeof (string)),     
                    new DataColumn ("�ͻ���ַ",typeof (string)),  
                    new DataColumn ("�����ַ",typeof (string)),    
                    new DataColumn ("������Ϣ",typeof (string)),  
                    new DataColumn ("���ö��",typeof (decimal)), 
                    new DataColumn ("��ע",typeof (string))
                };
                this.frmImport.SetImportColumn(columns,
                    "��ţ����,ȫ�Ʋ���Ϊ��,��˾�ͻ����ǻ��߷�δ���Է���,������ֶ���ͻ��ػ��������ûس�����;\nҵ��Ա������Ա�õ��ǹ���");
            }
            this.frmImport.ShowDialog();

        }

        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = "�ɹ�!";
            object objCompanyID = DBNull.Value;
            int SalePsnID = this.GetPsnID(drow["ҵ��Ա"].ToString());
            if (SalePsnID == -1)
            {
                flag = false;
                msg = "ҵ��Ա������";
                return;
            }
            int HandlePsnID = this.GetPsnID(drow["����Ա"].ToString());
            if (HandlePsnID == -1)
            {
                flag = false;
                msg = "����Ա������";
                return;
            }
            int CustomerTypeID = this.GetCustomerTypeID(drow["�ͻ�����"].ToString());
            if (CustomerTypeID == -1)
            {
                flag = false;
                msg = "�ͻ����Ͳ�����";
                return;
            }
            flag = this.accCustomers.InsertCustomer (ref msg,
                ref objCompanyID,
                drow["���"],
                drow["���"],
                drow["ȫ��"],
                drow["������"],
                drow["����"],
                CustomerTypeID,
                drow["ע��ʱ��"],
                this.GetAreaID (drow["����"].ToString ()),
                SalePsnID,
                HandlePsnID,
                drow["��ϵ��"],
                drow["�绰"],
                drow["�ֻ�"],
                drow["����"],
                drow["QQ"],
                drow["΢��"],
                drow["Email"],
                drow["��ַ"],
                drow["������Ϣ"],
                drow["���ö��"],
                drow["��ע"]);
            string  Address = drow["�ͻ���ַ"].ToString();
            string[] AddressList;
            string errormsg=string.Empty ;
            object objAddressID=DBNull .Value ;
            string[] spit = new string[]{@"
"};
            if (Address != string.Empty)
            {
                AddressList = Address.Split(spit, StringSplitOptions.RemoveEmptyEntries);
               for (int i = 0; i < AddressList.Length; i++)
               {
                   this.accDeliverAddress.InsertDeliverAddress(
                       ref errormsg,
                       ref objAddressID,
                       objCompanyID,
                       AddressList[i],
                       DBNull.Value,
                       DBNull.Value,
                       DBNull .Value );
               }
            }
            Address = drow["�����ַ"].ToString();
            if (Address != string.Empty)
            {
                AddressList = Address.Split(spit, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < AddressList.Length; i++)
                {
                    this.accFinanceAddress.InsertFinanceAddress (
                        ref errormsg,
                        ref objAddressID,
                        objCompanyID,
                        AddressList[i],
                        DBNull.Value,
                        DBNull.Value,
                        DBNull .Value );
                }
            }
            this.accCustomers.UpdateCustomerForAddress(ref errormsg, objCompanyID);

        }
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("��������Excel�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�ͻ��б�");
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