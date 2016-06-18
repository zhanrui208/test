using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Engineer
{
    public partial class FrmSupplierPrdCode : Form
    {
        public FrmSupplierPrdCode()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }
        
       
        private int PrdID = -1;
        public void SupplierPrdCode(int PrdID)
        {
            this.PrdID = PrdID;
            this.ctrlSupplierPrdCode.SupplierPrdCode(PrdID);
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            this.ctrlSupplierPrdCode.Save(this.PrdID);
            MessageBox.Show("�ɹ����浱ǰ��Ʒ��Ӧ���Ϻ�");
            this.Close();
        }
    }
}