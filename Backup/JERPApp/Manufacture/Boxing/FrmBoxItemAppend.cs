using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture.Boxing
{
    public partial class FrmBoxItemAppend : Form
    {
        public FrmBoxItemAppend()
        {
            InitializeComponent(); 
            this.accBoxItems = new JERPData.Packing.BoxItems();
            this.dtpDateNote.Value = DateTime.Now.Date;
            this.PrinterHelper = new JERPBiz.Packing.BoxItemPrinterHelper();
            this.btnSave .Click +=new EventHandler(btnSave_Click);
        }
        private JERPData.Packing.BoxItems accBoxItems;
        private JERPBiz.Packing.BoxItemPrinterHelper PrinterHelper;
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
        public  void New()
        {
            this.ctrlPrdID.PrdID = -1;
            this.txtCount.Text = string.Empty;
        }
        private bool ValidateData()
        {
            if (this.ctrlPrdID.PrdID == -1)
            {
                MessageBox.Show("��Ʒ����Ϊ��");
                return false;
            }
            int count;
            if (!int.TryParse(this.txtCount.Text, out count))
            {
                MessageBox.Show("������ʽ����!");
                return false ;
            }
            if (count < 0)
            {

                MessageBox.Show("��������С��0!");
                return false;
            }
            return true;
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("��������������룬һ�����ɲ��ܱ������ע�����ڼ���������", "����ȷ��", MessageBoxButtons.OKCancel , MessageBoxIcon.Question);
            if (rul == DialogResult.Cancel) return;
            object objBarcode = DBNull.Value;
            string errormsg=string.Empty ;
            bool flag = false;
            int count = int.Parse(this.txtCount.Text);
            for (int i = 0; i < count; i++)
            { 
                flag=this.accBoxItems.InsertBoxItems  (ref errormsg, 
                    DBNull .Value ,
                    this.ctrlPrdID.PrdID,
                    this.dtpDateNote .Value .Date );
               

            }
            rul = MessageBox.Show("�ɹ����ɲ�Ʒ��ǩ����Ҫ������ӡ������������ֽ�Ƿ���","����ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rul == DialogResult.Yes)
            {
                DataTable dtblNonPrint = this.accBoxItems.GetDataBoxItemsNonPrint().Tables[0];
                this.PrinterHelper.Export(dtblNonPrint.Select());
            } 
            if (this.affterSave != null) this.affterSave();
            this.ctrlPrdID.PrdID = -1;
            this.txtCount.Text = string.Empty;
            this.Close();
          
        }
    }
}