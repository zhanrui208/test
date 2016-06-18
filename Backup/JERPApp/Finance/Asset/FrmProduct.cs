using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Finance.Asset
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accProduct = new JERPData.Asset.Product();
            this.SetPermit();
        }
        private JERPData.Asset.Product accProduct;
        private FrmPrdRepair frmRepair;
        private DataTable dtblProduct;
        private JCommon.FrmExcelImport frmImport;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(688);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(689);
            if (this.enableBrowse)
            {
                this.dgrdv.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.btnExport .Click +=new EventHandler(btnExport_Click);
                this.LoadData();
            }
            this.btnImport.Enabled = this.enableSave;
            if (this.enableSave)
            {
                this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
                this.btnSave.Click += new EventHandler(btnSave_Click);
                this.btnImport.Click += new EventHandler(btnImport_Click);              
                this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            }
        }

        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name == this.ColumnDateLastRepair.Name)
            {
                object objPrdID = this.dtblProduct.DefaultView[irow]["PrdID"];
                if (objPrdID == DBNull.Value) return;
                if (frmRepair == null)
                {
                    frmRepair = new FrmPrdRepair();
                    new FrmStyle(frmRepair).SetPopFrmStyle(this);
                    frmRepair.AffterSave += new FrmPrdRepair.AffterSaveDelegate(frmRepair_AffterSave);
                }
                frmRepair.RepairRecord((int)objPrdID);
                frmRepair.ShowDialog();
            }
        }

        void frmRepair_AffterSave(int PrdID,object objDateLastRepair)
        {
            string errormsg=string.Empty ;
            this.accProduct.UpdateProductForRepair(ref errormsg,
                PrdID,
                objDateLastRepair);
            this.dgrdv.CurrentCell.Value  = objDateLastRepair;
        }


        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        private void LoadData()
        {
            this.dtblProduct  = this.accProduct .GetDataProduct ().Tables[0];   
            this.dgrdv.DataSource = this.dtblProduct ;

        }
        void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblProduct.DefaultView[irow].Row;
            string ErrorMsg = string.Empty;
            if (drow["PrdID"] == DBNull.Value)
            {
                e.Cancel = true;
                return;
            }
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accProduct .DeleteProduct (ref ErrorMsg,
                    drow["PrdID"]);
                if (!flag)
                {

                    MessageBox.Show(ErrorMsg);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        void btnImport_Click(object sender, EventArgs e)
        {

            if (this.frmImport == null)
            {
                this.frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this);
                this.frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                DataColumn[] columns = new DataColumn[] {
                    new DataColumn ("�ʲ����",typeof (string)),
                    new DataColumn ("�ʲ�����",typeof (string)),                     
                    new DataColumn ("ʹ����",typeof (string)),                   
                    new DataColumn ("������",typeof (string)),                      
                    new DataColumn ("��ŵ�",typeof (string)),   
                    new DataColumn ("���״̬",typeof (string)),                    
                    new DataColumn ("�ɹ�����",typeof (DateTime)),   
                    new DataColumn ("���״̬",typeof (string)),                      
                    new DataColumn ("��ֵ",typeof (decimal)),                     
                    new DataColumn ("����ֵ",typeof (decimal)), 
                    new DataColumn ("�ۻ��۾�",typeof (decimal))
                };
                this.frmImport.SetImportColumn(columns, "��Ʒ��Ų���Ϊ�գ���λ����Ĭ��Ϊ̨");
            }
            this.frmImport.ShowDialog();

        }
        private int GetPrdID(string PrdCode)
        {
            int rut = -1;
            this.accProduct.GetParmPrdID(PrdCode, ref rut);
            return rut;

        }
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = "�ɹ�!";
            object objPrdID = DBNull.Value;
           
            int PrdID = this.GetPrdID (drow["�ʲ����"].ToString());
            if (PrdID > -1)
            {
                flag = false;
                msg = "�Ѵ��ڴ˱��";
                return;
            } 
            DataRow drowNew = this.dtblProduct .NewRow();
            drowNew["PrdCode"] = drow["�ʲ����"];
            drowNew["PrdName"] = drow["�ʲ�����"];
            drowNew["UserName"] = drow["ʹ����"];
            drowNew["ResponsiblePsnName"] = drow["������"];
            drowNew["DepositPlace"] = drow["��ŵ�"];
            drowNew["DepositStatus"] = drow["���״̬"];
            drowNew["DatePurchase"] = drow["�ɹ�����"];
            drowNew["Cost"] = drow["��ֵ"];
            drowNew["Salvage"] = drow["����ֵ"];
            drowNew["AccumDepreciate"] = drow["�ۻ��۾�"];
            this.dtblProduct .Rows.Add(drowNew);
        }
        private bool ValidateData()
        {
           
            DataRow[] drows = this.dtblProduct.Select("PrdCode is null", "");
            if (drows.Length > 0)
            {
                MessageBox.Show("�ʲ���Ų���Ϊ��");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            string errormsg = string.Empty;
            bool flag = false;
            foreach (DataRow drow in this.dtblProduct .Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                if (drow["PrdID"] == DBNull.Value)
                {
                    object objPrdID = DBNull.Value;
                    flag = this.accProduct .InsertProduct (
                        ref errormsg,
                        ref objPrdID,
                        drow["PrdCode"],
                        drow["PrdName"],
                        drow["UserName"],
                        drow["ResponsiblePsnName"],
                        drow["DepositPlace"],
                        drow["DepositStatus"],
                        drow["DatePurchase"],
                        drow["Cost"],
                        drow["Salvage"],
                        drow["AccumDepreciate"]);
                    if (flag)
                    {
                        drow["PrdID"] = objPrdID;
                        drow.AcceptChanges();
                    }

                }
                else
                {
                    if (this.GetPrdID(drow["PrdCode"].ToString()) != (int)drow["PrdID"])
                    {
                        drow.RowError = "�˱���ѱ�����ʲ�ʹ��";
                        continue;
                    }
                    else
                    {
                        flag = this.accProduct .UpdateProduct (
                                ref errormsg,
                                drow["PrdID"],
                                drow["PrdCode"],
                                drow["PrdName"],
                                drow["UserName"],
                                drow["ResponsiblePsnName"],
                                drow["DepositPlace"],
                                drow["DepositStatus"],
                                drow["DatePurchase"],
                                drow["Cost"],
                                drow["Salvage"],
                                drow["AccumDepreciate"]);
                    }
                }
                if (flag)
                {
                    drow.AcceptChanges();
                }
                else
                {
                    MessageBox.Show(errormsg);
                }

            }
            MessageBox.Show("�ɹ��������ʲ�����");
        }
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("��������Excel�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "�ʲ�һ��");
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