using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture
{
    public partial class FrmManufPlanAdjust : Form
    {
        public FrmManufPlanAdjust()
        {
            InitializeComponent();
        
            this.SetPermit();
        } 
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(321);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(322);
            if (this.enableBrowse)
            {
                this.ctrlManufPlanAdjust.LoadData();
                this.ctrlPackingPlanAdjust.LoadData();
               
            }

            this.ctrlManufPlanAdjust.Enabled = this.enableSave;
            this.ctrlPackingPlanAdjust.Enabled = this.enableSave;
        } 
    }
}