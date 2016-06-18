using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
namespace JERPApp.Define.Manufacture
{
    /// <����>
    /// ��[OutSrcOrderNotesDeliverAddress]�ؼ���
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/6/29 14:32:52
    ///</ʱ��> 
    public partial class CtrlOutSrcOrderSupplierAddress : UserControl
    {
        public CtrlOutSrcOrderSupplierAddress()
        {
            InitializeComponent();
            this.accOrderNotes = new JERPData.Manufacture.OutSrcOrderNotes();
        }
        private JERPData.Manufacture.OutSrcOrderNotes  accOrderNotes;
        private DataTable dtblSupplierAddress;
        public  void LoadData(int CompanyID)
        {
            this.cmbItem.Items.Clear();
            this.dtblSupplierAddress = this.accOrderNotes .GetDataOutSrcOrderNotesSupplierAddress(CompanyID).Tables[0];
            foreach (DataRow drow in this.dtblSupplierAddress.Rows)
            {
                this.cmbItem.Items.Add(drow["SupplierAddress"].ToString());
            }
            if (this.cmbItem.Items.Count > 0)
            {
                this.cmbItem.SelectedIndex = 0;
            }
        }
        public string SupplierAddress
        {
            get
            {
                return this.cmbItem.Text;
            }
            set
            {
                this.cmbItem.Text = value;
            }
        }
    
    }
}