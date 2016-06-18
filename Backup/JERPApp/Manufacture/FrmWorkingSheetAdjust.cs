using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Manufacture
{
    public partial class FrmWorkingSheetAdjust : Form
    {
        public FrmWorkingSheetAdjust()
        {
            InitializeComponent();
           
            this.SetPermit();
        }
        
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(350);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(351);
            if (this.enableBrowse)
            {
                this.ctrlWorkingSheetAdjust.LoadData();
                this.ctrlPackingSheetAdjust.LoadData();
             
            }
            this.ctrlWorkingSheetAdjust.Enabled = this.enableSave;
            this.ctrlPackingSheetAdjust.Enabled = this.enableSave;
        }

      
    }
}