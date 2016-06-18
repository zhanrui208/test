using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material
{
    public partial class FrmOutStoreReplenishAdjust : Form
    {
        public FrmOutStoreReplenishAdjust()
        {
            InitializeComponent();     
            this.btnSave.Click += new EventHandler(btnSave_Click);
         
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
       
        public void AdjustOper()
        {
            this.ctrlOutStoreAjust.AdjustOper();
            this.ctrlOEMOutStoreAdjust .AdjustOper ();
            
            
        } 
      
      
        void btnSave_Click(object sender, EventArgs e)
        {
            
            DialogResult rul = MessageBox.Show("�����б�������һ�����˲��ܱ����", "����ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            this.ctrlOutStoreAjust .Save();
            this.ctrlOEMOutStoreAdjust .Save();  
            MessageBox.Show("�ɹ����沢���˵�ǰ����");
            if (this.affterSave != null) this.affterSave();
            this.Close();

        }
  
     

    }
}