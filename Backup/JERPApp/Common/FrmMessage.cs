using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections ;
namespace JERPApp.Common
{
    public partial class FrmMessage : Form
    {
        public FrmMessage()
        {
            InitializeComponent();

            this.accMsg = new JERPData.General.Message();
            this.accPsns = new JERPData.Hr.Personnel();
            this.TreadMessage = new Thread(new ThreadStart(this.SetMessageInfo));
            this.TreadMessage.IsBackground = true;
            this.HasToPsns = new Hashtable();
            ToolTip tip = new ToolTip();
            tip.SetToolTip(this.btnAddPsn, "������Ա");
            tip.SetToolTip(this.btnClear, "ȫ�����");
            tip.SetToolTip(this.btnSend, "Ctrl+Enter");
            tip.SetToolTip(this.lnkRecord , "����鿴��ʷ��¼");
            this.Load += new EventHandler(FrmMessage_Load);
            this.SetPermit();
        }

        void FrmMessage_Load(object sender, EventArgs e)
        {
            this.rchBoard.ReadOnly = true;
            this.rchBoard.BackColor = Color.White;
        }

        private JERPData.General.Message accMsg;
        private JERPData.Hr.Personnel accPsns;
        private delegate void FlushClient();//����
        private Thread TreadMessage;
        private Hashtable HasToPsns;
        private FrmMessageRecord frmRecord;
        private DataTable dtblAllPsns;
        //Ȩ����
        private bool enableBrowse = false;//���
        private void SetPermit()
        {
            this.enableBrowse = JERPBiz.Frame.PermitHelper.EnableFunction(332);
            if (this.enableBrowse)
            {
                this.dtblAllPsns = this.accPsns.GetDataPersonnelForUser().Tables[0];   
                //������Ϣ
                this.TreadMessage.Start();
                this.rchBoard .ContextMenuStrip = this.cMenu;
                this.mItemRefresh.Click += new EventHandler(mItemRefresh_Click);
                this.btnAddPsn.Click += new EventHandler(btnAddPsn_Click);
                this.btnClear.Click += new EventHandler(btnClear_Click);
                this.btnSend.Click += new EventHandler(btnSend_Click);
                this.lnkRecord.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkRecord_LinkClicked);
                this.rchMsgContent.KeyDown += new KeyEventHandler(rchMsgContent_KeyDown);
               
            }
          
        }

      

        void lnkRecord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (frmRecord == null)
            {
                frmRecord = new FrmMessageRecord();
                new FrmStyle(frmRecord).SetPopFrmStyle(this);               
            }
            frmRecord.ShowDialog();
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            this.txtToPsns.Text = string.Empty;
            this.HasToPsns.Clear(); 
        }
        private void AddPsn(int PsnID, string PsnName)
        {
            if (PsnID == JERPBiz.Frame.UserBiz.PsnID) return;
            if (this.HasToPsns.ContainsKey(PsnID)) return;
            this.HasToPsns.Add(PsnID, PsnName);
            if (this.txtToPsns.Text.Length > 0)
            {
                this.txtToPsns.Text += "��" + PsnName ;
            }
            else
            {
                this.txtToPsns.Text = "@" + PsnName;
            }
        }
        void btnAddPsn_Click(object sender, EventArgs e)
        {
            if (this.ckbAllPsnFlag.Checked)
            {
                foreach (DataRow drow in this.dtblAllPsns.Rows)
                {
                    this.AddPsn((int)drow["PsnID"], drow["PsnName"].ToString());
                }
            }
            else
            {
                if (this.ctrlToPsnID.PsnID > -1)
                {
                    this.AddPsn(this.ctrlToPsnID.PsnID, this.ctrlToPsnID.PsnName);
                }
            }
        }

        //�ڲ�������Ϣ
        private void SetMessageInfo()
        {
            while (true)
            {
                ThreadFunction();
                Thread.Sleep(8000);
            }
        }
        private void LoadData()
        {
        
            int PsnID=JERPBiz.Frame.UserBiz.PsnID;
            DataTable dtblMsg = this.accMsg.GetDataMessageNonReceive(PsnID).Tables[0];
            if(dtblMsg .Rows .Count ==0)return;
            StringBuilder sb=new StringBuilder ();          
            string errormsg=string .Empty ;
            foreach (DataRow drow in dtblMsg.Rows)
            {

                sb.Append(drow["DateMsg"].ToString() +drow["FromPsn"].ToString()+"@��\n");
                sb.Append(drow["MsgContent"].ToString()+"\n");
                sb.Append("-------------------------------------------------------------------\n");
                this.accMsg.UpdateMessageForReceive(ref errormsg, drow["MsgID"]);
            }
            this.rchBoard.AppendText (sb.ToString ());
            this.rchBoard.Focus();
            this.rchBoard.ScrollToCaret();
        }
        private void ThreadFunction()
        {
            if (this.rchBoard.InvokeRequired)//�ȴ��첽
            {
                FlushClient fc = new FlushClient(ThreadFunction);
                this.Invoke(fc);//ͨ���������ˢ�·���
            }
            else
            {
                this.LoadData();

            }
        }
        void mItemRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }
        bool ValidateData()
        {
            if (this.rchMsgContent.Text.Length == 0)
            {
                MessageBox.Show("���ݲ���Ϊ��");
                return false;
            }
            if (this.HasToPsns.Count == 0)
            {
                MessageBox.Show("�����˲���Ϊ��");
                return false;
            }
            return true;
        }
        private void SendMessage()
        {
            if (this.ValidateData() == false) return;
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString() + "��" + this.txtToPsns.Text + "\n");
            sb.Append(this.rchMsgContent.Text + "\n");
            sb.Append("-------------------------------------------------------------------\n");
            this.rchBoard.AppendText(sb.ToString());
            this.rchBoard.Focus();
            this.rchBoard.ScrollToCaret();
            string errormsg = string.Empty;
            DateTime now_ = DateTime.Now;
            foreach (int toPsnID in this.HasToPsns.Keys)
            {
                this.accMsg.InsertMessage(ref errormsg,
               now_,
               this.rchMsgContent.Text,
               JERPBiz.Frame.UserBiz.PsnID,
               toPsnID);
            }
            this.rchMsgContent.Text = string.Empty;
        }
        void rchMsgContent_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control ) && (e.KeyCode == Keys.Enter))
            {
                this.SendMessage();
            }
        }
        void btnSend_Click(object sender, EventArgs e)
        {
            this.SendMessage();   
        }
    }
}