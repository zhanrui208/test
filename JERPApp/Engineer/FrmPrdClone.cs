using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer
{
    public partial class FrmPrdClone : Form
    {
        public FrmPrdClone()
        {
            InitializeComponent();
          
            this.accPrds = new JERPData.Product.Product();
            this.accBOM = new JERPData.Product.BOM(); 
            this.accPackingType = new JERPData.Product.PackingType();
            this.accParmValues = new JERPData.Product.ParmValues();
            this.accManufProcess = new JERPData.Manufacture.ManufProcess();
            this.accDevelopProcess = new JERPData.Technology.DevelopProcess();
            this.accSupplierPrdCode = new JERPData.Product.SupplierPrdCode();
            this.ctrlPrdSrcID.AffterSelected += ctrlPrdSrc_AffterSelected;
            this.btnClose.Click += new EventHandler(btnClose_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
           
        }
        private JERPData.Product.Product accPrds;
        private JERPData.Product.BOM accBOM; 
        private JERPData.Product.PackingType accPackingType;
        private JERPData.Product.ParmValues accParmValues;
        private JERPData.Manufacture.ManufProcess accManufProcess;
        private JERPData.Technology.DevelopProcess accDevelopProcess;
        private JERPData.Product.SupplierPrdCode accSupplierPrdCode;
        private int SrcPrdID
        {
            get
            {
                return this.ctrlPrdSrcID.PrdID;
            }
        }
        private int newPrdID = -1;
        private int NewPrdID
        {
            get
            {
                return newPrdID;
            }
            set
            {
                this.newPrdID = value;
            }
        }
        
        void ctrlPrdSrc_AffterSelected()
        {
            this.ctrlPrdTypeID.PrdTypeID = this.ctrlPrdSrcID.PrdEntity.PrdTypeID;
            this.txtPrdCode.Text = this.ctrlPrdSrcID.PrdEntity.PrdCode;
            this.txtPrdName.Text = this.ctrlPrdSrcID.PrdEntity.PrdName;
            this.txtPrdSpec.Text = this.ctrlPrdSrcID.PrdEntity.PrdSpec;
            this.txtSurface.Text = this.ctrlPrdSrcID.PrdEntity.Surface;
            this.txtModel.Text = this.ctrlPrdSrcID.PrdEntity.Model;
            this.txtManufacturer.Text = this.ctrlPrdSrcID.PrdEntity.Manufacturer;
            this.txtDWGNo.Text = this.ctrlPrdSrcID.PrdEntity.DWGNo;
            this.txtAssistantCode.Text = this.ctrlPrdSrcID.PrdEntity.AssistantCode;
            this.txtICSolution.Text = this.ctrlPrdSrcID.PrdEntity.ICSolution; 
            this.txtMinPackingQty.Text = string.Empty;
            if (this.ctrlPrdSrcID.PrdEntity.MinPackingQty > 0)
            {
                this.txtMinPackingQty.Text = string.Format("{0:0.######}", this.ctrlPrdSrcID.PrdEntity.MinPackingQty);
            }
            
            if (this.ctrlPrdSrcID.PrdEntity.PrdWeight  > -1)
            {
                this.txtPrdWeight.Text = string.Format("{0:0.######}", this.ctrlPrdSrcID.PrdEntity .PrdWeight );
            }
            else
            {
                this.txtPrdWeight.Text = string.Empty;
            }
            this.txtRegisterPsn.Text = this.ctrlPrdSrcID.PrdEntity.RegisterPsn;
            this.ckbSaleFlag.Checked = this.ctrlPrdSrcID.PrdEntity.SaleFlag;
            this.ckbTaxfreeFlag.Checked = this.ctrlPrdSrcID.PrdEntity.TaxfreeFlag;
            this.ckbRohsFlag.Checked = this.ctrlPrdSrcID.PrdEntity.RohsFlag;
            this.ckbRohsRequireFlag.Checked = this.ctrlPrdSrcID.PrdEntity.RohsRequireFlag;
            this.ctrlUnitID.UnitID = this.ctrlPrdSrcID.PrdEntity.UnitID;
            this.txtSurface.Text = this.ctrlPrdSrcID.PrdEntity.Surface; 
            this.txtVersionCode.Text = this.ctrlPrdSrcID.PrdEntity.VersionCode;
            this.txtFileCode.Text = this.ctrlPrdSrcID.PrdEntity.FileCode;
            if (this.ctrlPrdSrcID .PrdEntity.DateRegister != DateTime.MinValue)
            {
                this.dtpDateRegister.Value = this.ctrlPrdSrcID.PrdEntity.DateRegister;
            }
            this.rchVersionRecord.Text = this.ctrlPrdSrcID.PrdEntity.VersionRecord;
            this.ckbStopFlag.Checked = this.ctrlPrdSrcID.PrdEntity.StopFlag;
            this.rchMemo.Text = this.ctrlPrdSrcID.PrdEntity.Memo;
        }


        private int GetPrdID(string PrdCode)
        {
            int PrdID = -1;
            this.accPrds.GetParmProductPrdID(PrdCode, ref PrdID);
            return PrdID;
        }
      
       
        //���Ʋ�Ʒ
        private void NewPrd()
        {
            bool flag = false;
            string errormsg = string.Empty;
            object objPrdID = DBNull.Value;
                    
            if (this.GetPrdID(this.txtPrdCode.Text) > -1)
            {
                MessageBox.Show("�˲�Ʒ����Ѵ���....");
                return;
            }
            object objMinPackingQty = DBNull.Value;
            decimal MinPackingQty=0;
            if (decimal.TryParse(this.txtMinPackingQty.Text, out MinPackingQty))
            {
                objMinPackingQty = MinPackingQty;   
            }
            object  objPrdWeight = DBNull.Value;
            decimal PrdWeight;
            if (decimal.TryParse(this.txtPrdWeight.Text, out PrdWeight))
            {
                objPrdWeight = PrdWeight;
            }
            if (this.GetPrdID(this.txtPrdCode.Text) > -1)
            {
                MessageBox.Show("�˲�Ʒ����Ѵ���....");
                return;
            }
            
            flag = this.accPrds.InsertProduct(
                    ref errormsg,
                    ref objPrdID,
                    this.ctrlPrdTypeID.PrdTypeID,
                    this.txtPrdCode.Text,
                    this.txtPrdName.Text,
                    this.txtPrdSpec.Text,
                    this.txtModel.Text,
                    this.txtSurface .Text , 
                    this.txtManufacturer .Text ,
                    this.txtAssistantCode.Text,
                    this.txtDWGNo.Text,
                    this.ckbTaxfreeFlag .Checked ,
                    this.ckbRohsFlag .Checked ,
                    this.ckbRohsRequireFlag .Checked ,
                    this.txtICSolution .Text ,
                    objPrdWeight ,
                    this.ckbSaleFlag.Checked,                 
                   this.ctrlUnitID.UnitID,
                   this.dtpDateRegister.Value.Date,
                   this.txtFileCode.Text,
                   this.txtRegisterPsn.Text,
                   this.txtVersionCode.Text,
                   this.rchVersionRecord.Text,
                   objMinPackingQty , 
                   this.txtURL .Text ,
                   this.rchMemo.Text ,
                   this.ckbStopFlag.Checked  
                   );
            if (flag)
            {
                this.NewPrdID = (int)objPrdID;
            }
            else
            {
                MessageBox.Show(errormsg);
                this.NewPrdID = -1;
            }
        }

    
        //���������嵥
        private void SetBOM()
        {
            bool flag = false;
            string errormsg = string.Empty;
            if (this.NewPrdID == -1) return;
            flag = this.accBOM.InsertBOMForCopy(ref errormsg, this.SrcPrdID , this.NewPrdID );
            if (!flag)
            {
                MessageBox.Show(errormsg);
            }
        }
         
        private void SetPackage()
        {
            bool flag = false;
            string errormsg = string.Empty;
            if (this.NewPrdID == -1) return;
            flag = this.accPackingType.InsertPackingTypeForCopy (ref errormsg, this.SrcPrdID , this.NewPrdID );
            if (!flag)
            {
                MessageBox.Show(errormsg);
            }
        }
      
        private void SetParm()
        {
            string errormsg = string.Empty;
            if (this.NewPrdID == -1) return;
            this.accParmValues.InsertParmValuesForCopy(
                ref errormsg,
                this.SrcPrdID ,
                this.NewPrdID );
        }
        private void SetSupplierPrdCode()
        {
            string errormsg = string.Empty;
            if (this.NewPrdID == -1) return;
            this.accSupplierPrdCode.InsertSupplierPrdCodeForCopy(ref errormsg,
                this.SrcPrdID,
                this.NewPrdID);
        }
        private void SetManufProcess()
        {
            string errormsg = string.Empty;
            if (this.NewPrdID == -1) return;
            this.accManufProcess.InsertManufProcessForCopy (
                ref errormsg,
                this.SrcPrdID,
                this.NewPrdID);
            this.accPrds.UpdateProductForManufProcessList(ref errormsg, this.NewPrdID);
        }
        private void SetDevelopProcess()
        {
            string errormsg = string.Empty;
            if (this.NewPrdID == -1) return;
            this.accDevelopProcess.InsertDevelopProcessForClone (
                ref errormsg,
                this.SrcPrdID,
                this.NewPrdID);
           
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
        void btnSave_Click(object sender, EventArgs e)
        {
            //���һ���Ƿ��ѽ�������
            int PrdID = -1;
            this.accPrds.GetParmProductPrdID (this.txtPrdCode.Text,ref PrdID);
            if (PrdID > 0)
            {
                MessageBox.Show("�Բ��𣬴˲�Ʒ����Ѵ��ڣ�");
                return;
            }
            string msg = "��Ҫͨ�����������²�Ʒ,�����ż�Ʒ����\n" +
                "�˸��ƽ����������״��:\n" +
                 "1.����һ���²�Ʒ;\n" +
                 "2.�²�Ʒ���˱��ʱ�䡢��š�Ʒ��, �����ɫ�⣬������ϢҲԴ��Ʒ��ͬ;\n" +
                 "3.�²�Ʒ�����Ͻṹ��ͬ;\n" +
                 "4.�²�Ʒ�ļ���������Դ������ͬ;\n" +
                 "5.�²�Ʒ�Ĺ�����Դ������ͬ;\n" +
                 "6.��װ��Դ��װ��ͬ;\n" + 
                 "7.����������Դ��Ʒ��ͬ;\n" + 
                 "8.��Ӧ����Դ��Ʒ��ͬ;\n" +
                 "9.����²�Ʒ��ͬ,���޸�֮;\n" +
                "�ǣ���¡����ȡ��!";
            DialogResult rlt = MessageBox.Show(msg, "����ȷ��",
MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.No) return;           
            this.NewPrd();

            this.SetManufProcess();
            this.SetBOM(); 
            this.SetPackage();
            this.SetParm();
            this.SetSupplierPrdCode();
            this.SetDevelopProcess();
            if (this.NewPrdID > -1)
            {
                MessageBox.Show("�ɹ������˵�ǰ��Ʒ�������Դ�汾�в�֮ͬ��������Ҳ��޸�֮!");
                if (this.affterSave != null) this.affterSave();
                this.Close();
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}