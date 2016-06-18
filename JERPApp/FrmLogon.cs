using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
namespace JERPApp
{
    public partial class FrmLogon : Form
    {
        public FrmLogon()
        {
            InitializeComponent();
            this.accUsers = new JERPData.Frame.Users();            
            this.txtUserName.Focus();
            this.plLogon.Click += new EventHandler(plLogon_Click);
            this.pnlCancel .Click +=new EventHandler(pnlCancel_Click);
            this.KeyPress += new KeyPressEventHandler(FrmLogon_KeyPress);
            this.accBackupPath = new JERPData.Frame.BackupPath();
            this.Load += new EventHandler(FrmLogon_Load);
            this.txtUserName .KeyPress +=new KeyPressEventHandler(txtUserName_KeyPress);
            this.txtPassword .KeyPress +=new KeyPressEventHandler(txtPassword_KeyPress);
        }

        private bool UserRegisterFlag = true;
        private JERPData.Frame.BackupPath accBackupPath;
        Config.FrmUserRegister frmregist = null;
        void FrmLogon_Load(object sender, EventArgs e)
        {
          

            FrmStyle.LoadForm(this);
            if (JERPData.ServerParameter .ServerName .ToLower().IndexOf("local") > -1)
            {
                //���˰��ҲҪ������ע��
                string sourcecode;
                FrmMsg.Show("������֤��,���Ժ�....");
                this.UserRegisterFlag = Config.FrmUserRegister.IsPass(out sourcecode);
                FrmMsg.Hide();
                if (!this.UserRegisterFlag)
                {
                    if (frmregist == null)
                    {
                        frmregist = new Config.FrmUserRegister(sourcecode);
                        frmregist.AffterRegister += new JERPApp.Config.FrmUserRegister.AffterRisterDelegate(frmregist_AffterRegister);
                    }
                    frmregist.ShowDialog();
                }
            }
        }

        void frmregist_AffterRegister(bool flag)
        {
            this.UserRegisterFlag = flag;
        }

        void pnlCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void plLogon_Click(object sender, EventArgs e)
        {
            this.Logon();
        }
    
        void FrmLogon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
        private JERPData.Frame.Users accUsers;
        FrmMain frmMain;
        private void Logon()
        {
            if (!this.UserRegisterFlag) return; 
            this.lblInfo.Visible = true;
            this.lblInfo.Text = "ϵͳ��֤��¼��.....";
            Application.DoEvents();
            short userID;
            bool RegisterFlag, RemindFlag, CloseFlag;
            string passport = JERPBiz.Frame.Passport.GetPassport();
            this.accUsers.GetParmUsersLogon(this.txtUserName.Text,
                this.txtPassword.Text,
                passport,
                out userID,
                out RegisterFlag,
                out RemindFlag,
                out CloseFlag);
            if (!RegisterFlag)
            {
                Config.FrmServRegister  frm = new JERPApp.Config.FrmServRegister ();
                new FrmStyle(frm).SetPopFrmStyle(this);
                frm.ShowDialog();
                return;
            }
            DateTime dbdate = JERPData.ServerParameter.DatabaseTime;       
            if (CloseFlag)
            {
                MessageBox.Show("�Բ���ϵͳ�ѹ��ڣ����ܼ���ʹ�ã�", "ϵͳ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (RemindFlag)
            {             
                MessageBox.Show("ϵͳ�ѹ������ڣ�������ʱͣ�ã����빩Ӧ����ϵ!", "ϵͳ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (userID == -1)
            {
                MessageBox.Show("�Բ����û������������", "��¼��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPassword.Text = string.Empty;
                this.txtUserName.Focus();
                this.txtUserName.SelectAll();
                this.lblInfo.Visible = false;
                return;
            }
            lblInfo.Text = "ϵͳ���������û���Ϣ.....";
            Application.DoEvents();
            JERPBiz.Frame.UserBiz.UserID = userID;
            string errormsg = string.Empty;
            this.accUsers.UpdateUsersForOnline(ref errormsg, userID);
            lblInfo.Text = "���������ļ�ϵͳ...";
            JCommon.NetworkConnection.Connect(JERPData.ServerParameter.ERPFileFolder, "X:", "����ERP", "902157");
            lblInfo.Text = "ϵͳ���ڼ���������.....";
            Application.DoEvents();
            if (frmMain == null)
            {
                frmMain = new FrmMain();
                frmMain.FormClosed += new FormClosedEventHandler(frmMain_FormClosed);
            }
            this.Visible = false;
            frmMain.Show();

        }
  
      
        void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string errormsg = string.Empty;
            this.accUsers.UpdateUsersForOffline(ref errormsg, JERPBiz.Frame.UserBiz.UserID);
            //FrmMsg.Show("���ڱ������ݿ�...");
            //new JERPData.ServerParameter().PM_BackupDatabase(JERPData.ServerParameter.DbName);
            //FrmMsg.Hide();
           
            this.Close();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }


        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Logon();
            }
        }
        void pctCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void pctLogon_Click(object sender, EventArgs e)
        {
            this.Logon();
        }
       


      

    }
}