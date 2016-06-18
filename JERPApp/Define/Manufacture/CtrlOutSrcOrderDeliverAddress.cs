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
    public partial class CtrlOutSrcOrderDeliverAddress : UserControl
    {
        public CtrlOutSrcOrderDeliverAddress()
        {
            InitializeComponent();
            this.accOrderNotes = new JERPData.Manufacture.OutSrcOrderNotes();
            this.LoadData();
        }
        private JERPData.Manufacture.OutSrcOrderNotes  accOrderNotes;
        private DataTable dtblDeliverAddress;
        public  void LoadData()
        {
            this.cmbItem.Items.Clear();
            this.dtblDeliverAddress = this.accOrderNotes .GetDataOutSrcOrderNotesDeliverAddress().Tables[0];
            foreach (DataRow drow in this.dtblDeliverAddress.Rows)
            {
                this.cmbItem.Items.Add(drow["DeliverAddress"].ToString());
            }
            if (this.cmbItem.Items.Count > 0)
            {
                this.cmbItem.SelectedIndex = 0;
            }
        }       
        public string DeliverAddress
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