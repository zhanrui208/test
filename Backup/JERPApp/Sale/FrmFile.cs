using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Sale
{
    public partial class FrmFile : Form
    {
        public FrmFile()
        {
            InitializeComponent();
            this.SetPermit();

        }
        //Ȩ����
        private bool enableBrowse = false;//���
        private bool enableSave = false;//����
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(147);
            this.enableSave = JERPBiz.Frame.PermitHelper.EnableFunction(148);
            if (this.enableBrowse)
            {
                string dir = JERPData.ServerParameter.ERPFileFolder  + @"\Sale\Public";
                this.ctrlFileBrowse.Browse(dir);
            }
            this.ctrlFileBrowse.ReadOnly = !this.enableSave;

        }
    }
}