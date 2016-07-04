using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Hr
{
    public partial class FrmPersonnel : Form
    {
        public FrmPersonnel()
        {
            InitializeComponent();
            this.dgrdvOnjob.AutoGenerateColumns = false;
            this.dgrdvOffjob.AutoGenerateColumns = false;
            this.accPsns = new JERPData.Hr.Personnel();
            this.SetPermit();
        }
        private JERPData.Hr.Personnel accPsns;
        private DataTable dtblPsnOnjob,dtblPsnOffjob;
        private JCommon.FrmExcelImport frmImport;
        private JCommon.FrmImgBrowse frmImgBrowse;
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(9);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(10);
            if (this.enableBrowse)
            {
                this.ctrlOnjobFind.SeachGridView = this.dgrdvOnjob;
                this.ctrlOffjobFind.SeachGridView = this.dgrdvOffjob;
                this.ctrlOnjobFind.BeforeFilter += this.LoadOnjob;
                this.ctrlOffjobFind.BeforeFilter += this.LoadOffjob;
                this.dgrdvOnjob.ContextMenuStrip = this.cMenu;
                this.dgrdvOffjob.ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.btnExportOnjob.Click+=new EventHandler(btnExportOnjob_Click);
                this.btnExportOutjob.Click += new EventHandler(btnExportOutjob_Click);
                //��������
                LoadData();
            }
            this.dgrdvOnjob.AllowUserToAddRows = enableSave;
            this.dgrdvOnjob.ReadOnly = !enableSave;
            this.btnSave.Enabled = this.enableSave;
            this.btnAlter.Enabled = this.enableSave;
            this.btnImport.Enabled = this.enableSave;
            if (this.enableSave)
            { 
                
                this.btnImport.Click += new EventHandler(btnImport_Click);
                this.dgrdvOffjob.CellContentClick += new DataGridViewCellEventHandler(dgrdvOffjob_CellContentClick);
                this.dgrdvOnjob.CellContentClick += new DataGridViewCellEventHandler(dgrdvOnjob_CellContentClick);
                this.dgrdvOnjob.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdvOnjob_UserDeletingRow);
                this.btnSave.Click += new EventHandler(btnSave_Click);
                this.btnAlter.Click += new EventHandler(btnAlter_Click);
            }

        
        }



        void dgrdvOnjob_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            object objPsnID = this.dtblPsnOnjob.DefaultView[e.Row.Index]["PsnID"];
            if (objPsnID != DBNull.Value)
            {
                MessageBox.Show("�Բ���,����Ա�ѱ���,����ɾ��,��������Ϊ����");
                e.Cancel = true;
            }
        }

        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        void dgrdvOnjob_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;            
            DataRow drow = this.dtblPsnOnjob.DefaultView[irow].Row;
            object objPsnID = drow["PsnID"];
            if ((objPsnID==null)||(objPsnID  == DBNull.Value)) return;
            string errormsg = string.Empty;
            if (this.dgrdvOnjob.Columns[icol].Name == this.ColumnbtnOffjob.Name)
            {
                if(this.dgrdvOnjob .Rows [irow].IsNewRow )return;
                DateTime DateDismiss = DateTime.Now.Date;
                JCommon.FrmSetDateTime.ShowDialog(ref DateDismiss);               
                this.accPsns.UpdatePersonnelForOffjob(ref errormsg,
                    objPsnID, DateDismiss);
                this.dgrdvOnjob.Rows.RemoveAt(irow);
                this.LoadOffjob();
            }
            if (this.dgrdvOnjob.Columns[icol].Name == this.ColumnSignImgCount.Name)
            {
                if (frmImgBrowse == null)
                {
                    frmImgBrowse = new JCommon.FrmImgBrowse();
                    frmImgBrowse.ReadOnly = !this.enableSave;
                    new FrmStyle(frmImgBrowse).SetPopFrmStyle(this);
                }
                frmImgBrowse.Browse(JERPData.ServerParameter.ERPFileFolder + @"\Hr\SignImg\" + objPsnID.ToString());
                frmImgBrowse.ShowDialog();
                this.dgrdvOnjob[icol, irow].Value = frmImgBrowse.Count;
                this.accPsns .UpdatePersonnelForSignImgCount (ref errormsg,
                    objPsnID,
                    frmImgBrowse.Count);
            }
            if (this.dgrdvOnjob.Columns[icol].Name == this.ColumnPortraitImgCount.Name)
            {
                if (frmImgBrowse == null)
                {
                    frmImgBrowse = new JCommon.FrmImgBrowse();
                    frmImgBrowse.ReadOnly = !this.enableSave;
                    new FrmStyle(frmImgBrowse).SetPopFrmStyle(this);
                }
                frmImgBrowse.Browse(JERPData.ServerParameter.ERPFileFolder + @"\Hr\PortraitImg\" + objPsnID.ToString());
                frmImgBrowse.ShowDialog();
                this.dgrdvOnjob[icol, irow].Value = frmImgBrowse.Count;
                this.accPsns.UpdatePersonnelForPortraitImgCount (ref errormsg,
                    objPsnID,
                    frmImgBrowse.Count);
            }
        }

        void dgrdvOffjob_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdvOffjob .Columns[icol].Name == this.ColumnbtnOnjob.Name)
            {
                if (this.dgrdvOffjob.Rows[irow].IsNewRow) return;
                DataRow drow = this.dtblPsnOffjob .DefaultView[irow].Row;
                string errormsg = string.Empty;
                this.accPsns.UpdatePersonnelForOnjob(ref errormsg, drow["PsnID"]);
                this.dgrdvOffjob.Rows.RemoveAt(irow);
                this.dtblPsnOnjob.ImportRow(drow);
                this.LoadOnjob();
            }
        }
        private void LoadOnjob()
        {
            this.dtblPsnOnjob = this.accPsns.GetDataPersonnelOnjob().Tables[0];
            this.dtblPsnOnjob.Columns["PsnName"].AllowDBNull = false;
            this.dgrdvOnjob.DataSource = this.dtblPsnOnjob;
        }
        private void LoadOffjob()
        {
            this.dtblPsnOffjob = this.accPsns.GetDataPersonnelOffjob().Tables[0];
            this.dgrdvOffjob.DataSource = this.dtblPsnOffjob;
        }
        private void LoadData()
        {

            this.LoadOnjob();
            this.LoadOffjob();
         
        }
        private int GetPsnID(string PsnCode)
        {
            int rut = -1;
            this.accPsns.GetParmPersonnelPnsID(PsnCode, ref rut);
            return rut;
        }
        private void SavePsn(DataRow drow)
        {
            bool flag = false;
            string errormsg = string.Empty;
            int psnid = -1;
            object objPsnID = DBNull.Value;
            psnid = this.GetPsnID(drow["PsnCode"].ToString());
            if (drow["PsnID"] == DBNull.Value)
            {
                if (psnid > -1)
                {
                    drow.RowError = "�Բ���,�˹����Ѵ���";
                    return;
                }
                objPsnID = DBNull.Value;
                flag = this.accPsns.InsertPersonnel(
                        ref errormsg,
                        ref objPsnID,
                        drow["PsnCode"],
                        drow["PsnName"],
                        drow["AssistantCode"],
                        drow["Sex"],
                        drow["JobType"],
                        drow["DeptName"],
                        drow["Position"],
                        drow["Nation"],
                        drow["DateHire"],
                        drow["ProbationMonth"],
                        drow["Province"],
                        drow["Diploma"],
                        drow["IDCode"],
                        drow["IDAddress"],
                        drow["BankCode"],
                        drow["BankName"],
                        drow["Telephone"],
                        drow["RoomFlag"],
                        drow["RoomNo"],
                        drow["Description"]);

                if (flag)
                {
                    drow["PsnID"] = objPsnID;
                }

            }
            else
            {
                if ((psnid > -1) && (psnid != (int)drow["PsnID"]))
                {

                    drow.RowError = "�Բ���,�˹����Ѵ���";
                    return;                  

                }
                flag = this.accPsns.UpdatePersonnel(
                        ref errormsg,
                        drow["PsnID"],
                        drow["PsnCode"],
                        drow["PsnName"],
                        drow["AssistantCode"],
                        drow["Sex"],
                        drow["JobType"],
                        drow["DeptName"],
                        drow["Position"],
                        drow["Nation"],
                        drow["DateHire"],
                        drow["ProbationMonth"],
                        drow["Province"],
                        drow["Diploma"],
                        drow["IDCode"],
                        drow["IDAddress"],
                        drow["BankCode"],
                        drow["BankName"],
                        drow["Telephone"],
                        drow["RoomFlag"],
                        drow["RoomNo"],
                        drow["Description"]);

                if (flag)
                {
                    drow.AcceptChanges();
                    drow.RowError = string.Empty;
                }
                else
                {
                    drow.RowError = errormsg;
                }

            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult rul = MessageBox.Show("�������б���,�����������룡", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
          
            foreach (DataRow drow in this.dtblPsnOnjob.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                this.SavePsn(drow);
            }
            this.dgrdvOnjob.Refresh();
            MessageBox.Show("�ɹ�����Ա������");
        }

        void btnAlter_Click(object sender, EventArgs e)
        {
            DialogResult rul = MessageBox.Show("�������б���,�����������룡", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            foreach (DataRow drow in this.dtblPsnOffjob.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                this.SavePsn(drow);
            }
            this.dgrdvOffjob .Refresh();
            MessageBox.Show("�ɹ�����Ա������");
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
                    new DataColumn ("���",typeof (string)),   
                    new DataColumn ("����",typeof (string)),   
                    new DataColumn ("����",typeof (string)),   
                    new DataColumn ("����",typeof (string)),  
                    new DataColumn ("������",typeof (string)), 
                    new DataColumn ("��ְʱ��",typeof (DateTime)),   
                    new DataColumn ("���֤��",typeof (string)),
                    new DataColumn ("��ס��",typeof (string)), 
                    new DataColumn ("�绰",typeof (string)),  
                    new DataColumn ("�Ա�",typeof (string)),                                     
                    new DataColumn ("ѧ��",typeof (string)),    
                    new DataColumn ("ְ��",typeof (string)),
                    new DataColumn ("����",typeof (string)), 
                    new DataColumn ("������(�·�)",typeof (int)),      
                    new DataColumn ("����",typeof (string)),     
                    new DataColumn ("���п���",typeof (string)),
                    new DataColumn ("������",typeof (string)),
                    new DataColumn ("ס��",typeof (string)), 
                    new DataColumn ("�����",typeof (string)),
                    new DataColumn ("����",typeof (string))
                };
                this.frmImport.SetImportColumn(columns, "��ְʱ��һ��Ҫ�淶,���������·�");
            }
            this.frmImport.ShowDialog();

        }
     
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = "�ɹ�!";
            if (drow["����"].ToString() == string.Empty)
            {
                msg = "���Ų���Ϊ��";
                flag = false;
                return;
            }
            if (drow["����"].ToString() == string.Empty)
            {
                msg = "��������Ϊ��";
                flag = false;
                return;
            }
            int PsnID = this.GetPsnID(drow["����"].ToString());
            if (PsnID > -1)
            {
                msg = "�˹����Ѵ���";
                flag = false;
                return;
            }
            object objPsnID = DBNull.Value;           
            bool RoomFlag = false;
            if (drow["ס��"].ToString() == "��")
            {
                RoomFlag = true;
            }
            DataRow drowNew = this.dtblPsnOnjob.NewRow();
            drowNew["PsnCode"] = drow["����"];
            drowNew["PsnName"] = drow["����"];
            drowNew["AssistantCode"] = drow["������"];
            drowNew["Sex"] = drow["�Ա�"];
            drowNew["JobType"] = drow["���"];
            drowNew["DeptName"] = drow["����"];
            drowNew["Position"] = drow["ְ��"];
            drowNew["Nation"] = drow["����"];
            drowNew["DateHire"] = drow["��ְʱ��"];
            drowNew["ProbationMonth"] = drow["������(�·�)"];
            drowNew["Province"] = drow["����"];
            drowNew["Diploma"] = drow["ѧ��"];
            drowNew["IDCode"] = drow["���֤��"];
            drowNew["IDAddress"] = drow["��ס��"];
            drowNew["BankCode"] = drow["���п���"];
            drowNew["BankName"] = drow["������"];
            drowNew["Telephone"] = drow["�绰"];
            drowNew["RoomFlag"] = RoomFlag;
            drowNew["RoomNo"] = drow["�����"];
            drowNew["Description"] = drow["����"];
            this.dtblPsnOnjob.Rows.Add(drowNew);
            

        }

        void btnExportOnjob_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("��������Excel�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��ְԱ��");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdvOnjob, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, true);
            FrmMsg.Hide();
            excel.Show();
        }

        void btnExportOutjob_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("��������Excel�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��ְԱ��");
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdvOffjob , ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            excel.SetRangeAutoFit(3, 1, rowIndex, colIndex, true, true);
            FrmMsg.Hide();
            excel.Show();
        }


    }
}