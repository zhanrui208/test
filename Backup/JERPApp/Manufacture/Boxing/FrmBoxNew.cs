using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JCommon;
namespace JERPApp.Manufacture.Boxing
{
    public partial class FrmBoxNew : Form
    {
        public FrmBoxNew()
        {
            InitializeComponent();
            this.accWorkingSheets = new JERPData.Packing.WorkingSheets();
            this.accBoxes = new JERPData.Packing.Boxes(); 
            this.WorkingSheetEntity = new JERPBiz.Packing .WorkingSheetEntity(); 
            this.BoxPrinter = new JERPBiz.Packing.BoxPrinterHelper();
            this.btnCreate.Click += new EventHandler(btnCreate_Click);
            this.txtOneBoxQty .TextChanged += new EventHandler(txtOneBoxQty_TextChanged);
        }

        private JERPData.Packing .WorkingSheets accWorkingSheets;
        private JERPData.Packing.Boxes accBoxes; 
        private JERPBiz.Packing .WorkingSheetEntity WorkingSheetEntity;
        private JERPBiz.Packing.BoxPrinterHelper BoxPrinter; 
        private long WorkingSheetID = -1;
        public void BarcodeOper(long WorkingSheetID)
        {
            this.WorkingSheetID = WorkingSheetID;
            this.WorkingSheetEntity.LoadData(WorkingSheetID);
            this.txtWorkingSheetCode.Text = this.WorkingSheetEntity.WorkingSheetCode; 
            this.txtPrdCode.Text = this.WorkingSheetEntity.PrdCode;
            this.txtPrdName.Text = this.WorkingSheetEntity.PrdName;
            this.txtPrdSpec.Text = this.WorkingSheetEntity.PrdSpec;
            this.txtPackingTypeName.Text = this.WorkingSheetEntity.PackingTypeName;
            this.lblUnitName.Text = this.WorkingSheetEntity.UnitName;
            this.txtBarcodeNonFinishedQty.Text = string.Format("{0:0}", this.WorkingSheetEntity.BarcodeNonFinishedQty); 
            this.txtBoxQty.Text = string.Empty;
            this.dtpDateNote.Value = DateTime.Now.Date;
            this.ctrlPackingType.LoadPacking(this.WorkingSheetEntity.PackingTypeID);
        }

        void txtOneBoxQty_TextChanged(object sender, EventArgs e)
        {
            int oneboxqty;
            if (!int.TryParse(this.txtOneBoxQty.Text, out oneboxqty))
            {
                return;
            }
            decimal BarcodeNonFinisehdQty=this.WorkingSheetEntity .BarcodeNonFinishedQty  ;
            this.txtBoxQty.Text = (Math.Ceiling(BarcodeNonFinisehdQty / oneboxqty)).ToString();
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
        private bool ValidateData()
        { 
            int i;
            if (!int.TryParse(this.txtBoxQty.Text, out i))
            {

                MessageBox.Show("������ʽ����");
                return false;
            }
             
            if (!int.TryParse(this.txtOneBoxQty .Text, out i))
            {

                MessageBox.Show("��������ʽ����");
                return false;
            }
            return true;
        }
        void btnCreate_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return ;
            DialogResult rul = MessageBox.Show("����������ű�ǩ��һ�����ɲ��ܱ��", "����ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rul == DialogResult.Cancel) return;
            object objBoxCode = DBNull.Value;
            string errormsg = string.Empty;
            bool flag = false;
            int boxqty = int.Parse(this.txtBoxQty.Text); 
            for (int i = 0; i < boxqty; i++)
            { 
                flag = this.accBoxes.InsertBoxes(ref errormsg, 
                    this.WorkingSheetID ,
                    this.WorkingSheetEntity.PrdID,
                    this.dtpDateNote .Value .Date );
            } 
            int oneboxqty = int.Parse(this.txtOneBoxQty.Text);
            this.accWorkingSheets.UpdateWorkingSheetsForAppendBarcodeFinishedQty(ref errormsg,
               this.WorkingSheetID,
               boxqty * oneboxqty
               );
             rul = MessageBox.Show("�ɹ����������ǩ����Ҫ������ӡ������������ֽ�Ƿ���","����ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
             if (rul == DialogResult.Yes)
             {
                 DataRow[] drowBoxs = this.accBoxes.GetDataBoxesNonPrint().Tables[0].Select();
                 this.BoxPrinter.Export(drowBoxs);
             } 
        
            if (this.affterSave != null) this.affterSave();
            this.Close();
        }
    }
}