using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Config
{
    public partial class FrmUserSelfAdm : Form
    {
       
        public FrmUserSelfAdm()
        {
            InitializeComponent();
            SetPermit();

        }
        //Ȩ����
        private bool enableBrowse = false;
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(2);
            this.UserID = JERPBiz.Frame.UserBiz.UserID;
            this.UserName = JERPBiz.Frame.UserBiz.UserName;
            this.UserPassword = JERPBiz.Frame.UserBiz.UserPassword;
        }
        private void txtBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        private short UserID = -1;
        private string UserName = string.Empty;
        private string UserPassword = string.Empty;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((this.UserName != this.txtUserName.Text)||(this.UserPassword !=this.txtOldPassword .Text ))
            {
                MessageBox.Show("�Բ���,����û��������벻�ԣ��㲻�ܸ��´��û�����");
                return;
            }
            if (this.txtNewPassword.Text != this.txtNewPasswordAgain.Text)
            {
                MessageBox.Show("�Բ�������������Ⱥ����벻��ȷ,���ܸ��´��û�����");
                return;
            }
            JERPData.Frame.Users acc = new JERPData.Frame.Users();
            bool flag = acc.UpdateUsersPassword(this.UserID, this.txtNewPassword.Text);
            if (flag)
            {
                this.UserPassword = this.txtNewPassword.Text;
                JERPBiz.Frame.UserBiz.UserPassword = this.txtNewPassword.Text;
                MessageBox.Show("�ɹ������˵�ǰ��¼���룡");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}