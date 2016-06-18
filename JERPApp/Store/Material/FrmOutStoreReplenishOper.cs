using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Store.Material
{
    public partial class FrmOutStoreReplenishOper : Form
    {
        public FrmOutStoreReplenishOper()
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
       
        public void NewNote()
        {
           
            this.ctrlOutStoreOper.NoteOper();
            this.ctrlOutOEMStoreOper.NoteOper();
            
        } 
      
        private bool ValidateData()
        {
            
            string errormsg = string.Empty;
            if (!this.ctrlOutStoreOper.ValidateData(out errormsg))
            {
                MessageBox.Show(errormsg );
                return false;
            }
            if (!this.ctrlOutOEMStoreOper.ValidateData(out errormsg))
            {
                MessageBox.Show(errormsg);
                return false;
            }
            return true;
        } 
        void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateData() == false) return;
            DialogResult rul = MessageBox.Show("�����б�������һ�����˲��ܱ����", "���ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rul == DialogResult.No) return;
            string errormsg = string.Empty;
           
            this.ctrlOutStoreOper.Save();
            this.ctrlOutOEMStoreOper.Save();  
            MessageBox.Show("�ɹ����沢���˵�ǰ����");
            if (this.affterSave != null) this.affterSave();
            this.Close();

        }
  
     

    }
}