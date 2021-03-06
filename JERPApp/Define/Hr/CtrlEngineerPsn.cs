using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Define.Hr
{
    public partial class CtrlEngineerPsn : UserControl
    {

        public CtrlEngineerPsn()
        {
            InitializeComponent();
            this.accPersonnel = new JERPData.Hr.Personnel();     
            this.cmbItem.SelectedIndexChanged +=new EventHandler(cmbItem_SelectedIndexChanged);
            this.RefreshDate();
           
        }
        private DataTable dtblPsns;
        private JERPData.Hr .Personnel accPersonnel;
        private void RefreshDate()
        {
           this.dtblPsns = this.accPersonnel.GetDataPersonnelForEngineer ().Tables[0];
           if (this.dtblPsns.Rows.Count > 1)
           {
               DataRow drow = this.dtblPsns.NewRow();
               drow["PsnID"] = -1;
               drow["PsnCode"] = "";
               drow["PsnName"] = "";
               this.dtblPsns.Rows.InsertAt(drow, 0);

           }
           this.dtblPsns.Columns.Add("ErpCode", typeof(string), "ISNULL(AssistantCode,'')+' '+ISNULL(PsnName,'')");
           JCommon.Others.SetCtrlBindSrc(this.cmbItem, dtblPsns, "PsnID", "ErpCode");
        }    
        public int PsnID
        {
            get
            {
                if (this.cmbItem.SelectedIndex == -1) return -1;
                return (int)this.cmbItem.SelectedValue;               
            }
            set
            {
                this.cmbItem.SelectedValue = value;
            }
        }
        public string  PsnName
        {
            get
            {
                if (this.cmbItem.SelectedIndex == -1) return string.Empty ;
                return this.dtblPsns.Rows[this.cmbItem.SelectedIndex]["PsnName"].ToString();
            }
           
        }
        public delegate void AffterSelectedDelegate(int PsnID);
        private AffterSelectedDelegate affterSelected;
        public event AffterSelectedDelegate AffterSelected
        {
            add
            {
                affterSelected += value;
            }
            remove
            {
                affterSelected -= value;
            }
        }
        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (affterSelected != null) this.affterSelected(this.PsnID);
        }
    }
}
