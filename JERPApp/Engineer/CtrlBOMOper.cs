using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer
{
    public partial class CtrlBOMOper : UserControl
    {
        public CtrlBOMOper()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.ctrlQFind.SeachGridView = this.dgrdv;
            this.accBOM = new JERPData.Product.BOM();
            this.prdEntity = new JERPBiz.Product.ProductEntity();
            this.accPrds = new JERPData.Product.Product();
            this.accSourceType = new JERPData.Product.SourceType();
            this.ManufProcessEntity = new JERPBiz.Manufacture.ManufProcessEntity();
            this.accManufProcess = new JERPData.Manufacture.ManufProcess();
            this.dgrdv.CellContentClick += new DataGridViewCellEventHandler(dgrdv_CellContentClick);
            this.dgrdv.DataError += new DataGridViewDataErrorEventHandler(dgrdv_DataError);
            this.dgrdv.CellContextMenuStripNeeded +=dgrdv_CellContextMenuStripNeeded;
            this.btnImport.Click += new EventHandler(btnImport_Click);
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            this.dgrdv.CellValueChanged += new DataGridViewCellEventHandler(dgrdv_CellValueChanged);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);

            this.btnClone.Click += new EventHandler(btnClone_Click); 
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);
            this.lnkMemo.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkMemo_LinkClicked);
            this.SetColumnDataSrc();
            this.lnkFileCount.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkFileCount_LinkClicked);
           
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
        }

       
        private JERPBiz.Product.ProductEntity prdEntity;
        private JERPData.Product.Product accPrds;
        private JERPData.Product.BOM accBOM;
        private JERPData.Manufacture.ManufProcess accManufProcess;
        private JERPData.Product.SourceType accSourceType;
        private JERPApp.Define.Manufacture.FrmProcessParmType frmParmType; 
        private JCommon.FrmFileBrowse frmFileBrowse;
        private JCommon.FrmExcelImport frmImport;
        private JERPApp.Define.Product.FrmProduct frmPrd;
        private JERPApp.Define.Product.FrmProduct frmPrdAlter;
        private JERPBiz.Manufacture.ManufProcessEntity ManufProcessEntity;
        private FrmBOMClone frmClone;
        private FrmProductOper frmOper;
        private DataTable dtblSourceType; 
        private int GetDefaultSourceTypeID(int PrdID)
        {
            int rut = 1;
            this.accBOM.GetParmBOMDefaultSourceTypeID(PrdID, ref rut);
            return rut;
        }

        public delegate void AffterDeleteDelegate(object sender);
        private AffterDeleteDelegate affterDelete;
        public event AffterDeleteDelegate AffterDelete
        {
            add
            {
                affterDelete += value;
            }
            remove
            {
                affterDelete -= value;
            }
        }
     
      
    
        private void SetColumnDataSrc()
        { 
            this.dtblSourceType = this.accSourceType.GetDataSourceType().Tables[0];
            this.ColumnSourceTypeID.DataSource = this.dtblSourceType;
            this.ColumnSourceTypeID.ValueMember = "SourceTypeID";
            this.ColumnSourceTypeID.DisplayMember = "SourceTypeName";
        }
        private DataTable dtblBom;
        private long manufprocessid = -1;
        private   long ManufProcessID
        {
            get
            {
                return this.manufprocessid;
            }
            set
            {
                this.manufprocessid = value;
                this.btnImport.Enabled = (value > -1);
                this.btnAdd.Enabled = (value > -1);
                this.btnClone.Enabled = (value > -1);
                this.btnDelete.Enabled = (value > -1);

            }
        }
        private bool GetParentFlag(int PrdID)
        {
            bool rut = false;
            this.accPrds.GetParmProductParentFlag(PrdID, ref rut);
            return rut;
        }
        void lnkMemo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmParmType == null)
            {
                frmParmType = new JERPApp.Define.Manufacture.FrmProcessParmType();
                new FrmStyle(frmParmType).SetPopFrmStyle(this.ParentForm);
                frmParmType.AffterConfirm += new JERPApp.Define.Manufacture.FrmProcessParmType.AffterConfirmDelegate(frmParmType_AffterConfirm);
            }
            frmParmType.LoadData(this.ctrlProcessTypeID.ProcessTypeID );
            frmParmType.ShowDialog();
        }
        void frmParmType_AffterConfirm(string ParmValueInfor)
        {
            this.rchMemo.Text = ParmValueInfor;
        }
      
        private void LoadBom()
        {
            if (this.dtblBom!=null) this.dtblBom.Clear();
          
            this.dtblBom = this.accBOM.GetDataBOMByManufProcessID (this.ManufProcessID ).Tables[0];
            this.dtblBom.Columns["AssemblyQty"].DefaultValue = 1;
            this.dtblBom.Columns["LossRate"].DefaultValue = 0;
            this.dtblBom.Columns["FixedFlag"].DefaultValue = true; 
            this.dtblBom.Columns.Add("Mark",typeof (Image));
            this.dtblBom.Columns["ParentFlag"].DefaultValue = false;
            foreach (DataRow drow in this.dtblBom.Rows)
            {
                if ((bool)drow["ParentFlag"])
                {
                    drow["Mark"] = global::JERPApp.Properties.Resources.plus;
                }
                else
                {
                    drow["Mark"] = global::JERPApp.Properties.Resources.subtract;
                }
            }
            this.dgrdv.DataSource = this.dtblBom;
        }    
        public void BomOper(long ManufProcessID)
        {

            this.ManufProcessID = ManufProcessID;
            this.ManufProcessEntity.LoadData(ManufProcessID);
            this.txtSerialNo.Text = this.ManufProcessEntity.SerialNo.ToString();
            this.ctrlProcessTypeID.ProcessTypeID = this.ManufProcessEntity.ProcessTypeID;
            this.ckbOutSrcFlag.Checked = this.ManufProcessEntity.OutSrcFlag;
            this.ctrlSupplierID.CompanyID = this.ManufProcessEntity.OutSrcCompanyID;
            this.rchMemo.Text = this.ManufProcessEntity.Memo;
            this.txtMouldCode.Text = this.ManufProcessEntity.MouldCode;
            this.lnkFileCount.Text = this.ManufProcessEntity.FileCount.ToString();
            this.LoadBom();
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadBom();
        }

        void dgrdv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //ɶҲ��Ҫ��,��ʱ�������֮����
        }

        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {               
                grow.ErrorText = string.Empty;
              
                int PrdID = (int)grow.Cells[this.ColumnPrdID.Name].Value;
                if (PrdID ==-1)
                {
                    grow.ErrorText = "�����ڴ˲�Ʒ";
                    continue;
                }         
                if (grow.Cells[this.ColumnPrdID.Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "��Ʒ����Ϊ��";
                    continue;
                }
                if (grow.Cells[this.ColumnSourceTypeID .Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "��Դ��ʽ����Ϊ��";
                    continue;
                }
                if (grow.Cells[this.ColumnAssemblyQty .Name].Value == DBNull.Value)
                {
                    grow.ErrorText = "��������Ϊ��";
                    continue;
                }
                
            }
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.frmPrd  == null)
            {
                this.frmPrd = new JERPApp.Define.Product.FrmProduct();
                this.frmPrd.AllowAppendFlag = true;
                new FrmStyle(this.frmPrd).SetPopFrmStyle(this.ParentForm);
                this.frmPrd .AffterSelected+=new JERPApp.Define.Product.FrmProduct.AffterSelectedDelegate(frmPrd_AffterSelected);
            }
            this.frmPrd.ShowDialog(); 
        }
        void frmPrd_AffterSelected(DataRow drow)
        {
            if (this.GetAllowChildFlag((int)drow["PrdID"]) == false)
            {
                MessageBox.Show("�Բ���,�˲�ƷΪ��ǰ��Ʒ���ϼ���Ʒ,��������");
                return;
            }
            DataRow drowNew = this.dtblBom.NewRow(); 
            drowNew["PrdID"] = drow["PrdID"];
            drowNew["PrdCode"] = drow["PrdCode"];
            drowNew["PrdName"] = drow["PrdName"];
            drowNew["PrdSpec"] = drow["PrdSpec"];
            drowNew["Model"] = drow["Model"];
            drowNew["Manufacturer"] = drow["Manufacturer"];
            drowNew["UnitName"] = drow["UnitName"];
            drowNew["AssemblyQty"] = 1;
            drowNew["SourceTypeID"] =this.GetDefaultSourceTypeID ((int)drow["PrdID"]);
            bool ParentFlag = this.GetParentFlag((int)drow["PrdID"]);
            drowNew["ParentFlag"] = ParentFlag;
            if (ParentFlag)
            {
                drowNew["Mark"] = global::JERPApp.Properties.Resources.plus;
                drowNew["SourceTypeID"] = 1;
            }
            else
            {
                drowNew["Mark"] = global::JERPApp.Properties.Resources.subtract;
            }
            this.dtblBom.Rows.Add(drowNew); 
        }
        void btnClone_Click(object sender, EventArgs e)
        {
         
            if (frmClone == null)
            {
                frmClone = new FrmBOMClone();
                new FrmStyle(frmClone).SetPopFrmStyle(this.ParentForm );
                frmClone.AffterImport += new FrmBOMClone.AffterImportSaveDelegate(frmClone_AffterImport);
            }
            frmClone.BOMClone  ();
            frmClone.ShowDialog();
        }

        void frmClone_AffterImport(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            if (this.dtblBom.Select("PrdID=" + PrdID.ToString(), "", DataViewRowState.CurrentRows).Length > 0)
            {
                MessageBox.Show("�Բ���,�Ѵ��ڴ˲�Ʒ");
                return;
            }
            if (this.GetAllowChildFlag((int)drow["PrdID"]) == false)
            {
                MessageBox.Show("�Բ���,�˲�ƷΪ��ǰ��Ʒ���ϼ���Ʒ,��������");
                return;
            }
            drow["ID"] = DBNull.Value;
            drow["ManufProcessID"] = DBNull.Value;
            this.dtblBom.ImportRow(drow); 
        }

      
        void dgrdv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].DataPropertyName == "PrdID")
            {

                object objPrdID = this.dgrdv[icol, irow].Value;
                if ( (objPrdID != null)&&(objPrdID != DBNull.Value))
                {
                    if (this.GetAllowChildFlag((int)objPrdID) == false)
                    {
                        MessageBox.Show("�˲�ƷΪ����Ʒ����һ����Ʒ����������");
                        this.dgrdv[icol, irow].Value = DBNull.Value;
                    }
                    this.dgrdv[this.ColumnSourceTypeID.Name, irow].Value =
                        this.GetDefaultSourceTypeID((int)objPrdID);
                    bool ParentFlag = this.GetParentFlag((int)objPrdID);
                    this.dgrdv[this.ColumnParentFlag.Name, irow].Value = ParentFlag;
                    if (ParentFlag)
                    {
                       this.dgrdv[this.ColumnSourceTypeID.Name, irow].Value = 1;
                       this.dgrdv [this.ColumnMark .Name ,irow].Value = global::JERPApp.Properties.Resources.plus;
                    }
                    else
                    {
                        this.dgrdv[this.ColumnMark.Name, irow].Value = global::JERPApp.Properties.Resources.subtract;
                    }
                }
            }
        }
       
     
     
        void dgrdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if (this.dgrdv.Columns[icol].Name==this.ColumnMark.Name)
            {
                object objPrdID = this.dgrdv[this.ColumnPrdID.Name, irow].Value;
                if ((objPrdID != null) && (objPrdID != DBNull.Value))
                {
                    if (frmOper  == null)
                    {
                        frmOper  = new FrmProductOper();
                        new FrmStyle(frmOper).SetPopFrmStyle(this.ParentForm); 
                    }
                    frmOper.Edit((int)objPrdID);
                    frmOper.ShowDialog();

                }
            }
        }
        void dgrdv_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            if ((this.dgrdv.Columns[icol].DataPropertyName == "PrdCode")
                ||(this.dgrdv.Columns[icol].DataPropertyName == "PrdName")
                 ||(this.dgrdv.Columns[icol].DataPropertyName == "PrdSpec")
                 ||(this.dgrdv.Columns[icol].DataPropertyName == "Model")
                 ||(this.dgrdv.Columns[icol].DataPropertyName == "Manufacturer")
                )
            {
                this.dgrdv.CurrentCell = this.dgrdv[icol, irow];
                if (this.frmPrdAlter  == null)
                {
                    this.frmPrdAlter = new JERPApp.Define.Product.FrmProduct();
                    this.frmPrdAlter.AllowAppendFlag = true;
                    new FrmStyle(this.frmPrdAlter).SetPopFrmStyle(this.ParentForm);
                    this.frmPrdAlter.AffterSelected += new JERPApp.Define.Product.FrmProduct.AffterSelectedDelegate(frmPrdAlter_AffterSelected);
                }
                this.frmPrdAlter .ShowDialog();
            }
        } 
        void frmPrdAlter_AffterSelected(DataRow drow)
        {
            int PrdID = (int)drow["PrdID"];
            if (this.GetAllowChildFlag(PrdID) == false)
            {
                MessageBox.Show("�Բ��𣬴������Ǳ����ϵĸ�����Ʒ�����ܱ��");
                return;
            }
           DataGridViewRow grow = this.dgrdv.CurrentRow;
           grow.Cells [this.ColumnPrdID .Name ].Value  =PrdID;
           grow.Cells[this.ColumnPrdCode.Name].Value = drow["PrdCode"];
           grow.Cells[this.ColumnPrdName.Name].Value = drow["PrdName"];
           grow.Cells[this.ColumnPrdSpec.Name].Value = drow["PrdSpec"];
           grow.Cells[this.ColumnModel.Name].Value = drow["Model"];
           grow.Cells[this.ColumnManufacturer.Name].Value = drow["Manufacturer"];
           grow.Cells[this.ColumnUnitName .Name].Value = drow["UnitName"];
           bool ParentFlag = this.GetParentFlag(PrdID);
           grow.Cells[this.ColumnParentFlag.Name].Value = ParentFlag;
           grow.Cells[this.ColumnSourceTypeID.Name].Value = this.GetDefaultSourceTypeID(PrdID);
           if (ParentFlag)
           {
               grow.Cells[this.ColumnMark .Name ].Value  = global::JERPApp.Properties.Resources.plus;
               grow.Cells[this.ColumnSourceTypeID .Name].Value = 1;
           }
           else
           {
                grow.Cells[this.ColumnMark .Name ].Value  = global::JERPApp.Properties.Resources.subtract;
           } 
           this.frmPrdAlter.Close();
        }
      
        void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row.Index;
            bool flag = false;
            DataRow drow = this.dtblBom.DefaultView[irow].Row;
            if (drow["ID"] == DBNull.Value)
            {
                return;
            }
            string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("���ɾ�������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                flag = this.accBOM.DeleteBOM(ref errormsg, drow["ID"]);
             
            }
            else
            {
                e.Cancel = true;
            }
        }

        private int GetPrdID(string PrdCode,string PrdName,string PrdSpec)
        {
            int PrdID = -1;
            this.accPrds.GetParmProductPrdID(PrdCode, ref PrdID);
            if (PrdID > -1) return PrdID;
            this.accPrds.GetParmProductPrdIDFromOtherInfor(PrdName, PrdSpec, ref PrdID);
            return PrdID;
        }
       
        private int GetSourceTypeID(string SourceTypeName)
        {
            int rut = 0;
            this.accSourceType.GetParmSourceTypeSourceTypeID(SourceTypeName, ref rut);
            return rut;
        }
        private bool GetAllowChildFlag(int PrdID)
        {
            bool flag = false;
            this.accPrds.GetParmProductAllowChildrenFlag(this.ManufProcessEntity .PrdID , PrdID, ref flag);
            return flag;
        }
        void btnImport_Click(object sender, EventArgs e)
        {            
            string errormsg = string.Empty;        
            if (this.frmImport == null)
            {
                frmImport = new JCommon.FrmExcelImport();
                new FrmStyle(frmImport).SetPopFrmStyle(this.ParentForm);
                DataColumn[] columns = new DataColumn[]{    
                    new DataColumn ("��Ʒ���",typeof (string)), 
                    new DataColumn ("��Ʒ����",typeof (string)), 
                    new DataColumn ("��Ʒ���",typeof (string)), 
                    new DataColumn ("��λ",typeof (string)), 
                    new DataColumn ("����",typeof (decimal)),                                              
                    new DataColumn ("�����%",typeof (decimal)),                       
                    new DataColumn ("��Դ��ʽ",typeof (string)),                                           
                    new DataColumn ("λ��",typeof (string)),         
                    new DataColumn ("��ע",typeof (string)),                                              
                    new DataColumn ("����",typeof (string)),       
                    new DataColumn ("�����",typeof (string)),    
                    new DataColumn ("����Ҫ��",typeof (string)),                        
                    new DataColumn ("�̶�",typeof (string))
                };

                frmImport.SetImportColumn(columns, "[��Ʒ���]�����Ƽ������Ϊ�� \n[����]�ǿ�����;�����%" +
                    "\n��Դ��ʽ:����;�ɹ�;�͹�;��Ӧ���Ա�;�̶�:�ǻ��,�մ�����");
                frmImport.ImportHandle += new JCommon.FrmExcelImport.ImportDelegate(frmImport_ImportHandle);
                frmImport.AffterSave += new JCommon.FrmExcelImport.AffterSaveDelegate(frmImport_AffterSave);
            }
            frmImport.ShowDialog();
        }
        void frmImport_AffterSave()
        {
            MessageBox.Show("������ɣ�����������û�г�����У��ٵ㱣��!");
            this.frmImport.Close();
        } 
  
        void frmImport_ImportHandle(DataRow drow, out bool flag, out string msg)
        {
            flag = true;
            msg = string.Empty;
            string errormsg = string.Empty; 
            int PrdID = this.GetPrdID(drow["��Ʒ���"].ToString(),drow["��Ʒ����"].ToString (),drow["��Ʒ���"].ToString ()) ;
            if ((PrdID > -1) && (this.dtblBom.Select("PrdID=" + PrdID.ToString(), "", DataViewRowState.CurrentRows).Length > 0))
            {
                return;
            } 
            decimal AssemblyQty = (decimal)drow["����"];
         
            object objID = DBNull.Value;
            decimal LossRate = 0;
            if (drow["�����%"] != DBNull.Value)
            {
                LossRate = (decimal)drow["�����%"] / 100;
            }          
            DataRow drowNew = this.dtblBom.NewRow(); 
            drowNew["PrdID"] = PrdID; 
            drowNew["PrdCode"] = drow["��Ʒ���"];
            drowNew["PrdName"] = drow["��Ʒ����"];
            drowNew["PrdSpec"] = drow["��Ʒ���"];
            this.prdEntity.LoadData(PrdID);
            drowNew["Model"] = this.prdEntity.Model ;
            drowNew["Manufacturer"] = this.prdEntity.Manufacturer;
            drowNew["UnitName"] = this.prdEntity.UnitName ;
            drowNew["AssemblyQty"] = AssemblyQty;
            string SourceTypeName = drow["��Դ��ʽ"].ToString();
            if (SourceTypeName == string.Empty)
            {
                if (this.GetParentFlag(PrdID))
                {
                    SourceTypeName = "����";
                }
                else
                {
                    SourceTypeName = "�ɹ�";
                }
            }
            drowNew["SourceTypeID"]=this.GetSourceTypeID (SourceTypeName);
            drowNew["Technology"] = drow["����Ҫ��"];
            drowNew["PostProcessing"] = drow["����"];
            drowNew["Substitute"] = drow["�����"];
            drowNew["LossRate"] = LossRate;
            drowNew["Element"] = drow["λ��"];
            if (drow["�̶�"] == DBNull.Value)
            {
                drowNew["FixedFlag"] = true;
            }
            else
            {
                drowNew["FixedFlag"] = (drow["�̶�"].ToString() == "��");
            }
            drowNew["Memo"] = drow["��ע"];
            if (this.GetParentFlag(PrdID))
            {
                drowNew["Mark"] = global::JERPApp.Properties.Resources.plus;
            }
            else
            {
                drowNew["Mark"] = global::JERPApp.Properties.Resources.subtract;
            }
            this.dtblBom.Rows.Add(drowNew);
          
        }

        void lnkFileCount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmFileBrowse == null)
            {
                frmFileBrowse = new JCommon.FrmFileBrowse();
                frmFileBrowse.ReadOnly = false;
                new FrmStyle(frmFileBrowse).SetPopFrmStyle(this.ParentForm);

            }
            frmFileBrowse.Browse(JERPData.ServerParameter.ERPFileFolder + @"\Engineer\ManufProcess\" + this.ManufProcessID .ToString());
            frmFileBrowse.ShowDialog();
            string errormsg = string.Empty;
            this.lnkFileCount .Text  = frmFileBrowse.Count.ToString ();
            this.accManufProcess.UpdateManufProcessForFileCount(
                ref errormsg,
                this.ManufProcessID,
                frmFileBrowse.Count);
        }

        private bool ValidateData()
        {
            int i;
            if(int.TryParse  (this.txtSerialNo .Text ,out i)==false)
            {
                MessageBox.Show("���������");
                return false;

            }
            foreach (DataGridViewRow grow in this.dgrdv.Rows)
            {
                if (grow.IsNewRow) continue;
                if (grow.ErrorText != string.Empty)
                {
                    MessageBox.Show("�����嵥��ϸ����,���ܱ����嵥");
                    return false;

                }
            }
            return true;
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
             string errormsg = string.Empty;
            DialogResult rul = MessageBox.Show("һ��ɾ����ǰ�������ϼ����������ָܻ�����ȷ�ϣ�", "ɾ��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes )
            {
               
                this.accManufProcess.DeleteManufProcess(ref errormsg,
                    this.ManufProcessID);
                if (this.affterDelete != null) this.affterDelete(this);
            }
        }

       
        public void Save()
        {            
          
            if (this.ValidateData() == false) return;

            string errormsg = string.Empty;
            bool flag = false;
            object objOutSrcCompanyID = DBNull.Value;
            if (this.ctrlSupplierID.CompanyID > -1)
            {
                objOutSrcCompanyID = this.ctrlSupplierID.CompanyID;
            }
          
            this.accManufProcess.UpdateManufProcess(ref errormsg,
                this.ManufProcessID,
                this.txtSerialNo.Text,
                this.ctrlProcessTypeID.ProcessTypeID,
                this.rchMemo.Text,
                this.txtMouldCode.Text,
                this.ckbOutSrcFlag .Checked ,
                objOutSrcCompanyID);
            foreach (DataRow drow in this.dtblBom.Rows)            
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;
                if (drow["PrdID"] == DBNull.Value) continue;
                if (drow["ID"] == DBNull.Value)
                {
                    object objID = DBNull.Value;
                    flag = this.accBOM.InsertBOM(ref errormsg,
                        ref objID,
                        this.ManufProcessEntity .PrdID ,
                        this.ManufProcessID ,
                        drow["PrdID"],
                        drow["AssemblyQty"],
                        drow["SourceTypeID"],
                        drow["Element"],
                        drow["Substitute"],
                        drow["Technology"],
                        drow["PostProcessing"],
                        drow["LossRate"],
                        drow["FixedFlag"],
                        drow["Memo"]);
                    if (flag)
                    {
                        drow["ID"] = objID; 
                    }
                }
                else
                {
                    
                    flag = this.accBOM.UpdateBOM(
                        ref errormsg,
                        drow["ID"],
                        this.ManufProcessID,
                        drow["PrdID"],
                        drow["AssemblyQty"],
                        drow["SourceTypeID"],
                        drow["Element"],
                        drow["Substitute"],
                        drow["Technology"],
                        drow["PostProcessing"],
                        drow["LossRate"],
                        drow["FixedFlag"],
                      drow["Memo"]);
                }
                if (flag)
                {
                    this.accManufProcess.UpdateManufProcessForMtrCostPrice(ref errormsg, this.ManufProcessID );   
                    
                }
                drow.AcceptChanges();
             
            }
         
        } 
    }
}